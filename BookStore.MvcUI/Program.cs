using BookStore.Common.Secrets;
using BookStore.Data;
using BookStore.MvcUI.Extensions;
using BookStore.Services.Implementations;
using BookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ApplicationSecrets>(c => builder.Configuration.GetSection(nameof(ApplicationSecrets)).Bind(c));

string connectionString = builder.Configuration.GetSection("ApplicationSecrets:ConnectionStrings:ApplicationConnectionString").Value;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IPublicationServices, PublicationServices>();
builder.Services.AddScoped<IBookServices, BookServices>();

var app = builder.Build();

await app.InitialDatabase();

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

app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=AdminHome}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
