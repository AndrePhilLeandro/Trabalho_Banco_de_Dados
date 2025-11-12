using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Avaliacao
    {
        [Key]
        public int Id_avaliacao { get; set; }
        public string? Descricao { get; set; }
        public float Nota { get; set; }
        public int Id_turmaFK { get; set; }
    }
}