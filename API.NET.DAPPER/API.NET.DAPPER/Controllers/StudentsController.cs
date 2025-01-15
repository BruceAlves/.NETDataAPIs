using API.NET.DAPPER.Models;
using API.NET.DAPPER.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.NET.DAPPER.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsRepository _repository;

        public StudentsController(IConfiguration configuration)
        {
            _repository = new StudentsRepository(configuration);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Student student)
        {
            if (student == null) return BadRequest("Estudante inválido.");

            try
            {
                _repository.Create(student);
                return Ok(new { success = true, message = "Estudante criado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao criar estudante.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Student student)
        {
            if (student == null || student.Id != id) return BadRequest("Dados inválidos.");

            try
            {
                _repository.Edit(student);
                return Ok(new { success = true, message = "Estudante atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao atualizar estudante.", error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ListAll()
        {
            try
            {
                var students = _repository.ListAll();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao listar estudantes.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            try
            {
                var student = _repository.FindById(id);
                if (student == null) return NotFound(new { message = "Estudante não encontrado." });

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar estudante.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok(new { success = true, message = "Estudante deletado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao deletar estudante.", error = ex.Message });
            }
        }
    }
}
