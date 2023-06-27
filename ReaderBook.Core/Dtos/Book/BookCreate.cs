using MongoDB.Bson;

namespace ReaderBook.Core.Dtos.Book
{
    public class BookCreate
    {
        public string Title { get; set; }

        public int Gender { get; set; }

        public ICollection<PageDto> Pages { get; set; }
    }
}
