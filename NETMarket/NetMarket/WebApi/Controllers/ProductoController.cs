using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Errors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class ProductoController : BaseApiController
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoController(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            //spec = debe incluir la logica de la condición de la consulta y también las relaciones entre
            //las entidades, la relación entre Producto, Marca y Categoria.

            var spec = new ProductoWithCategoriaAndMarcaSpecification();

            var productos = await _productoRepository.GetAllWithSpec(spec);
            return Ok(_mapper.Map<IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>>(productos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            //spec = debe incluir la logica de la condición de la consulta y también las relaciones entre
            //las entidades, la relación entre Producto, Marca y Categoria.

            var spec = new ProductoWithCategoriaAndMarcaSpecification(id);

            var producto = await _productoRepository.GetByIdWithSpec(spec);

            if(producto == null)
            {
                return NotFound(new CodeErrorResponse(404, "El producto no existe"));
            }

            return _mapper.Map<Producto, ProductoDto>(producto);
        }
    }
}
