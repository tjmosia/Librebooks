using LibrebooksRazor.Data;
using LibrebooksRazor.Extensions.Identity;
using LibrebooksRazor.Models.Entity.IdentitySpace;
using LibrebooksRazor.Providers.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(config.GetConnectionString("LibrebooksSchema")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IdentityErrorDescriberExtension>();
builder.Services.AddIdentity<User, Role>
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

builder.Services.AddSession(options =>
{
	options.Cookie.Name = "librebooks_session";
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<MailSender>();
builder.Services.AddServerSideBlazor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthorization();
app.UseSession();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapBlazorHub();
app.Run();
