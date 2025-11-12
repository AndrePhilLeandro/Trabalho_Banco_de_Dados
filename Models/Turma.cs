using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Turma
    {
        [Key]
        public int Id_turma { get; set; }
        public int Semestre { get; set; }
        public int Ano { get; set; }
        public int Id_disciplinaFK { get; set; }
        public int Id_professorFK { get; set; }
    }
}