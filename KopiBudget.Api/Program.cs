using KopiBudget.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DB context with Supabase/Postgres connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Production: simple documentation redirect
    app.MapGet("/swagger", () => "API documentation is available in development mode only");
}
// Add this to Program.cs for testing
app.MapGet("/test", () => new
{
    message = "API is working!",
    timestamp = DateTime.UtcNow
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();