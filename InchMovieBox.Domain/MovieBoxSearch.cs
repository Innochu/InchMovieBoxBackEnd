using Newtonsoft.Json;

namespace InchMovieBox.Domain
{

    public class MovieBoxSearch
    {

        public string Title { get; set; } = string.Empty;

        public string Year { get; set; } = string.Empty;

        [JsonProperty("imdbID")]
        public string ImdbID { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Poster { get; set; } = string.Empty;
    }


}
