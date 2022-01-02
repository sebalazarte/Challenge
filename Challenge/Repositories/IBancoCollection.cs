using Challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public interface IBancoCollection
    {
        Task Insertar(Banco item);
        Task InsertarVarios(IEnumerable<Banco> items);
        Task Modificar(Banco item, string id);
        Task Eliminar(string id);
        Task<List<Banco>> ObtenerTodos();
        Task<Banco> ObtenerPorId(string id);
    }
}
