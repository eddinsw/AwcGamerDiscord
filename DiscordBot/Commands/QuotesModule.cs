using Discord;
using Discord.Commands;
using DiscordBot.Common.QuotesList;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    [Name("quote")]
    public class QuotesModule : ModuleBase<SocketCommandContext>
    {
        [Command("quote")]
        [Summary("Make the bot say Random Quote")]
        public Task SayQuote()
            => new Random().Next(6) switch
            {
                0 => SayAlcoholQuote(),
                1 => SayBeerQuote(),
                2 => SayItQuote(),
                3 => SayMovieQuote(),
                4 => SayRonQuote(),
                5 => SayChuckQuote(),
                6 => QuoteEmbed(Quotes.RandomMessage),
                _ => ReplyAsync("Shoot Developer")
            };

        [Command("alcohol")]
        [Summary("Make the bot say Random Alcohol Quote")]
        public Task SayAlcoholQuote()
            => QuoteEmbed(AlcoholQuotes.RandomMessage);

        [Command("beer")]
        [Summary("Make the bot say Random Beer Quote")]
        public Task SayBeerQuote()
            => QuoteEmbed(BeerQuotes.RandomMessage);

        [Command("it")]
        [Summary("Make the bot say Random IT Quote")]
        public Task SayItQuote()
            => QuoteEmbed(DeveloperQuotes.RandomMessage);

        [Command("movie")]
        [Summary("Make the bot say Random movie Quote")]
        public Task SayMovieQuote()
            => QuoteEmbed(MovieQuotes.RandomMessage);

        [Command("ron swanson")]
        [Summary("Make the bot say Random Ron Swanson Quote")]
        public Task SayRonQuote()
            => QuoteEmbed(RonSwansonQuotes.RandomMessage);

        [Command("chuck norris")]
        [Summary("Make the bot say Random Chuck Norris Quote")]
        public Task SayChuckQuote()
            => QuoteEmbed(ChuckNorrisQuotes.RandomMessage());

        [Command("dad joke")]
        [Summary("Make the bot say Random Dad Joke")]
        public Task SayDadJokeQuote()
            => QuoteEmbed(DadJokes.RandomMessage());

        private Task QuoteEmbed(string message)
            => ReplyAsync("", false, new EmbedBuilder { Color = new Color(114, 137, 218), Description = $"**{message}**" }.Build());
    }
}