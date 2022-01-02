﻿using Challenge.Models;
using Challenge.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedioPagoController : ControllerBase
    {
        private readonly IMedioPagoCollection db = new MedioPagoCollection();

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var lista = await db.ObtenerTodos();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(string id)
        {
            var item = await db.ObtenerPorId(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MedioPago item)
        {
            if (item == null) return BadRequest();
            await db.Insertar(item);
            return Created("Creado", true);
        }

        [HttpPost("inicializar")]
        public async Task<IActionResult> Inicializar([FromBody] IEnumerable<MedioPago> items)
        {
            await db.InsertarVarios(items);
            return Created("Creado", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Modificar([FromBody] MedioPago item, string id)
        {
            if (item == null) return BadRequest();

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
