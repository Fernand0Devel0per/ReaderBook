using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ReaderBook.Core.Helpers.Enums;

namespace ReaderBook.Core.Domain.Book;

public class BookSchema
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string Title { get; set; }

    public int Gender { get; set; }

    public ICollection<Page> Pages { get; set; }

    public class Page
    {
        public int Number { get; set; }
        public string Content { get; set; }
    }
}
