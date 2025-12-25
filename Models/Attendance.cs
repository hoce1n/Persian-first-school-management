namespace School.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int ClassId { get; set; }
        public Class? Class { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Reason { get; set; }
    }
}