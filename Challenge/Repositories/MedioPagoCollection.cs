using Challenge.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public class MedioPagoCollection : IMedioPagoCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private readonly IMongoCollection<MedioPago> MediosPagos;

        public MedioPagoCollection()
        {
            MediosPagos = _repository.db.GetCollection<MedioPago>("MediosPagos");
        }

        public async Task Eliminar(string id)
        {
            var filter = Builders<MedioPago>.Filter.Eq(p => p.Id, new ObjectId(id));
            await MediosPagos.DeleteOneAsync(filter);
        }

        public async Task Insertar(MedioPago item)
        {
            await MediosPagos.InsertOneAsync(item);
        }

        public async Task InsertarVarios(IEnumerable<MedioPago> items)
        {
            await MediosPagos.InsertManyAsync(items);
        }

        public async Task Modificar(MedioPago item, string id)
        {
            var filter = Builders<MedioPago>.Filter.Eq(p => p.Id, new ObjectId(id));
            await MediosPagos.ReplaceOneAsync(filter, item);
        }

        public async Task<MedioPago> ObtenerPorId(string id)
        {
            return await MediosPagos.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } })
                .Result
                .FirstAsync();
        }

        public async Task<List<MedioPago>> ObtenerTodos()
        {
            return await MediosPagos.FindAsync(new BsonDocument())
                .Result
                .ToListAsync();
        }
    }
}
