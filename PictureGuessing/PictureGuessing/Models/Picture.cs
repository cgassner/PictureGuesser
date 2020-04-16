using System;
using System.ComponentModel.DataAnnotations;

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
