using Contexts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using Perstistance;
using Perstistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// AddNewtonSoftJson (de AspNetCore.Mvc.NewtonSoftJson pour s�rialiser des objets d�riv�es)
// ---> Absoluement indispensable
builder.Services.AddControllers().AddNewtonsoftJson(
    opt => opt.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto);
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mapper profiles utilis�
builder.Services.AddAutoMapper(typeof(DtoEntityProfile).Assembly); // il faudrait un moyen de s�parer les profiles dans la m�me assembly

// Context Manager utilis� (non -> Context utilis� directement : on ne peut pas passer par un repository comme sa!)
//builder.Services.AddDbContext<WikiBeerSqlContext>(options =>
//   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultContext")));
// a creuser � la place de ce qu'il y a au dessu: https://www.c-sharpcorner.com/blogs/net-core-mvc-with-entity-framework-core-using-dependency-injection-and-repository
//builder.Services.AddScoped<IBeerRepository, BddBeerManager>();
//builder.Services.AddDbContextPool<WikiBeerSqlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultContext")));
//builder.Services.AddDbContextPool<WikiBeerSqlContext>(opt => opt.UseSqlServer(ConnectionStrings, b => b.MigrationsAssembly("Persistance")));

//builder.Services.AddScoped<IBeerRepository, BddBeerManager>();

//builder.Services.AddDbContext<WikiBeerSqlContext>(options =>
//   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultContext")));


//builder.Services.AddDbContext<WikiBeerSqlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultContext")));
//builder.Services.AddScoped<IBeerRepository, BeerRepository>();
//builder.Services.AddDbContext<WikiBeerSqlContext>();
//builder.Services.AddScoped<IBeerRepository, BddToApiManager>();

//builder.Services.AddScoped<IBeerRepository, BeerRepository>();
// a creuser � la place de ce qu'il y a au dessu: https://www.c-sharpcorner.com/blogs/net-core-mvc-with-entity-framework-core-using-dependency-injection-and-repository
//builder.Services.AddDbContextPool<WikiBeerSqlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultContext")));

// Sur les Scoped : a creuser un peu (doit normalement se faire apr�s le AddDbContext)
//builder.Services.AddSingleton<IBeerRepository, BeerRepository>; // donne un repos partag� entre tt les utilisateurs
//builder.Services.AddScoped<IBeerRepository, BeerRepository>(); // donne un repo qui sera cr�e et d�truit pour chauqe utilisateur 
//builder.Services.AddTransient<IBeerRepository, BeerRepository>(); // a creuser

// Singleton: IoC container will create and share a single instance of a service throughout the application's lifetime.
// Transient: The IoC container will create a new instance of the specified service type every time you ask for it.
// Scoped: IoC container will create an instance of the specified service type once per request and will be shared in a single request.

// Notes si l'on passe par des Repository, il faut en d�clarer autant que l'on a d'objets que l'on veut g�rer
// A ce moment la d�clarer simplement un DbContext typ� est plus simple
// Par contre on doit se refaire tt les CRUD dans chaque Controller et une fois rendue la, les type g�n�riques
// vont bien aider...

// Ci dessous des choses qui marchent
var cs = builder.Configuration.GetConnectionString("DefaultContext"); // TODO : utiliser plut�t un string stock� cot� persistance??? (voir DefaultMigration)
// Syntax pour passer un IBeerReposetory avec connection string
//builder.Services.AddScoped<IBeerRepository>(param => new BddToApiManager(cs));
//// Syntax pour passer un IBeerReposetory avec DbContext particulier
//builder.Services.AddScoped<IBeerRepository>(param => new BddToApiManager(new WikiBeerSqlContext(cs)));
//// Syntax pour passer un IBeerReposetory de type BeerRepository directement
//builder.Services.AddScoped<IBeerRepository>(param => new BeerRepository(new WikiBeerSqlContext(cs)));
// Syntax pour passer un IBeerRepository g�n�rique de type Beer avec un DbContext associ� de type WikiBeerSqlContext
builder.Services.AddScoped<IGenericDbRepository<BeerEntity>>(param => new GenericDbRepository<BeerEntity>(new WikiBeerSqlContext(cs)));
// Syntax pour passer ler repositories de test (utilisation d'AutoFixture)
builder.Services.AddSingleton<IFakeBeerRepository, FakeBeerRepository>(); // Singleton pour reproductilibilit� dans swagger
                                                                          // et test put/post via copi� coll� des Guid id



var app = builder.Build();

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
