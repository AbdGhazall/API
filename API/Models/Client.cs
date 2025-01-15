using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Index("Email", IsUnique = true)]
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; //CONFIGURE TO BE UNIQE
        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }





    }
}
