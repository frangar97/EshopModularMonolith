using Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services
    .AddBasketModule(builder.Configuration)
    .AddCatalogModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services
    .AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app
    .MapCarter();

app.UseExceptionHandler(options => { });

app
    .UseBasketModule()
    .UseCatalogModule()
    .UseOrderingModule();

app.Run();
