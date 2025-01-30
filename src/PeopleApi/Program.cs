using Microsoft.EntityFrameworkCore;
using PeopleApi.Data;
using PeopleApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = "Server=peopledb,1433;Database=people-db;User Id=SA;Password=password123!;TrustServerCertificate=True;";

// Register DbContext
builder.Services.AddDbContext<PeopleDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Or whatever port your app is using
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
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

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
