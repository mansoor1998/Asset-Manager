using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();


if(builder.Environment.IsDevelopment()) {
    builder.Services.AddDbContext<AppDbContext>(opt =>opt.UseInMemoryDatabase("InMem"));
    // Server=mssql-clusterip-srv,1433;Initial Catalog=platformsdb;User ID=sa;Password=Mansoor123?;
    // builder.Services.AddDbContext<AppDbContext>(opt =>opt.UseSqlServer("Server=localhost,1433;Initial Catalog=platformsdb;User ID=sa;Password=Mansoor123?;TrustServerCertificate=true;"));
} else if (builder.Environment.IsProduction()) {
    builder.Services.AddDbContext<AppDbContext>(opt =>opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development Environment");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction()) 
{
    Console.WriteLine("Production Environment");
}
else 
{
    Console.WriteLine("Env is unkown");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();