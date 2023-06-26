using ReaderBook.Core.Helpers.Enums;
using ReaderBook.Core.Helpers.Exceptions;
using ReaderBook.Core.Helpers.Validations;
using ReaderBook.Core.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace ReaderBook.Core.Models.ValueObject.Book;

public class Book : ValidatableObject
{

    public string Id { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public string Title { get; private set; }

    [Required(ErrorMessage = "{0} is required")]
    [EnumRange(typeof(BookGenre), ErrorMessage = "{0} value is not valid.")]
    public BookGenre Gender { get; private set; }

    [NotEmptyCollection(ErrorMessage = "The list of pages cannot be empty.")]
    public ICollection<Page> Pages { get; private set; }

    private Book(string title, BookGenre gender, ICollection<Page> pages)
    {
        Title = title;
        Gender = gender;
        Pages = pages;
    }

    public static Book Create(string title, BookGenre gender, ICollection<Page> pages)
    {
        var book = new Book(title, gender, pages);

        var validationResults = book.Validate();

        if (validationResults.Any())
        {
            throw new CustomValidationException(validationResults);
        }

        return book;
    }
}


