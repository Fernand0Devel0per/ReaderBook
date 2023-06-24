using System.ComponentModel.DataAnnotations;

namespace ReaderBook.Core.Helpers.Validations;

public class EnumRangeAttribute : ValidationAttribute
{
    private readonly Type _enumType;

    public EnumRangeAttribute(Type enumType)
    {
        _enumType = enumType;
    }

    public override bool IsValid(object value)
         => value is null || Enum.GetValues(_enumType)
            .Cast<int>()
            .Contains(Convert.ToInt32(value));


    public override string FormatErrorMessage(string name) 
        => $"The field {name} has an invalid value.";
    
}
