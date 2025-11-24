using System.Text;
using BancodeDados_Backend.Database;
using BancodeDados_Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PasswordHasher<string>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy", p =>
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
});



var chave = "Projeto_Banco_de_Dados_Puc_Minas_2025";
var chaveEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Nome do Projeto",
        ValidAudiences = new[] { "Aluno", "Professor" },
        IssuerSigningKey = chaveEncriptada
    };
});

AdicionarConexoes(builder);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UsuarioDb>();

    // Aplica as migrations automaticamente
    db.Database.Migrate();

    // Se não existir nenhum usuário, cria o primeiro
    if (!db.Usuarios.Any())
    {
        db.Usuarios.Add(new Usuario
        {
            Nome = "Admin",
            Email = "Administrador@master.com",
            Senha = "123456",
            Tipo = 0,
            Cpf = "00000000000",
            Data_nascimento = "01/01/2000",
            Telefone = "000000000",
            Ativo = true
        });

        db.SaveChanges();
    }
}



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "teste");
        options.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();
app.UseCors("corsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

void AdicionarConexoes(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<UsuarioDb>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

    builder.Services.AddDbContext<AvaliacaoDb>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

    builder.Services.AddDbContext<CursoDb>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

    builder.Services.AddDbContext<DisciplinaDb>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

    builder.Services.AddDbContext<NotaDb>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

    builder.Services.AddDbContext<TurmaDb>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
}