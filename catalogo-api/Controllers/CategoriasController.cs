using AutoMapper;
using catalogo_api.DTOs;
using catalogo_api.Models;
using catalogo_api.Pagination;
using catalogo_api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace catalogo_api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork context, IMapper mapper)
        {
            _unitOfWork = context;
            _mapper = mapper;
        }


        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> getCategoriasProdutos()
        {
            var categorias = await _unitOfWork.CategoriaRepository.GetCategoriasProdutos();
            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDto;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            var categorias = await _unitOfWork.CategoriaRepository.GetCategorias(categoriasParameters);

            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious

            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDto;
        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]

        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {

            var categoria = await _unitOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDto;
        }


        [HttpPost]
        public async Task<ActionResult> Post(CategoriaDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            _unitOfWork.CategoriaRepository.Add(categoria);
            await _unitOfWork.Commit();
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoriaDto);

        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CategoriaDTO categoriaDto)
        {

            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _unitOfWork.CategoriaRepository.Update(categoria);
            await _unitOfWork.Commit();
            return Ok();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {

            var categoria = await _unitOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoriaRepository.Delete(categoria);
            await _unitOfWork.Commit();
            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDto;

        }
    }
}
