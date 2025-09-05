using AutoMapper;
using GAC.Integration.Infrastructure.Data;
using GAC.Integration.Service;
using GAC.Integration.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.AspNetCore;
using Quartz.Simpl;
using System.Data;
using System.Reflection;
using System.Text;

namespace MG.Marine.Ticketing.API.DependencyConfig
{
    public static class DependencyConfig
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHttpClient<ILookupService, LookupService>((sp, client) => { client.BaseAddress = new Uri(configuration["MasterDataService:MasterDataBaseURL"]); });
            //services.AddHttpClient<IDocumentManagementClient, DocumentManagementClient>(nameof(DocumentManagementClient),
            //                                        (sp, config) =>
            //                                        {
            //                                            var options = sp.GetRequiredService<IOptions<DocumentManagementOptions>>();
            //                                            config.BaseAddress = new Uri(options.Value.Url);
            //                                        });
        }

        public static void AddRepositories(this IServiceCollection services)
        {

            var types = Assembly.Load(new AssemblyName("GAC.Integration.Infrastructure"))
                                .DefinedTypes
                                .ToList();
            var repos = new Dictionary<Type, Type>();
            foreach (var typeInfo in types)
            {
                var repoInterface = typeInfo.ImplementedInterfaces.FirstOrDefault(e => e.GetInterfaces().Any(i => i.IsGenericType));
                if (repoInterface == null)
                    continue;

                repos.Add(repoInterface, typeInfo);
            }

            foreach (var repo in repos)
            {
                services.AddScoped(repo.Key, repo.Value);
            }
        }

        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<IUserSession,UserSession>();
            
        }

        public static void AddOptionsBinders(this IServiceCollection services, IConfiguration configuration)
        {
          
         //   services.AddOptions<DocumentManagementOptions>().Bind(configuration.GetSection("DocumentManagement"));
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            var connectionString = configuration.GetConnectionString("DB");
            services.AddDbContext<ServiceDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(90));
            });
        }

     
        public static void AddQuartz(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            services.AddQuartz(q =>
            {
                var quartzConnectionString = configuration["SchedulerService:dataSource:default:connectionString"];

                q.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();

                q.UsePersistentStore(options =>
                {
                    options.UseSqlServer(sqlServer =>
                    {
                        sqlServer.ConnectionString = configuration.GetConnectionString("DynamicJobs");
                    });
                    options.UseSerializer<BinaryObjectSerializer>();
                });
              
                //var jobsJsonPath = Path.Combine(AppContext.BaseDirectory, "jobs.json");
                //if (File.Exists(jobsJsonPath))
                //{
                //    var jobs = System.Text.Json.JsonSerializer.Deserialize<List<JobsDTO>>(File.ReadAllText(jobsJsonPath));
                //    foreach (var job in jobs)
                //    {
                //        var jobType = Type.GetType(job.JobParameters.HandlerType + ", " + job.JobParameters.HandlerAssembly);
                //        if (jobType != null)
                //        {
                //            var jobKey = new JobKey(job.JobParameters.JobId);

                //            q.AddJob(jobType, jobKey);
                //            if (!string.IsNullOrEmpty(job.JobParameters.CronExpression))
                //            {
                //                q.AddTrigger(opts => opts
                //                    .ForJob(jobKey)
                //                    .WithIdentity(job.JobParameters.JobId + ".trigger")
                //                    .WithCronSchedule(job.JobParameters.CronExpression));
                //            }
                //            else if (!string.IsNullOrEmpty(job.JobParameters.ExecuteEvery))
                //            {
                //                var interval = TimeSpan.Parse(job.JobParameters.ExecuteEvery);
                //                q.AddTrigger(opts => opts
                //                    .ForJob(jobKey)
                //                    .WithIdentity(job.JobParameters.JobId + ".trigger")
                //                    .StartNow()
                //                    .WithSimpleSchedule(x => x.WithInterval(interval).RepeatForever()));
                //            }
                //        }
                //    }
                //}
            });
            services.AddQuartzServer(options => options.WaitForJobsToComplete = true);
        }

        

    


        //public static void AddServiceBus(this IServiceCollection services,
        //                                 IConfiguration configurations,
        //                                 IHostEnvironment hostEnvironment,
        //                                 Action<IServiceCollectionConfigurator> registerAdditionalConsumers)
        //{
        //    Log.Information("Configuring RabbitMQ");

        //    var massTransitConfig = configurations.GetSection("MassTransit");
        //    var _protocol = massTransitConfig["Protocol"];
        //    var _rabbitMQHost = massTransitConfig["ClusterUrl"];
        //    var _rabbitMQUserName = massTransitConfig["UserName"];
        //    var _rabbitMQPassword = massTransitConfig["Password"];
        //    var nodes = massTransitConfig.GetSection("Nodes").Get<string[]>();
        //    services.AddMassTransit(x =>
        //    {
        //        //x.AddConsumer<BillingLineHandler>();
        //        x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
        //        {
        //            var host = cfg.Host(new Uri($"{_protocol}://{_rabbitMQHost}"),
        //                                hostConfig =>
        //                                {
        //                                    hostConfig.Username(_rabbitMQUserName);
        //                                    hostConfig.Password(_rabbitMQPassword);
        //                                    if (nodes != null && nodes.Any())
        //                                        hostConfig.UseCluster(e =>
        //                                        {
        //                                            foreach (var node in nodes)
        //                                            {
        //                                                e.Node(node);
        //                                            }
        //                                        });
        //                                });
        //            // Sale Cancellation Email Notification
        //            cfg.Publish<SaleCancelledNotification>(publishConfig => publishConfig.ExchangeType = ExchangeType.Topic);
        //            cfg.Send<SaleCancelledNotification>(sendConfig => sendConfig.UseRoutingKeyFormatter(y => y.Message.Source));

        //            // Sale Cancellation Customer Email Notification
        //            cfg.Publish<SaleCancelledCustomerNotification>(publishConfig => publishConfig.ExchangeType = ExchangeType.Topic);
        //            cfg.Send<SaleCancelledCustomerNotification>(sendConfig => sendConfig.UseRoutingKeyFormatter(y => y.Message.Source));

        //            // Sale Cancellation Email Notification
        //            cfg.Publish<SailRescheduledNotification>(publishConfig => publishConfig.ExchangeType = ExchangeType.Topic);
        //            cfg.Send<SailRescheduledNotification>(sendConfig => sendConfig.UseRoutingKeyFormatter(y => $"SailRescheduledNotification.SailRescheduled"));

        //            //Sending email to internal user
        //            cfg.Publish<NewInternalUserNotification>(publishConfig => publishConfig.ExchangeType = ExchangeType.Topic);
        //            cfg.Send<NewInternalUserNotification>(sendConfig => sendConfig.UseRoutingKeyFormatter(y => $"NewInternalUserNotification.NewInternalUser"));

        //            //Booking Confirmation Email
        //            cfg.Publish<BookingConfirmation>(publishConfig => publishConfig.ExchangeType = ExchangeType.Topic);
        //            cfg.Send<BookingConfirmation>(sendConfig => sendConfig.UseRoutingKeyFormatter(y => y.Message.Source));

        //            //Booking Cancellation Email
        //            cfg.Publish<BookingCancellation>(publishConfig => publishConfig.ExchangeType = ExchangeType.Topic);
        //            cfg.Send<BookingCancellation>(sendConfig => sendConfig.UseRoutingKeyFormatter(y =>  y.Message.Source));

        //            //Booking Reschedule
        //            cfg.Publish<BookingReschedule>(publishConfig => publishConfig.ExchangeType = ExchangeType.Topic);
        //            cfg.Send<BookingReschedule>(sendConfig => sendConfig.UseRoutingKeyFormatter(y => y.Message.Source));

        //            //Sail Checkin manifest Notification
        //            cfg.Publish<SailCheckinManifestEmail>(publishConfig => publishConfig.ExchangeType = ExchangeType.Topic);
        //            cfg.Send<SailCheckinManifestEmail>(sendConfig => sendConfig.UseRoutingKeyFormatter(y => y.Message.Source));

        //            // Sale Cancellation Customer Email Notification
        //            cfg.Publish<SaleCancelledBookingRescheduleNotification>(publishConfig => publishConfig.ExchangeType = ExchangeType.Topic);
        //            cfg.Send<SaleCancelledBookingRescheduleNotification>(sendConfig => sendConfig.UseRoutingKeyFormatter(y => y.Message.Source));


        //            services.AddSingleton(c => host);
        //        }));
        //    });

        //    // Start Service Bus
        //    var busControl = services.BuildServiceProvider()
        //                             .GetService<IBusControl>();

        //    busControl.Start();
        //}



        public static void AddCoresAllowAll(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowAllOrigin",
                            options => options.AllowAnyOrigin()
                                              .AllowAnyMethod()
                                              .AllowAnyHeader());
            });
        }
        
    }
}