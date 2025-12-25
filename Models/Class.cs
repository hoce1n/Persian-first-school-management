namespace School.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public List<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
    }
}