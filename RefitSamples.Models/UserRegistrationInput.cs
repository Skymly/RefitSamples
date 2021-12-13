using RefitSamples.Models;

using System.ComponentModel.DataAnnotations;

namespace RefitSamples.Models
{
    public class UserRegistrationInput
    {
        public UserRegistrationInput()
        {
        }

        public UserRegistrationInput(string email, string password, string username)
        {
            Email = email;
            Password = password;
            Username = username ?? Email;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
