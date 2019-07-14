using System;
using core.Queue.Manager;
using Election.Api.Rest;
using GreenPipes;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Vote.Queue.Job;


namespace Vote.Queue
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;           
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ConsumerQueue>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services             
              .AddMassTransit(x => {
                  x.AddConsumer<ConsumerQueue>();
                  x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg => {
                        RabbitMqOptions options = new RabbitMqOptions();
                        Configuration.GetSection("RabbitMQConfiguration").Bind(options);

                        var host = cfg.Host(options.HostName, hostConfig => {
                            hostConfig.Username(options.UserName);
                            hostConfig.Password(options.Password);
                        });

                        cfg.ReceiveEndpoint(host, "vote_queue", ep => {
                            ep.PrefetchCount = 16;
                            ep.UseMessageRetry(mr => mr.Interval(2, 100));

                            ep.ConfigureConsumer<ConsumerQueue>(provider);
                        });
                    }));
            });

            services
             .AddSingleton<IQueueManager, QueueManager>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();
            // Add our job
            services.AddSingleton<SaveVoteJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(SaveVoteJob),
                cronExpression: "0/10 * * * * ?")); // run every 10 seconds
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            var bus = app.ApplicationServices.GetService<IBusControl>();
            var busHandle = TaskUtil.Await(() =>
            {
                return bus.StartAsync();
            });

            lifetime.ApplicationStopping.Register(() =>
            {
                busHandle.Stop();
            });

        }
    }
    public class RabbitMqOptions
    {
        public Uri HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
