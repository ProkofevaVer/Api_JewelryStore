using Microsoft.EntityFrameworkCore;
using Api_JewelryStore.Models;
using Api_JewelryStore.Product_Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Добавление контекста базы данных
builder.Services.AddDbContext<DiplomDb3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация ProductService
builder.Services.AddScoped<ProductService>();
// Добавление политики CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7075") // Укажите URL вашего клиентского приложения
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin"); // Включите CORS
app.UseAuthorization();

app.MapControllers();

app.Run();
