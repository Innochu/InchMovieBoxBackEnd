using InchMovieBox.Application.Services;
using Moq;
using System.Net;

namespace InchMovieTests
{
    public class MovieBoxServiceTests
    {
        [Fact]
        public async Task SearchMoviesAsync_ReturnsSearchResults_WhenApiCallSucceeds()
        {
            // Arrange
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"Search\":[{\"Title\":\"Guardians of the Galaxy Vol. 2\",\"Year\":\"2017\",\"imdbID\":\"tt3896198\",\"Type\":\"movie\",\"Poster\":\"https://m.media-amazon.com/images/M/MV5BNjM0NTc0NzItM2FlYS00YzEwLWE0YmUtNTA2ZWIzODc2OTgxXkEyXkFqcGdeQXVyNTgwNzIyNzg@._V1_SX300.jpg\"}]}")
                });
            mockHttpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient.Object);

            var service = new MovieBoxService(mockHttpClientFactory.Object);

            // Act
            var searchResults = await service.SearchMoviesAsync("Guardians of the Galaxy Vol. 2");

            // Assert
            Assert.NotEmpty(searchResults);
            Assert.Equal("Guardians of the Galaxy Vol. 2", searchResults.First().Title);
        }

        [Fact]
        public async Task MoviesBoxDetailsAsync_ReturnsMovieDetails_WhenApiCallSucceeds()
        {
            // Arrange
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"Title\":\"Guardians of the Galaxy Vol. 2\",\"Year\":\"2017\",\"Rated\":\"PG-13\",\"Released\":\"05 May 2017\",\"Runtime\":\"136 min\",\"Genre\":\"Action, Adventure, Comedy\",\"Director\":\"James Gunn\",\"Writer\":\"James Gunn, Dan Abnett, Andy Lanning\",\"Actors\":\"Chris Pratt, Zoe Saldana, Dave Bautista\",\"Plot\":\"The Guardians struggle to keep together as a team while dealing with their personal family issues, notably Star-Lord's encounter with his father, the ambitious celestial being Ego.\",\"Language\":\"English\",\"Country\":\"United States\",\"Awards\":\"Nominated for 1 Oscar. 15 wins & 60 nominations total\",\"Poster\":\"https://m.media-amazon.com/images/M/MV5BNjM0NTc0NzItM2FlYS00YzEwLWE0YmUtNTA2ZWIzODc2OTgxXkEyXkFqcGdeQXVyNTgwNzIyNzg@._V1_SX300.jpg\",\"Ratings\":[{\"Source\":\"Internet Movie Database\",\"Value\":\"7.6/10\"},{\"Source\":\"Rotten Tomatoes\",\"Value\":\"85%\"},{\"Source\":\"Metacritic\",\"Value\":\"67/100\"}],\"Metascore\":\"67\",\"imdbRating\":\"7.6\",\"imdbVotes\":\"757,026\",\"imdbID\":\"tt3896198\",\"Type\":\"movie\",\"DVD\":\"10 Jul 2017\",\"BoxOffice\":\"$389,813,101\",\"Production\":\"N/A\",\"Website\":\"N/A\",\"Response\":\"True\"}")
                });
            mockHttpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient.Object);

            var service = new MovieBoxService(mockHttpClientFactory.Object);

            // Act
            var movieDetails = await service.MoviesBoxDetailsAsync("tt3896198");

            // Assert
            Assert.NotNull(movieDetails);
            Assert.Equal("Guardians of the Galaxy Vol. 2", movieDetails.Title);
            Assert.Equal("2017", movieDetails.Year);
        }
    }
}
