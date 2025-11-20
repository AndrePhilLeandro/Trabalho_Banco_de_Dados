using BancodeDados_Backend.Database;
using Microsoft.AspNetCore.Mvc;
using BancodeDados_Backend.Models;

namespace BancodeDados_Backend.Controller
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class CursoController : ControllerBase
    {
        private readonly CursoDb cursoDb;
        public CursoController(CursoDb cursoDb)
        {
            this.cursoDb = cursoDb;
        }
        [HttpPost("Cadastro_Curso")]
        private IActionResult Cadastro_Curso([FromBody] Curso curso)
        {
            curso.Nome = curso.Nome.ToUpper();
            var Verifica_Nome = cursoDb.Cursos.FirstOrDefault(cursoNome => cursoNome.Nome == curso.Nome);
            if (Verifica_Nome.Nome == curso.Nome)
            {
                return Conflict("Curso ja Cadastrado!");
            }
            if (string.IsNullOrWhiteSpace(curso.Nome))
            {
                return UnprocessableEntity("Nome do Curso nao pode estar vazio!");
            }
            if (curso.Carga_horaria <= 0)
            {
                return UnprocessableEntity("Carga Horaria do curso deve ser menor que zero Horas");
            }

            cursoDb.Cursos.Add(curso);
            return Created("Curso Cadastrado!", "");
        }
        [HttpPut("{Id}")]
        private IActionResult Edita_Curso(int Id, CursoPut curso)
        {
            if (Id <= 0)
            {
                return UnprocessableEntity("Id deve ser maior que Zero!");
            }
            var Achacurso = cursoDb.Cursos.FirstOrDefault(cur => cur.Id_curso == Id);
            if (Achacurso != null)
            {
                Achacurso.Nome = curso.Nome.ToUpper();
                Achacurso.Carga_horaria = curso.Carga_horaria;
                Achacurso.Ativo = curso.Ativo;
                cursoDb.SaveChanges();
                return Content("Informaçoes Alteradas com sucesso!");
            }
            else
            {
                return NotFound("Curso não encontrado!");
            }
        }
        [HttpDelete("{Id}")]
        private IActionResult ApagaCurso(int Id)
        {
            if (Id <= 0)
            {
                return UnprocessableEntity("Id deve ser maior que Zero!");
            }
            var Achacurso = cursoDb.Cursos.FirstOrDefault(cur => cur.Id_curso == Id);
            if (Achacurso.Id_curso != Id)
            {
                return NotFound("Curso não encontrado!");
            }
            Achacurso.Ativo = false;
            cursoDb.SaveChanges();
            return NoContent();
        }
    }
}