using LibrebooksBlazor.Components;
using LibrebooksRazor.Data;
using LibrebooksRazor.Extensions.Identity;
using LibrebooksRazor.Models.Entity.IdentitySpace;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var config = builder.Configuration;
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(config.GetConnectionString("LibrebooksSchema")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IdentityErrorDescriberExtension>();
builder.Services.AddIdentity<User, Role>
	(options =>
	{
		options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
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
	.AddErrorDescriber<IdentityErrorDescriberExtension>()
	.AddUserManager<UserManagerExtension>()
	.AddSignInManager<SignInManagerExtension>()
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/auth";
	options.LogoutPath = "/auth/logout";
	options.ReturnUrlParameter = "returnUrl";
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
	options.Cookie.SameSite = SameSiteMode.Strict;
});

//builder.Services.AddSession(options =>
//{
//	options.Cookie.Name = "librebooks_session";
//	options.Cookie.HttpOnly = true;
//	options.Cookie.IsEssential = true;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
