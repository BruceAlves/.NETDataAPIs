using API.Entity_Framework.Dtos;
using API.Entity_Framework.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Entity_Framework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsRepository _repository;
        private readonly RabbitMqService _rabbitMqService;

        public StudentsController(StudentsRepository repository,RabbitMqService rabbitMqService)
        {
            _repository = repository;
            _rabbitMqService = rabbitMqService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _repository.ListAll();
            return Ok(students);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var studentDto = _repository.FindById(id);
            if (studentDto == null)
            {
                return NotFound("Student not found.");
            }
            return Ok(studentDto);
        }

        [HttpPost]
        public IActionResult Create(StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Invalid student data.");
            }

            _repository.Create(studentDto);
            _rabbitMqService.PublishAluno(studentDto);
            return CreatedAtAction(nameof(GetById), new { id = studentDto.Id }, studentDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Invalid student data.");
            }

            var existingStudent = _repository.FindById(id);
            if (existingStudent == null)
            {
                return NotFound("Student not found.");
            }

            _repository.Edit(id, studentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingStudent = _repository.FindById(id);
            if (existingStudent == null)
            {
                return NotFound("Student not found.");
            }

            _repository.Delete(id);
            return NoContent();
        }
    }
}
