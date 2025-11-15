using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class UsuarioDb : DbContext
    {
        public UsuarioDb(DbContextOptions<UsuarioDb> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}