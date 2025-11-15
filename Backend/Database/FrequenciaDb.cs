using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class FrequenciaDb : DbContext
    {
        public FrequenciaDb(DbContextOptions<FrequenciaDb> options) : base(options) { }
        public DbSet<Frequencia > Frequencias { get; set; }
    }
}