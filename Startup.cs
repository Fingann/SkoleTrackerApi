using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Database;
using Application.Infrastructure;
using Application.Notifications;
using Application.Project.Querys.GetProject;
using AutoMapper;
//using Application.Users.Querys.GetUser;
using MediatR;
using MediatR.Pipeline;
//using DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.IISUrlRewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Database;
using Swashbuckle.AspNetCore.Swagger;

namespace SkoleTrackerApi
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
            services.AddDbContext<ProjectTrackerContext>();
            using (var s = new ProjectTrackerContext())
            {
                s.Database.EnsureCreated();

                var projects = s.Projects.ToList();
                var sessions = s.Sessions.ToList();
                var tasks = s.Tasks.ToList();
                var simplesession = s.SimpleTaskSessions.ToList();
                var session1 = s.Sessions
                        .Where(p => p.Id == -1)
                        .SelectMany(p => p.WorkedOnTasks)
                    .Select(pc => pc.SimpleTask).ToList();
            }           
            services.AddAutoMapper();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestLogger<>));

            services.AddTransient<INotificationService>(x =>new NotificationService());

           
            services.AddMediatR(typeof(GetProjectQuery).GetTypeInfo().Assembly);  
            services.AddMediatR(typeof(GetProjectQueryHandler).GetTypeInfo().Assembly);  
            
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Skole Tracker API",  Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skole Tracker API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}