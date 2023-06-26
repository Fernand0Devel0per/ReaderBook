using ReaderBook.Core.Helpers.Enums;

namespace ReaderBook.Core.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static BookGenre ToBookGenre(this int value)
        {
            if (Enum.IsDefined(typeof(BookGenre), value))
            {
                return (BookGenre)value;
            }
            else
            {
                throw new ArgumentException("The integer value does not correspond to a valid BookGenre enum value.");
            }
        }
    }

}
