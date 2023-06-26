using ReaderBook.Core.Helpers.Exceptions.Base;
using System.Runtime.Serialization;

namespace ReaderBook.Core.Helpers.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : SerializableException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
