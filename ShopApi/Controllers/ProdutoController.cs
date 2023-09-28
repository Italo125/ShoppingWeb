using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Extensions;
using ShopApi.Repositories.Contrato;

using ShopModels.Dtos;

namespace ShopApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{

    private readonly IProdutoRepository _produtoRepository;
    public ProdutoController(IProdutoRepository produtoRepository)
    { 
        _produtoRepository = produtoRepository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetItems()
    {
        try
        {
            var produto = await _produtoRepository.GetItens();
            if (produto == null)
            {
                return NotFound();
            }
            else
            {
                var produtoDto = produto.ConverteParaDto();
                return Ok(produtoDto);
            }
        }
        catch (Exception ex)
        {
            //Implementar log Auditoria
            return StatusCode(StatusCodes.Status500InternalServerError,
             "Erro ao acessar a base de dados");
        }
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdutoDto>> GetItem(int id)
    {
        try
        {
            var produto = await _produtoRepository.GetItem(id);
            if (produto is null)
            {
                return NotFound("Produto não localizado");
            }
            else
            {
                var produtoDto = produto.ConverterProdutoParaDto();
                return Ok(produtoDto);
            }
        }
        catch (Exception)
        {
            //Implementar log Auditoria
            return StatusCode(StatusCodes.Status500InternalServerError,
                              "Erro ao acessar o banco de dados");
        }
    }


    [HttpGet]
    [Route("{categoriaId}/GetItensPorCategoria")]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>>
    GetItensPorCategoria(int categoriaId)
    {
        try
        {
            var produtos = await _produtoRepository.GetItensPorCategoria(categoriaId);
            var produtosDto = produtos.ConverterProdutosParaDto();
            return Ok(produtosDto);
        }
        catch (Exception)
        {
            //Implementar log Auditoria
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Erro ao acessar o banco de dados");
        }
    }


    [HttpGet]
    [Route("GetCategorias")]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategorias()
    {
        try
        {
            var categoria = await _produtoRepository.GetCategorias();
            var CategoriaasDto = categoria.ConverterCategoriasParaDto();
            return Ok(CategoriaasDto);
        }
        catch (Exception ex) 
        {
            //Implementar log Auditoria
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Erro ao acessar o banco de dados");
        }
    }


}
