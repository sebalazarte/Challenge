using Domain.Entities;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios.Consigna
{
    public class CajaRepository : IBaseRepository<Caja>
    {
        public Task<IEnumerable<Caja>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Caja> GetOneAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class SucursalRepository : IBaseRepository<Sucursal>
    {
        public Task<IEnumerable<Sucursal>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Sucursal> GetOneAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
