using Challenge.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public class CategoriaCollection : ICategoriaCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private readonly IMongoCollection<Categoria> Categorias;

        public CategoriaCollection()
        {
            Categorias = _repository.db.GetCollection<Categoria>("Categorias");
        }

        public async Task Eliminar(string id)
        {
            var filter = Builders<Categoria>.Filter.Eq(p => p.Id, new ObjectId(id));
            await Categorias.DeleteOneAsync(filter);
        }

        public async Task Insertar(Categoria item)
        {
            await Categorias.InsertOneAsync(item);
        }

        public async Task InsertarVarios(IEnumerable<Categoria> items)
        {
            await Categorias.InsertManyAsync(items);
        }

        public async Task Modificar(Categoria item, string id)
        {
            var filter = Builders<Categoria>.Filter.Eq(p => p.Id, new ObjectId(id));
            await Categorias.ReplaceOneAsync(filter, item);
        }

        public async Task<Categoria> ObtenerPorId(string id)
        {
            return await Categorias.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } })
                .Result
                .FirstAsync();
        }

        public async Task<List<Categoria>> ObtenerTodos()
        {
            return await Categorias.FindAsync(new BsonDocument())
                .Result
                .ToListAsync();
        }
    }
}
