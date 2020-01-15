using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PictureGuessing.Models
{
    public class LeaderboardEntry
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public Guid DifficultyID { get; set; }
        //Category
    }
}
