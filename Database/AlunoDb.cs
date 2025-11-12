using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class AlunoDb : DbContext
    {
        public AlunoDb(DbContextOptions<AlunoDb> options) : base(options) { }
        public DbSet<Aluno> Alunos{ get; set; }
    }
}