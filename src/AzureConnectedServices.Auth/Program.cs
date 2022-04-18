using System.Reflection;
using System.Text;
using AzureConnectedServices.Auth;
using AzureConnectedServices.Auth.Services;
using AzureConnectedServices.Auth.Services.Interfaces;
using AzureConnectedServices.Core.HealthChecks;
using AzureConnectedServices.Core.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

SetupLogger(builder);
RegisterServices(builder);

var app = builder.Build();

SetupMiddleware(app);
SetupApi(app);

app.Run();

static void SetupLogger(WebApplicationBuilder bldr)
{
    bldr.Host.UseSerilog(Logging.ConfigureLogger);
}

static void RegisterServices(WebApplicationBuilder bldr)
{
    var connectionString = bldr.Configuration.GetConnectionString("DefaultConnection");
    bldr.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    bldr.Services.AddControllers();
    bldr.Services.AddEndpointsApiExplorer();
    bldr.Services.AddSwaggerGen(s =>
    {
        var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
        s.IncludeXmlComments(xmlFilePath);
        s.ExampleFilters();
        s.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
        s.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme (JWT). Example: \"bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    });
    bldr.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

    // Health Checks
    bldr.Services.AddHealthChecks()
        .AddCheck<VersionHealthCheck>("version");

    bldr.Services
        .AddHealthChecksUI()
        .AddInMemoryStorage();

    bldr.Services.AddAuthentication("Bearer")
        .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = bldr.Configuration["Authentication:Issuer"],
                    ValidAudience = bldr.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(bldr.Configuration["Authentication:SecretForKey"]))
                };
            }
        );

    bldr.Services.AddAuthorization(options =>
    {
        //options.AddPolicy("MustBeFromAntwerp", policy =>
        //{
        //    policy.RequireAuthenticatedUser();
        //    policy.RequireClaim("city", "Antwerp");
        //});
    });

    // Services
    bldr.Services.AddTransient<IUserValidationService, UserValidationService>();
}

static void SetupMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();
}

static void SetupApi(WebApplication app)
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("healthz", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    });
}
