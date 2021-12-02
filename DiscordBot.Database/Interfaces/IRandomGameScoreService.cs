using DiscordBot.Database.RandomGame.Models;
using System;

namespace DiscordBot.Database.Interfaces
{
    public interface IRandomGameScoreService
    {
        bool CanGetNewScore(Game game, Guid userGuid);

        int GetNewScore(Game game, Guid userGuid);
    }
}