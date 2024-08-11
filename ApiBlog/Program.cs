using ApiBlog.Data;
using ApiBlog.Mappers;
using ApiBlog.Repositorio;
using ApiBlog.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//configuracion cadena conexion

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

// Add services to the container.

builder.Services.AddScoped<IPostRepositorio, PostRepositorio>();//creamos el servicio primero la interface y después su implementación

//Agregar Automapper
builder.Services.AddAutoMapper(typeof(BlogMapper));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
