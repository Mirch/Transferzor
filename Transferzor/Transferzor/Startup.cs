using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Transferzor.Data;
using Transferzor.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace Transferzor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public AwsParameterStoreClient AwsParameterStoreClient { get { return new AwsParameterStoreClient(RegionEndpoint.EUCentral1); } }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnectionString = AwsParameterStoreClient.GetValue("Transferzor-DB");

            services.AddDbContext<TransferzorDbContext>(options =>
            {
                options.UseSqlServer(dbConnectionString);
            });

            services.AddHangfire(x => x.UseSqlServerStorage(dbConnectionString));
            services.AddHangfireServer();

            services.AddScoped(sp => new AwsParameterStoreClient(RegionEndpoint.EUCentral1));
            services.AddAWSService<IAmazonS3>();
            services.AddScoped<IAwsS3FileManager, AwsS3FileManager>();
            services.AddScoped<IFileHandler, FileHandler>();
            services.AddScoped<IEmailSender, GmailEmailSender>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHangfireDashboard();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllerRoute("default", "api/{controller}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
