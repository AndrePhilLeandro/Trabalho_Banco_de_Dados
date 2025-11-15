using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Nota
    {
        [Key]
        public int Id_nota { get; set; }
        public float Valor { get; set; }
        public int Id_avaliacaoFK { get; set; }
        public int Id_matriculaFK { get; set; }
        public int Id_disciplinaFK { get; set; }
        public int Id_cursoFK { get; set; }
    }
}