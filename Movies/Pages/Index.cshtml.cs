using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The movies to display on the index page 
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms 
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; }

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] MPAARating { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] Genres { get; set; }

        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        public double? IMDBMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        public double? IMDBMax { get; set; }

        /// <summary>
        /// Does the response initialization for incoming GET requests
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax)
        {

            /*this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARating);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);*/

            Movies = MovieDatabase.All;
            // Search for title
            if(SearchTerms != null)
            {
                Movies = Movies.Where(movie => { return movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase); });
            }
            // Filter by mpaa rating
            if(MPAARating != null && MPAARating.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MPAARating != null && MPAARating.Contains(movie.MPAARating));
            }
            // Filter by Genre
            if (Genres != null)
            {
                Movies = Movies.Where(movie => movie.MajorGenre != null && Genres.Contains(movie.MajorGenre));
            }

            // Filter by IMDB score
            if(IMDBMax != null && IMDBMin == null || IMDBMin != null && IMDBMax == null || IMDBMax != null && IMDBMin != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax);
            }
        }
    }
}
