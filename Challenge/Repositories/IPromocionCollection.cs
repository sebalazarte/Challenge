using Challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public interface IPromocionCollection
    {
        Task Insertar(Promocion item);
        Task Modificar(Promocion item, string id);
        Task Eliminar(string id);
        Task<List<Promocion>> ObtenerTodos();
        Task<Promocion> ObtenerPorId(string id);
        Task<List<Promocion>> ObtenerPorVigencia(DateTime? fecha);
        Task<List<Promocion>> ObtenerVigentesPorVenta(string medio, string banco, string categoria);
    }
}
