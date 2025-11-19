using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BancodeDados_Backend.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Data_nascimento { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public bool EhAluno { get; set; }
        [JsonIgnore]
        public bool Ativo { get; set; } = true;

    }
    public class UsuarioLogin
    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public bool EhAluno { get; set; }
        [JsonIgnore]
        public bool Ativo { get; set; } = true;
    }
    public class UsuarioPut
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }
    }
}