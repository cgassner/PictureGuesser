using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureGuessing.Models
{
    public class PictureResponse
    {
        public Guid Id { get; set; }
        public string URL { get; set; }
        public int AnswerLength { get; set; }
        public string Category { get; set; }
    }
}
