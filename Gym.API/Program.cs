
using Gym.API.Middleware;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add Controllers (مهم جداً)
builder.Services.AddControllers();

// ✅ Global Exception Middleware
builder.Services.AddTransient<GlobalExceptionMiddleware>();

// ✅ Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gym Management API Project",
        Version = "v1",
        Description = "Clean Architecture API"
    });

    options.UseInlineDefinitionsForEnums();
});

// ✅ IMPORTANT MISSING LINE
builder.Services.AddSwaggerExamples();

// 🔥 HERE (مكان Swagger Examples الصح)
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

// ✅ DbContext
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ✅ Application Layer
builder.Services.AddApplication();


// ✅ Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// ✅ Global Exception Handling Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym API V1");
        c.RoutePrefix = "swagger"; // http://localhost:xxxx/swagger
    });

}


app.UseHttpsRedirection();

app.UseAuthorization();

// ✅ مهم جداً
app.MapControllers();

app.Run();