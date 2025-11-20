using BancodeDados_Backend.Database;
using BancodeDados_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BancodeDados_Backend.Controller
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly TurmaDb turmaDb;
        private readonly CursoDb cursoDb;
        private readonly UsuarioDb usuarioDb;
        private readonly DisciplinaDb disciplinaDb;
        public TurmaController(TurmaDb turmaDb, CursoDb cursoDb, UsuarioDb usuarioDb, DisciplinaDb disciplinaDb)
        {
            this.turmaDb = turmaDb;
            this.cursoDb = cursoDb;
            this.usuarioDb = usuarioDb;
            this.disciplinaDb = disciplinaDb;
        }
        [HttpPost("Turma")]
        public IActionResult Turma([FromBody] Turma turma)
        {
            if (turma.Id_professorFK <= 0)
            {
                return BadRequest("Professor nao localizado!");
            }
            if (turma.Id_cursoFK <= 0)
            {
                return BadRequest("Curso nao localizado!");
            }
            if (turma.Id_disciplinaFK <= 0)
            {
                return BadRequest("Discplina nao localizado!");
            }
            if (turma.Semestre <= 0)
            {
                return BadRequest("Semestre precisa ser maior que Zero!");
            }
            if (turma.Ano <= 0)
            {
                return BadRequest("O ano precisa ser maior que Zero!");
            }
            try
            {
                /* var VerificaUsuario = usuarioDb.Usuarios.FirstOrDefault(user => user.Id == turma.Id_professorFK);
                var VerificaCurso = cursoDb.Cursos.FirstOrDefault(Curso => Curso.Id_curso == turma.Id_cursoFK);
                var Verificadisciplina = disciplinaDb.Disciplinas.FirstOrDefault(disc => disc.Id_disciplina == turma.Id_cursoFK);

                if (VerificaUsuario != null && VerificaCurso != null && Verificadisciplina != null)
                {
                    turmaDb.Turmas.Add(turma);
                    turmaDb.SaveChanges();
                    return Created();
                } */
                turmaDb.Turmas.Add(turma);
               // turmaDb.SaveChanges();
                return Ok(turma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }
    }
}