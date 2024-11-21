using Microsoft.EntityFrameworkCore;
using Wab.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WabDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WabDbContext>();
    db.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");

if (!builder.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.Run();