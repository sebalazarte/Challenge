using MongoDB.Driver;

namespace Challenge.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient client;
        public IMongoDatabase db;

        public MongoDBRepository()
        {
            client = new MongoClient("mongodb+srv://slazarte:48006321Benja@miclustercafe.n5ymv.mongodb.net");
            db = client.GetDatabase("PromocionesDB");
        }
    }
}
