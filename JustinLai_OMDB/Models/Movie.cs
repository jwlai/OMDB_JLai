using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JustinLai_OMDB.Models
{
    [Serializable]
    [DataContract]
    public class Movie
    {
        [DataMember]
        [JsonProperty("title")]
        public string Title { get; set; }

        [DataMember]
        [JsonProperty("year")]
        public string Year { get; set; }

        [DataMember]
        [JsonProperty("rated")]
        public string Rated { get; set; }

        [DataMember]
        [JsonProperty("released")]
        public string Released { get; set; }

        [DataMember]
        [JsonProperty("runtime")]
        public string Runtime { get; set; }


        [DataMember]
        [JsonProperty("genre")]
        public string Genre { get; set; }


        [DataMember]
        [JsonProperty("director")]
        public string Director { get; set; }

        [DataMember]
        [JsonProperty("writer")]
        public string Writer { get; set; }
        
        [DataMember]
        [JsonProperty("actors")]
        public string Actors { get; set; }

        [DataMember]
        [JsonProperty("plot")]
        public string Plot { get; set; }

        [DataMember]
        [JsonProperty("language")]
        public string Language { get; set; }

        [DataMember]
        [JsonProperty("country")]
        public string Country { get; set; }

        [DataMember]
        [JsonProperty("awards")]
        public string Awards { get; set; }

        [DataMember]
        [JsonProperty("poster")]
        public string Poster { get; set; }

        [DataMember]
        [JsonProperty("ratings")]
        public List<Rating> Ratings { get; set; }

        [DataMember]
        [JsonProperty("metascore")]
        public string Metascore { get; set; }

        [DataMember]
        [JsonProperty("imdbrating")]
        public string ImdbRating { get; set; }

        [DataMember]
        [JsonProperty("imdbvotes")]
        public string ImdbVotes { get; set; }

        [DataMember]
        [JsonProperty("imdbid")]
        public string ImdbId { get; set; }

        [DataMember]
        [JsonProperty("type")]
        public string Type { get; set; }

        [DataMember]
        [JsonProperty("dvd")]
        public string DVD { get; set; }

        [DataMember]
        [JsonProperty("boxoffice")]
        public string BoxOffice { get; set; }

        [DataMember]
        [JsonProperty("production")]
        public string Production { get; set; }

        [DataMember]
        [JsonProperty("website")]
        public string Website { get; set; }

        [DataMember]
        [JsonProperty("response")]
        public string Response { get; set; }
    }
}