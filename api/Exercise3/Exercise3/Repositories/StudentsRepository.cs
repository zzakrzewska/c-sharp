using Exercise3.Models;
using Exercise3.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Exercise3.Repositories
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> GetStudents();
        Task DeleteStudent (Student student);
        Task AddStudent(Student student);
        Task UpdateStudent(Student student, Student newData);
    }

    public class StudentsRepository : IStudentsRepository
    {

        private readonly IFileDbService _fileDbService;

        public StudentsRepository(IFileDbService fileDbService)
        {
            _fileDbService = fileDbService;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _fileDbService.Students;
        }

        public async Task DeleteStudent(Student student)
        {
            var students = _fileDbService.Students.ToList();
            students.Remove(student);
            _fileDbService.Students = students;
            _fileDbService.SaveChanges();
        }

        public async Task AddStudent(Student student)
        {
            var students = _fileDbService.Students.ToList();
            students.Add(student);
            _fileDbService.Students = students;
            _fileDbService.SaveChanges();
        }

        public async Task UpdateStudent(Student student, Student newData)
        {
            student.FirstName = newData.FirstName;
            student.LastName = newData.LastName;
            student.BirthDate = newData.BirthDate;
            student.StudyName = newData.StudyName;
            student.StudyMode = newData.StudyMode;
            student.MothersName = newData.MothersName;
            student.FathersName = newData.FathersName;
            student.Email = newData.Email;

            _fileDbService.SaveChanges();
        }
    }
}
