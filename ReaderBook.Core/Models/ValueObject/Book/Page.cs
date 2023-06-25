using ReaderBook.Core.Dtos.Base;
using ReaderBook.Core.Helpers.Enums;
using ReaderBook.Core.Helpers.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;

namespace ReaderBook.Core.Dtos.ValueObject.Book;

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
