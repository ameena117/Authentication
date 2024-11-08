using System.ComponentModel.DataAnnotations;

namespace AuthTask.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string? UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
