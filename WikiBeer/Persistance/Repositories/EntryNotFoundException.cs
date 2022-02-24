using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance.Repositories
{
    [Serializable]
    public class EntryNotFoundException : EntityRepositoryException
    {
        public EntryNotFoundException()
        {
        }

        public EntryNotFoundException(string? message) : base(message)
        {
        }

        public EntryNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
