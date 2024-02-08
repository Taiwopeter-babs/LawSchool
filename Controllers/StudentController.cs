using LawSchool.Data;
using LawSchool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LawSchool.Controllers;



[ApiController]
[Route("api/v1/students")]
public class StudentController : ControllerBase
{
    private readonly SchoolContext _context;

    public StudentController(SchoolContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
    {
        try
        {
            var students = await _context.Students
                    .AsNoTracking()
                    .Select(st => ToStudentDTO(st)).ToListAsync();
            return StatusCode(200, students);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddStudent([FromBody] StudentDTO studentDTO)
    {
        try
        {
            if (_context.Students != null)
            {
                var studentExists = await _context.Students.FirstOrDefaultAsync(st => st.Email == studentDTO.Email);
                if (studentExists != null)
                {
                    return BadRequest();
                }
            }

            Student student = new()
            {
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                Email = studentDTO.Email,
                Department = studentDTO.Department,
                GPA = studentDTO.GPA
            };


            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddStudent), ToStudentDTO(student));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.GetType().ToString());
            Console.WriteLine(ex);
            return StatusCode(500);
        }

    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentDTO>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return StatusCode(200, ToStudentDTO(student));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateStudent(int id, [FromBody] StudentDTO studentDTO)
    {
        try
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            student.FirstName = studentDTO.FirstName;
            student.LastName = studentDTO.LastName;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex.GetType().ToString() == "DbUpdateConcurrencyException")
            {
                return StatusCode(500);
            }
            return StatusCode(500);
        }
    }

    private static StudentDTO ToStudentDTO(Student student)
    {
        return new StudentDTO
        {
            Id = student.Id,
            FirstName = student.FirstName!,
            LastName = student.LastName!,
            Department = student.Department!,
            Email = student.Email!,
            GPA = student.GPA
        };
    }

}