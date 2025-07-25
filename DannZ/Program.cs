using DannZ.Context;
using DannZ.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using DannZ.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using DannZ.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add the String connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

//Set Up Cloudinary As a Service
var cloudinaryConfig = builder.Configuration.GetSection("Cloudinary"); //All the data needed 

//vinculate with the cloudinary Account
var cloudinary = new Cloudinary(new Account(
    cloudinaryConfig["CloudName"],
    cloudinaryConfig["ApiKey"],
    cloudinaryConfig["ApiSecret"]
    ));

//We added a single instance of cloudinary since opens the app until the end
builder.Services.AddSingleton(cloudinary);


//Cookies
var cookieName = builder.Configuration["CookieName"];

builder.Services.AddAuthentication().AddCookie(cookieName!, options =>
{
    options.LoginPath = "/Auth/Login";

    options.AccessDeniedPath = "/Auth/Forbidden";

    options.Cookie.Name = cookieName;

});

//Necesario para obtener los datos de la url
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


//Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("permission", "IsAdmin"));
    options.AddPolicy("OwnsProfile", policy => policy.Requirements.Add(new OwnsResourceRequirement("User")));
});

//para que .net haga inyección de dependencias automaticamente
builder.Services.AddScoped<IAuthorizationHandler, OwnsResourceHandler>();
builder.Services.AddScoped<IUploadProfileImageService, UploadProfileImageService>();
builder.Services.AddScoped<IGetUserClaimsService, GetUserClaimsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();

//Usar autenticación y autorización - Middlewares
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
