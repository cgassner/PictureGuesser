using System.ComponentModel.DataAnnotations;

namespace PictureGuessing.Models
{
    public class Difficulty
    {
        [Key]
        public float DifficultyScale { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }
        public float revealDelay { get; set; }
    }
}
