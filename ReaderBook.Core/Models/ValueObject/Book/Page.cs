using ReaderBook.Core.Helpers.Exceptions;
using ReaderBook.Core.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace ReaderBook.Core.Models.ValueObject.Book;

public class Page : ValidatableObject
{
    [Required(ErrorMessage = "{0} is required")]
    public int Number { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [MinLength(100, ErrorMessage = "{0} must be at least 100 characters long.")]
    public string Content { get; set; }

    private Page(int number, string content)
    {
        Number = number;
        Content = content;
    }

    public static Page Create(int number,string content)
    {
        var page = new Page(number, content);

        var validationResults = page.Validate();

        if (validationResults.Any())            
            throw new CustomValidationException(validationResults);
       
        return page;
    }
}
