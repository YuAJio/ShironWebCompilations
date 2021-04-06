using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YrsMQTTNet.Core;

namespace YrsMQNetServer
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

        }


        public void IOC_Copy(IServiceCollection services)
        {
            var hostIP = Configuration["MqttConfig:HostIP"].ToString();
            var hostPort = Configuration["MqttConfig:HostPort"];
            var timeOut = Configuration["MqttConfig:ConnectTimeOut"].ToString();
            var auUserName = Configuration["MqttConfig:Auth_UserName"].ToString();
            var auPassWord = Configuration["MqttConfig:Auth_PassWord"].ToString();

            services.AddSingleton(new MQServer(hostIP, int.Parse(hostPort), auUserName, auPassWord, long.Parse(timeOut)));
            services.AddScoped(typeof(MQService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseAuthentication();
            //app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
