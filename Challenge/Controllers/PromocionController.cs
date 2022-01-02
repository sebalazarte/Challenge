using Challenge.Models;
using Challenge.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionController : ControllerBase
    {
        private readonly IPromocionCollection db = new PromocionCollection();
        private readonly IBancoCollection dbBancos = new BancoCollection();
        private readonly ICategoriaCollection dbCategoria = new CategoriaCollection();
        private readonly IMedioPagoCollection dbMedioPago = new MedioPagoCollection();

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var promociones = await db.ObtenerTodos();
            return Ok(promociones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(string id)
        {
            var promocion = await db.ObtenerPorId(id);
            return Ok(promocion);
        }

        private async Task<string> ValidarParametros(Promocion item)
        {
            if (!item.MaximaCantidadCuotas.HasValue && !item.PorcentajeDeDescuentos.HasValue)
            {
                return "Cantidad de cuotas o percentaje de descuento: alguno debe tener valor";
            }

            if (!item.MaximaCantidadCuotas.HasValue && item.PorcentajeDeInteres.HasValue)
            {
                return "Porcentaje de interes solo puede tener valor si Cantidad de cuotas tiene valor";
            }

            if (item.FechaInicio > item.FechaFin)
            {
                return "Fecha fin no puede ser menor a fecha inicio";
            }

            if (item.MaximaCantidadCuotas.HasValue && item.PorcentajeDeDescuentos.HasValue)
            {
                return "Cantidad de cuotas o percentaje de descuento: Solo uno debe tener valor";
            }

            if (item.Bancos.Any())
            {
                var bancos = await dbBancos.ObtenerTodos();
                var bancosPermitidos = bancos.Select(i => i.Nombre).ToList();
                foreach (var b in item.Bancos)
                {
                    if (!bancosPermitidos.Contains(b))
                    {
                        return $"El banco {b} no esta entre los bancos permitidos";
                    }
                }
            }

            if (item.CategiriasProductos.Any())
            {
                var categorias = await dbCategoria.ObtenerTodos();
                List<string> categoriasPermitidas = categorias.Select(i => i.Nombre).ToList();
                foreach (var c in item.CategiriasProductos)
                {
                    if (!categoriasPermitidas.Contains(c))
                    {
                        return $"La categoria de producto {c} no esta entre las categorias permitidas permitidos";
                    }
                }
            }

            if (item.MediosPagos.Any())
            {
                var medios = await dbMedioPago.ObtenerTodos();
                var mediosPermitidos = medios.Select(i => i.Nombre).ToList();
                foreach (var m in item.MediosPagos)
                {
                    if (!mediosPermitidos.Contains(m))
                    {
                        return $"El medio de pago {m} no esta entre los medios de pagos permitidos";
                    }
                }
            }

            return string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Promocion item)
        {
            if (item == null) return BadRequest();

            var error = await ValidarParametros(item);
            if (!string.IsNullOrEmpty(error)) return BadRequest(error);

            await db.Insertar(item);
            return Ok(item.Id.ToString());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Modificar([FromBody] Promocion item, string id)
        {
            if (item == null) return BadRequest();

            var error = await ValidarParametros(item);
            if (!string.IsNullOrEmpty(error)) return BadRequest(error);

            await db.Modificar(item, id);
            return Ok("Modificado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            await db.Eliminar(id);
            return Ok("Eliminado");
        }

    }
}
