using Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Business;
using Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new Exception("Error: 'DefaultConnection' not found.");

//Repositorios
builder.Services.AddScoped<IGenreRepository, GenreEfRepository>();
builder.Services.AddScoped<IUserRepository, UserEfRepository>();
builder.Services.AddScoped<IComicRepository, ComicEfRepository>();

//Servicios
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IComicService, ComicService>();

//--> inyectar context. new version of Pomelo needs more arguments (ServerVersion)
builder.Services.AddDbContext<DataContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



using (var connection = new MySqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection succeeded.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Connection error: {ex.Message}");
    }
}


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
