using AutoMapper;
using catalogo_api.DTOs;
using catalogo_api.Models;
using catalogo_api.Pagination;
using catalogo_api.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace catalogo_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProdutosController(IUnitOfWork context, IMapper mapper)
        {
            _unitOfWork = context; //injeção do serviço do unity of work que utiliza context
            _mapper = mapper;
        }


        [HttpGet("menorPreco")]
        public async Task<IEnumerable<ProdutoDTO>> GetProdutoPorPreco()
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosPorPreco();
            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDTO;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutos(produtosParameters);

            // possível pois GetProdutos retorna um pagedlist

            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata)); // serializa os dados do tipo anônimo para string json

            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDTO;
        }


        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            return produtoDTO;

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _unitOfWork.ProdutoRepository.Add(produto);
            await _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }

            var produto = _mapper.Map<Produto>(produtoDto);
            _unitOfWork.ProdutoRepository.Update(produto);
            await _unitOfWork.Commit();

            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {

            var produto = await _unitOfWork.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            _unitOfWork.ProdutoRepository.Delete(produto);
            await _unitOfWork.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return produtoDto;
        }

    }
}
