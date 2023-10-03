using DatabaseConfigClassLibrary.DTO;
using DatabaseConfigClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CustomerDetailsManagementApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DatabaseConfigClassLibrary.DatabaseConfig;
using DatabaseConfigClassLibrary.DataManipulate;
using DatabaseConfigClassLibrary.Repositories;
using DatabaseConfigClassLibrary.RepositoryImpl;
using CustomerDetailsManagementApp.Services.ServiceInterfaces;

var builder = WebApplication.CreateBuilder(args);

//add the appsettings.json as configuration file
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<DataImporter>();
builder.Services.AddScoped<DataAccessService>();

//Add Identity framework
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(
        options => options.SignIn.RequireConfirmedAccount = false
    )
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Allow cross origin resource sharing for the frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

//validating the JWT token
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
            )
        };
    });

// API versioning configurations
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
});

// Register the repository implementations
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Defining services related to APIs with interfaces
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRequestService, LoginRequestService>();
builder.Services.AddScoped<IEditUserService, EditUserService>();
builder.Services.AddScoped<IGetDistanceService, GetDistanceService>();
builder.Services.AddScoped<ISearchUserService, SearchUserService>();
builder.Services.AddScoped<IGetCustomerListByZipCodeService, GetCustomerListByZipCodeService>();
builder.Services.AddScoped<IGetAllCustomerListService, GetAllCustomerListService>();

//Adding mappers
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<UserUpdateDTO, UserData>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    SeedData.Initialize(userManager, roleManager);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowLocalhost");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
