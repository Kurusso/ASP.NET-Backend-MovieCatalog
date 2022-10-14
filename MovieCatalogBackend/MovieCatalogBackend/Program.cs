using Microsoft.EntityFrameworkCore;
using MovieCatalogBackend.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB:
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieCatalogDbContext>(options => options.UseSqlServer(connection));


var app = builder.Build();


//DB init and update:
using var serviceScope=app.Services.CreateScope();
var dbContext = serviceScope.ServiceProvider.GetService<MovieCatalogDbContext>();
dbContext?.Database.Migrate();

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
