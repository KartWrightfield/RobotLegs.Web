using System;
using System.Collections.Generic;
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

    public class ChipPlay
    {
        [JsonProperty("chip_name")]
        public string ChipName { get; set; }

        [JsonProperty("num_played")]
        public int NumPlayed { get; set; }
    }

    public class TopElementInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }
    }

    public class Event
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("deadline_time")]
        public DateTime DeadlineTime { get; set; }

        [JsonProperty("average_entry_score")]
        public int AverageEntryScore { get; set; }

        [JsonProperty("finished")]
        public bool Finished { get; set; }

        [JsonProperty("data_checked")]
        public bool DataChecked { get; set; }

        [JsonProperty("highest_scoring_entry")]
        public int? HighestScoringEntry { get; set; }

        [JsonProperty("deadline_time_epoch")]
        public int DeadlineTimeEpoch { get; set; }

        [JsonProperty("deadline_time_game_offset")]
        public int DeadlineTimeGameOffset { get; set; }

        [JsonProperty("highest_score")]
        public int? HighestScore { get; set; }

        [JsonProperty("is_previous")]
        public bool IsPrevious { get; set; }

        [JsonProperty("is_current")]
        public bool IsCurrent { get; set; }

        [JsonProperty("is_next")]
        public bool IsNext { get; set; }

        [JsonProperty("chip_plays")]
        public List<ChipPlay> ChipPlays { get; set; }

        [JsonProperty("most_selected")]
        public int? MostSelected { get; set; }

        [JsonProperty("most_transferred_in")]
        public int? MostTransferredIn { get; set; }

        [JsonProperty("top_element")]
        public int? TopElement { get; set; }

        [JsonProperty("top_element_info")]
        public TopElementInfo TopElementInfo { get; set; }

        [JsonProperty("transfers_made")]
        public int TransfersMade { get; set; }

        [JsonProperty("most_captained")]
        public int? MostCaptained { get; set; }

        [JsonProperty("most_vice_captained")]
        public int? MostViceCaptained { get; set; }
    }

    public class GameSettings
    {
        [JsonProperty("league_join_private_max")]
        public int LeagueJoinPrivateMax { get; set; }

        [JsonProperty("league_join_public_max")]
        public int LeagueJoinPublicMax { get; set; }

        [JsonProperty("league_max_size_public_classic")]
        public int LeagueMaxSizePublicClassic { get; set; }

        [JsonProperty("league_max_size_public_h2h")]
        public int LeagueMaxSizePublicH2h { get; set; }

        [JsonProperty("league_max_size_private_h2h")]
        public int LeagueMaxSizePrivateH2h { get; set; }

        [JsonProperty("league_max_ko_rounds_private_h2h")]
        public int LeagueMaxKoRoundsPrivateH2h { get; set; }

        [JsonProperty("league_prefix_public")]
        public string LeaguePrefixPublic { get; set; }

        [JsonProperty("league_points_h2h_win")]
        public int LeaguePointsH2hWin { get; set; }

        [JsonProperty("league_points_h2h_lose")]
        public int LeaguePointsH2hLose { get; set; }

        [JsonProperty("league_points_h2h_draw")]
        public int LeaguePointsH2hDraw { get; set; }

        [JsonProperty("league_ko_first_instead_of_random")]
        public bool LeagueKoFirstInsteadOfRandom { get; set; }

        [JsonProperty("cup_start_event_id")]
        public object CupStartEventId { get; set; }

        [JsonProperty("cup_stop_event_id")]
        public object CupStopEventId { get; set; }

        [JsonProperty("cup_qualifying_method")]
        public object CupQualifyingMethod { get; set; }

        [JsonProperty("cup_type")]
        public object CupType { get; set; }

        [JsonProperty("squad_squadplay")]
        public int SquadSquadplay { get; set; }

        [JsonProperty("squad_squadsize")]
        public int SquadSquadsize { get; set; }

        [JsonProperty("squad_team_limit")]
        public int SquadTeamLimit { get; set; }

        [JsonProperty("squad_total_spend")]
        public int SquadTotalSpend { get; set; }

        [JsonProperty("ui_currency_multiplier")]
        public int UiCurrencyMultiplier { get; set; }

        [JsonProperty("ui_use_special_shirts")]
        public bool UiUseSpecialShirts { get; set; }

        [JsonProperty("ui_special_shirt_exclusions")]
        public List<object> UiSpecialShirtExclusions { get; set; }

        [JsonProperty("stats_form_days")]
        public int StatsFormDays { get; set; }

        [JsonProperty("sys_vice_captain_enabled")]
        public bool SysViceCaptainEnabled { get; set; }

        [JsonProperty("transfers_cap")]
        public int TransfersCap { get; set; }

        [JsonProperty("transfers_sell_on_fee")]
        public double TransfersSellOnFee { get; set; }

        [JsonProperty("league_h2h_tiebreak_stats")]
        public List<string> LeagueH2hTiebreakStats { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }

    public class Phase
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_event")]
        public int StartEvent { get; set; }

        [JsonProperty("stop_event")]
        public int StopEvent { get; set; }
    }

    public class Team
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("form")]
        public object Form { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("loss")]
        public int Loss { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("played")]
        public int Played { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("strength")]
        public int Strength { get; set; }

        [JsonProperty("team_division")]
        public object TeamDivision { get; set; }

        [JsonProperty("unavailable")]
        public bool Unavailable { get; set; }

        [JsonProperty("win")]
        public int Win { get; set; }

        [JsonProperty("strength_overall_home")]
        public int StrengthOverallHome { get; set; }

        [JsonProperty("strength_overall_away")]
        public int StrengthOverallAway { get; set; }

        [JsonProperty("strength_attack_home")]
        public int StrengthAttackHome { get; set; }

        [JsonProperty("strength_attack_away")]
        public int StrengthAttackAway { get; set; }

        [JsonProperty("strength_defence_home")]
        public int StrengthDefenceHome { get; set; }

        [JsonProperty("strength_defence_away")]
        public int StrengthDefenceAway { get; set; }

        [JsonProperty("pulse_id")]
        public int PulseId { get; set; }
    }

    public class Element
    {
        [JsonProperty("chance_of_playing_next_round")]
        public int? ChanceOfPlayingNextRound { get; set; }

        [JsonProperty("chance_of_playing_this_round")]
        public int? ChanceOfPlayingThisRound { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("cost_change_event")]
        public int CostChangeEvent { get; set; }

        [JsonProperty("cost_change_event_fall")]
        public int CostChangeEventFall { get; set; }

        [JsonProperty("cost_change_start")]
        public int CostChangeStart { get; set; }

        [JsonProperty("cost_change_start_fall")]
        public int CostChangeStartFall { get; set; }

        [JsonProperty("dreamteam_count")]
        public int DreamteamCount { get; set; }

        [JsonProperty("element_type")]
        public int ElementType { get; set; }

        [JsonProperty("ep_next")]
        public string EpNext { get; set; }

        [JsonProperty("ep_this")]
        public string EpThis { get; set; }

        [JsonProperty("event_points")]
        public int EventPoints { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("form")]
        public string Form { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("in_dreamteam")]
        public bool InDreamteam { get; set; }

        [JsonProperty("news")]
        public string News { get; set; }

        [JsonProperty("news_added")]
        public DateTime? NewsAdded { get; set; }

        [JsonProperty("now_cost")]
        public int NowCost { get; set; }

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
        public string Status { get; set; }

        [JsonProperty("team")]
        public int Team { get; set; }

        [JsonProperty("team_code")]
        public int TeamCode { get; set; }

        [JsonProperty("total_points")]
        public int TotalPoints { get; set; }

        [JsonProperty("transfers_in")]
        public int TransfersIn { get; set; }

        [JsonProperty("transfers_in_event")]
        public int TransfersInEvent { get; set; }

        [JsonProperty("transfers_out")]
        public int TransfersOut { get; set; }

        [JsonProperty("transfers_out_event")]
        public int TransfersOutEvent { get; set; }

        [JsonProperty("value_form")]
        public string ValueForm { get; set; }

        [JsonProperty("value_season")]
        public string ValueSeason { get; set; }

        [JsonProperty("web_name")]
        public string WebName { get; set; }

        [JsonProperty("minutes")]
        public int Minutes { get; set; }

        [JsonProperty("goals_scored")]
        public int GoalsScored { get; set; }

        [JsonProperty("assists")]
        public int Assists { get; set; }

        [JsonProperty("clean_sheets")]
        public int CleanSheets { get; set; }

        [JsonProperty("goals_conceded")]
        public int GoalsConceded { get; set; }

        [JsonProperty("own_goals")]
        public int OwnGoals { get; set; }

        [JsonProperty("penalties_saved")]
        public int PenaltiesSaved { get; set; }

        [JsonProperty("penalties_missed")]
        public int PenaltiesMissed { get; set; }

        [JsonProperty("yellow_cards")]
        public int YellowCards { get; set; }

        [JsonProperty("red_cards")]
        public int RedCards { get; set; }

        [JsonProperty("saves")]
        public int Saves { get; set; }

        [JsonProperty("bonus")]
        public int Bonus { get; set; }

        [JsonProperty("bps")]
        public int Bps { get; set; }

        [JsonProperty("influence")]
        public string Influence { get; set; }

        [JsonProperty("creativity")]
        public string Creativity { get; set; }

        [JsonProperty("threat")]
        public string Threat { get; set; }

        [JsonProperty("ict_index")]
        public string IctIndex { get; set; }

        [JsonProperty("influence_rank")]
        public int InfluenceRank { get; set; }

        [JsonProperty("influence_rank_type")]
        public int InfluenceRankType { get; set; }

        [JsonProperty("creativity_rank")]
        public int CreativityRank { get; set; }

        [JsonProperty("creativity_rank_type")]
        public int CreativityRankType { get; set; }

        [JsonProperty("threat_rank")]
        public int ThreatRank { get; set; }

        [JsonProperty("threat_rank_type")]
        public int ThreatRankType { get; set; }

        [JsonProperty("ict_index_rank")]
        public int IctIndexRank { get; set; }

        [JsonProperty("ict_index_rank_type")]
        public int IctIndexRankType { get; set; }

        [JsonProperty("corners_and_indirect_freekicks_order")]
        public int? CornersAndIndirectFreekicksOrder { get; set; }

        [JsonProperty("corners_and_indirect_freekicks_text")]
        public string CornersAndIndirectFreekicksText { get; set; }

        [JsonProperty("direct_freekicks_order")]
        public int? DirectFreekicksOrder { get; set; }

        [JsonProperty("direct_freekicks_text")]
        public string DirectFreekicksText { get; set; }

        [JsonProperty("penalties_order")]
        public int? PenaltiesOrder { get; set; }

        [JsonProperty("penalties_text")]
        public string PenaltiesText { get; set; }
    }

    public class ElementStat
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ElementType
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("plural_name")]
        public string PluralName { get; set; }

        [JsonProperty("plural_name_short")]
        public string PluralNameShort { get; set; }

        [JsonProperty("singular_name")]
        public string SingularName { get; set; }

        [JsonProperty("singular_name_short")]
        public string SingularNameShort { get; set; }

        [JsonProperty("squad_select")]
        public int SquadSelect { get; set; }

        [JsonProperty("squad_min_play")]
        public int SquadMinPlay { get; set; }

        [JsonProperty("squad_max_play")]
        public int SquadMaxPlay { get; set; }

        [JsonProperty("ui_shirt_specific")]
        public bool UiShirtSpecific { get; set; }

        [JsonProperty("sub_positions_locked")]
        public List<int> SubPositionsLocked { get; set; }

        [JsonProperty("element_count")]
        public int ElementCount { get; set; }
    }

    public class Root
    {
        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        [JsonProperty("game_settings")]
        public GameSettings GameSettings { get; set; }

        [JsonProperty("phases")]
        public List<Phase> Phases { get; set; }

        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }

        [JsonProperty("total_players")]
        public int TotalPlayers { get; set; }

        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }

        [JsonProperty("element_stats")]
        public List<ElementStat> ElementStats { get; set; }

        [JsonProperty("element_types")]
        public List<ElementType> ElementTypes { get; set; }
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