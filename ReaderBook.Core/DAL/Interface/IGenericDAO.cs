namespace ReaderBook.Core.DAL.Interface
{
    public interface IGenericDAO<T>
    {
        Task<T> GetByIdAsync(string id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
