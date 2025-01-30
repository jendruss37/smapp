using IdentityApi.Data;
using IdentityApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connectionString = "Server=identitydb,1433;Database=credentials-db;User Id=sa;Password=password123!;TrustServerCertificate=True;";


builder.Services.AddDbContext<CredentialsDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IIdentityService, IdentityService>();
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
