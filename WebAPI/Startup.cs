using System;
using AutoMapper;
using Business;
using Business.Abstract;
using Business.Concrete;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebAPI
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
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebAPI", Version = "v1"}); });
            services.AddDbContext<SchoolContext>(options =>
            {
                options.UseNpgsql(Environment.GetEnvironmentVariable("STUDENTSCOREEXAMPLE_CONNECTION_STRING"));
            });
            services.AddScoped<IStudentService, StudentManager>();
            services.AddScoped<IUnitOfWorks, UnitOfWork>();
            services.AddScoped<ILessonService, LessonManager>();
            services.AddScoped<ITeacherService, TeacherManager>();
            services.AddScoped<IStudentScoreService, StudentScoreManager>();
            services.AddScoped<IGroupService, GroupManager>();

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new SchoolAutoMapperProfile()); });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}