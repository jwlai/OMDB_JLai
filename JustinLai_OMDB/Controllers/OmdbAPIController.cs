using JustinLai_OMDB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JustinLai_OMDB.Controllers
{
    public class OmdbAPIController : Controller
    {
        private static HttpClient client = new HttpClient();
        private static string host = "www.omdbapi.com";

        public OmdbAPIController()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private Uri BuildSearchUri(string type = "movie", string title = "", string year = "", int page = 1)
        {
            UriBuilder builder = new UriBuilder();
            builder.Host = host;
            builder.Scheme = "http://";

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["s"] = title;

            if (!String.IsNullOrWhiteSpace(title))
            {
                query["type"] = type;
            }

            if (!String.IsNullOrWhiteSpace(year))
            {
                query["y"] = year;
            }

            if(page > 0 && page <= 100)
            {
                query["page"] = page.ToString();
            }

            builder.Query = query.ToString();

            return builder.Uri;
        }

        private Uri BuildMovieUri(string type = "movie", string title = "", string imdbId = "", string year = "")
        {
            UriBuilder builder = new UriBuilder();
            builder.Host = host; 
            builder.Scheme = "http://";

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["plot"] = "full";

            if (!String.IsNullOrWhiteSpace(type))
            {
                query["type"] = type;
            }

            if (!String.IsNullOrWhiteSpace(title))
            {
                query["t"] = title;
            }
            if (!String.IsNullOrWhiteSpace(imdbId))
            {
                query["i"] = imdbId;
            }
            if (!String.IsNullOrWhiteSpace(year))
            {
                query["y"] = year;
            }

            builder.Query = query.ToString();

            return builder.Uri;
        }

        private MovieSearch SearchMovieAPI(Uri uri)
        {
            MovieSearch searchResults = new MovieSearch();
            HttpResponseMessage result = client.GetAsync(uri).Result;
            if (result.IsSuccessStatusCode)
            {
                try
                {
                    searchResults = JsonConvert.DeserializeObject<MovieSearch>(result.Content.ReadAsStringAsync().Result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return searchResults;
        }

        public Movie GetMovieAPI(Uri uri)
        {
            Movie resultMovie = new Movie();
            HttpResponseMessage result = client.GetAsync(uri).Result;

            if (result.IsSuccessStatusCode)
            {
                try
                {
                    resultMovie = JsonConvert.DeserializeObject<Movie>(result.Content.ReadAsStringAsync().Result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return resultMovie;
        }

        public JsonResult GetMovieByTitle(string title, string imdbId, string year, string type = "movie")
        {
            Uri uri = BuildMovieUri(type, title, imdbId, year);
            Movie result = GetMovieAPI(uri);
            
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SearchMovieByTitle(string title, string year, int page, string type = "movie")
        {
            Uri uri = BuildSearchUri(type,title,year,page);
            MovieSearch result = SearchMovieAPI(uri);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}