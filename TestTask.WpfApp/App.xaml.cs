using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using System.Configuration;
using System.Data;
using System.Windows;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Services;
using TestTask.Application.Services;
using TestTask.Persistence;
using TestTask.Persistence.Configurations;
using TestTask.Persistence.Repositories;
using TestTask.WpfApp.ViewModels;
using TestTask.WpfApp.ViewModels.Edit;
using Order = TeskTask.Core.Models.Order;

namespace TestTask.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(
                    ServiceProvider.GetRequiredService<EmployeeViewModel>(),
                    ServiceProvider.GetRequiredService<ContractorViewModel>(),
                    ServiceProvider.GetRequiredService<OrderViewModel>())
            };
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // DI
            services.AddSingleton<ISessionFactory>(provider => NHibernateHelper.SessionFactory);
            services.AddSingleton<IMapper>(provider =>
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<PersistenceProfile>());
                return config.CreateMapper();
            });

            // Repositories
            services.AddScoped<IRepository<Employee>, EmployeeRepository>();
            services.AddScoped<IRepository<Contractor>, ContractorRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();

            // Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IContractorService, ContractorService>();
            services.AddScoped<IOrderService, OrderService>();

            // ViewModels
            services.AddTransient<EmployeeViewModel>();
            services.AddTransient<ContractorViewModel>();
            services.AddTransient<OrderViewModel>();

            services.AddTransient<EmployeeEditViewModel>();
            services.AddTransient<ContractorEditViewModel>();
            services.AddTransient<OrderEditViewModel>();
        }
    }

}
