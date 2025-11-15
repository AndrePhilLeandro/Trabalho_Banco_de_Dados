using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class AvaliacaoDb : DbContext
    {
        public AvaliacaoDb(DbContextOptions<AvaliacaoDb> options) : base(options) { }
        public DbSet<Avaliacao> Avaliacoes{ get; set; }
    }
}