using Challenge.Models;
using Challenge.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IBancoCollection db = new BancoCollection();

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var bancos = await db.ObtenerTodos();
            return Ok(bancos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(string id)
        {
            var banco = await db.ObtenerPorId(id);
            return Ok(banco);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Banco item)
        {
            if (item == null) return BadRequest();
            await db.Insertar(item);
            return Created("Creado", true);
        }

        [HttpPost("inicializar")]
        public async Task<IActionResult> Inicializar([FromBody] IEnumerable<Banco> items)
        {
            await db.InsertarVarios(items);
            return Created("Creado", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Modificar([FromBody] Banco item, string id)
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
