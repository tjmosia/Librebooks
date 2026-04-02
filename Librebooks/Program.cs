using Librebooks.Areas;
using Librebooks.Areas.Identity.Services;
using Librebooks.Core.EFCore;
using Librebooks.Data;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Providers;
using Librebooks.Providers.Managers;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Config = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(Config.GetConnectionString("LibrebooksSchema")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddIdentityCore<User>
	(options =>
	{
		options.Password.RequireDigit = true;
		options.Password.RequiredLength = 8;
		options.Password.RequiredUniqueChars = 6;
		options.Password.RequireLowercase = true;
		options.Password.RequireUppercase = true;
		options.Password.RequireNonAlphanumeric = false;
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
builder.Services.AddSingleton<AppErrorDescriber>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
	options.MinimumSameSitePolicy = SameSiteMode.None;
	//options.HttpOnly = HttpOnlyPolicy.Always;
	//options.Secure = CookieSecurePolicy.Always;
	options.CheckConsentNeeded = context => true;
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("DevOrigin", options =>
	{
		options.WithOrigins("https://localhost:5173")
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
AreaServices.ConfigureAll(builder.Services);
builder.Services.AddScoped<VerificationStore>();
builder.Services.AddScoped<IVerificationManager, VerificationManager>();
builder.Services.AddSingleton<MailSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) { }

app.UseCookiePolicy();
app.UseCors("DevOrigin");
app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
	var token = context.Request.Cookies[JwtTokenKeys.AccessToken];

	if (token != null)
	{
		context.Response.Headers.Authorization = $"Bearer {token}";
		Console.Write(token);
	}

	await next.Invoke(context);
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
