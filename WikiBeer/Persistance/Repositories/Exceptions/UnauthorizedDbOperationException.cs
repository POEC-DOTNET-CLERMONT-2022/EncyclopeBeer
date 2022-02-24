using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance.Repositories
{
    [Serializable]
    public class UnauthorizedDbOperationException : EntityRepositoryException
    {
        public UnauthorizedDbOperationException()
        {
        }

        public UnauthorizedDbOperationException(string? message) : base(message)
        {
        }

        public UnauthorizedDbOperationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedDbOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }    
}
