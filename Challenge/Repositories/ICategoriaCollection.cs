using Challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public interface ICategoriaCollection
    {
        Task Insertar(Categoria item);
        Task InsertarVarios(IEnumerable<Categoria> items);
        Task Modificar(Categoria item, string id);
        Task Eliminar(string id);
        Task<List<Categoria>> ObtenerTodos();
        Task<Categoria> ObtenerPorId(string id);
    }
}
