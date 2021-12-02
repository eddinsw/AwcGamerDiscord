namespace DiscordBot.Common.QuotesList.Model
{
    internal class JsonQuote
    {
        public string Author { get; set; }
        public string Text { get; set; }

        public string FullFormat => $"\"{Text}\" - {Author}";
    }
}