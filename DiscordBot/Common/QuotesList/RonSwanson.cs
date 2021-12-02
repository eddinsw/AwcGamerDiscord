using System;
using System.Collections.Generic;

namespace DiscordBot.Common.QuotesList
{
    public static class RonSwansonQuotes
    {
        private static readonly Random Rand = new Random();

        public static List<string> All =>
            FileHelper.EmbeddedJsonResourceToList<string>("DiscordBot.Common.Files.ronSwanson.json");

        public static int Count => All.Count;

        public static string RandomMessage => $"Ron Swanson - {All[Rand.Next(Count)]}";
    }
}