using Microsoft.AspNetCore.Authentication.Cookies;

using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(600);
    opt.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.Cookie.Name = "MySessionCookie";
                 options.LoginPath = new PathString("/Account/Login");
                 options.SlidingExpiration = true;
                 options.Cookie.HttpOnly = true;
                 options.AccessDeniedPath = new PathString("/Account/Forbidden");
                 options.ExpireTimeSpan = TimeSpan.FromDays(50);
                 options.Cookie.MaxAge = TimeSpan.FromDays(50);
                 options.SlidingExpiration = true;
                 options.LogoutPath = new PathString("/Account/Logout");
                 options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;//This cookie cannot be used as a third-party cookie under any circumstances, without exception. For example, suppose b.com sets the following cookies:
             });

builder.Services.Configure<CookiePolicyOptions>(opt =>
{

    opt.MinimumSameSitePolicy = SameSiteMode.Strict;
    opt.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
    opt.Secure = CookieSecurePolicy.None;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
