using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutoApi;
using ProdutosApi;

namespace ProdutoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Produto>>> Get()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetById(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado" });
        }
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> Post(Produto produto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Produto produtoAtualizado)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        produto.Nome = produtoAtualizado.Nome;
        produto.Preco = produtoAtualizado.Preco;
        produto.estoque = produtoAtualizado.estoque;

        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado" });
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return Ok(new { mensagem = "Produto deletado com sucesso" });
    }
}