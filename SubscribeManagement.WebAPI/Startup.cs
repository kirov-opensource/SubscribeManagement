using IdGen;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLite;
using SubscribeManagement.WebAPI.DA.Entities;
using SubscribeManagement.WebAPI.Middlewares;
using SubscribeManagement.WebAPI.Services;
using System.IO;
using System.Text.Json.Serialization;

namespace SubscribeManagement.WebAPI
{
    public class Startup
    {
        private void InitialDB(string dbPath)
        {
            if (dbPath.EndsWith("/"))
                dbPath = dbPath[..^1];
            if (!Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }

            var databasePath = Path.Combine(dbPath, "MyData.db");
            if (!File.Exists(databasePath))
            {
                var db = new SQLiteConnection(databasePath, false);
                db.CreateTable<Connection>();
                db.CreateTable<ConnectionExtraProperty>();
                db.CreateTable<ConnectionGroup>();
                db.CreateTable<ConnectionGroupItem>();
                db.CreateTable<ProtocolParseRule>();
                db.CreateTable<SubscribeParseRule>();
                db.CreateTable<User>();
            }
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private void RegisterService(IServiceCollection services)
        {
            services.AddScoped<ConnectionService>();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Id Worker
            IdGenerator generator = new IdGenerator(0);
            services.AddScoped(c => generator);

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //Health check
            services.AddHealthChecks();

            //DB
            var dbPath = Configuration.GetValue<string>("DBPath") ?? "./AppData";
            InitialDB(dbPath);

            var dbConnection = new SQLiteConnection(Path.Combine(dbPath, "MyData.db"), false);
            services.AddSingleton(c => dbConnection);

            // Swagger
            services.AddSwaggerGen();


            //×¢²á·þÎñ
            RegisterService(services);

            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandleMiddleware>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SubscribeManagement API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseHealthChecks("/healthz");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
