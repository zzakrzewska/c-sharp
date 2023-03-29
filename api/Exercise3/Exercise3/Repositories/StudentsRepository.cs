using Exercise3.Models;
using Exercise3.Services;

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
            throw new NotImplementedException();
        }

        public async Task DeleteStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStudent(Student student, Student newData)
        {
            student.FirstName = newData.FirstName;
            student.LastName = newData.LastName;
            //uzup
        }
    }
}
