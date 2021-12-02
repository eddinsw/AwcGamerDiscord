using System;
using System.ComponentModel.DataAnnotations;

namespace DiscordBot.Database.RandomGame.Models
{
    public class GameScore
    {
        [Key]
        public Guid GameScoreId { get; set; }

        public Guid userGuid { get; set; }
        public long NextScoreDate { get; set; }
        public int Score { get; set; }

        public virtual Game Game { get; set; }
    }
}