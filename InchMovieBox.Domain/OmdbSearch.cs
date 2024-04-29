using Newtonsoft.Json;

namespace InchMovieBox.Domain
{
    public class OmdbSearch
    {
        [JsonProperty(nameof(Search))]
        public IEnumerable<MovieBoxSearch> Search { get; set; } = Enumerable.Empty<MovieBoxSearch>();

        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }  = string.Empty;

        public string Response { get; set; } = string.Empty;
    }
}
