using catalogo_api.Context;
using catalogo_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catalogo_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        public readonly AppDbContext _context;
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }



        [HttpGet("produtos")]
        public IEnumerable<Categoria> getCategoriasProdutos()
        {
            //return _context.Categorias.Include(categoria => categoria.Produtos).AsNoTracking().ToList();
            return _context.Categorias.Include(categoria => categoria.Produtos).Where(categoria => categoria.CategoriaId <= 5).AsNoTracking().ToList();
        }


        [HttpGet]

        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _context.Categorias.Take(10).AsNoTracking().ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação!");
            }
        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]

        public IActionResult Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada!");
            }
            return Ok(categoria);
        }


        [HttpPost]
        public IActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados inválidos!");
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);

        }


        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada!");
            }

            return Ok(categoria);

        }

    }
}
