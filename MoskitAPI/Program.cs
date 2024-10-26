using System.Text;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Moskit.Data;
using Moskit.Extensions.Identity;
using Moskit.Models.Entity.IdentitySpace;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(Configuration.GetConnectionString("MoskitContext"));
});

builder.Services.AddIdentity<User, Role>
    (options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 6;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.SignIn.RequireConfirmedEmail = true;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddUserManager<UserManagerExt>()
    .AddSignInManager<SignInManagerExt>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddErrorDescriber<IdentityErrorDescriberExt>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(
    options =>
    {
        //options.Cookie.Name = "moskit.session.cookie";
        //options.ExpireTimeSpan = TimeSpan.FromMinutes(24);
        //options.Cookie.IsEssential = true;
        //options.Cookie.HttpOnly = true;
        //options.Cookie.SameSite = SameSiteMode.None;
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = (context) =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = (context) =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            }
        };
    }
);

builder.Services.Configure<JwtTokenValidationParameters>(Configuration.GetSection("JwtTokenValidationParameters"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = Configuration[JwtTokenValidationParameters.GetConfigurationKeyNames().Issuer],
        ValidAudience = Configuration[JwtTokenValidationParameters.GetConfigurationKeyNames().Issuer],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Configuration[JwtTokenValidationParameters.GetConfigurationKeyNames().SecretKey]!)
        )
    };
}).AddBearerToken();

builder.Services.AddSingleton<IdentityErrorDescriberExt>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options =>
    {
        options.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

    options.AddPolicy("DevOrigin",
        options =>
        {
            options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevOrigin");
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.MapFallbackToFile("/index.html");
app.Run();
