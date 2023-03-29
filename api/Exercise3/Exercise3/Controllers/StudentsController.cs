using Exercise3.Models;
using Exercise3.Models.DTOs;
using Exercise3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Exercise3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _studentsRepository;
        public StudentsController(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_studentsRepository.GetStudents());
            }
            catch (Exception e)
            {
                return Problem();
            }
        }

        [HttpGet("{index}")]
        public async Task<IActionResult> Get(string index)
        {
            try
            {
                var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == index).FirstOrDefault();
                if (student is null)
                {
                    return NotFound();
                }
                
                return Ok(student);
            }
            catch (Exception e)
            {
                return Problem();
            }

        }

        [HttpPut("{index}")]
        public async Task<IActionResult> Put(string index, StudentPUT newStudentData)
        {
            try
            {
                var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == index).FirstOrDefault();
                if (student is null)
                {
                    return NotFound();
                }

                _studentsRepository.UpdateStudent(student,
                    new Student
                    {
                        FirstName = newStudentData.FirstName,
                        LastName = newStudentData.LastName,
                        //uzup
                    }
                );

                return Ok();
            }
            catch (Exception e)
            {
                return Problem();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentPOST newStudent)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{index}")]
        public async Task<IActionResult> Delete(string index)
        {
            throw new NotImplementedException();
        }

    }
}
