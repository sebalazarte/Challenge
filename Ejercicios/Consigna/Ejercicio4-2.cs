using Domain.Entities;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios.Consigna2
{
    public class CajaRepository : BaseRepository<Caja>
    {
        public override Task<IEnumerable<Caja>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Caja> GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }

    public class SucursalRepository : BaseRepository<Sucursal>
    {
        public override Task<IEnumerable<Sucursal>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Caja> GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
