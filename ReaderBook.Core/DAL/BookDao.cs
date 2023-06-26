using MongoDB.Driver;
using ReaderBook.Core.DAL.Base;
using ReaderBook.Core.DAL.Interface;
using ReaderBook.Core.Data.Contexts.Interfaces;
using ReaderBook.Core.Domain.Book;

namespace ReaderBook.Core.DAL;

public class BookDao : GenericDAO<BookSchema>, IBookDao<BookSchema>
{
    public BookDao(IReaderBookDBContext context) : base(context.GetCollection<BookSchema>())
    {
    }
}
