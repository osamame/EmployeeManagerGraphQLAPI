using AspNetCoreRateLimit;
using EmployeeManager.Api.GraphQLServices;
using EmployeeManager.Core.Configuration;
using EmployeeManager.Data.Data;
using HotelManager.Api.ServiceExtention;
using Microsoft.EntityFrameworkCore;
using OcelotAPIGatewayJWTAuthenticationManager.JwtExtension;
using OcelotAPIGatewayJWTAuthenticationManager.JwtHandler;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();








//Serilog
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


//DatabaseContext Injection
builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});


//AutoMapper
builder.Services.AddAutoMapper(typeof(MapperInitializer));


//Register Interfaces and Classes
builder.Services.ConfigureInterfacesAndClasses();


//Add MicroSoft Newton Soft To Ignore Reference Loop Handling And API Caching
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


//JWT Implementation
//builder.Services.ConfigurationJWT(builder.Configuration);
builder.Services.ConfigurationSwaggerDoc();
builder.Services.AddSingleton<JwtTokenHandler>();
builder.Services.ConfigureJwtAuthentication();


//API Versioning
//builder.Services.ConfigureAPIVersioning();

//API HealthCheck


//API Chaching
builder.Services.ConfigureHTTPCacheHeaders();


//API Memory Cache
builder.Services.AddMemoryCache();


//API Rate Limiting
builder.Services.ConfigureRateLimiting();
builder.Services.AddHttpContextAccessor();


//Register the GraphQL Server
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
        options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Employee Manager API");
    });
}

app.UseHttpsRedirection();


//JWT Implementation
app.UseAuthentication();
app.UseAuthorization();


//Implement Global Exception Handler
app.ConfigureExceptionHandler();


//API HealthCheck



//API Chaching
app.UseResponseCaching();
app.UseHttpCacheHeaders();


//API Rate Limiting
app.UseIpRateLimiting();


//Map GraphQL Endpoints
app.MapGraphQL();


app.MapControllers();

app.Run();
