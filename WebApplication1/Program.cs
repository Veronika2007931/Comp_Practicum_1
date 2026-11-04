using CoffeeShopApi.Data;


var builder = WebApplication.CreateBuilder(args);

// 1. Додаємо підтримку контролерів
builder.Services.AddControllers();

// 2. Реєструємо сховище в пам'яті як Singleton
builder.Services.AddSingleton<InMemoryStore>();

// 3. Налаштування OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Підключаємо сторінку Swagger в режимі розробки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 5. Підключаємо маршрутизацію до контролерів
app.MapControllers();

app.Run();