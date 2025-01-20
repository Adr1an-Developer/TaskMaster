using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System.Reflection;
using TaskMaster.AutofacModule;
using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Domain.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((container) =>
    {
        container.RegisterModule<RepositoryModule>();
        container.RegisterModule<ServiceModule>();
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetSection("ConnectionStrings")["MyConn"];

builder.Services.AddEntityFrameworkMySQL()
        .AddDbContext<IEfDbContext, DatabaseContext>(item => item.UseMySQL(connectionString,
                                b =>
                                {
                                    b.MigrationsAssembly("TaskMaster");
                                    b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), new List<int>());
                                }
                                )
        );

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();