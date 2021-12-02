using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    [Name("roll")]
    [RequireContext(ContextType.Guild)]
    public class RollModule : ModuleBase<SocketCommandContext>
    {
        [Name("roll")]
        [Group("roll")]
        public class RollBase : ModuleBase
        {
            [Command, Priority(0)]
            [Summary("Roll up to 5 Dice")]
            public async Task RollDice(int numberOfDice = 1, int numberOfSides = 6)
            {
                if (!(Context.User is SocketGuildUser user)) return;
                await ReplyAsync(Message(user.Nickname, numberOfDice, $"D{numberOfSides}", ListOfDice(numberOfDice, numberOfSides)));
            }

            [Command("D4"), Priority(1)]
            [Summary("Roll up to 5 D4")]
            public async Task RollMoreDice4([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 4);

            [Command("D5"), Priority(1)]
            [Summary("Roll up to 5 D5")]
            public async Task RollMoreDice5([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 5);

            [Command("D6"), Priority(1)]
            [Summary("Roll up to 5 D6")]
            public async Task RollMoreDice6([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 6);

            [Command("D7"), Priority(1)]
            [Summary("Roll up to 5 D7")]
            public async Task RollMoreDice7([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 7);

            [Command("D8"), Priority(1)]
            [Summary("Roll up to 5 D8")]
            public async Task RollMoreDice8([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 8);

            [Command("D10"), Priority(1)]
            [Summary("Roll up to 5 D10")]
            public async Task RollMoreDice10([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 10);

            [Command("D12"), Priority(1)]
            [Summary("Roll up to 5 D12")]
            public async Task RollMoreDice12([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 12);

            [Command("D20"), Priority(1)]
            [Summary("Roll up to 5 D20")]
            public async Task RollMoreDice20([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 20);

            [Command("D48"), Priority(1)]
            [Summary("Roll up to 5 D48")]
            public async Task RollMoreDice48([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 48);

            [Command("D100"), Priority(1)]
            [Summary("Roll up to 5 D100")]
            public async Task RollMoreDice100([Remainder] int numberOfDice = 1)
                => await RollDice(numberOfDice, 100);

            private static string Message(string user, int numberOfDice, string diceType, string roll) =>
                $"**{user}** rolled {MaxNumber(numberOfDice)} * {diceType}{Environment.NewLine}{roll}";

            private static string ListOfDice(int numberOfDice, int numberOfSides = 6)
            {
                var listOfDice = new List<int>();
                var totalOfDice = 0;
                for (var i = 0; i < MaxNumber(numberOfDice); i++)
                {
                    var roll = RollRand(numberOfSides);
                    listOfDice.Add(roll);
                    totalOfDice += roll;
                }
                return $"{(string.Join(":", listOfDice)).Replace(":", " - ")}{Environment.NewLine}Total: {totalOfDice}";
            }

            private static int RollRand(int customSize) => (new Random()).Next(customSize) + 1;

            private static int MaxNumber(int numberOfDice) => numberOfDice > MaxDice ? MaxDice : numberOfDice;

            private const int MaxDice = 5;
        }
    }
}