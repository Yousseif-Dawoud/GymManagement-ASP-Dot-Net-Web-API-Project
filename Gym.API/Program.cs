


var builder = WebApplication.CreateBuilder(args);

// Register services before Build
builder.Services.AddOpenApi();



// Register DbContext with SQL Server provider and connection string from  appsettings.json
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register application services with the dependency injection container 
// AddApplication => Contains All the services related to the application layer
builder.Services.AddApplication();


// Register Unit of Work in DI (API)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();