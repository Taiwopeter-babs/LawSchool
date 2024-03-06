using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LawSchool.ModelsDto;
using LawSchool.Contracts;
using LawSchool.Utilities.Filters;
using LawSchool.Utilities;


namespace LawSchool.Controllers;


[ApiController]
[Route("api/v1/students")]
public class StudentController : ControllerBase
{
    private readonly IServiceManager _service;

    public StudentController(IServiceManager service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetStudents([FromQuery] StudentParameters studentParameters)
    {
        var (students, pageMetaData) = await _service.StudentService
                .GetAllStudentsAsync(studentParameters, trackChanges: false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageMetaData));

        return StatusCode(200, students);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> AddStudent([FromBody] StudentForCreationDto studentDTO)
    {
        var studentEntity = await _service.StudentService
                .CreateStudentAsync(studentDTO, trackChanges: false);

        return CreatedAtRoute("GetStudent", new { id = studentEntity.Id }, studentEntity);

    }


    [HttpGet("{id:int}", Name = "GetStudent")]
    public async Task<ActionResult<StudentDto>> GetStudent(int id)
    {
        var student = await _service.StudentService.GetStudentAsync(id, trackChanges: false);

        return StatusCode(200, student);
    }

    // [HttpPut("{id:int}")]
    // public async Task<ActionResult> UpdateStudent(int id, [FromBody] StudentDTO studentDTO)
    // {

    //     var student = await _context.Students.FindAsync(id) ??
    //             throw new StudentNotFoundException(id); ;

    //     string[] names = studentDTO.FullName.Split(' ');

    //     student.FirstName = names[0];
    //     student.LastName = string.IsNullOrEmpty(names[1]) ? "" : names[1];

    //     await _context.SaveChangesAsync();

    //     return NoContent();
    // }
}
