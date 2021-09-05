using System.ComponentModel.DataAnnotations;

namespace zbw.Auftragsverwaltung.Domain.Users
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
