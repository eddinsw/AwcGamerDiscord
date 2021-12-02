using System;
using System.Collections.Generic;

namespace DiscordBot.Common.QuotesList
{
    public static class Insults
    {
        private static readonly Random Rand = new Random();

        public static List<string> All =>
            FileHelper.EmbeddedJsonResourceToList<string>("DiscordBot.Common.Files.insults.json");

        public static int Count => All.Count;

        public static string RandomMessage => All[Rand.Next(Count)];
    }
}