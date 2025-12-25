using Microsoft.AspNetCore.Identity;

namespace School.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? PasswordHash { get; set; }
        public Role Role { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public Teacher? Teacher { get; set; }
        public Student? Student { get; set; }
    }
}