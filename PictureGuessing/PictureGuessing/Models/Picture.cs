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
        private string answer;
        public string Answer
        {
            get => answer;
            set
            {
                if (value!="")
                    AnswerLength = value.Length;
                answer = value;
            }
        }

        public int AnswerLength { get; set; }
        public string Category { get; set; }
    }

}
