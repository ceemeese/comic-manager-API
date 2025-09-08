using Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Business;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Utils;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    //.WriteTo.Console()
    .WriteTo.File(
        Environment.GetEnvironmentVariable("LOG_PATH") ?? "logs/app.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

Log.Information("Aplicación iniciada");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };
    });

// Registrar LoggerService, solo una instancia
builder.Services.AddSingleton<Logger>();

//Usar Serilog
builder.Host.UseSerilog();

// Add services to the container.

//Conexión a la base de datos segun variable entorno
var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "my-secret-pw";
var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "ComicManagerAPIDB";

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
var connectionString = $"Server={host};Port=3306;Database={database};Uid={user};Pwd={password};AllowPublicKeyRetrieval=True;Max Pool Size=1000"
    ?? throw new Exception("Error: 'DefaultConnection' not found.");

//Repositorios
builder.Services.AddScoped<IGenreRepository, GenreEfRepository>();
builder.Services.AddScoped<IUserRepository, UserEfRepository>();
builder.Services.AddScoped<IComicRepository, ComicEfRepository>();
builder.Services.AddScoped<IUserComicRepository, UserComicEfRepository>();
builder.Services.AddScoped<IComicGenreRepository, ComicGenreEfRepository>();

//Servicios
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IComicService, ComicService>();
builder.Services.AddScoped<IUserComicService, UserComicService>();
builder.Services.AddScoped<IComicGenreService, ComicGenreService>();

//--> inyectar context. new version of Pomelo needs more arguments (ServerVersion)
builder.Services.AddDbContext<DataContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


bool connected = false;
while (!connected)
{
    try
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            connected = true;
            Console.WriteLine("Database connection succeeded.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection error: {ex.Message}");
        System.Threading.Thread.Sleep(5000);
    }
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Comic Manager API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

//Migraciones
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Swagger para poder visualizar en 7877
app.Urls.Add("http://+:7877");

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
