using GAC.Integration.Infrastructure;
using GAC.Integration.Infrastructure.Data;
using GAC.Integration.Infrastructure.IRepositories;
using GAC.Integration.Infrastructure.Repositories;
using GAC.Integration.Service;
using GAC.Integration.Service.Interfaces;
using GAC.Integration.Service.Validation;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Data;
using System.Reflection;

namespace GAC.Integration.Api.MiddleWare
{
    public static class DependencyConfig
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            var assembly = Assembly.Load("GAC.Integration.Infrastructure");
            var types = assembly.GetTypes();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            foreach (var type in types)
            {
                // Register only concrete, non-abstract classes
                if (type.IsClass && !type.IsAbstract)
                {
                    // Find all interfaces implemented by this class that are in the IRepositories namespace
                    var interfaces = type.GetInterfaces()
                        .Where(i => i.Namespace != null && i.Namespace.Contains("GAC.Integration.Infrastructure"));

                    foreach (var iface in interfaces)
                    {
                        // Skip IRepository<T> (open generic)
                        if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IRepository<>))
                            continue;

                        services.AddScoped(iface, type);
                    }
                }
            }

        }
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<HasPermissionFilterOptions>(provider => new HasPermissionFilterOptions("YourStringValue", "dummy"));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<IUserSession, UserSession>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<ISalesOrderService, SalesOrderService>();
            services.AddScoped<IUserService, UserService>();
        }
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DB");
            services.AddDbContext<ServiceDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(90));
            });
        }

    }
}