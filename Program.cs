using API_Application.Application.DTOs;
using API_Application.Identity;
using API_Application.Infrastructure.AddressRepository;
using API_Application.Infrastructure.Context;
using API_Application.Infrastructure.EmployeeRepository;
using API_Application.Middleware;
using API_Application.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
 
var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<APIContext>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Manually configure AutoMapper
var mapperConfig = new MapperConfiguration(options =>
{
    options.AddMaps(typeof(Program).Assembly);

    options.CreateMap<Address, AddressDTO>();
    options.CreateMap<AddressDTO, Address>();

    options.CreateMap<AddressWithoutEmpUuidDTO, Address>();
    options.CreateMap<Address, AddressWithoutEmpUuidDTO>();

    options.CreateMap<EmployeeWithoutEmpUuidDTO, Employee>();
    options.CreateMap<Employee, EmployeeWithoutEmpUuidDTO>();

    options.CreateMap<Employee, EmployeeDTO>();
    options.CreateMap<EmployeeDTO, Employee>();
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddSingleton<ExceptionMiddleware>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => 
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireClaim("ClaimType", ClaimType.Admin.ToString());
    });

    options.AddPolicy("ManagerPolicy", policy =>
    {
        policy.RequireClaim("ClaimType", ClaimType.Manager.ToString());
    });

    options.AddPolicy("CombinedPolicy", policy =>
    {
        policy.RequireAssertion(context =>
        {
            return context.User.HasClaim(c =>
                (c.Type == "ClaimType" && (c.Value == ClaimType.Admin.ToString() || c.Value == ClaimType.Manager.ToString())));
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please provide a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
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
                    Array.Empty<string>()
                }
            });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
