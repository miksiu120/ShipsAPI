using ShipsAPI.Factories.ShipFactories;
using ShipsAPI.Models.Ships;
using ShipsAPI.Repositories;
using ShipsAPI.Services.Ships;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;
using ShipsAPI.Models;
using ShipsAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                ti =>
                {
                    if (ti.Type == typeof(Ship))
                    {
                        ti.PolymorphismOptions = new JsonPolymorphismOptions
                        {
                            TypeDiscriminatorPropertyName = "shipType",
                            IgnoreUnrecognizedTypeDiscriminators = true,
                            UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType,
                            DerivedTypes =
                            {
                                new JsonDerivedType(typeof(TankerShip), "Tanker"),
                                new JsonDerivedType(typeof(PassengerShip), "Passenger"),
                            }
                        };
                    }
                }
            }
        };
    });


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IShipRepository,ShipRepository>();

builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<ITankerShipService, TankerShipService>();
builder.Services.AddScoped<IPassengerShipService,PassengerShipService>();

builder.Services.AddTransient<IShipFactory<NewPassengerShipDto>, PassengerShipFactory>();
builder.Services.AddTransient<IShipFactory<NewTankerShipDto>, TankerShipFactory>();
builder.Services.AddTransient<IShipFactoryProvider, ShipFactoryProvider>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
