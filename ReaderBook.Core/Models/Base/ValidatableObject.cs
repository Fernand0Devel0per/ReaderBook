using System.ComponentModel.DataAnnotations;

namespace ReaderBook.Core.Models.Base;

public abstract class ValidatableObject
{
    protected ICollection<ValidationResult> Validate()
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(this);
        Validator.TryValidateObject(this, validationContext, validationResults, true);

        return validationResults;
    }
}

