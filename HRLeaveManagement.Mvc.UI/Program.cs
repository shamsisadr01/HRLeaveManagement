using System.Reflection;
using HRLeaveManagement.Mvc.UI.Contracts;
using HRLeaveManagement.Mvc.UI.Infrastructure.Handlers;
using HRLeaveManagement.Mvc.UI.Middlewares;
using HRLeaveManagement.Mvc.UI.Services;
using HRLeaveManagement.Mvc.UI.Services.Base;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(configs =>
    {
        configs.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        configs.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        configs.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie( options =>
    {
        options.Cookie.Name = "AuthToken";          // نام کوکی
        options.LoginPath = "/Account/Login";       // مسیر Login
        options.LogoutPath = "/Account/Logout";
      //  options.ExpireTimeSpan = TimeSpan.FromHours(1); // زمان انقضا
      //  options.SlidingExpiration = true;           // تمدید خودکار
        options.Cookie.HttpOnly = true;             // جلوگیری از دسترسی JS
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // فقط HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict; // جلوگیری از CSRF
    });

builder.Services.AddTransient<AuthenticatedHttpClientHandler>();
builder.Services.AddHttpClient<IClient, Client>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:HRLeaveManagementAPI"]);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddAutoMapper(configs =>
{
},Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<TokenValidationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
