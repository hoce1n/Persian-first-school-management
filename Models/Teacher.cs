namespace School.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Class> Classes { get; set; } = new List<Class>();
    }
}