using MongoDB.Driver;
using ReaderBook.Core.Data.Contexts.Interfaces;

namespace ReaderBook.Core.Data.Context
{
    public class ReaderBookDBContext : IReaderBookDBContext
    {
        private readonly IMongoDatabase _database;

        public ReaderBookDBContext(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string databaseName = configuration.GetSection("DataBases")["ReaderBook"];

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(nameof(T));
        }
    }
}
