using Proyecto_Web.Models;
using Proyecto_Web.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUsuarioModel, UsuarioModel>();
builder.Services.AddSingleton<IComunModel, ComunModel>();
builder.Services.AddScoped<IProductoModel, ProductoModel>();
builder.Services.AddDistributedMemoryCache();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",

    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
