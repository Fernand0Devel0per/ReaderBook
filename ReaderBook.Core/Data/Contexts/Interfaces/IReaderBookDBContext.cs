using MongoDB.Driver;

namespace ReaderBook.Core.Data.Contexts.Interfaces;

public interface IReaderBookDBContext
{
    IMongoCollection<T> GetCollection<T>();
}
