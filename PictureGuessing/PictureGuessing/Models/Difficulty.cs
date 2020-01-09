using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
