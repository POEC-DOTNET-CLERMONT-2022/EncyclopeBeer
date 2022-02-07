using System.Runtime.Serialization;

namespace Ipme.WikiBeer.Persistance.Repositories
{
    [Serializable]
    public class UndesiredBorderEffectException : Exception
    {
        public UndesiredBorderEffectException()
        {
        }

        public UndesiredBorderEffectException(string? message) : base(message)
        {
        }

        public UndesiredBorderEffectException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UndesiredBorderEffectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}