using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using Infrastructore.IdentityConfig;
using Api.EndPoint.MappingProfiles;
using Application.Interfaces.Contexts;
using Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.MappingProfile;
using Aplication.Services.Products.ProductItem.Commands.Create;
using Application.Services.Products.Favourites.Queries;
using Application.Services.UriComposer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiVersioning(opitons =>
{
    opitons.AssumeDefaultVersionWhenUnspecified = true;
    opitons.DefaultApiVersion = new ApiVersion(1, 0);
    opitons.ReportApiVersions = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["JWtConfig:issuer"],
        ValidAudience = builder.Configuration["JWtConfig:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWtConfig:Key"])),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
    };
    configureOptions.SaveToken = true; // HttpContext.GetTokenAsunc();
    configureOptions.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            return Task.CompletedTask;
        }
    };
});

#region Connection String
var ContectionString = builder.Configuration["ConnectionString:SqlServer"];
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<IIdentityDataBaseContext, IdentityDataBaseContext>();
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(ContectionString, sqlServerOptionsAction: sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
}));
builder.Services.AddIdentityService(builder.Configuration);
#endregion

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductItemCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetFavouriteForUserRequest).Assembly));

builder.Services.AddTransient<IUriComposerService, UriComposerService>();

builder.Services.AddAutoMapper(typeof(UserEndPointMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProductEndPointMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {

    var security = new OpenApiSecurityScheme
    {
        Name = "JWT Auth",
        Description = "Enter your token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(security.Reference.Id, security);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { security, new string[] { } }
        });

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Cache.xml"), true);

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api SourceStore.ir ", Version = "v1" });

    options.DocInclusionPredicate((doc, apiDescription) =>
    {
        if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

        var version = methodInfo.DeclaringType
            .GetCustomAttributes<ApiVersionAttribute>(true)
            .SelectMany(attr => attr.Versions);

        return version.Any(v => $"v{v.ToString()}" == doc);
    });
});
// builder.Services.AddOutputCache(options => 
// {
//     options.AddBasePolicy(builder => 
//     {
//         builder.Cache();
//     });
// });

builder.Services.AddMemoryCache();

WebApplication app = builder.Build();

// app.UseOutputCache();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
