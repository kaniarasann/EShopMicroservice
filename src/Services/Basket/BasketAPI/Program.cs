

using DiscountGRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

TypeAdapterConfig<SaveBasketRequest, SaveBasketCommand>.NewConfig()
    .Map(dest => dest.cart, src => src.cart);

TypeAdapterConfig<GetBaksetResult,GetBasketResponse>.NewConfig()
    .Map(dest => dest.cart, src => src.cart);

TypeAdapterConfig<SaveBasketResult, SaveBasketResponse>.NewConfig()
    .Map(dest => dest.username, src => src.username);

builder.Services.AddLogging();
builder.Services.AddCarter();
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMarten(opts => {
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x=>x.UserName); /*Setting Identity field for a callss*/
}).UseLightweightSessions();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redisConn");
    options.InstanceName = "basketCache";
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository,CacheBasketRepository>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("redisConn")!);

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opts => {
    opts.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(opt => { });
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
