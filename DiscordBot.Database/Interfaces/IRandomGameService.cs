using DiscordBot.Database.RandomGame.Models;
using System;
using System.Collections.Generic;

namespace DiscordBot.Database.Interfaces
{
    public interface IRandomGameService
    {
        bool GameExists(Guid guildId);

        string CreateNewGame(Guid guildId, int numberOfDays, int minutesBetweenRequest);

        IEnumerable<(Guid, int)> GetTop5(Guid guildGuid);

        Game GetActiveGame(Guid guildId);
    }
}