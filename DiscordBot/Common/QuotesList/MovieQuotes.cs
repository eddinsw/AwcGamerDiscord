using DiscordBot.Common.QuotesList.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscordBot.Common.QuotesList
{
    public class MovieQuotes
    {
        private static readonly Random Rand = new Random();

        private static IEnumerable<JsonMovieQuote> FullList =>
            FileHelper.EmbeddedJsonResourceToList<JsonMovieQuote>("DiscordBot.Common.Files.MovieQuotes.json");

        public static List<string> All => FullList.Select(x => $"{x.Quote} - {x.Movie} ({x.Year})").ToList();

        public static int Count => All.Count;

        public static string RandomMessage => All[Rand.Next(Count)];
    }
}