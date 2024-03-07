using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LawSchool.ModelsDto;
using LawSchool.Contracts;
using LawSchool.Utilities.Filters;
using LawSchool.Utilities;
using Microsoft.AspNetCore.JsonPatch;
using LawSchool.Errors;


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

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageMetaData));

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
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await _service.StudentService.GetStudentAsync(id, trackChanges: false);

        return StatusCode(200, student);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateStudent(int id,
        [FromBody] JsonPatchDocument<StudentForUpdateDto> patchDoc)
    {
        var errorObject = new ErrorDetails
        {
            StatusCode = 400,
            Message = "Patch document object sent from client is null"
        };

        if (patchDoc is null)
            return BadRequest(errorObject.ToString());


        var (studentToPatch, studentEntity) = await _service.StudentService
            .GetStudentForPatch(id, trackChanges: true);

        patchDoc.ApplyTo(studentToPatch);

        await _service.StudentService.SaveChangesForPatch(studentToPatch, studentEntity);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        await _service.StudentService.DeleteStudentAsync(id, trackChanges: false);

        return NoContent();
    }
}
