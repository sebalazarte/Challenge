using Challenge.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public class PromocionCollection : IPromocionCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private readonly IMongoCollection<Promocion> Promociones;

        public PromocionCollection()
        {
            Promociones = _repository.db.GetCollection<Promocion>("Promociones");
        }

        public async Task Eliminar(string id)
        {
            var filter = Builders<Promocion>.Filter.Eq(p => p.Id, new ObjectId(id));
            await Promociones.DeleteOneAsync(filter);
        }

        public async Task Insertar(Promocion item)
        {
            await Promociones.InsertOneAsync(item);
        }

        public async Task Modificar(Promocion item, string id)
        {
            var filter = Builders<Promocion>.Filter.Eq(p => p.Id, new ObjectId(id));
            await Promociones.ReplaceOneAsync(filter, item);
        }

        public async Task<Promocion> ObtenerPorId(string id)
        {
            return await Promociones.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } })
                .Result
                .FirstAsync();
        }

        public async Task<List<Promocion>> ObtenerTodos()
        {
            return await Promociones.FindAsync(new BsonDocument())
                .Result
                .ToListAsync();
        }

        public async Task<List<Promocion>> ObtenerPorVigencia(DateTime? fecha)
        {
            var fechaParam = fecha.HasValue ? fecha.Value : DateTime.Now;
            var filter = Builders<Promocion>.Filter.Where(i => i.FechaInicio <= fechaParam && fechaParam <= i.FechaFin && i.Activo);
            return await Promociones.FindAsync(filter).Result.ToListAsync();
        }

        public async Task<List<Promocion>> ObtenerVigentesPorVenta(string medio, string banco, string categoria)
        {
            var filter = Builders<Promocion>.Filter
                .Where(i => i.FechaInicio <= DateTime.Now && DateTime.Now <= i.FechaFin && i.Activo && 
                       (i.MediosPagos.Contains(medio) || !i.MediosPagos.Any()) &&
                       (i.Bancos.Contains(banco) || !i.Bancos.Any()) &&
                       (i.CategoriasProductos.Contains(categoria) || !i.CategoriasProductos.Any()));

            var vigentes = await Promociones.FindAsync(filter).Result.ToListAsync();
            
            return vigentes;
        }
    }
}
