using DiscordBot.Database.Common;
using DiscordBot.Database.Interfaces;
using DiscordBot.Database.RandomGame;
using DiscordBot.Database.RandomGame.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscordBot.Database.Services
{
    public class RandomGameService : IRandomGameService
    {
        private readonly RandomGameContext _context;
        private readonly ILogger<RandomGameService> _logger;

        public RandomGameService(RandomGameContext context, ILogger<RandomGameService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool GameExists(Guid guildId) => _context.Games.Any(x => x.GuildId == guildId && !x.Completed);

        public string CreateNewGame(Guid guildId, int numberOfDays = Constants.DEFAULTDAYS, int minutesBetweenRequest = Constants.MINUTESBETWEENREQUEST)
        {
            DeleteGames(guildId);
            if (GameExists(guildId))
            {
                _logger.LogInformation($"Game already exists fro  {guildId}");
                return "Game already exists";
            }

            _logger.LogInformation($"Creating Game for {guildId}");
            try
            {
                var newGame = GetNewGame(guildId, numberOfDays, minutesBetweenRequest);
                _context.Add(newGame);
                _context.SaveChanges();
                return "Created new game";
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Create Game for {guildId}", e);
                return $"Error Creating Game: {e.Message}";
            }
        }

        public IEnumerable<(Guid, int)> GetTop5(Guid guildGuid) =>
            GetActiveGame(guildGuid).GameScores.GroupBy(x => x.userGuid)
                .Select(s => (s.Key, s.Sum(t => t.Score)))
                .OrderByDescending(or => or.Item2)
                .Take(5);

        public Game GetActiveGame(Guid guildId) =>
            _context.Games.FirstOrDefault(x => x.GuildId == guildId && !x.Completed);

        private void DeleteGames(Guid guildId)
        {
            _logger.LogInformation($"Deleting Games for {guildId}");
            var toRemove = _context.Games.Where(x => x.GameId == guildId && x.ReadyToDelete);
            if (!toRemove.Any()) return;
            _logger.LogInformation($"Trying to delete {toRemove.Count()} Games for {guildId}");
            _context.RemoveRange(toRemove);
            _context.SaveChanges();
            _logger.LogInformation($"Deleted {toRemove.Count()} Games for {guildId}");
        }

        private static Game GetNewGame(Guid guildId, int numberOfDays, int minutesBetweenRequest) => new()
        {
            GameId = new Guid(),
            GuildId = guildId,
            DaysToRun = numberOfDays,
            MinutesBetweenRequest = minutesBetweenRequest,
            CreateDate = DateTime.Now.ToString("yyyy-MM-dd")
        };
    }
}