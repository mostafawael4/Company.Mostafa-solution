using Company.Mostafa.BLL;
using Company.Mostafa.BLL.Interfaces;
using Company.Mostafa.BLL.Repositories;
using Company.Mostafa.DAL.Data.Contexts;
using Company.Mostafa.DAL.Models;
using Company.Mostafa.PL.Helpers;
using Company.Mostafa.PL.Mapping;
using Company.Mostafa.PL.Services;
using Company.Mostafa.PL.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Mostafa.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<AppDBContext>();
            builder.Services.AddDbContext<AppDBContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
                 


            }
            );

            builder.Services.AddScoped<IDepartment, DepartmentRepository>();
            /*builder.Services.AddScoped<IEmployee, EmployeeRepository>();*/

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IScopedService,ScopedService>();
            builder.Services.AddTransient<ITransientService,TransientService>();
            builder.Services.AddSingleton<ISingletonService,SingletonService>();
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));


            builder.Services.Configure<MailSetting>(builder.Configuration.GetSection(nameof(MailSetting)));

            builder.Services.AddScoped<IMailService , MailService>();

            builder.Services.Configure<TwilioSetting>(builder.Configuration.GetSection(nameof(TwilioSetting)));

            builder.Services.AddScoped<ITwilioService, TwilioServicee>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDBContext>()
                            .AddDefaultTokenProviders();
              
            builder.Services.ConfigureApplicationCookie(configure =>
            {
                configure.LoginPath = "/Account/SignIn";
               
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

