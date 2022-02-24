using System.Runtime.Serialization;

namespace Ipme.WikiBeer.Persistance.Repositories
{
    [Serializable]
    public class EntityRepositoryException : Exception
    {
        public EntityRepositoryException()
        {
        }

        public EntityRepositoryException(string? message) : base(message)
        {
        }

        public EntityRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}