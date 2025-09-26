using Librebooks.Areas;
using Librebooks.Areas.Identity.Services;
using Librebooks.Core.EFCore;
using Librebooks.Data;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Providers;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Config = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(Config.GetConnectionString("MoskitContext")));
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Config.GetConnectionString("MoskitContext")));
builder.Services.AddDistributedMemoryCache();

builder.Services.AddIdentityCore<User>
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
    .AddRoles<Role>()
    .AddUserManager<UserManagerExtension>()
    .AddSignInManager<SignInManagerExtension>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddErrorDescriber<IdentityErrorDescriberExtension>()
    .AddDefaultTokenProviders();

JwtBearerProvider.Configure(builder);
builder.Services.AddSingleton<IdentityErrorDescriberExtension>();
AreaServices.ConfigureAll(builder.Services);
builder.Services.AddSingleton<AppErrorDescriber>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevOrigin", options =>
    {
        _ = options.WithOrigins("http://localhost:51570", "https://localhost:5262")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.Configure<JsonOptions>(opts =>
    opts.SerializerOptions.IncludeFields = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<VerificationStore>();
builder.Services.AddScoped<IVerificationManager, VerificationManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("DevOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
