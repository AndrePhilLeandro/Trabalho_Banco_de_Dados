using BancodeDados_Backend.Database;
using BancodeDados_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BancodeDados_Backend.Controller
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly DisciplinaDb disciplinaDb;
        private readonly CursoDb cursoDb;
        public DisciplinaController(DisciplinaDb disciplinaDb, CursoDb cursoDb)
        {
            this.disciplinaDb = disciplinaDb;
            this.cursoDb = cursoDb;
        }
        [HttpPost("Cadastro_Disciplina")]
        public IActionResult Cadastro_Disciplina(Disciplina disciplina)
        {
            var achaDisci = disciplinaDb.Disciplinas.FirstOrDefault(di => di.Nome == disciplina.Nome.ToUpper());
            if (achaDisci != null)
            {
                return Conflict("Disciplina ja Cadastrado!");
            }
            if (string.IsNullOrWhiteSpace(disciplina.Nome))
            {
                return UnprocessableEntity("Nome da Disciplina nao pode estar vazio!");
            }
            if (disciplina.Carga_horaria <= 0)
            {
                return UnprocessableEntity("Carga Horaria do curso deve ser maior que Zero Horas");
            }
            disciplina.Nome = disciplina.Nome.ToUpper();
            disciplinaDb.Disciplinas.Add(disciplina);
            disciplinaDb.SaveChanges();
            return Created("Disciplina", $"{disciplina.Nome} cadastrado com Sucesso!");
        }
        [HttpDelete("Id")]
        private IActionResult Deleta_Disciplina(int id)
        {
            if (id <= 0)
            {
                return UnprocessableEntity("Id deve ser maior que Zero!");
            }
            var achaDisci = disciplinaDb.Disciplinas.FirstOrDefault(disc => disc.Id_disciplina == id);
            if (achaDisci.Id_disciplina != null || achaDisci.Id_disciplina == id)
            {
                achaDisci.Ativo = false;
                disciplinaDb.SaveChanges();
                return NoContent();
            }
            return NotFound("Discplina nao encontrada!");
        }
        [HttpPut("Id")]
        private IActionResult Edita_Disciplina(int id, [FromBody] DisciplinaPut disciplina)
        {
            disciplina.Nome = disciplina.Nome.ToUpper();
            if (id <= 0 || disciplina.Id_cursoFK <= 0)
            {
                return UnprocessableEntity("Id deve ser maior que Zero!");
            }
            var achaDisci = disciplinaDb.Disciplinas.FirstOrDefault(disc => disc.Id_disciplina == id);
            if (achaDisci.Id_disciplina != null || achaDisci.Id_disciplina == id && achaDisci.Ativo == true)
            {
                achaDisci.Nome = disciplina.Nome;
                achaDisci.Carga_horaria = disciplina.Carga_horaria;
                achaDisci.Id_cursoFK = disciplina.Id_cursoFK;
                disciplinaDb.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }
        [HttpGet("MostraDiscplina")]
        public IActionResult MostraDiscplina()
        {
            var retornadisciplina = disciplinaDb.Disciplinas.ToList();
            return Ok(retornadisciplina);
        }
    }
}