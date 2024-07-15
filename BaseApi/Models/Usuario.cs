using System.ComponentModel.DataAnnotations;

namespace PGP.Models;

public class Usuario
{
    public int Id { get; set; }
        
    public string Cpf { get; set; }
        
    public string Senha { get; set; }
}