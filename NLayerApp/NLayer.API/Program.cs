using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Contexts;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWork;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options=> 
{ 
    options.Filters.Add(new ValidateFilterAttribute()); //Tüm controllerlara global bir yerden validator ekleme işlemi.
}).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>()); //FluentValidation sisteme dahil edildi. Assemmbly'sini sisteme kayıt ediyoruz.

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myHome"),options=>
{
    // NOT : Connection string'i aldigimiz yerden sonra yaptıgimiz islem ise migrations klasorunun nerede olusmasini istedigimize dair bir islemdir.

    options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); 
    
    // NOT : Burada tip güvensiz olarak dogrudan katmanin ismini tirnaklar icerisinde yazabilirdik ancak daha sonraki bir zamanda dosya isminde degisiklik oldugu zaman hata verecektir. Bu sebeble bize bir assembly ver bu assembly appdbcontex'in oldugu assembly olsun ve onun ismini al dedik.
}));


//AutoFac kutuphanesinin implemanetasyonu.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //Service sağlayıcısı olarak AutoFac'i kullandığımı belirtiyorum.

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule())); //AutoFac kütüphanesi ile yazdığım modulu sisteme ekliyorum.



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
