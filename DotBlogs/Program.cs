using DotBlogs.BusinessProviders;
using DotBlogs.BusinessProviders.Collections;
using DotBlogs.DataProviders;
using DotBlogs.DataProviders.Collections;
using DotBlogs.Helpers;
using DotBlogs.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile(ApplicationConstant.AppSettingsPath, optional: true);

IConfiguration configuration = configurationBuilder.Build();


// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ValidateModelAttribute>(); // Add your custom filter globally
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

builder.Services.AddTransient<ICategoryBusinessProvider, CategoryBusinessProvider>();

builder.Services.AddTransient<ICategoryDataProvider, CategoryDataProvider>();

builder.Services.AddTransient<ICommentBusinessProvider, CommentBusinessProvider>();

builder.Services.AddTransient<ICommentDataProvider, CommentDataProvider>();

builder.Services.AddTransient<IPostBusinessProvider, PostBusinessProvider>();

builder.Services.AddTransient<IPostDataProvider, PostDataProvider>();

builder.Services.AddDbContext<AuthenticationContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString(ApplicationConstant.DbContextConnectionStringSection))
);

builder.Services.AddDbContext<DotBlogsContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString(ApplicationConstant.DbContextConnectionStringSection))
);

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AuthenticationContext>();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();