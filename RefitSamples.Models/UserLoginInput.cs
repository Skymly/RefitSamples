using RefitSamples.Models;

using System.ComponentModel.DataAnnotations;

 namespace RefitSamples.Models
{
    public class UserLoginInput
    {
        public UserLoginInput()
        {
        }

        public UserLoginInput(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
