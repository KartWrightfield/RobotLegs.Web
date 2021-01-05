using System;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FPLCore
{
    public partial class GameInfo
    {
        [JsonProperty("events")]
        public Event[] Events { get; set; }

        [JsonProperty("game_settings")]
        public GameSettings GameSettings { get; set; }

        [JsonProperty("phases")]
        public Phase[] Phases { get; set; }

        [JsonProperty("teams")]
        public Team[] Teams { get; set; }

        [JsonProperty("total_players")]
        public long TotalPlayers { get; set; }

        [JsonProperty("elements")]
        public Element[] Elements { get; set; }

        [JsonProperty("element_stats")]
        public ElementStat[] ElementStats { get; set; }

        [JsonProperty("element_types")]
        public ElementType[] ElementTypes { get; set; }
    }

    public partial class ElementStat
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class ElementType
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("plural_name")]
        public string PluralName { get; set; }

        [JsonProperty("plural_name_short")]
        public string PluralNameShort { get; set; }

        [JsonProperty("singular_name")]
        public string SingularName { get; set; }

        [JsonProperty("singular_name_short")]
        public string SingularNameShort { get; set; }

        [JsonProperty("squad_select")]
        public long SquadSelect { get; set; }

        [JsonProperty("squad_min_play")]
        public long SquadMinPlay { get; set; }

        [JsonProperty("squad_max_play")]
        public long SquadMaxPlay { get; set; }

        [JsonProperty("ui_shirt_specific")]
        public bool UiShirtSpecific { get; set; }

        [JsonProperty("sub_positions_locked")]
        public long[] SubPositionsLocked { get; set; }

        [JsonProperty("element_count")]
        public long ElementCount { get; set; }
    }

    public partial class Element
    {
        [JsonProperty("chance_of_playing_next_round")]
        public long? ChanceOfPlayingNextRound { get; set; }

        [JsonProperty("chance_of_playing_this_round")]
        public long? ChanceOfPlayingThisRound { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("cost_change_event")]
        public long CostChangeEvent { get; set; }

        [JsonProperty("cost_change_event_fall")]
        public long CostChangeEventFall { get; set; }

        [JsonProperty("cost_change_start")]
        public long CostChangeStart { get; set; }

        [JsonProperty("cost_change_start_fall")]
        public long CostChangeStartFall { get; set; }

        [JsonProperty("dreamteam_count")]
        public long DreamteamCount { get; set; }

        [JsonProperty("element_type")]
        public long ElementType { get; set; }

        [JsonProperty("ep_next")]
        public string EpNext { get; set; }

        [JsonProperty("ep_this")]
        public string EpThis { get; set; }

        [JsonProperty("event_points")]
        public long EventPoints { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("form")]
        public string Form { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("in_dreamteam")]
        public bool InDreamteam { get; set; }

        [JsonProperty("news")]
        public string News { get; set; }

        [JsonProperty("news_added")]
        public DateTimeOffset? NewsAdded { get; set; }

        [JsonProperty("now_cost")]
        public long NowCost { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("points_per_game")]
        public string PointsPerGame { get; set; }

        [JsonProperty("second_name")]
        public string SecondName { get; set; }

        [JsonProperty("selected_by_percent")]
        public string SelectedByPercent { get; set; }

        [JsonProperty("special")]
        public bool Special { get; set; }

        [JsonProperty("squad_number")]
        public object SquadNumber { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("team")]
        public long Team { get; set; }

        [JsonProperty("team_code")]
        public long TeamCode { get; set; }

        [JsonProperty("total_points")]
        public long TotalPoints { get; set; }

        [JsonProperty("transfers_in")]
        public long TransfersIn { get; set; }

        [JsonProperty("transfers_in_event")]
        public long TransfersInEvent { get; set; }

        [JsonProperty("transfers_out")]
        public long TransfersOut { get; set; }

        [JsonProperty("transfers_out_event")]
        public long TransfersOutEvent { get; set; }

        [JsonProperty("value_form")]
        public string ValueForm { get; set; }

        [JsonProperty("value_season")]
        public string ValueSeason { get; set; }

        [JsonProperty("web_name")]
        public string WebName { get; set; }

        [JsonProperty("minutes")]
        public long Minutes { get; set; }

        [JsonProperty("goals_scored")]
        public long GoalsScored { get; set; }

        [JsonProperty("assists")]
        public long Assists { get; set; }

        [JsonProperty("clean_sheets")]
        public long CleanSheets { get; set; }

        [JsonProperty("goals_conceded")]
        public long GoalsConceded { get; set; }

        [JsonProperty("own_goals")]
        public long OwnGoals { get; set; }

        [JsonProperty("penalties_saved")]
        public long PenaltiesSaved { get; set; }

        [JsonProperty("penalties_missed")]
        public long PenaltiesMissed { get; set; }

        [JsonProperty("yellow_cards")]
        public long YellowCards { get; set; }

        [JsonProperty("red_cards")]
        public long RedCards { get; set; }

        [JsonProperty("saves")]
        public long Saves { get; set; }

        [JsonProperty("bonus")]
        public long Bonus { get; set; }

        [JsonProperty("bps")]
        public long Bps { get; set; }

        [JsonProperty("influence")]
        public string Influence { get; set; }

        [JsonProperty("creativity")]
        public string Creativity { get; set; }

        [JsonProperty("threat")]
        public string Threat { get; set; }

        [JsonProperty("ict_index")]
        public string IctIndex { get; set; }

        [JsonProperty("influence_rank")]
        public long InfluenceRank { get; set; }

        [JsonProperty("influence_rank_type")]
        public long InfluenceRankType { get; set; }

        [JsonProperty("creativity_rank")]
        public long CreativityRank { get; set; }

        [JsonProperty("creativity_rank_type")]
        public long CreativityRankType { get; set; }

        [JsonProperty("threat_rank")]
        public long ThreatRank { get; set; }

        [JsonProperty("threat_rank_type")]
        public long ThreatRankType { get; set; }

        [JsonProperty("ict_index_rank")]
        public long IctIndexRank { get; set; }

        [JsonProperty("ict_index_rank_type")]
        public long IctIndexRankType { get; set; }

        [JsonProperty("corners_and_indirect_freekicks_order")]
        public long? CornersAndIndirectFreekicksOrder { get; set; }

        [JsonProperty("corners_and_indirect_freekicks_text")]
        public string CornersAndIndirectFreekicksText { get; set; }

        [JsonProperty("direct_freekicks_order")]
        public long? DirectFreekicksOrder { get; set; }

        [JsonProperty("direct_freekicks_text")]
        public string DirectFreekicksText { get; set; }

        [JsonProperty("penalties_order")]
        public long? PenaltiesOrder { get; set; }

        [JsonProperty("penalties_text")]
        public string PenaltiesText { get; set; }
    }

    public partial class Event
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("deadline_time")]
        public DateTimeOffset DeadlineTime { get; set; }

        [JsonProperty("average_entry_score")]
        public long AverageEntryScore { get; set; }

        [JsonProperty("finished")]
        public bool Finished { get; set; }

        [JsonProperty("data_checked")]
        public bool DataChecked { get; set; }

        [JsonProperty("highest_scoring_entry")]
        public long? HighestScoringEntry { get; set; }

        [JsonProperty("deadline_time_epoch")]
        public long DeadlineTimeEpoch { get; set; }

        [JsonProperty("deadline_time_game_offset")]
        public long DeadlineTimeGameOffset { get; set; }

        [JsonProperty("highest_score")]
        public long? HighestScore { get; set; }

        [JsonProperty("is_previous")]
        public bool IsPrevious { get; set; }

        [JsonProperty("is_current")]
        public bool IsCurrent { get; set; }

        [JsonProperty("is_next")]
        public bool IsNext { get; set; }

        [JsonProperty("chip_plays")]
        public ChipPlay[] ChipPlays { get; set; }

        [JsonProperty("most_selected")]
        public long? MostSelected { get; set; }

        [JsonProperty("most_transferred_in")]
        public long? MostTransferredIn { get; set; }

        [JsonProperty("top_element")]
        public long? TopElement { get; set; }

        [JsonProperty("top_element_info")]
        public TopElementInfo TopElementInfo { get; set; }

        [JsonProperty("transfers_made")]
        public long TransfersMade { get; set; }

        [JsonProperty("most_captained")]
        public long? MostCaptained { get; set; }

        [JsonProperty("most_vice_captained")]
        public long? MostViceCaptained { get; set; }
    }

    public partial class ChipPlay
    {
        [JsonProperty("chip_name")]
        public ChipName ChipName { get; set; }

        [JsonProperty("num_played")]
        public long NumPlayed { get; set; }
    }

    public partial class TopElementInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("points")]
        public long Points { get; set; }
    }

    public partial class GameSettings
    {
        [JsonProperty("league_join_private_max")]
        public long LeagueJoinPrivateMax { get; set; }

        [JsonProperty("league_join_public_max")]
        public long LeagueJoinPublicMax { get; set; }

        [JsonProperty("league_max_size_public_classic")]
        public long LeagueMaxSizePublicClassic { get; set; }

        [JsonProperty("league_max_size_public_h2h")]
        public long LeagueMaxSizePublicH2H { get; set; }

        [JsonProperty("league_max_size_private_h2h")]
        public long LeagueMaxSizePrivateH2H { get; set; }

        [JsonProperty("league_max_ko_rounds_private_h2h")]
        public long LeagueMaxKoRoundsPrivateH2H { get; set; }

        [JsonProperty("league_prefix_public")]
        public string LeaguePrefixPublic { get; set; }

        [JsonProperty("league_points_h2h_win")]
        public long LeaguePointsH2HWin { get; set; }

        [JsonProperty("league_points_h2h_lose")]
        public long LeaguePointsH2HLose { get; set; }

        [JsonProperty("league_points_h2h_draw")]
        public long LeaguePointsH2HDraw { get; set; }

        [JsonProperty("league_ko_first_instead_of_random")]
        public bool LeagueKoFirstInsteadOfRandom { get; set; }

        [JsonProperty("cup_start_event_id")]
        public long CupStartEventId { get; set; }

        [JsonProperty("cup_stop_event_id")]
        public long CupStopEventId { get; set; }

        [JsonProperty("cup_qualifying_method")]
        public string CupQualifyingMethod { get; set; }

        [JsonProperty("cup_type")]
        public string CupType { get; set; }

        [JsonProperty("squad_squadplay")]
        public long SquadSquadplay { get; set; }

        [JsonProperty("squad_squadsize")]
        public long SquadSquadsize { get; set; }

        [JsonProperty("squad_team_limit")]
        public long SquadTeamLimit { get; set; }

        [JsonProperty("squad_total_spend")]
        public long SquadTotalSpend { get; set; }

        [JsonProperty("ui_currency_multiplier")]
        public long UiCurrencyMultiplier { get; set; }

        [JsonProperty("ui_use_special_shirts")]
        public bool UiUseSpecialShirts { get; set; }

        [JsonProperty("ui_special_shirt_exclusions")]
        public object[] UiSpecialShirtExclusions { get; set; }

        [JsonProperty("stats_form_days")]
        public long StatsFormDays { get; set; }

        [JsonProperty("sys_vice_captain_enabled")]
        public bool SysViceCaptainEnabled { get; set; }

        [JsonProperty("transfers_cap")]
        public long TransfersCap { get; set; }

        [JsonProperty("transfers_sell_on_fee")]
        public double TransfersSellOnFee { get; set; }

        [JsonProperty("league_h2h_tiebreak_stats")]
        public string[] LeagueH2HTiebreakStats { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }

    public partial class Phase
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_event")]
        public long StartEvent { get; set; }

        [JsonProperty("stop_event")]
        public long StopEvent { get; set; }
    }

    public partial class Team
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("draw")]
        public long Draw { get; set; }

        [JsonProperty("form")]
        public object Form { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("loss")]
        public long Loss { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("played")]
        public long Played { get; set; }

        [JsonProperty("points")]
        public long Points { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("strength")]
        public long Strength { get; set; }

        [JsonProperty("team_division")]
        public object TeamDivision { get; set; }

        [JsonProperty("unavailable")]
        public bool Unavailable { get; set; }

        [JsonProperty("win")]
        public long Win { get; set; }

        [JsonProperty("strength_overall_home")]
        public long StrengthOverallHome { get; set; }

        [JsonProperty("strength_overall_away")]
        public long StrengthOverallAway { get; set; }

        [JsonProperty("strength_attack_home")]
        public long StrengthAttackHome { get; set; }

        [JsonProperty("strength_attack_away")]
        public long StrengthAttackAway { get; set; }

        [JsonProperty("strength_defence_home")]
        public long StrengthDefenceHome { get; set; }

        [JsonProperty("strength_defence_away")]
        public long StrengthDefenceAway { get; set; }

        [JsonProperty("pulse_id")]
        public long PulseId { get; set; }
    }

    public enum Status { A, D, I, N, S, U };

    public enum ChipName { Bboost, Freehit, The3Xc, Wildcard };

    public partial class GameInfo
    {
        public static GameInfo FromJson(string json) => JsonConvert.DeserializeObject<GameInfo>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GameInfo self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                StatusConverter.Singleton,
                ChipNameConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "a":
                    return Status.A;
                case "d":
                    return Status.D;
                case "i":
                    return Status.I;
                case "n":
                    return Status.N;
                case "s":
                    return Status.S;
                case "u":
                    return Status.U;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.A:
                    serializer.Serialize(writer, "a");
                    return;
                case Status.D:
                    serializer.Serialize(writer, "d");
                    return;
                case Status.I:
                    serializer.Serialize(writer, "i");
                    return;
                case Status.N:
                    serializer.Serialize(writer, "n");
                    return;
                case Status.S:
                    serializer.Serialize(writer, "s");
                    return;
                case Status.U:
                    serializer.Serialize(writer, "u");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }

    internal class ChipNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ChipName) || t == typeof(ChipName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "3xc":
                    return ChipName.The3Xc;
                case "bboost":
                    return ChipName.Bboost;
                case "freehit":
                    return ChipName.Freehit;
                case "wildcard":
                    return ChipName.Wildcard;
            }
            throw new Exception("Cannot unmarshal type ChipName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ChipName)untypedValue;
            switch (value)
            {
                case ChipName.The3Xc:
                    serializer.Serialize(writer, "3xc");
                    return;
                case ChipName.Bboost:
                    serializer.Serialize(writer, "bboost");
                    return;
                case ChipName.Freehit:
                    serializer.Serialize(writer, "freehit");
                    return;
                case ChipName.Wildcard:
                    serializer.Serialize(writer, "wildcard");
                    return;
            }
            throw new Exception("Cannot marshal type ChipName");
        }

        public static readonly ChipNameConverter Singleton = new ChipNameConverter();
    }
}