using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Data
{
    public class MarketDbContextData
    {
        public static async Task CargarDataAsync(MarketDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                //Cargar data de Marcas y insertarlas a la base de datos:
                if (!context.Marca.Any())
                {
                    //Leer los datos desde el archivo Json:
                    var marcaData = File.ReadAllText("../BusinessLogic/CargarData/marca.json");

                    //Deserializar los datos en un tipo de archivo de listado de marcas:
                    var marcas = JsonSerializer.Deserialize<List<Marca>>(marcaData);

                    //Bucle para leer cada uno de los elementos y insertarlos en el interior de la entidad tabla marca:
                    foreach (var marca in marcas)
                    {
                        context.Marca.Add(marca);
                    }

                    //Confirmación de la transacción:
                    await context.SaveChangesAsync();
                }

                //Cargar data de Categorias y insertarlas a la base de datos:
                if (!context.Categoria.Any())
                {
                    //Leer los datos desde el archivo Json:
                    var categoriaData = File.ReadAllText("../BusinessLogic/CargarData/categoria.json");

                    //Deserializar los datos en un tipo de archivo de listado de categorias:
                    var categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriaData);

                    //Bucle para leer cada uno de los elementos y insertarlos en el interior de la entidad tabla categoria:
                    foreach (var categoria in categorias)
                    {
                        context.Categoria.Add(categoria);
                    }

                    //Confirmación de la transacción:
                    await context.SaveChangesAsync();
                }

                //Cargar data de Productos y insertarlas a la base de datos:
                if (!context.Producto.Any())
                {
                    //Leer los datos desde el archivo Json:
                    var productoData = File.ReadAllText("../BusinessLogic/CargarData/producto.json");

                    //Deserializar los datos en un tipo de archivo de listado de productos:
                    var productos = JsonSerializer.Deserialize<List<Producto>>(productoData);

                    //Bucle para leer cada uno de los elementos y insertarlos en el interior de la entidad tabla productos:
                    foreach (var producto in productos)
                    {
                        context.Producto.Add(producto);
                    }

                    //Confirmación de la transacción:
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                //Imprimir el mensaje de error por si ocurre algun problema:
                var logger = loggerFactory.CreateLogger<MarketDbContextData>();
                logger.LogError(ex.Message);
            }
        }
    }
}
