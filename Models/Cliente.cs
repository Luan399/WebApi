using System.ComponentModel.DataAnnotations;
namespace ProdutoApi;

public class Cliente
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome não pode ter mais de 100 caracteres")]
    [MinLength(3, ErrorMessage = "Nome deve ter pelo menos 3 caracteres")]
    public string Nome { get; set; } = "";

    [Required(ErrorMessage = "Email em formato inválido")]
    [EmailAddress(ErrorMessage = "Email em formato inválido")]
    public string Email { get; set; } = "";

    [Range(18, int.MaxValue, ErrorMessage = "Idade mínima é 18 anos")]
    public int Idade { get; set; } = 0;
}