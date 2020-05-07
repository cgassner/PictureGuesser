using System;
using System.ComponentModel.DataAnnotations;

namespace PictureGuessing.Models
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public Difficulty Difficulty { get; set; }
        public Guid pictureID { get; set; }
        public bool isFinished { get; set; } = false;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime Endtime { get; set; }
        public string Category { get; set; }
    }
}
