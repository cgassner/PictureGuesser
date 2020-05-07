using System;
using System.ComponentModel.DataAnnotations;

namespace PictureGuessing.Models
{
    public class LeaderboardEntry
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public float DifficultyScale { get; set; }
        public string Category { get; set; }
        public double TimeInSeconds { get; set; }
    }
}
