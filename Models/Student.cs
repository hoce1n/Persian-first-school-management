namespace School.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public List<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
    }
}