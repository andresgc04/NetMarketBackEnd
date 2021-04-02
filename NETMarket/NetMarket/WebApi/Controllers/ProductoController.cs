using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly IGenericRepository<Producto> _productoRepository;

        public ProductoController(IGenericRepository<Producto> productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            //spec = debe incluir la logica de la condición de la consulta y también las relaciones entre
            //las entidades, la relación entre Producto, Marca y Categoria.

            var spec = new ProductoWithCategoriaAndMarcaSpecification();

            var productos = await _productoRepository.GetAllWithSpec(spec);
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            //spec = debe incluir la logica de la condición de la consulta y también las relaciones entre
            //las entidades, la relación entre Producto, Marca y Categoria.

            var spec = new ProductoWithCategoriaAndMarcaSpecification(id);
            return await _productoRepository.GetByIdWithSpec(spec);
        }
    }
}
