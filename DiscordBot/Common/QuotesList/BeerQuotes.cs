using System;
using System.Collections.Generic;

namespace DiscordBot.Common.QuotesList
{
    public static class BeerQuotes
    {
        private static readonly Random Rand = new Random();

        public static List<string> All => FileHelper.EmbeddedTextFileResourceToStringList("DiscordBot.Common.Files.BeerQuotes.txt");

        public static int Count => All.Count;

        public static string RandomMessage => All[Rand.Next(Count)];
    }
}