using Exercise3.Models;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Exercise3.Services
{
    public interface IFileDbService
    {
        public IEnumerable<Student> Students { get; set; }
        Task SaveChanges();
    }

    public class FileDbService : IFileDbService
    {
        private readonly string _pathToFileDatabase;
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public FileDbService(IConfiguration configuration)
        {
            _pathToFileDatabase = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException(nameof(configuration));
            Initialize();
        }

        private void Initialize()
        {
            if (!File.Exists(_pathToFileDatabase))
            {
                return;
            }
            var lines = File.ReadLines(_pathToFileDatabase);

            var students = new List<Student>();

            lines.ToList().ForEach(row =>
            {
                var splitted = row.Split(",");
                
                students.Add(new Student
                {
                    FirstName = splitted[0],
                    LastName = splitted[1],
                    IndexNumber = splitted[2],
                    BirthDate = splitted[3],
                    StudyName = splitted[4],
                    StudyMode = splitted[5],
                    Email = splitted[6],
                    FathersName = splitted[7],
                    MothersName = splitted[8]
                });
            });
            
            Students = students;
        }

        public async Task AddStudent(Student student)
        {
            var students = Students.ToList();
            students.Add(student);
            Students = students;
        }

        public async Task SaveChanges()
        {
            await File.WriteAllLinesAsync(
                _pathToFileDatabase,
                Students.Select(e => $"{e.FirstName},{e.LastName},{e.IndexNumber},{e.BirthDate},{e.StudyName},{e.StudyMode},{e.Email},{e.FathersName},{e.MothersName}")
                );
        }

    }
}
