using Discord;
using Discord.Commands;
using DiscordBot.Common.Image;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    [Name("Image")]
    public class ImageModule : ModuleBase<SocketCommandContext>
    {
        [Command("image dog")]
        [Summary("Random Dog Image")]
        public Task DisplayDog()
        {
            return ImageEmbed("Dog", DogImage.Image());
        }

        [Command("image cat")]
        [Summary("Random Cat Image")]
        public Task DisplayCat()
        {
            return ImageEmbed("Cat", CatImage.Image());
        }

        [Command("image duck")]
        [Summary("Random Duck Image")]
        public Task DisplayDuck()
        {
            return ImageEmbed("Duck", DuckImage.Image());
        }

        [Command("image fox")]
        [Summary("Random Fox Image")]
        public Task DisplayFox()
        {
            return ImageEmbed("Fox", FoxImage.Image());
        }

        [Command("image unsplash")]
        [Summary("Random Image")]
        public Task DisplayUnsplash()
        {
            return ImageEmbed("Image From UnSplash", UnsplashImage.Image());
        }

        [Command("image background")]
        [Summary("Random background")]
        public Task DisplayBackGround()
        {
            return ImageEmbedWithLinks(UnsplashImage.Background());
        }

        [Command("image background p")]
        [Summary("Random background portrait")]
        public Task DisplayBackGroundP()
        {
            return ImageEmbedWithLinks(UnsplashImage.Background(true));
        }

        [Command("image")]
        [Summary("Random Image")]
        public Task DisplayRandom()
            => new Random().Next(5) switch
            {
                0 => DisplayCat(),
                1 => DisplayDog(),
                2 => DisplayDuck(),
                3 => DisplayFox(),
                4 => DisplayUnsplash(),
                _ => ReplyAsync("Shoot Developer")
            };

        private Task ImageEmbed(string imageType, string url)
        {
            var newMessage = new EmbedBuilder
            {
                Title = $"Random {imageType}",
                ImageUrl = url
            };

            return ReplyAsync("", false, newMessage.Build());
        }

        private Task ImageEmbedWithLinks(Dictionary<string, string> urls)
        {
            string small = urls["small"];
            string fullSize = urls["fullSize"];

            var newMessage = new EmbedBuilder
            {
                Title = "Link to FullSize Image",
                ImageUrl = small
            };

            newMessage.WithUrl(fullSize);

            return ReplyAsync("", false, newMessage.Build());
        }
    }
}