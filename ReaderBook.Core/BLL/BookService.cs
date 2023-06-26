using AutoMapper;
using ReaderBook.Core.BLL.Interface;
using ReaderBook.Core.DAL.Interface;
using ReaderBook.Core.Data.Caching.Interface;
using ReaderBook.Core.Domain.Book;
using ReaderBook.Core.Dtos.Book;
using ReaderBook.Core.Helpers.Enums;
using ReaderBook.Core.Helpers.Exceptions;
using ReaderBook.Core.Helpers.Extensions;
using ReaderBook.Core.Helpers.Useful;
using ReaderBook.Core.Models.ValueObject.Book;
using System.Text.Json;

namespace ReaderBook.Core.BLL
{
    public class BookService : IBookService
    {
        private readonly ICachingService _cache;
        private readonly IBookDao<BookSchema> _bookDao;
        private readonly IMapper _mapper;

        public BookService(ICachingService cache, IBookDao<BookSchema> bookDao, IMapper mapper)
        {
            _cache = cache;
            _bookDao = bookDao;
            _mapper = mapper;
        }

        public async Task<PaginatedBookResponse> GetPagedAsync(string id, int pageNumber = 1, int pageSize = 10)
        {
            BookSchema bookSchema = null;

            var bookJson = await _cache.GetAsync(id);

            if (bookJson is not null)
            {
                bookSchema = JsonSerializer.Deserialize<BookSchema>(bookJson);
            }
            else
            {
                bookSchema = await _bookDao.GetByIdAsync(id);

                if (bookSchema is null)
                    throw new ArgumentException("Invalid book ID.");
                await _cache.SetAsync(id, JsonSerializer.Serialize(bookSchema));

            }

            var totalPages = bookSchema.Pages.Count;
            if (totalPages is 0)
            {
                throw new ArgumentException("The book does not contain any pages.");
            }

            return  await GetPagedBookResponse(id, bookSchema, pageNumber, pageSize, totalPages);

        }

        public async Task<BookResponse> InsertAsync(BookCreate book)
        {
            var bookModel = Book.Create(book.Title, book.Gender.ToBookGenre(), _mapper.Map<ICollection<Page>>(book.Pages));
            var bookSchema = _mapper.Map<BookSchema>(book);

            await _bookDao.InsertAsync(bookSchema);

            return _mapper.Map<BookResponse>(bookSchema);
        }

        public async Task UpdateAsync(Book book, string id)
        {
            var bookSchema = _mapper.Map<BookSchema>(book);
            
            await _bookDao.UpdateAsync(bookSchema);

            await _cache.RemoveAsync(book.Id);
        }

        public async Task DeleteAsync(string id)
        {

            var book = await _bookDao.GetByIdAsync(id);

            if (book is null) 
                throw new EntityNotFoundException($"Book with ID {id} was not found.");
           
            await _bookDao.DeleteAsync(book);

            await _cache.RemoveAsync(id);
        }

        private async Task<PaginatedBookResponse> GetPagedBookResponse(string id, BookSchema bookSchema, int pageNumber, int pageSize, int totalPages)
        {
            var skipCount = (pageNumber - 1) * pageSize;
            var pages = bookSchema.Pages.OrderBy(pg => pg.Number).Skip(skipCount).Take(pageSize).ToList();

            return new PaginatedBookResponse
            {
                Id = id,
                Gender = bookSchema.Gender.ToBookGenre().ToString(),
                PaginatedResult = new PaginatedResult<PageDto>
                {
                    Items = _mapper.Map<ICollection<PageDto>>(pages),
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalItems = totalPages
                }
            };
        }

    }
}
