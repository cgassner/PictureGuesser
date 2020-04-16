using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureGuessing.Models
{
    public class LeaderboardEntryRequest
    {
        public Guid gameId { get; set; }
        public string playername { get; set; }
    }
}
