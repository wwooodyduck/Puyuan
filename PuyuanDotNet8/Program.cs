global using Microsoft.EntityFrameworkCore;
global using PuyuanDotNet8.Services;
global using PuyuanDotNet8.Data;
global using PuyuanDotNet8.Dtos;
global using PuyuanDotNet8.Helpers;

using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

var emailConfig = builder.Configuration.GetSection("EmailConfiguartion").Get<EmailConfigDto>();
if (emailConfig != null)
{
    builder.Services.AddSingleton(emailConfig);
}
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "PuYuan API"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Please enter a valid token",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                Reference= new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[]{ }
        }
    });
});
builder.Services.AddAuthentication().AddJwtBearer(option =>
{
    option.IncludeErrorDetails = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Secret:JwtSettings:Issuer"),

        ValidateAudience = false,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = false,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Secret:JwtSettings:SignKey")))

    };
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PasswordHelper>();
/*builder.Services.AddSingleton<JwtHelper>();
builder.Services.AddSingleton<EmailSenderHelper>();*/


builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<VerificationService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ForgetPasswordService>();
builder.Services.AddScoped<UsersetService>();

builder.Services.AddSingleton<EmailSenderHelper>();
builder.Services.AddSingleton<JwtHelper>();
builder.Services.AddSingleton<RandomCodeHelper>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
