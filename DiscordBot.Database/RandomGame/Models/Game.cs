using DiscordBot.Database.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscordBot.Database.RandomGame.Models
{
    public class Game
    {
        [Key, Required] public Guid GameId { get; set; }
        [Required] public Guid GuildId { get; set; }
        public string CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public int DaysToRun { get; set; } = Constants.DEFAULTDAYS;
        public int MinutesBetweenRequest { get; set; } = Constants.MINUTESBETWEENREQUEST;

        public virtual bool Completed =>
            Convert.ToDateTime(CreateDate).Add(new TimeSpan(DaysToRun, 0, 0, 0)) <= DateTime.Now;

        public virtual bool ReadyToDelete =>
            Convert.ToDateTime(CreateDate).Add(new TimeSpan(DaysToRun, 0, 0, 0)) <= DateTime.Now;

        public virtual List<GameScore> GameScores { get; set; }
    }
}