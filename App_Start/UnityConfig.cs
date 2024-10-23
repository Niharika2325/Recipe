using System.Web.Http;
using Unity;
using Unity.WebApi;
using System.Configuration;
using CookingRecipe.DAL.Interfaces;
using CookingRecipe.DAL.Services;
using Unity.Injection;

namespace CookingRecipe
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IRecipe, RecipeService>(
                    new InjectionConstructor(ConfigurationManager.ConnectionStrings["conn"].ConnectionString)
                );
            container.RegisterType<ICategory, CategoryService>(
                  new InjectionConstructor(ConfigurationManager.ConnectionStrings["conn"].ConnectionString)
              );
            container.RegisterType<IProfile, PorfileService>(
                 new InjectionConstructor(ConfigurationManager.ConnectionStrings["conn"].ConnectionString)
             );
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}