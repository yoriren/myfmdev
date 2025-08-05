using myfm_dev_api.Clients;
using myfm_dev_api.Interfaces;
using myfm_dev_api.Middleware;
using myfm_dev_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IApiClient, MyfmApiClient>();
builder.Services.AddScoped<IProductService, ProductService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:5173") 
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});

var app = builder.Build();
app.ConfigureExceptionHandler(app.Logger);

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowFrontend");
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();