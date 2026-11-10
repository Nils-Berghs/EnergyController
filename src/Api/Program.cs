using Berghs.Nils.EnergyController.Api;
using Berghs.Nils.EnergyController.Api.Endpoints;
using Berghs.Nils.EnergyController.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApi(builder.Configuration, builder.Environment).AddInfrastructure();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapSolarEndpoints();

app.UseHttpsRedirection();

app.Run();

