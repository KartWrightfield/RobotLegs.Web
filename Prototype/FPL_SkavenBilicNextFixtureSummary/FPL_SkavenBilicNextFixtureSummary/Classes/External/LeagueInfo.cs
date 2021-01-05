using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FPLCore
{  

    public partial class LeagueInfo
    {
        [JsonProperty("league")]
        public League League { get; set; }

        [JsonProperty("new_entries")]
        public NewEntries NewEntries { get; set; }

        [JsonProperty("standings")]
        public NewEntries Standings { get; set; }
    }

    public partial class League
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("max_entries")]
        public object MaxEntries { get; set; }

        [JsonProperty("league_type")]
        public string LeagueType { get; set; }

        [JsonProperty("scoring")]
        public string Scoring { get; set; }

        [JsonProperty("admin_entry")]
        public long AdminEntry { get; set; }

        [JsonProperty("start_event")]
        public long StartEvent { get; set; }

        [JsonProperty("code_privacy")]
        public string CodePrivacy { get; set; }

        [JsonProperty("rank")]
        public object Rank { get; set; }
    }

    public partial class NewEntries
    {
        [JsonProperty("has_next")]
        public bool HasNext { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("event_total")]
        public long EventTotal { get; set; }

        [JsonProperty("player_name")]
        public string PlayerName { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("last_rank")]
        public long LastRank { get; set; }

        [JsonProperty("rank_sort")]
        public long RankSort { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("entry")]
        public long Entry { get; set; }

        [JsonProperty("entry_name")]
        public string EntryName { get; set; }
    }

    public partial class LeagueInfo
    {
        public static LeagueInfo FromJson(string json) => JsonConvert.DeserializeObject<LeagueInfo>(json, LeagueConverter.Settings);
    }

    public static class LeagueSerialize
    {
        public static string ToJson(this LeagueInfo self) => JsonConvert.SerializeObject(self, LeagueConverter.Settings);
    }

    internal static class LeagueConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
