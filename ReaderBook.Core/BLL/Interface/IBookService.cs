using ReaderBook.Core.Dtos.Book;

namespace ReaderBook.Core.BLL.Interface
{
    public interface IBookService
    {
        Task<PaginatedBookResponse> GetPagedAsync(string id, int pageNumber = 1, int pageSize = 10);
        Task<BookResponse> InsertAsync(BookCreate book);
        Task UpdateAsync(BookCreate bookCreate, string id);
        Task DeleteAsync(string id);
    }
}
