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
            if (!item.MaximaCantidadDeCuotas.HasValue && !item.PorcentajeDeDescuento.HasValue)
            {
                return "Cantidad de cuotas o percentaje de descuento: alguno debe tener valor";
            }

            if (!item.MaximaCantidadDeCuotas.HasValue && item.PorcentajeDeInteres.HasValue)
            {
                return "Porcentaje de interes solo puede tener valor si Cantidad de cuotas tiene valor";
            }

            if (item.FechaInicio > item.FechaFin)
            {
                return "Fecha fin no puede ser menor a fecha inicio";
            }

            if (item.MaximaCantidadDeCuotas.HasValue && item.PorcentajeDeDescuento.HasValue)
            {
                return "Cantidad de cuotas o percentaje de descuento: Solo uno debe tener valor";
            }

            if (item.PorcentajeDeDescuento.HasValue)
            {
                if(item.PorcentajeDeDescuento < 5 || item.PorcentajeDeDescuento > 80)
                {
                    return "El porcentaje de descuento debe estar entre 5 y 80";
                }
            }

            //si los valores de bancos, categorias o medios de pago es enviado, tienen que ser dentro de los valores posibles

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

            if (item.CategoriasProductos.Any())
            {
                var categorias = await dbCategoria.ObtenerTodos();
                List<string> categoriasPermitidas = categorias.Select(i => i.Nombre).ToList();
                foreach (var c in item.CategoriasProductos)
                {
                    if (!categoriasPermitidas.Contains(c))
                    {
                        return $"La categoria de producto {c} no esta entre las categorias permitidas permitidos";
                    }
                }
            }

            if (item.MediosDePago.Any())
            {
                var medios = await dbMedioPago.ObtenerTodos();
                var mediosPermitidos = medios.Select(i => i.Nombre).ToList();
                foreach (var m in item.MediosDePago)
                {
                    if (!mediosPermitidos.Contains(m))
                    {
                        return $"El medio de pago {m} no esta entre los medios de pagos permitidos";
                    }
                }
            }

            if(!item.FechaInicio.HasValue || !item.FechaFin.HasValue)
            {
                return "Fecha de incio y fecha fin deben tener valores";
            }


            return string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Promocion item)
        {
            if (item == null) return BadRequest();

            var error = await ValidarParametros(item);

            var existentes = await db.ObtenerPorRango(item.FechaInicio.Value, item.FechaFin.Value);
            if (existentes.Any())
            {
                return BadRequest("La promocion se solapa con otra ya existente");
            }

            if (!string.IsNullOrEmpty(error)) return BadRequest(error);

            await db.Insertar(item);
            return Ok(item.Id.ToString());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Modificar([FromBody] Promocion item, string id)
        {
            if (item == null) return BadRequest();

            item.Id = new ObjectId(id);
            var error = await ValidarParametros(item);
            if (!string.IsNullOrEmpty(error)) return BadRequest(error);

            var existentes = await db.ObtenerPorRango(item.FechaInicio.Value, item.FechaFin.Value);
            existentes = existentes.Where(i => i.Id != new ObjectId(id)).ToList();

            if (existentes.Any())
            {
                return BadRequest("La promocion se solapa con otra ya existente");
            }

            await db.Modificar(item, id);
            return Ok("Modificado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            await db.Eliminar(id);
            return Ok("Eliminado");
        }

        [HttpGet("vigentes/{fecha}")]
        public async Task<IActionResult> ObtenerVigentes(DateTime fecha)
        {
            var promociones = await db.ObtenerPorVigencia(fecha);
            return Ok(promociones);
        }

        [HttpGet("vigentes/hoy")]
        public async Task<IActionResult> ObtenerVigentesHoy()
        {
            var promociones = await db.ObtenerPorVigencia(null);
            return Ok(promociones);
        }

        [HttpGet("venta/{medio}/{banco}/{categoria}")]
        public async Task<IActionResult> ObtenerPorVenta(string medio, string banco, string categoria)
        {
            var promociones = await db.ObtenerVigentesPorVenta(medio, banco, categoria);
            return Ok(promociones);
        }

    }
}
