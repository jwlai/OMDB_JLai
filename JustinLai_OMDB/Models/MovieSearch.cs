using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JustinLai_OMDB.Models
{
    [DataContract]
    public class MovieSearch
    {
        [DataMember]
        [JsonProperty("search")]
        public List<MoviePartial> Search {get; set;}

        [DataMember]
        [JsonProperty("totalresults")]
        public int TotalResults { get; set; }

        [DataMember]
        [JsonProperty("response")]
        public bool Response { get; set; }

        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; }
    }

    [DataContract]
    public class MoviePartial
    {
        [DataMember]
        [JsonProperty("title")]
        public string Title { get; set; }

        [DataMember]
        [JsonProperty("year")]
        public int Year { get; set; }

        [DataMember]
        [JsonProperty("imdbid")]
        public string ImdbId { get; set; }

        [DataMember]
        [JsonProperty("type")]
        public string Type { get; set; }

        [DataMember]
        [JsonProperty("poster")]
        public string Poster { get; set; }
    }
}