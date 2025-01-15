using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ClientDto //DATA WE WILL SEND TO API
    {
        [Required(ErrorMessage = "First Name is required")] 
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last Name is required")] 
        public string LastName { get; set; } = string.Empty ;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Phone]
        public string? Phone { get; set; } //OPTIONAL
        public string? Address { get; set; } //OPTIONAL
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
