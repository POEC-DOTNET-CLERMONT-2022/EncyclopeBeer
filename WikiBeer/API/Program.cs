using Ipme.WikiBeer.API.MapperProfiles;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Persistance.Contexts;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;

using Ipme.WikiBeer.API;
using Ipme.WikiBeer.Dtos.SerializerSettings;

/// <summary>
/// Notes sur de la désérialisation polymorphic en typescript : 
/// https://www.npmjs.com/package/json2typescript
/// </summary>

var builder = WebApplication.CreateBuilder(args);
// voir : https://docs.microsoft.com/fr-fr/aspnet/core/security/cors?view=aspnetcore-6.0 pour les police à allouer
builder.Services.AddCors(
    options => options.AddPolicy("LocalPolicy",
                    builder =>
        {
            builder.WithOrigins("http://localhost:4200"); // ici l'adresse du front Angular
        })
    );

// AddNewtonSoftJson (de AspNetCore.Mvc.NewtonSoftJson pour sérialiser des objets dérivées)
// ---> Absoluement indispensable
builder.Services.AddControllers().AddNewtonsoftJson(
    opt => {
        opt.SerializerSettings.Converters.Add(DtoSettings.Converter);
    });

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mapper profiles utilisé
builder.Services.AddAutoMapper(typeof(DtoEntityProfile).Assembly);

// Conection String 
string cs;
if (args.Any())
{
    cs = args[0];
}
else
{
    cs = builder.Configuration.GetConnectionString("DefaultConnection");
}
//var cs = "Data Source = (LocalDb)\\MSSQLLocalDB; Initial Catalog = WikiBeer; Integrated Security = True;";
// Injection de dépendance
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<DbContext, WikiBeerSqlContext>(); // pour utilisation dans le GenericRepository
builder.Services.AddDbContext<WikiBeerSqlContext>(opt => opt.UseSqlServer(cs)); // le AddDbContext enregistre plus que le AddScoped (comme les classes d'options)

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
