using System.ComponentModel.DataAnnotations;

namespace Authentication.Services.Users.Request
{
    public class RegistrationDTO
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required]
        //public byte?[] LogoBits { get; set; }
    }
}
