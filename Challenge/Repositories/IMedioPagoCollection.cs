using Challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public interface IMedioPagoCollection
    {
        Task Insertar(MedioPago item);
        Task InsertarVarios(IEnumerable<MedioPago> items);
        Task Modificar(MedioPago item, string id);
        Task Eliminar(string id);
        Task<List<MedioPago>> ObtenerTodos();
        Task<MedioPago> ObtenerPorId(string id);
    }
}
