using Microsoft.EntityFrameworkCore;
using mvc.Controllers;
using mvc.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")
));


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

app.UseAuthorization();

app.MapGet("/rawsql",(ApplicationDbContext _db)=>{
   
var result = _db.Posts.FromSqlRaw("SELECT * FROM dbo.Posts ORDER BY Id DESC").ToList();
var d = _db.Comments.FromSqlRaw($"SELECT * FROM dbo.Comments WHERE PostId = {1080}").ToList();
return d;
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
