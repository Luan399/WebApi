using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutoApi;
using ProdutosApi;

namespace ProdutoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Categoria>>> Get()
    {
        var categorias = await _context.Categorias.ToListAsync();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetById(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound(new { mensagem = "Categoria não encontrada" });
        }
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Post(Categoria categoria)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Categoria categoriaAtualizada)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound(new { mensagem = "Categoria não encontrada" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        categoria.Nome = categoriaAtualizada.Nome;
        categoria.Descricao = categoriaAtualizada.Descricao;

        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound(new { mensagem = "Categoria não encontrada" });
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return Ok(new { mensagem = "Categoria deletada com sucesso" });
    }
}
