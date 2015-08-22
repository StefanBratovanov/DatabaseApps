using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstMovies
{
    class CodeFirstMovies
    {
        static void Main()
        {
            var context = new MoviesContext();

            var count = context.Movies.Count();
            Console.WriteLine(count);
        }
    }
}
