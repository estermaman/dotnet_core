var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// הפניה ל-HTTPS רק אם הוגדר פורט HTTPS —
// אחרת המידלוור מתריע "Failed to determine the https port" בכל בקשה
if (!string.IsNullOrEmpty(builder.Configuration["ASPNETCORE_HTTPS_PORT"])
    || app.Urls.Any(url => url.StartsWith("https", StringComparison.OrdinalIgnoreCase)))
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
