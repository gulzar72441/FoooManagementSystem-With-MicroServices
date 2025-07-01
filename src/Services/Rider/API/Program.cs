using FoodOrderingSystem.Rider.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FoodOrderingSystem.Rider.Application.Services;
using FoodOrderingSystem.Rider.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RiderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRiderService, RiderService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin() // Replace with the actual origin of your frontend application
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin"); // Use the defined CORS policy here, BEFORE UseAuthentication and UseAuthorization

app.UseAuthorization();

app.MapControllers();

await ApplyMigrations(app.Services);

app.Run();

async Task ApplyMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<RiderDbContext>();
    if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
    {
        await dbContext.Database.MigrateAsync();
    }
    await DbSeeder.SeedAsync(dbContext);
}
