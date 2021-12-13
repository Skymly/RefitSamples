using System;

namespace RefitSamples.Models
{
    public class RetryModel
    {
        public RetryModel()
        {
            Id = Guid.NewGuid();
        }

        public RetryModel(string count) : this()
        {
            Count = count;
        }

        public Guid Id { get; set; }

        public string Count { get; set; }

    }
}
