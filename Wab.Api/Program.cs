using System.Reflection;
using System.Resources;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Wab.Api.Exception;
using Wab.Core.Repository;
using Wab.Core.Service;
using Wab.Core.Service.DailyPoint;
using Wab.DbContext;
using Wab.DbContext.Repository;

var builder = WebApplication.CreateBuilder(args);

// Misc
builder.Services.AddDbContext<WabDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder
    .Services.AddControllers()
    .AddJsonOptions(opt => { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddAutoMapper(typeof(DbContextAutoMapperProfile));

// Repositories
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPointCalculator>(_ => new SeasonPointCalculator(() => DateTime.Now));

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<TransactionService>();

var app = builder.Build();

app.UseMiddleware<CoreExceptionMiddleware>();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WabDbContext>();
    db.Database.Migrate();

    if (!builder.Environment.IsProduction())
    {
        var rm = new ResourceManager("Wab.Api.Scripts.dummy.sql",
            Assembly.GetExecutingAssembly());
        db.Database.ExecuteSqlRaw(rm.GetString("Sql"));
    }
}

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