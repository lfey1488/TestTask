using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TestTask.Application.Interfaces.Services;
using TestTask.Application.Services;
using TestTask.Persistence.Repositories;
using TestTask.WPF.ViewModels;

namespace TestTask
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Регистрация сервисов и ViewModels
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            // Пример запуска главного окна с DI
            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<EmployeeViewModel>()
            };
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Регистрация сервисов, репозиториев, NHibernate, AutoMapper и т.д.
            // services.AddSingleton<ISessionFactory>(...);
            // services.AddSingleton<IMapper>(...);

            // Repositories
            services.AddScoped<EmployeeRepository, EmployeeRepository>();
            services.AddScoped<IContractorRepository, ContractorRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IContractorService, ContractorService>();
            services.AddScoped<IOrderService, OrderService>();

            // ViewModels
            services.AddTransient<EmployeeViewModel>();
            services.AddTransient<ContractorViewModel>();
            services.AddTransient<OrderViewModel>();
        }
    }
}