using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Hosting;
using System.Windows;
using WOCS.Application.Interfaces.Repositories;
using WOCS.Application.Interfaces.Services;
using WOCS.Application.Services;
using WOCS.Domain.Entities;
using WOCS.Infrastructure.Data;
using WOCS.Infrastructure.Repositories;
using WOCS.UI.Navigation;
using WOCS.UI.ViewModels;
using WOCS.UI.Views;

namespace WOCS.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private IHost _host;
        private ILogger<App> _logger;
        public static IServiceProvider Services { get; private set; } = null!;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                       .ConfigureAppConfiguration((context, config) =>
                       {
                           // ensure appsettings.json is loaded
                           config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                       })
                       .ConfigureServices((context, services) => RegisterServices(services, context.Configuration))
                        .ConfigureLogging((context, logging) =>
                        {
                            logging.ClearProviders();
                        })
                       .UseNLog()
                       .Build();

            Services = _host.Services;
            _logger = _host.Services.GetRequiredService<ILogger<App>>();
        }

        private void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // EF DbContext (Infrastructure project contains WocsContext)
            services.AddDbContext<WocsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WOCSContext")));

            // Repositories
            services.AddScoped<IExceptionLogRepository, ExceptionLogRepository>();
            services.AddScoped<IExproJobRepository, ExproJobRepository>();

            // Services
            services.AddScoped<IExceptionLogService, ExceptionLogService>();
            services.AddScoped<IExproJobService, ExproJobService>();

            // ViewModels
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HeaderViewModel>();
            services.AddSingleton<FooterViewModel>();

            // Views
            services.AddSingleton<DashboardView>();
            services.AddSingleton<ShellWindow>();
            services.AddSingleton<HeaderView>();
            services.AddSingleton<FooterView>();

            // Navigation
            services.AddSingleton<INavigationService, NavigationService>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            // ✅ GLOBAL UI EXCEPTION HANDLER
            DispatcherUnhandledException += async (s, args) =>
            {
                // ✅ Mark as handled IMMEDIATELY — prevents app crash
                args.Handled = true;

                // ✅ Show MessageBox FIRST — before any async work
                MessageBox.Show(
                    "An unexpected error occurred. Please contact support.",
                    "Application Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                // ✅ Log AFTER — failure here won't affect the MessageBox
                try
                {
                    var logger = Services.GetRequiredService<IExceptionLogService>();
                    await logger.LogAsync(
                        args.Exception,
                        layer: "UI",
                        context: "DispatcherUnhandledException"
                    );
                }
                catch (Exception logEx)
                {
                    _logger.LogError(logEx, "Failed to log exception to database");
                }
            };

            // ✅ OPTIONAL: non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += async (s, args) =>
            {
                if (args.ExceptionObject is Exception ex)
                {
                    try
                    {
                        var logger = Services.GetRequiredService<IExceptionLogService>();
                        await logger.LogAsync(ex, "AppDomain", "UnhandledException");
                    }
                    catch { }
                }
            };

            // ---- existing startup code ----
            _logger.LogInformation("========== APPLICATION STARTUP STARTED ==========");

            await _host.StartAsync();

            var shell = _host.Services.GetRequiredService<ShellWindow>();
            shell.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            _logger.LogInformation("========== APPLICATION SHUTDOWN STARTED ==========");
            _logger.LogInformation("Exit Time: {ExitTime}", DateTime.UtcNow);

            try
            {
                await _host.StopAsync();
                _logger.LogInformation("Host stopped successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during application shutdown");
            }
            finally
            {
                _host.Dispose();
                _logger.LogInformation("========== APPLICATION SHUTDOWN COMPLETED ==========");
            }

            base.OnExit(e);
        }
    }
}

