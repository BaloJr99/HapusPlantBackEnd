using System.Text;
using AutoMapper;
using HapusPlant.Bussiness.Automapper;
using HapusPlant.Bussiness.Interfaces;
using HapusPlant.Bussiness.Services;
using HapusPlant.Bussiness.UnitOfWork.Interfaces;
using HapusPlant.Bussiness.UnitOfWork.Services;
using HapusPlant.Client.Middleware;
using HapusPlant.Common;
using HapusPlant.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonalProject.Business.Services;
using PersonalProject.Client.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));
var automapper = new MapperConfiguration(cfg => {
    cfg.AddProfile(new AutomapperProfile());
});

IMapper mapper = automapper.CreateMapper();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters(){
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUnitOfWorkHapus, UnitOfWorkHapus>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPersonalDatumService, PersonalDatumService>();
builder.Services.AddScoped<ISucculentFamilyService, SucculentFamilyService>();
builder.Services.AddScoped<ISucculentKindService, SucculentKindService>();
builder.Services.AddScoped<ISharedCollectionService, SharedCollectionService>();
builder.Services.AddDbContext<HapusplantContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("HapusPlantSql"));
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);
builder.Services.AddSession();

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
app.UseSession();  

app.UseMiddleware<MyAuthenticationMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
