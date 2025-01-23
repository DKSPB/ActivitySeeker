using System.Text.Json.Serialization;

namespace ActivitySeeker.Domain.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatesEnum
{
    Start,
    MainMenu,
    ActivityTypeChapter,
    SelectActivityFormat,
    SaveOfferFormat,
    SaveActivityFormat,
    ListOfActivities,
    ListOfChildrenActivities,
    ActivityPeriodChapter,
    NextActivity,
    PreviousActivity,
    UserPeriod,
    TodayPeriod,
    TomorrowPeriod,
    AfterTomorrowPeriod,
    WeekPeriod,
    MonthPeriod,
    PeriodFromDate,
    PeriodToDate,
    Result,
    Offer,
    SaveOfferDescription,
    SaveOfferDate,
    ConfirmOffer,
    SetDefaultSettings,
    SaveDefaultSettings,
    SelectOfferCity
}