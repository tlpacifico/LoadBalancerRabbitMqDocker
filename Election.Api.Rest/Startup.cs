using System;
using GreenPipes;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Election.Api.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)        {
          
            services.Configure<RabbitMqOptions>(Configuration.GetSection("RabbitMQConfiguration"));
            services.AddMassTransit(x => {             
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg => {
                    RabbitMqOptions options = new RabbitMqOptions();
                    Configuration.GetSection("RabbitMQConfiguration").Bind(options);
                    
                    var host = cfg.Host(options.HostName, hostConfig => {
                        hostConfig.Username(options.UserName);
                        hostConfig.Password(options.Password);
                    });
                 
                }));
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
