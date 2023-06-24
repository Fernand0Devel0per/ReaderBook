using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ReaderBook.Core.Dtos.Base;
using ReaderBook.Core.Helpers.Enums;
using ReaderBook.Core.Helpers.Exceptions;
using ReaderBook.Core.Helpers.Validations;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;

namespace ReaderBook.Core.Dtos.ValueObject.Book
{
    public class Book : ValidatableObject
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Title { get; private set; }
        [Required(ErrorMessage = "{0} is required")]
        [EnumRange(typeof(BookGenre), ErrorMessage = "{0} value is not valid.")]
        public BookGenre Gender { get; private set; }
        [NotEmptyCollection(ErrorMessage = "The list of pages cannot be empty.")]
        public ICollection<Page> Pages { get; private set; }

        private Book(string name, BookGenre gender, ICollection<Page> pages)
        {
            Title = name;
            Gender = gender;
            Pages = pages;
        }

        public static Book Create(string name, BookGenre gender, ICollection<Page> pages)
        {
            var book = new Book(name, gender, pages);

            var validationResults = book.Validate();

            if (validationResults.Any())
            {
                throw new CustomValidationException(validationResults);
            }

            return book;
        }
    }

    

}
