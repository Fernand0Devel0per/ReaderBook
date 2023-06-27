using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace ReaderBook.Core.DAL.Base;

public abstract class GenericDAO<T>
{
    protected readonly IMongoCollection<T> _collection;

    protected GenericDAO(IMongoCollection<T> collection)
    {
        _collection =  collection;
    }

    protected IQueryable<T> QueryableCollection => _collection.AsQueryable();

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var documents = await _collection.FindAsync(filter);
        return await documents.FirstAsync();
    }

    public async Task InsertAsync(T entity)
    {

        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", ((dynamic)entity).Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", ((dynamic)entity).Id);
        await _collection.DeleteOneAsync(filter);
    }
}
