using Business.Factories;
using Business.Service;
using Data.Contexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PresentationConsoleApp;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Dator\\source\\repos\\DataStorege_Assignmet\\Data\\Databases\\zara.mdf;Integrated Security=True;Connect Timeout=30"))
    .AddScoped<CustomerRepository>()
    .AddScoped<ProjectRepository>()
    //.AddScoped<ProductRepository>()
    //.AddScoped<StatusTypeRepository>()
    //.AddScoped<UserRepository>()
    .AddScoped<CustomerService>()
    .AddScoped<ProjectService>()
    .AddScoped<MenuDialogs>()

  
    .BuildServiceProvider();

var menuDialogs = services.GetRequiredService<MenuDialogs>();
await menuDialogs.MenuOptions();
