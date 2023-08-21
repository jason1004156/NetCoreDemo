using System.Collections.Generic;
using Demo.Service;
using Demo.Service.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISecurtiesService, SecurtiesService>();
builder.Services.AddTransient<DemoContext>();
var app = builder.Build();

var collection = new ServiceCollection();

//collection.Scan(scan => scan
//.FromAssemblyOf<IBaseService>()
//        .AddClasses(classes => classes.InNamespaces("Demo.Service.Base"))        
//            .AsImplementedInterfaces()
//            .WithTransientLifetime()
//    .FromAssemblyOf<IBaseService>()
//        .AddClasses(classes => classes.AssignableTo<IBaseService>())
//        .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
//            .AsImplementedInterfaces()
//            .WithTransientLifetime()
// );

//foreach (var item in collection)
//{
//    Console.WriteLine($"{item.Lifetime},{item.ImplementationType},{item.ServiceType}");
//}

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

