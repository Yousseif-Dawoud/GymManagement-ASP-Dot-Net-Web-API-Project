
var builder = WebApplication.CreateBuilder(args);

// Register services before Build

builder.Services.AddOpenApi();

// Register DbContext with SQL Server provider and connection string from  appsettings.json
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Unit of Work in DI (API)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register repositories and services for dependency injection
// ...
// ...
// ...

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();