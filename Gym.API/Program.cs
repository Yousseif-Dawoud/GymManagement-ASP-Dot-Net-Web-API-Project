
using Gym.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add Controllers (مهم جداً)
builder.Services.AddControllers();


// ✅ Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gym Management API Project",
        Version = "v1",
        Description = "Clean Architecture API"
    });

    // 🔥 هنا المكان الصح
    options.UseInlineDefinitionsForEnums();
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym API V1");
        c.RoutePrefix = "swagger"; // http://localhost:xxxx/swagger
    });

}


// ✅ Global Exception Handling Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

// ✅ مهم جداً
app.MapControllers();

app.Run();