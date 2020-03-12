using System;
using System.Collections.Generic;
using System.Text;

namespace TschechenProjektUI
{
    public class Game_
    {
        public Guid Id { get; set; } = new Guid();
        public Difficulty Difficulty { get; set; }
        public Guid pictureID { get; set; }
        public bool isFinished { get; set; }

    }
}
