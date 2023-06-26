using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Contexts;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myHome"),options=>
{
    // NOT : Connection string'i aldigimiz yerden sonra yaptıgimiz islem ise migrations klasorunun nerede olusmasini istedigimize dair bir islemdir.

    options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); 
    
    // NOT : Burada tip güvensiz olarak dogrudan katmanin ismini tirnaklar icerisinde yazabilirdik ancak daha sonraki bir zamanda dosya isminde degisiklik oldugu zaman hata verecektir. Bu sebeble bize bir assembly ver bu assembly appdbcontex'in oldugu assembly olsun ve onun ismini al dedik.
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
