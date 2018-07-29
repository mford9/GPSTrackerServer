using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.ServiceProvider;
using Rebus.Config;
using Ford.Tracker.Api.Constants;
using Rebus.Routing.TypeBased;
using Ford.Tracker.Api.Messaging.Constants;
using Ford.Tracker.Api.Messaging.Payload;
using Ford.Tracker.Api.Business;

namespace Ford.Tracker.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();            
            
            services.AddTransient<IGlobalPositioningSystemBusiness, GlobalPositioningSystemBusiness>();

            services.AddRebus(con =>
            con.Logging(l => l.None())
            .Transport(t => t.UseRabbitMqAsOneWayClient(Configuration.GetSection(AppSettingConstants.ServiceBusConnectionString).Value))
            .Routing(r => r.TypeBased().Map<GlobalPositioningSystemMessage>(QueueName.GlobalPositioningSystemPersistanceQueueName)));

            // TODO 
            // .Transport(t => t.UseRabbitMq(Configuration.GetSection(AppSettingConstants.ServiceBusConnectionString).Value, "AwesomeQueue"))); this needs to be added
            if (Environment.IsDevelopment())
            {                
            }
            else
            {                
            }           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRebus();
            
            app.UseMvc();
        }
    }
}
