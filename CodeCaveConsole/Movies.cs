using System.Collections.Generic;

namespace CodeCaveConsole
{
    public static class Movies
    {
        public static List<string> MoviesList = new();

        public static List<string> LoadMoviesList()
        {
            MoviesList = new List<string>()
            {
                new string("Killer Bean"),
                new string("Rush Hour"),
                new string("Black Widow"),
                new string("Black Panther"),
                new string("Joker"),
                new string("Spider-man"),
                new string("G.I Joe"),
            };
            return MoviesList;
        }
    }
}
