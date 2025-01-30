using Microsoft.EntityFrameworkCore;
using PostApi.Data;
using PostApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Connection string
var connectionString = "Server=postdb,1433;Database=post-db;User Id=sa;Password=password123!;TrustServerCertificate=True;";

// Register DbContext
builder.Services.AddDbContext<PostDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPostService, PostService>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Or whatever port your app is using
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();




// Enable CORS
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();


