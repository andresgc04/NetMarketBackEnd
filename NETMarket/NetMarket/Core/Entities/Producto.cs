﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Producto : ClaseBase
    {
        public Producto()
        {
        }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public int MarcaId { get; set; }

        public int CategoriaId { get; set; }

        public decimal Precio { get; set; }

        public string Imagen { get; set; }

        public Marca marca { get; set; }

        public Categoria categoria { get; set; }
    }
}
