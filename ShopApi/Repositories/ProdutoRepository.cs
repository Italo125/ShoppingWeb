using Microsoft.EntityFrameworkCore;
using ShopApi.Context;
using ShopApi.Entities;
using ShopApi.Repositories.Contrato;

namespace ShopApi.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private  readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext dbContext)
    {
        this._context = dbContext;
    }

    public async Task<Produto> GetItem(int id)
    {
        var produto = await _context.Produtos.Include(c => c.Categoria).SingleOrDefaultAsync(c => c.Id == id); ;

        return produto;
    }

    public async Task<IEnumerable<Produto>> GetItens()
    {
        var produtos = await _context.Produtos
                              .Include(p => p.Categoria)
                              .ToListAsync();
        return produtos;
    }

   

    public async Task<IEnumerable<Categoria>> GetCategorias()
    {
        var categoria = await this._context.Categorias.ToListAsync();

        return categoria;
    }

    public async Task<IEnumerable<Produto>> GetItensPorCategoria(int id)
    {
        var produtos = await _context.Produtos
                           .Include(p => p.Categoria)
                           .Where(p => p.CategoriaId == id).ToListAsync();

        return produtos;
    }
}
