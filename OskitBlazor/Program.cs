using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

using OskitBlazor.Components;
using OskitBlazor.Components.Identity;
using OskitBlazor.Components.Identity.Pages;
using OskitBlazor.Data;
using OskitBlazor.Extensions.Identity;
using OskitBlazor.Models.Entity.IdentitySpace;
using OskitBlazor.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddLogging(options => options.AddConsole());
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies(options =>
{
    options.ApplicationCookie!.Configure(p =>
    {
        p.LoginPath = new PathString("/" + IdentityURIs.Login);
        p.LogoutPath = new PathString("/" + IdentityURIs.Logout);
        p.ReturnUrlParameter = "returnUrl";
        p.AccessDeniedPath = new PathString("/" + IdentityURIs.AccessDenied);
    });
});

var connectionString = builder.Configuration
    .GetConnectionString("MoskitContext") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddUserManager<UserManagerExt>()
    .AddSignInManager<SignInManagerExt>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();

builder.Services.AddFluentUIComponents(options =>
{
    options.ValidateClassNames = false;
});

/****************************************************************************************************************
 ** DB_PROVIDERS
 ***************************************************************************************************************/
builder.Services.AddSingleton<IdentityErrorDescriberExt>();
AppProviders.ConfigureServices(builder.Services);


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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
