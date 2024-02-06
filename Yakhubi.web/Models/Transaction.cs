using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yakhubi.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string RecipientName { get; set; }
        public string RecipientId { get; set; }
        public string Amount { get; set; }
        public string RecipientMail { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientAddress { get; set; }
        public string Method { get; set; }
        public DateTime Created { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Src { get; set; }
        public string Dst { get; set; }
        public string User { get; set; }
        
        
    }
    public class Method
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
     public partial class Country
    {
        [JsonProperty("name")]
        public Name Name { get; set; }
    }

    public partial class Name
    {
        [JsonProperty("common")]
        public string Common { get; set; }
    }

    public partial class Country
    {
        public static Country FromJson(string json) => JsonConvert.DeserializeObject<Country>(json, Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Country self) => JsonConvert.SerializeObject(self, Models.Converter.Settings);
    }

    internal static class Converter
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

// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var welcome = Welcome.FromJson(jsonString);
   