using EasyEmployerBackend.Services;
using EasyEmployerBackend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Configure Loket API settings
builder.Services.Configure<LoketApiSettings>(
    builder.Configuration.GetSection("LoketApi"));

// Register HttpClient for LoketService with timeout
builder.Services.AddHttpClient<ILoketService, LoketService>()
    .ConfigureHttpClient(client =>
    {
        client.Timeout = TimeSpan.FromSeconds(30);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
