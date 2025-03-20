using System.Text.RegularExpressions;

namespace UniversityMVC.Models
{
    public class Group
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Student>? Students { get; set; }
        public ICollection<SubjectGroup>? SubjectsGroups { get; set; }
        public ICollection<Subject>? Subjects { get; set; }
    }
    public class Student
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
    }
    public class Teacher
    { 
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public ICollection<SubjectGroup>? SubjectsGroups { get; set; }

    }
    public class Subject
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Group>? Groups { get; set; }
        public ICollection<SubjectGroup>? SubjectsGroups { get; set; }
    }
    public class SubjectGroup
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
