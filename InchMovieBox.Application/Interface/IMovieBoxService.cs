using InchMovieBox.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InchMovieBox.Application.Interface
{
    public interface IMovieBoxService
    {
        Task<MovieBoxDetail> MoviesBoxDetailsAsync(string imdbId);
        Task<IEnumerable<MovieBoxSearch>> SearchMoviesAsync(string title);
    }
}
