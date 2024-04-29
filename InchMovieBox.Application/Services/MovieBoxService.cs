using InchMovieBox.Application.Interface;
using InchMovieBox.Domain;
using Newtonsoft.Json;

namespace InchMovieBox.Application.Services
{
    public class MovieBoxService : IMovieBoxService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly string _apiKey = "95c14f9d";
        private readonly string _id = "tt3896198";

        public MovieBoxService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        public async Task<IEnumerable<MovieBoxSearch>> SearchMoviesAsync(string title)
        {


            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri($"http://www.omdbapi.com/");

                var response = await httpClient.GetAsync($"?apikey={_apiKey}&i={_id}&s={title}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var searchResponse = JsonConvert.DeserializeObject<OmdbSearch>(content);

                    return searchResponse.Search;
                }
                else
                {
                    return Enumerable.Empty<MovieBoxSearch>();
                }
            }
        }

        public async Task<MovieBoxDetail> MoviesBoxDetailsAsync(string imdbId)
        {


            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri($"http://www.omdbapi.com/");

                var response = await httpClient.GetAsync($"?apikey={_apiKey}&i={imdbId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MovieBoxDetail>(content);

                }
                else
                {
                    return null;
                }
            }
        }
    }


}
