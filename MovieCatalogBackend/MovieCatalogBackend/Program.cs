using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MovieCatalogBackend.Configurations;
using MovieCatalogBackend.Context;
using MovieCatalogBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserAddService, UserAddService>();
builder.Services.AddScoped<IMoviePageGetService, MoviePageGetService>();
builder.Services.AddScoped<IUserIdentityService, UserIdentityService>();
builder.Services.AddScoped<IReviewAddService, ReviewAddService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IFavoriteMovieService, FavoriteMovieService>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JwtConfiguration.Issuer,
            ValidateAudience = true,
            ValidAudience = JwtConfiguration.Audience,
            ValidateLifetime= true,
            IssuerSigningKey = JwtConfiguration.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey= true,

        };
    });

//DB:
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieCatalogDbContext>(options => options.UseSqlServer(connection));

var app = builder.Build();
//Auth init:
app.UseAuthentication();
app.UseAuthorization();

//Db init and update:
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
