namespace ReaderBook.Core.Dtos.Book
{
    public class BookResponse
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Gender { get; set; }

        public ICollection<PageDto> Pages { get; set; }
    }
}
