using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class NotaDb : DbContext
    {
        public NotaDb(DbContextOptions<NotaDb> options) : base(options) { }
        public DbSet<Nota> Notas { get; set; }
    }
}