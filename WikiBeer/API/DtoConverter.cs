using Ipme.WikiBeer.Dtos.Ingredients;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ipme.WikiBeer.API
{
    public class DtoConverter : JsonConverter
    {
        private JsonSerializer StandartSerializer { get; }
        private JsonSerializer SpecialSerializer { get; }
        private IEnumerable<Type> SpecialTypes { get; }
        private IEnumerable<string> SpecialNames { get; }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public DtoConverter(JsonSerializerSettings standartSettings, JsonSerializerSettings specialSettings, IEnumerable<Type> specialTypes = null)
        {
            StandartSerializer = JsonSerializer.Create(standartSettings);
            SpecialSerializer = JsonSerializer.Create(specialSettings);
            if (specialTypes == null)
            {
                SpecialTypes = Enumerable.Empty<Type>();
                SpecialNames = Enumerable.Empty<string>();
            }
            else
            {
                SpecialTypes = specialTypes;
                SpecialNames = SpecialTypes.Select(t => t.Name);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var isSpecial = SpecialNames.Contains(objectType.Name);
            if (isSpecial)
            {
                return SpecialSerializer.Deserialize(reader, objectType);
            }
            else
            {
                return StandartSerializer.Deserialize(reader, objectType); ;
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var isSpecial = SpecialTypes.Any(t => t.IsInstanceOfType(value));            
            if (isSpecial)
            {
                SpecialSerializer.Serialize(writer, value);
            }
            else
            {
                StandartSerializer.Serialize(writer, value);
            }
        }
    }
}
