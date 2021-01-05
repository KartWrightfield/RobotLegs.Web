using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FPLCore
{
    

    public partial class TeamGameweekSelections
    {
        [JsonProperty("active_chip")]
        public object ActiveChip { get; set; }

        [JsonProperty("automatic_subs")]
        public object[] AutomaticSubs { get; set; }

        [JsonProperty("entry_history")]
        public Dictionary<string, long?> EntryHistory { get; set; }

        [JsonProperty("picks")]
        public Pick[] Picks { get; set; }
    }

    public partial class Pick
    {
        [JsonProperty("element")]
        public long Element { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("multiplier")]
        public long Multiplier { get; set; }

        [JsonProperty("is_captain")]
        public bool IsCaptain { get; set; }

        [JsonProperty("is_vice_captain")]
        public bool IsViceCaptain { get; set; }
    }

    public partial class TeamGameweekSelections
    {
        public static TeamGameweekSelections FromJson(string json) => JsonConvert.DeserializeObject<TeamGameweekSelections>(json, TeamGameweekConverter.Settings);
    }

    public static class TeamGameweekSerialize
    {
        public static string ToJson(this TeamGameweekSelections self) => JsonConvert.SerializeObject(self, TeamGameweekConverter.Settings);
    }

    internal static class TeamGameweekConverter
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
