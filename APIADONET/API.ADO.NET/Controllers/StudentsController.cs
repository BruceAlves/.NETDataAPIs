using Microsoft.AspNetCore.Mvc;
using API.ADO.NET.Repository;
using API.ADO.NET.Models;
using System;

namespace API.ADO.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsRepository _repository;

        public StudentsController()
        {
            // Instância manual do repositório
            _repository = new StudentsRepository();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            try
            {
                _repository.Create(student);
                return Ok(new { message = "Estudante criado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao criar estudante.", error = ex.Message });
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
                if (student == null)
                {
                    return NotFound(new { message = "Estudante não encontrado." });
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar estudante.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Student student)
        {
            try
            {
                student.Id = id;
                _repository.Edit(student);
                return Ok(new { message = "Estudante atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao atualizar estudante.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok(new { message = "Estudante excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao excluir estudante.", error = ex.Message });
            }
        }
    }
}
