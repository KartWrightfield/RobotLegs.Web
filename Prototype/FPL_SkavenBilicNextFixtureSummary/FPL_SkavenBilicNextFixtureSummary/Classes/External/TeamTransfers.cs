using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FPL_SkavenBilicNextFixtureSummary.Classes.External
{
    public partial class TeamTransfers
    {
        [JsonProperty("element_in")]
        public int ElementIn { get; set; }

        [JsonProperty("element_in_cost")]
        public int ElementInCost { get; set; }

        [JsonProperty("element_out")]
        public int ElementOut { get; set; }

        [JsonProperty("element_out_cost")]
        public int ElementOutCost { get; set; }

        [JsonProperty("entry")]
        public int Entry { get; set; }

        [JsonProperty("event")]
        public int Event { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }

    public partial class TeamTransfers
    {
        public static TeamTransfers[] FromJson(string json) => JsonConvert.DeserializeObject<TeamTransfers[]>(json, TeamTransfersConverter.Settings);
    }

    public static class TeamTransfersSerialize
    {
        public static string ToJson(this TeamTransfers[] self) => JsonConvert.SerializeObject(self, TeamTransfersConverter.Settings);
    }

    internal static class TeamTransfersConverter
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
