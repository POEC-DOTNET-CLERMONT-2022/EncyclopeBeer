using Newtonsoft.Json.Serialization;

/// <summary>
/// Note le typeName DOIT Correspondre au nom du type que l'on veut sérialiser. 
/// Aucuns écart n'est permis (on ne peut pas enelver le Suffixe Dto.)
/// </summary>
namespace Ipme.WikiBeer.Dtos.SerializerSettings
{
    internal class KnownTypesBinder : ISerializationBinder
    {
        private IEnumerable<Type> KnownTypes { get;}

        public KnownTypesBinder(IEnumerable<Type> knownTypes)
        {
            KnownTypes = knownTypes;
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            return KnownTypes.SingleOrDefault(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;            
        }
    }
}
