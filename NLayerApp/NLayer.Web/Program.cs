using Autofac.Extensions.DependencyInjection;
using Autofac;
using NLayer.Web.Modules;
using Microsoft.EntityFrameworkCore;
using NLayer.Repository.Contexts;
using System.Reflection;
using NLayer.Service.Mapping;
using FluentValidation.AspNetCore;
using NLayer.Service.Validations;
using NLayer.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myHome"), options =>
{
    // NOT : Connection string'i aldigimiz yerden sonra yaptıgimiz islem ise migrations klasorunun nerede olusmasini istedigimize dair bir islemdir.

    options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

    // NOT : Burada tip güvensiz olarak dogrudan katmanin ismini tirnaklar icerisinde yazabilirdik ancak daha sonraki bir zamanda dosya isminde degisiklik oldugu zaman hata verecektir. Bu sebeble bize bir assembly ver bu assembly appdbcontex'in oldugu assembly olsun ve onun ismini al dedik.
}));

builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddAutoMapper(typeof(MapProfile));


//AutoFac kutuphanesinin implemanetasyonu.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //Service sağlayıcısı olarak AutoFac'i kullandığımı belirtiyorum.

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule())); //AutoFac kütüphanesi ile yazdığım modulu sisteme ekliyorum.

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
