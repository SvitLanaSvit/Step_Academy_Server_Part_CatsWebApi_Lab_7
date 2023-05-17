using CatsWebApi.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICatDBService, CatCosmosService>(option =>
{
    IConfigurationSection section = builder.Configuration.GetSection("AzureCosmosDBSettings");
    string uri = section.GetValue<string>("uri");
    string key = section.GetValue<string>("key");
    string databaseId = section.GetValue<string>("databaseId");
    string containerId = section.GetValue<string>("containerId");
    CosmosClient cosmosClient = new (uri, key);

    return new CatCosmosService(cosmosClient, databaseId, containerId);
});

builder.Services.AddCors(); //add CORS

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Chabge Config by CORS
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
