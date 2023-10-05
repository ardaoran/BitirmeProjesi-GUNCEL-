


using BitirmeProjesi.BL.Repositories;
using BitirmeProjesi.DAL.Content;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BitirmeProjesi.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SQLContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));

builder.Services.AddIdentity<AppUser,AppRole>(x=>
{
    x.Password.RequireUppercase = false;
    x.Password.RequireNonAlphanumeric = false;
}
    
    )
    .AddEntityFrameworkStores<SQLContext>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the timeout as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt=>
{
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    opt.LoginPath = "/admin/login"; //yetkisi olmadan giri� yapmaya �al��t���nda
    opt.LogoutPath = "/admin/logout"; // s�re doldu�unda gidece�i sayfa

}
);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();
if (!app.Environment.IsDevelopment()) app.UseStatusCodePagesWithRedirects("/hata/{0}");
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); //kimlik do�rulama
app.UseAuthorization();  //yetkilendirme

app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=User}/{action=Index}/{id?}");
app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSession();


app.Run();
