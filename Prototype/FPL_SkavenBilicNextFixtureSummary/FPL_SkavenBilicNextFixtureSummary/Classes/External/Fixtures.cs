using System;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FPLCore
{    

    public partial class Fixtures
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("event")]
        public long? Event { get; set; }

        [JsonProperty("finished")]
        public bool Finished { get; set; }

        [JsonProperty("finished_provisional")]
        public bool FinishedProvisional { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("kickoff_time")]
        public DateTimeOffset? KickoffTime { get; set; }

        [JsonProperty("minutes")]
        public long Minutes { get; set; }

        [JsonProperty("provisional_start_time")]
        public bool ProvisionalStartTime { get; set; }

        [JsonProperty("started")]
        public bool? Started { get; set; }

        [JsonProperty("team_a")]
        public long TeamA { get; set; }

        [JsonProperty("team_a_score")]
        public long? TeamAScore { get; set; }

        [JsonProperty("team_h")]
        public long TeamH { get; set; }

        [JsonProperty("team_h_score")]
        public long? TeamHScore { get; set; }

        [JsonProperty("stats")]
        public Stat[] Stats { get; set; }

        [JsonProperty("team_h_difficulty")]
        public long TeamHDifficulty { get; set; }

        [JsonProperty("team_a_difficulty")]
        public long TeamADifficulty { get; set; }

        [JsonProperty("pulse_id")]
        public long PulseId { get; set; }
    }

    public partial class Stat
    {
        [JsonProperty("identifier")]
        public Identifier Identifier { get; set; }

        [JsonProperty("a")]
        public A[] A { get; set; }

        [JsonProperty("h")]
        public A[] H { get; set; }
    }

    public partial class A
    {
        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("element")]
        public long Element { get; set; }
    }

    public enum Identifier { Assists, Bonus, Bps, GoalsScored, OwnGoals, PenaltiesMissed, PenaltiesSaved, RedCards, Saves, YellowCards, MngUnderdogWin, MngUnderdogDraw };

    public partial class Fixtures
    {
        public static Fixtures[] FromJson(string json) => JsonConvert.DeserializeObject<Fixtures[]>(json, FixturesConverter.Settings);
    }

    public static class FixturesSerialize
    {
        public static string ToJson(this Fixtures[] self) => JsonConvert.SerializeObject(self, FixturesConverter.Settings);
    }

    internal static class FixturesConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                IdentifierConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class IdentifierConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Identifier) || t == typeof(Identifier?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "assists":
                    return Identifier.Assists;
                case "bonus":
                    return Identifier.Bonus;
                case "bps":
                    return Identifier.Bps;
                case "goals_scored":
                    return Identifier.GoalsScored;
                case "own_goals":
                    return Identifier.OwnGoals;
                case "penalties_missed":
                    return Identifier.PenaltiesMissed;
                case "penalties_saved":
                    return Identifier.PenaltiesSaved;
                case "red_cards":
                    return Identifier.RedCards;
                case "saves":
                    return Identifier.Saves;
                case "yellow_cards":
                    return Identifier.YellowCards;
                case "mng_underdog_win":
                    return Identifier.MngUnderdogWin;
                case "mng_underdog_draw":
                    return Identifier.MngUnderdogDraw;
            }
            throw new Exception("Cannot unmarshal type Identifier");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Identifier)untypedValue;
            switch (value)
            {
                case Identifier.Assists:
                    serializer.Serialize(writer, "assists");
                    return;
                case Identifier.Bonus:
                    serializer.Serialize(writer, "bonus");
                    return;
                case Identifier.Bps:
                    serializer.Serialize(writer, "bps");
                    return;
                case Identifier.GoalsScored:
                    serializer.Serialize(writer, "goals_scored");
                    return;
                case Identifier.OwnGoals:
                    serializer.Serialize(writer, "own_goals");
                    return;
                case Identifier.PenaltiesMissed:
                    serializer.Serialize(writer, "penalties_missed");
                    return;
                case Identifier.PenaltiesSaved:
                    serializer.Serialize(writer, "penalties_saved");
                    return;
                case Identifier.RedCards:
                    serializer.Serialize(writer, "red_cards");
                    return;
                case Identifier.Saves:
                    serializer.Serialize(writer, "saves");
                    return;
                case Identifier.YellowCards:
                    serializer.Serialize(writer, "yellow_cards");
                    return;
                case Identifier.MngUnderdogWin:
                    serializer.Serialize(writer, "mng_underdog_win");
                    return;
                case Identifier.MngUnderdogDraw:
                    serializer.Serialize(writer, "mng_underdog_draw");
                    return;
            }
            throw new Exception("Cannot marshal type Identifier");
        }

        public static readonly IdentifierConverter Singleton = new IdentifierConverter();
    }
}
