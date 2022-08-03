using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using SystemYNP.Data;
using SystemYNP.Domains;

namespace SystemYNP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddHttpClient();
            services.AddQuartz(q =>
            {
                q.SchedulerId = "Scheduler-Core";
                q.SchedulerName = "Quartz ASP.NET Core Sample Scheduler";
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });
                var jobKey = new JobKey("User notification job", "notification-group");
                q.AddJob<UserNotificationJob>(jobKey);

                q.AddTrigger(t => t
                    .WithIdentity("Cron Trigger")
                    .ForJob(jobKey)
                    .WithCronSchedule("0 46 8 ? * * *")
                );
            });
            services.AddTransient<UserNotificationJob>();
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
            services.AddTransient<MailService>();
            services.AddTransient<NotificationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
