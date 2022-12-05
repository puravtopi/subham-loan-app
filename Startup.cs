using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApi.EMILAEntities;
using WebApi.Helpers;
using WebApi.Middleware;

using WebApi.Services.Implementation;
using WebApi.Services.Interface;

using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            services.AddDbContext<emiladbContext>();
            // services.AddDbContextPool<emiladbContext>();
            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEmailService, EmailService>();
            //services.AddScoped<IUsersService, UsersService>();
            services.AddTransient<IAuthenticateService, AuthenticateService>();
            services.AddTransient<IUserPersonaService, UserPersonaService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<ILoanService, LoanService>();

            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            //services.AddScoped<ITenantMasterService, TenantMasterService>();


            //    services.AddDbContextPool<MariaDbContext>(options => options.UseLazyLoadingProxies().
            //.UseMySQL(
            //    Configuration. GetConnectionString("MariaDbConnectionString")));

        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, DataContext context)
        {
            // migrate database changes on startup (includes initial db creation)
            context.Database.Migrate();

            // generated swagger json and swagger ui middleware
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Sign-up and Verification API"));

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
