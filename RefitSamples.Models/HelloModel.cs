using System;
using System.Collections.Generic;
using System.Text;

namespace RefitSamples.Models
{
    public class HelloModel
    {
        public HelloModel()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
        }

        public HelloModel(string text)
        {
            Text = text;
            CreateAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public string Text { get; set; }
    }
}
