using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using MassTransit.Util;
using NSwag.SwaggerGeneration.Processors.Security;
using NSwag.AspNetCore;
using NSwag;
using RabbitMQ.Client;
using Persistence.DependencyInjection;
using Models;

namespace Command.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } 
        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string dbConnString = Configuration.GetConnectionString("ProductDB");

            services.AddDbContext<ProductWriterContext>(options =>
                options.UseSqlServer( dbConnString, builder1 => builder1.MigrationsAssembly(typeof(Startup).Assembly.FullName)
                )).AddUnitOfWork<ProductWriterContext>();

                 // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Productwriter API Service";
                    document.Info.Description = "This service is to serve create, update and delete command as command layer in CQRS.";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Young Ho Son",
                        Email = string.Empty,
                        Url = ""
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Unlimited",
                        Url = "https://example.com/license"
                    };
                };
            });


            var builder = new ContainerBuilder();
          
            builder.Register(c =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(sbc =>
                    {
                        sbc.Host(new Uri("rabbitmq://localhost"), h =>
                        {
                            h.Username("rabbitmq");
                            h.Password("rabbitmq");
                        });

                        sbc.ExchangeType = ExchangeType.Fanout;
                    });
                })
                .As<IBusControl>()
                .As<IBus>()
                .As<IPublishEndpoint>()
                .SingleInstance();           
            

            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

  

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());            
            app.UseMvc();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwagger();
            app.UseSwaggerUi3();                   

            var bus = ApplicationContainer.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());
            lifetime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}
