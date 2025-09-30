using Movies.API.Mapping;
using Movies.Application;
using Movies.Application.Database;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

builder.Services.AddDatabase(config["ConnectionStrings:DefaultConnection"]!);
builder.Services.AddJwtSettings(config);


var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API V1");
    c.RoutePrefix = ""; 
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ValidationMappingMiddleware>();

app.MapControllers();


var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
