﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace BusinessLogic.Logic
{
    public class ProductoRepository : IProductoRepository
    {
        public ProductoRepository()
        {
        }

        public Task<Producto> GetProductoByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Producto>> GetProductosAsync()
        {
            throw new NotImplementedException();
        }
    }
}