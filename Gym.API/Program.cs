
var builder = WebApplication.CreateBuilder(args);

// ✅ Add Controllers (مهم جداً)
builder.Services.AddControllers();


// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gym API",
        Version = "v1",
        Description = "Clean Architecture API"
    });
});


// ✅ DbContext
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ✅ Application Layer
builder.Services.AddApplication();


// ✅ Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();


// ✅ Middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym API V1");
    c.RoutePrefix = "swagger"; // http://localhost:xxxx/swagger
});

app.UseHttpsRedirection();

app.UseAuthorization();

// ✅ مهم جداً
app.MapControllers();

app.Run();