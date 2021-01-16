using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VitoshaBank.Data.Models;
using VitoshaBank.Services;
using VitoshaBank.Services.BankAccountService;
using VitoshaBank.Services.BankAccountService.Interfaces;
using VitoshaBank.Services.CalculateDividendService;
using VitoshaBank.Services.CalculateDividendService.Interfaces;
using VitoshaBank.Services.CalculateInterestService;
using VitoshaBank.Services.CreditService;
using VitoshaBank.Services.CreditService.Interfaces;
using VitoshaBank.Services.DebitCardService;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.DepositService;
using VitoshaBank.Services.IBANGeneratorService;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.Interfaces;
using VitoshaBank.Services.Interfaces.UserService;
using VitoshaBank.Services.Interfaces.WalletService;
using VitoshaBank.Services.TransactionService;
using VitoshaBank.Services.TransactionService.Interfaces;
using VitoshaBank.Services.UserService;
using VitoshaBank.Services.WalletService;

namespace VitoshaBank
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

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IWalletsService, WalletsService>();
            services.AddScoped<IDepositService, DepositService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IDebitCardService, DebitCardService>();
            services.AddScoped<IBCryptPasswordHasherService, BCryptPasswordHasherService>();
            services.AddScoped<IIBANGeneratorService, IBANGeneratorService>();
            services.AddScoped<ICreditService, CreditService>();
            services.AddScoped<ICalculateInterestService, CalculateInterestService>();
            services.AddScoped<ICalculateDividentService, CalculateDividentService>();
            services.AddScoped<ITransactionService, TransactionsService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddControllersWithViews()
                                .AddNewtonsoftJson(options =>
                                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                    );

            services.AddDbContext<BankSystemContext>(options => options.UseMySQL(Configuration.GetConnectionString("BankConnection")));
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "frontend/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "frontend";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
