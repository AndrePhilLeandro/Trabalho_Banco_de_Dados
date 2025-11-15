using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Disciplina
    {
        [Key]
        public int Id_disciplina { get; set; }
        public string? Nome { get; set; }
        public int Carga_horaria { get; set; }
        public int Id_cursoFK { get; set; }
    }
    public class DisciplinaPut
    {
        public string? Nome { get; set; }
        public int Carga_horaria { get; set; }
        public int Id_cursoFK { get; set; }
    }
}