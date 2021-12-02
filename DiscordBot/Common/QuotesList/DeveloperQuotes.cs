using DiscordBot.Common.QuotesList.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscordBot.Common.QuotesList
{
    public class DeveloperQuotes
    {
        private static readonly Random Rand = new Random();

        private static List<JsonQuote> FullList =>
            FileHelper.EmbeddedJsonResourceToList<JsonQuote>("DiscordBot.Common.Files.DeveloperQuotes.json");

        public static List<string> All => FullList.Select(x => x.FullFormat).ToList();

        public static int Count => All.Count;

        public static string RandomMessage => All[Rand.Next(Count)];
    }
}