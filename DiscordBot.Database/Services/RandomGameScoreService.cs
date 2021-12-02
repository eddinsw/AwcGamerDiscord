using DiscordBot.Database.Interfaces;
using DiscordBot.Database.RandomGame;
using DiscordBot.Database.RandomGame.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DiscordBot.Database.Services
{
    public class RandomGameScoreService : IRandomGameScoreService
    {
        private readonly RandomGameContext _context;
        private readonly ILogger<RandomGameService> _logger;

        public RandomGameScoreService(RandomGameContext context, ILogger<RandomGameService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool CanGetNewScore(Game game, Guid userGuid) =>
            _context.GameScores.Any(x =>
                x.Game == game && x.userGuid == userGuid && x.NextScoreDate < DateTime.Now.Ticks);

        public int GetNewScore(Game game, Guid userGuid)
        {
            var errString = $"for user {userGuid} on {game.GuildId}";
            try
            {
                _logger.LogInformation($"Getting new Score {errString}");
                var newScore = new Random().Next(10);
                _logger.LogInformation($"Got new Score of {newScore} {errString}");
                SaveNewGameScore(CreateNewGameScore(game, userGuid, newScore));
                return newScore;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error Saving {errString}: {e.Message}", e);
                throw;
            }
        }

        private void SaveNewGameScore(GameScore newScore)
        {
            var errString = $"new score of {newScore.Score} for user {newScore.userGuid} on {newScore.Game.GuildId}";
            _logger.LogInformation($"Trying to save {errString}");
            _context.GameScores.Add(newScore);
            _context.SaveChanges();
            _logger.LogInformation($"Saved {errString}");
        }

        private static GameScore CreateNewGameScore(Game game, Guid userGuid, int newScore) => new()
        {
            NextScoreDate = DateTime.Now.AddMinutes(game.MinutesBetweenRequest).Ticks,
            Game = game,
            Score = newScore,
            userGuid = userGuid
        };
    }
}