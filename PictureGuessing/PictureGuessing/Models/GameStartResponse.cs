using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PictureGuessing.Models
{
    public class GameStartResponse
    {
        public Guid Id { get; set; }
        public Difficulty Difficulty { get; set; }
        public Guid pictureID { get; set; }
        public bool isFinished { get; set; } = false;
    }
}
