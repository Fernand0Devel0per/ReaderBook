using ReaderBook.Core.Helpers.Useful;

namespace ReaderBook.Core.Dtos.Book
{
    public class PaginatedBookResponse
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Gender { get; set; }

        public PaginatedResult<PageDto> PaginatedResult { get; set; }
    }
}
