using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PictureGuessing.Models
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public Difficulty Difficulty { get; set; }
        public Guid pictureID { get; set; }
        public bool isFinished { get; set; } = false;
    }
}
