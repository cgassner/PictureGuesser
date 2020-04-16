using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Drawing;
using System.Threading.Tasks;

namespace PictureGuessing.Models
{
    public class Picture
    {

        [Key]
        public Guid Id { get; set; } = new Guid();
        public string URL { get; set; }
        public string Answer { get; set; }
        public int AnswerLength => Answer.Length;
        public string Category { get; set; }
    }

}
