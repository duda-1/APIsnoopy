var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SupabaseService>();

// CORS - Adicionando política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Vite dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS - Usando a política de CORS antes do Authorization
app.UseCors("AllowLocalhostFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
