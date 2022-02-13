using Ipme.WikiBeer.Dtos.Ingredients;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Ipme.WikiBeer.Dtos.SerializerSettings
{
    public static class DtoSettings
    {
        private static Assembly DtoAssembly { get; } = typeof(BeerDto).Assembly;

        private static IEnumerable<Type> AllowedDtos = DtoAssembly.GetTypes().Where(dtoType => typeof(IDto).IsAssignableFrom(dtoType));
        
        public static ISerializationBinder KnownTypesBinder { get; } = new KnownTypesBinder(AllowedDtos);

        public static DefaultContractResolver contractResolver { get; } = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy(),
        };

        public static JsonSerializerSettings StandartSettings { get; } = new JsonSerializerSettings { 
            TypeNameHandling = TypeNameHandling.Auto, 
            SerializationBinder = KnownTypesBinder,
            ContractResolver = contractResolver};
        
        public static JsonSerializerSettings SpecialSettings = new JsonSerializerSettings { 
            TypeNameHandling = TypeNameHandling.Objects, 
            SerializationBinder = KnownTypesBinder,
            ContractResolver = contractResolver};
        
        public static IEnumerable<Type> SpecialTypes { get; } = new Type[] { typeof(IngredientDto) };
        
        public static JsonConverter Converter { get; }= new DtoConverter(StandartSettings, SpecialSettings, SpecialTypes);
        
        public static JsonSerializerSettings DefaultSettings
        {
            get
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(Converter);
                return settings;
            }
        }
    }
}
