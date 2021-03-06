﻿using JuniorTennis.Domain.Tournaments;
using JuniorTennis.Domain.UseCases.Tournaments;
using JuniorTennis.Mvc.Features.Shared;
using JuniorTennis.Mvc.Validations;
using JuniorTennis.SeedWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JuniorTennis.Mvc.Features.Tournaments
{
    /// <summary>
    /// 登録画面。
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// 大会名を取得または設定します。
        /// </summary>
        [Required(ErrorMessage = "大会名を入力してください。")]
        [Display(Name = "大会名")]
        [DataType(DataType.Text)]
        public string TournamentName { get; set; }

        /// <summary>
        /// 大会種別を取得または設定します。
        /// </summary>
        [Display(Name = "大会種別")]
        public List<SelectListItem> TournamentTypes { get; set; }

        /// <summary>
        /// 登録年度を取得または設定します。
        /// </summary>
        public List<SelectListItem> RegistrationYears { get; set; }

        /// <summary>
        /// 年度種別を取得または設定します。
        /// </summary>
        [Display(Name = "年度種別")]
        public List<SelectListItem> TypeOfYears { get; set; }

        /// <summary>
        /// 集計月度を取得または設定します。
        /// </summary>
        public List<SelectListItem> AggregationMonths { get; set; }

        /// <summary>
        /// 大会種目を取得または設定します。
        /// </summary>
        [Display(Name = "大会種目")]
        public List<SelectListItem> TennisEvents { get; set; }

        /// <summary>
        /// 大会の開催開始日を取得または設定します。
        /// </summary>
        [Display(Name = "大会の開催開始日")]
        [DataType(DataType.Date)]
        [DateTimeAfter("ApplicationEndDate", "申込期間と重複した日付が入力されています。")]
        public DateTime? HoldingStartDate { get; set; }

        /// <summary>
        /// 大会の開催終了日を取得または設定します。
        /// </summary>
        [Display(Name = "大会の開催終了日")]
        [DataType(DataType.Date)]
        [DateTimeAfter("HoldingStartDate", "開始日よりも前の日付が入力されています。")]
        public DateTime? HoldingEndDate { get; set; }

        /// <summary>
        /// 全ての開催日を取得または設定します。
        /// </summary>
        public List<string> AllHoldingDates { get; set; }

        /// <summary>
        /// 大会の開催日を取得または設定します。
        /// </summary>
        [Display(Name = "開催日")]
        public List<SelectListItem[]> HoldingDates => this.CreateHoldingDates().ToList();

        /// <summary>
        /// 会場を取得または設定します。
        /// </summary>
        [Display(Name = "会場")]
        [DataType(DataType.Text)]
        public string Venue { get; set; }

        /// <summary>
        /// 参加費を取得または設定します。
        /// </summary>
        [Display(Name = "参加費")]
        [DataType(DataType.Currency)]
        public int? EntryFee { get; set; }

        /// <summary>
        /// 支払い方法を取得または設定します。
        /// </summary>
        [Display(Name = "支払い方法")]
        public List<SelectListItem> MethodOfPayments { get; set; }

        /// <summary>
        /// 大会の申込み開始日を取得または設定します。
        /// </summary>
        [Display(Name = "大会の申込み開始日")]
        [DataType(DataType.Date)]
        public DateTime? ApplicationStartDate { get; set; }

        /// <summary>
        /// 大会の申込み終了日を取得または設定します。
        /// </summary>
        [Display(Name = "大会の申込み終了日")]
        [DataType(DataType.Date)]
        [DateTimeAfter("ApplicationStartDate", "開始日よりも前の日付が入力されています。")]
        public DateTime? ApplicationEndDate { get; set; }

        /// <summary>
        /// 大会要領を取得または設定します。
        /// </summary>
        [Display(Name = "大会要領")]
        [DataType(DataType.MultilineText)]
        public string Outline { get; set; }

        /// <summary>
        /// 大会申込受付メールの件名を取得または設定します。 
        /// </summary>
        [Required(ErrorMessage = "メールの件名を入力してください。")]
        [Display(Name = "メール件名")]
        public string TournamentEntryReceptionMailSubject { get; set; }

        /// <summary>
        /// 大会申込受付メールの本文を取得または設定します。 
        /// </summary>
        [Required(ErrorMessage = "メールの本文を入力してください。")]
        [Display(Name = "メール本文")]
        public string TournamentEntryReceptionMailBody { get; set; }

        /// <summary>
        /// 大会申込受付メールの本文テンプレートを取得します。
        /// </summary>
        public Dictionary<string, string> TournamentEntryReceptionMailBodies { get; set; }

        /// <summary>
        /// 登録画面の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="tournamentEntryReceptionMailBodies">大会申込受付メールの本文テンプレート。</param>
        public RegisterViewModel(Dictionary<string, string> tournamentEntryReceptionMailBodies)
        {
            this.TournamentTypes = MvcViewHelper.CreateSelectListItem<TournamentType>(TournamentType.WithDraw.Id);
            this.RegistrationYears = this.CreateRegistrationYears();
            this.TypeOfYears = MvcViewHelper.CreateSelectListItem<TypeOfYear>(TypeOfYear.Odd.Id);
            this.AggregationMonths = this.CreateAggregationMonths(DateTime.Now.Year);
            this.TennisEvents = this.CreateTennisEvents();
            this.MethodOfPayments = MvcViewHelper.CreateSelectListItem<MethodOfPayment>(MethodOfPayment.PrePayment.Id);
            this.AllHoldingDates = new List<string>();
            this.TournamentEntryReceptionMailBody = tournamentEntryReceptionMailBodies["PrePayment"];
            this.TournamentEntryReceptionMailBodies = tournamentEntryReceptionMailBodies;
        }
        /// <summary>
        /// 登録画面の新しいインスタンスを生成します。
        /// </summary>
        public RegisterViewModel()
        {
            this.TournamentTypes = MvcViewHelper.CreateSelectListItem<TournamentType>(TournamentType.WithDraw.Id);
            this.RegistrationYears = this.CreateRegistrationYears();
            this.TypeOfYears = MvcViewHelper.CreateSelectListItem<TypeOfYear>(TypeOfYear.Odd.Id);
            this.AggregationMonths = this.CreateAggregationMonths(DateTime.Now.Year);
            this.TennisEvents = this.CreateTennisEvents();
            this.MethodOfPayments = MvcViewHelper.CreateSelectListItem<MethodOfPayment>(MethodOfPayment.PrePayment.Id);
            this.AllHoldingDates = new List<string>();
        }

        /// <summary>
        /// 登録年度の一覧を作成します。
        /// </summary>
        /// <returns>登録年度の一覧。</returns>
        private List<SelectListItem> CreateRegistrationYears() =>
            Enumerable
                .Range(DateTime.Now.Year - 1, 3)
                .Select(o => new RegistrationYear(new DateTime(o, 1, 1)))
                .Select(o => new SelectListItem(o.DisplayValue, $"{o.Value:yyyy/MM/dd}", o.Value.Year == DateTime.Now.Year))
                .ToList();

        /// <summary>
        /// 集計月度の一覧を作成します。
        /// </summary>
        /// <param name="selectedYear"></param>
        /// <returns>集計月度の一覧。</returns>
        private List<SelectListItem> CreateAggregationMonths(int selectedYear) =>
            Enumerable
                .Range(0, 16)
                .Select(o => new AggregationMonth(new DateTime(selectedYear, 4, 1).AddMonths(o)))
                .Select(o => new SelectListItem(o.DisplayValue, $"{o.Value:yyyy/MM/dd}", $"{o.Value:yyyy/MM}" == $"{DateTime.Now.Year:yyyy/MM}"))
                .ToList();

        /// <summary>
        /// 種目の一覧を作成します。
        /// </summary>
        /// <returns>種目の一覧。</returns>
        private List<SelectListItem> CreateTennisEvents() =>
            TennisEvent
                .GetAllEvents()
                .Select(o => new SelectListItem(o.Value.DisplayTournamentEvent, o.Key))
                .ToList();

        #region 選択内容
        /// <summary>
        /// 選択された大会種別を取得または設定します。
        /// </summary>
        public string SelectedTournamentType { get; set; }

        /// <summary>
        /// 選択された登録年度を取得または設定します。
        /// </summary>
        [Display(Name = "登録年度")]
        public string SelectedRegistrationYear { get; set; }

        /// <summary>
        /// 選択された年度種別を取得または設定します。
        /// </summary>
        public string SelectedTypeOfYear { get; set; }

        /// <summary>
        /// 選択された集計月度を取得または設定します。
        /// </summary>
        [Display(Name = "集計月度")]
        public string SelectedAggregationMonth { get; set; }

        /// <summary>
        /// 選択された大会種目を取得または設定します。
        /// </summary>
        public List<string> SelectedTennisEvents { get; set; }

        /// <summary>
        /// 選択された大大会の開催日を取得または設定します。
        /// </summary>
        public List<string> SelectedHoldingDates { get; set; }

        /// <summary>
        /// 選択された大支払い方法を取得または設定します。
        /// </summary>
        public string SelectedMethodOfPayments { get; set; }
        #endregion

        /// <summary>
        /// 大会登録用 DTO に変換します。
        /// </summary>
        /// <returns>大会登録用 DTO。</returns>
        public RegisterTournamentDto ToDto()
        {
            var tournamentType = Enumeration.FromValue<TournamentType>(int.Parse(this.SelectedTournamentType));
            return tournamentType == TournamentType.WithDraw
                ? new RegisterTournamentDto()
                {
                    TournamentName = this.TournamentName,
                    TournamentType = int.Parse(this.SelectedTournamentType),
                    RegistrationYear = DateTime.Parse(this.SelectedRegistrationYear),
                    TypeOfYear = int.Parse(this.SelectedTypeOfYear),
                    AggregationMonth = DateTime.Parse(this.SelectedAggregationMonth),
                    TennisEvents = this.ParseEventIds(this.SelectedTennisEvents),
                    HoldingStartDate = this.HoldingStartDate.Value,
                    HoldingEndDate = this.HoldingEndDate.Value,
                    HoldingDates = this.SelectedHoldingDates.Select(o => DateTime.Parse(o)).ToList() ?? new List<DateTime>(),
                    Venue = this.Venue,
                    EntryFee = this.EntryFee.Value,
                    MethodOfPayment = int.Parse(this.SelectedMethodOfPayments),
                    ApplicationStartDate = this.ApplicationStartDate.Value,
                    ApplicationEndDate = this.ApplicationEndDate.Value,
                    Outline = this.Outline,
                    TournamentEntryReceptionMailBody = this.TournamentEntryReceptionMailBody,
                    TournamentEntryReceptionMailSubject = this.TournamentEntryReceptionMailSubject,
                }

                : new RegisterTournamentDto()
                {
                    TournamentName = this.TournamentName,
                    TournamentType = int.Parse(this.SelectedTournamentType),
                    RegistrationYear = DateTime.Parse(this.SelectedRegistrationYear),
                    TypeOfYear = int.Parse(this.SelectedTypeOfYear),
                    AggregationMonth = DateTime.Parse(this.SelectedAggregationMonth),
                    TennisEvents = this.ParseEventIds(this.SelectedTennisEvents)
                };
        }

        private List<(int, int, int)> ParseEventIds(List<string> selectedTennisEvents)
        {
            return selectedTennisEvents
                .Select(o => o.Split('_').Select(v => int.Parse(v)).ToArray())
                .Select(o => (o[0], o[1], o[2]))
                .ToList();
        }

        /// <summary>
        /// 開催日の一覧を作成します。
        /// </summary>
        /// <returns>種目の一覧。</returns>
        public IEnumerable<SelectListItem[]> CreateHoldingDates()
        {
            var allHoldingDates = this.AllHoldingDates
                .Select(o => new JsonHoldingDate(DateTime.Parse(o)))
                .Select(o => new SelectListItem(o.Text, o.Value, this.IsSelected(o.Value), !this.IsEnabled(o.Date)))
                .ToList();

            var createHoldingDates = new List<SelectListItem[]>();
            for (var i = 0; i < allHoldingDates.Count; i += 7)
            {
                yield return allHoldingDates.Skip(i).Take(7).ToArray();
            }
        }

        private bool IsSelected(string targetHoldingDate) =>
             this.SelectedHoldingDates.Any(o => o == targetHoldingDate);

        private bool IsEnabled(DateTime holdingDates) =>
            this.HoldingStartDate <= holdingDates && holdingDates <= this.HoldingEndDate;
    }
}
