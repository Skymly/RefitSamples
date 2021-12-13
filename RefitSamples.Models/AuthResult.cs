using RefitSamples.Models;

using System.Collections.Generic;

namespace RefitSamples.Models
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
