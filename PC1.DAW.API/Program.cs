using Microsoft.EntityFrameworkCore;
using PC1.DAW.CORE.Core.Interfaces;
using PC1.DAW.CORE.Core.Services;
using PC1.DAW.CORE.Infrastructure.Data;
using PC1.DAW.CORE.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _config = builder.Configuration;
var cnx = _config.GetConnectionString("DevConnection");
builder.Services.AddDbContext<TallerDbContext>(options =>
    options.UseSqlServer(cnx));

// Register OrdenServicio Repository and Service
builder.Services.AddTransient<IOrdenServicioRepository, OrdenServicioRepository>();
builder.Services.AddTransient<IOrdenServicioService, OrdenServicioService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
