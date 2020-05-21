using Hangfire;
using Log_system.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Log_system
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireDB")));
            services.AddHangfireServer();
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireDashboard();

            CreateHangfireJobs();

            app.UseMvc();
        }

        private void CreateHangfireJobs()
        {
            RecurringJob.AddOrUpdate<DataController>(h => h.ReceiveInfo(), Cron.Minutely);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IHttpClient, HttpClient>();
            services.AddTransient<IDataClient, DataClient>();
        }
    }
}
