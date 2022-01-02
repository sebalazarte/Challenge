using Challenge.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public class BancoCollection : IBancoCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private readonly IMongoCollection<Banco> Bancos;

        public BancoCollection()
        {
            Bancos = _repository.db.GetCollection<Banco>("Bancos");
        }

        public async Task Eliminar(string id)
        {
            var filter = Builders<Banco>.Filter.Eq(p => p.Id, new ObjectId(id));
            await Bancos.DeleteOneAsync(filter);
        }

        public async Task Insertar(Banco item)
        {
            await Bancos.InsertOneAsync(item);
        }

        public async Task InsertarVarios(IEnumerable<Banco> items)
        {
            await Bancos.InsertManyAsync(items);
        }

        public async Task Modificar(Banco item, string id)
        {
            var filter = Builders<Banco>.Filter.Eq(p => p.Id, new ObjectId(id));
            await Bancos.ReplaceOneAsync(filter, item);
        }

        public async Task<Banco> ObtenerPorId(string id)
        {
            return await Bancos.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } })
                .Result
                .FirstAsync();
        }

        public async Task<List<Banco>> ObtenerTodos()
        {
            return await Bancos.FindAsync(new BsonDocument())
                .Result
                .ToListAsync();
        }
    }
}
