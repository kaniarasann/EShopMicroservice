using CatalogAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddCarter();
builder.Services.AddMediatR((config) => {
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMarten(opts => { 
    opts.Connection(builder.Configuration.GetConnectionString("Database")!); 
}).UseLightweightSessions();

/*To check if is development then initial data*/
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapCarter();

//Say we rely on the custom comfigured exception
app.UseExceptionHandler(opt => { });

app.MapHealthChecks("/health");

app.Run();
