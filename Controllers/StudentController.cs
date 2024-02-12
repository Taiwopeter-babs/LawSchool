using AutoMapper;
using LawSchool.Data;
using LawSchool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LawSchool.ModelsDto;


namespace LawSchool.Controllers;


[ApiController]
[Route("api/v1/students")]
public class StudentController : ControllerBase
{
    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    public StudentController(SchoolContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);


    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
    {

        var students = await _context.Students
                .AsNoTracking()
                .ToListAsync();

        var studentsDTO = _mapper.Map<IEnumerable<StudentDTO>>(students);

        return StatusCode(200, studentsDTO);

    }

    [HttpPost]
    public async Task<ActionResult> AddStudent([FromBody] StudentDTO studentDTO)
    {

        var studentExists = await _context.Students.FirstOrDefaultAsync(st => st.Email == studentDTO.Email);
        if (studentExists != null)
        {
            return BadRequest();
        }

        string[] names = studentDTO.FullName.Split(' ');

        Student student = new()
        {
            FirstName = names[0],
            LastName = string.IsNullOrEmpty(names[1]) ? "" : names[1],
            Email = studentDTO.Email,
            Department = studentDTO.Department,
            GPA = studentDTO.GPA
        };


        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddStudent), _mapper.Map<StudentDTO>(student));
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentDTO>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        var studentDTO = _mapper.Map<StudentDTO>(student);

        return StatusCode(200, studentDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateStudent(int id, [FromBody] StudentDTO studentDTO)
    {

        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        string[] names = studentDTO.FullName.Split(' ');

        student.FirstName = names[0];
        student.LastName = string.IsNullOrEmpty(names[1]) ? "" : names[1];

        await _context.SaveChangesAsync();

        return NoContent();


    }
}
