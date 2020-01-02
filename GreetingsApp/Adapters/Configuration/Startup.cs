using System;
using GreetingsCore.Adapters.Db;
using GreetingsCore.Adapters.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace GreetingsApp.Adapters.Configuration
{
    public class Startup
    {
        private readonly Container _container;

        public IConfiguration Configuration { get; private set; }
        
        public Startup(IHostingEnvironment env)
        {
            //use a sensible constructor resolution approach
            _container = new Container();
            _container.Options.ConstructorResolutionBehavior = new MostResolvableParametersConstructorResolutionBehavior(_container);

            BuildConfiguration(env);
        }

        private void BuildConfiguration(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            InitializeContainer(app);
            _container.Verify();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors("AllowAll");

            app.UseMvc();

            EnsureDatabaseCreated();

        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            IntegrateSimpleInjector(services);
        }
        
        private void EnsureDatabaseCreated()
        {
            var contextOptions = _container.GetInstance<DbContextOptions<GreetingContext>>();
            var context = new GreetingContext(contextOptions);
            context.Database.EnsureCreated();
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(_container));

            services.UseSimpleInjectorAspNetRequestScoping(_container);
   
        }
       
        private void InitializeContainer(IApplicationBuilder app)
        {
            _container.Register( 
                () => new DbContextOptionsBuilder<GreetingContext>()
                    .UseMySql(Configuration["Database:Greetings"])
                    .Options, 
                Lifestyle.Singleton);
         
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

        }
   }
    
}
