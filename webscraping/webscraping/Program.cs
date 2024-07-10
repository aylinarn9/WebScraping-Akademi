

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;
using webscraping;

using webscraping.Model;
using webscraping.Models;
using webscraping.Services;


//string connectionString = "mongodb://localhost:27017";
//string databaseName = "yazlab_db";
//string collectionName = "bilgi";



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add services to the container.
builder.Services.Configure<DatabaseSetting>(
                builder.Configuration.GetSection(nameof(DatabaseSetting)));

builder.Services.AddSingleton<IDatabaseSetting>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSetting>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("DatabaseSetting:ConnectionString")));

builder.Services.AddScoped<IBilgiModel, BilgiModelService>();


builder.Services.AddControllersWithViews();

builder.Services.AddSession();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
