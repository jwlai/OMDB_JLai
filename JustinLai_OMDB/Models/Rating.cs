using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JustinLai_OMDB.Models
{
    [Serializable]
    public class Rating
    {
        [DataMember]
        [JsonProperty("source")]
        public string Source { get; set; }
        
        [DataMember]
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}