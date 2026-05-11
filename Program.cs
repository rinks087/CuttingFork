using CSFTWebAPI.Services;
//using Duende.IdentityServer;
//using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Load configuration based on the current environment
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Get IdentityServer client settings from configuration
//var identityServerSettings = builder.Configuration.GetSection("IdentityServer:Clients:MyClient");

//string clientId = identityServerSettings["ClientId"];
//string clientSecret = identityServerSettings["ClientSecret"];


// Add services to the container.
builder.Services.AddControllers();

// Configure IdentityServer
//builder.Services.AddIdentityServer(options =>
//{
//    options.IssuerUri = "https://10.0.2.2:5001";
//})
//.AddInMemoryApiScopes(new List<ApiScope>
//{
//    new ApiScope("api1", "RFID API") // Define your API scope here
//})
//.AddInMemoryClients(new List<Client>
//{
//    new Client
//    {
//        ClientId = clientId, // Use client ID from appsettings.json
//        AllowedGrantTypes = GrantTypes.ClientCredentials,
//        ClientSecrets =
//        {
//            new Secret(clientSecret.Sha256()) // Use client secret from appsettings.json
//        },
//        AllowedScopes = { "api1" } // Define scopes allowed for this client
//    }
//})
//.AddInMemoryApiResources(new List<ApiResource>
//{
//    new ApiResource("api1", "RFID API")
//    {
//        Scopes = { "api1" },
//        ApiSecrets = { new Secret("secret".Sha256()) }
//    }
//});

// Configure JWT Bearer Authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "https://10.0.2.2:5001"; // URL of your IdentityServer
//        options.Audience = "api1"; // Must match the audience defined in IdentityServer
//        options.RequireHttpsMetadata = false; // For development only, set to true in production
//    });

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);

    //options.ListenAnyIP(5001, ListenOptions => ListenOptions.UseHttps());
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEmulator",
        builder => builder
            .WithOrigins("http://10.0.2.2") // Change this to your frontend's origin
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddAuthorization();

// Add other services
builder.Services.AddSingleton<SQLQueryProvider>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddScoped<DataService>(sp =>
//{

//    var queryProvider = sp.GetRequiredService<SQLQueryProvider>();
//    var logger = sp.GetRequiredService<ILogger<DataService>>();
//    return new DataService(connectionString, logger, queryProvider);
//});

builder.Services.AddScoped<DataService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var queryProvider = sp.GetRequiredService<SQLQueryProvider>();
    var logger = sp.GetRequiredService<ILogger<DataService>>();

    return new DataService(logger,configuration);
});


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerInputFormatter(options));
    options.OutputFormatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());
})
.AddXmlSerializerFormatters();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RFID API", Version = "v1" });
});

//c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "oauth2"
//                }
//            },
//            new[] { "api1" }
//        }
//    });

//    c.OperationFilter<AuthorizeCheckOperationFilter>();
//});



// Build the application
var app = builder.Build();


app.UseRouting();
app.UseAuthentication(); // Make sure authentication middleware is added
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Use IdentityServer middleware
//app.UseIdentityServer();

app.UseCors("AllowAll");

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");


    });
}

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();

// AuthorizeCheckOperationFilter for Swagger
//public class AuthorizeCheckOperationFilter : IOperationFilter
//{
//    public void Apply(OpenApiOperation operation, OperationFilterContext context)
//    {
//        if (operation.Security == null)
//            operation.Security = new List<OpenApiSecurityRequirement>();

//        var oauth2Requirement = new OpenApiSecurityRequirement
//        {
//            {
//                new OpenApiSecurityScheme
//                {
//                    Reference = new OpenApiReference
//                    {
//                        Type = ReferenceType.SecurityScheme,
//                        Id = "oauth2"
//                    }
//                },
//                new[] { "api1" }
//            }
//        };

//        operation.Security.Add(oauth2Requirement);
//    }
//}
