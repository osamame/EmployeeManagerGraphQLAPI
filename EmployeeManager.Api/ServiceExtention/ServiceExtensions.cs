using AspNetCoreRateLimit;
using EmployeeManager.Api.GraphQLServices;
using EmployeeManager.Core.Dto;
using EmployeeManager.Core.IRepository;
using EmployeeManager.Core.Repository;
using EmployeeManager.Core.Service.Implementaion;
using EmployeeManager.Core.Service.Interface;
using Ganss.Xss;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace HotelManager.Api.ServiceExtention
{
    public static class ServiceExtensions
    {
        // Implement JWT
        public static void ConfigurationJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWT");
            var key = jwtSettings.GetSection("Key").Value;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        ValidateIssuer = false,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateIssuerSigningKey = true,

                        ValidAudience = jwtSettings.GetSection("Audience").Value,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        }

        // Implement Swagger Doc And Oauth2 (Bearer) for JWT
        public static void ConfigurationSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Beare Scheme.
            Enter 'Bearer' [space] and then your token in the text input below.
            Example: 'Bearer 1234567890abcdef",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement(){
            {
                new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
            }
        });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HotelManager",
                    Version = "v1"
                });
            });
        }

        //Global Exception Handler
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Something went wrong in the {contextFeature.Error}");

                        await context.Response.WriteAsync(new ResponseDto
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error Please Try Again Later."
                        }.ToString());
                    }
                });
            });
        }


        //Configure Global API Versioning to get {api-version} parameter
        public static void ConfigureAPIVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }


        //Implement Global Caching to Update with New Data from the DB
        public static void ConfigureHTTPCacheHeaders(this IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddHttpCacheHeaders(
                (expirationOpt) =>
                {
                    expirationOpt.MaxAge = 65;
                    expirationOpt.CacheLocation = CacheLocation.Private;
                },
                (validationOpt) =>
                {
                    validationOpt.MustRevalidate = true;
                }
          );
        }


        //Implement Global RateLimting
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 100,
                    Period = "50s" // or "10m"
                }
            };

            services.Configure<IpRateLimitOptions>(options =>
            {
                options.GeneralRules = rateLimitRules;
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }


        //Register Interfaces and Classes
        public static void ConfigureInterfacesAndClasses(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISalaryService, SalaryService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<HtmlSanitizer>();

            //Add GraphQl Service to Scope
            services.AddScoped<Query>();
            services.AddScoped<Mutation>();
        }
    }
}
