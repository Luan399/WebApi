using System.ComponentModel.DataAnnotations;

namespace ProdutoApi;
public class Categoria
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome da categoria é obrigatório")]
    [MaxLength(80, ErrorMessage = "Nome deve ter entre 3 e 80 caracteres")]
    [MinLength(3, ErrorMessage = "Nome deve ter entre 3 e 80 caracteres")]
    public string Nome { get; set; } = "";
    [MaxLength(200, ErrorMessage = "Descrição deve ter no máximo 200 caracteres")]
    public string Descricao { get; set; } = "";
}