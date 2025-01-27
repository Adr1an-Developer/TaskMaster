using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskMaster.AutofacModule;
using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Domain.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

//builder.AddServiceDefaults();

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

Console.WriteLine(connectionString);

builder.Services
        .AddDbContext<IEfDbContextBase, DatabaseContext>(item => item.UseNpgsql(connectionString,
                                b =>
                                {
                                    b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), new List<string>());
                                }
                                ).EnableDetailedErrors(true)
                                .LogTo(Console.WriteLine, LogLevel.Information)
        );

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

//app.MapDefaultEndpoints();

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