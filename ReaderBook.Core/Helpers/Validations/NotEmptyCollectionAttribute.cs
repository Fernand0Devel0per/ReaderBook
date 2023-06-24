using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace ReaderBook.Core.Helpers.Validations;

public class NotEmptyCollectionAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
        => value is ICollection collection && collection.Count > 0;

    public override string FormatErrorMessage(string name)
        => $"The collection {name} cannot be empty.";
}



