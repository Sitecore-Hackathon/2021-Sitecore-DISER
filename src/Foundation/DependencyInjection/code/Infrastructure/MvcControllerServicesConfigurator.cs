using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace SitecoreDiser.Foundation.DependencyInjection.Infrastructure
{
    public class MvcControllerServicesConfigurator : IServicesConfigurator
    {

        public void Configure(IServiceCollection serviceCollection)

        {
            serviceCollection.AddMvcControllers("*.Feature.*");
            serviceCollection.AddMvcControllers("*.Project.*");
            serviceCollection.AddMvcControllers("*.Foundation.*");
           serviceCollection.AddMvcControllers("*.Web");
            serviceCollection.AddClassesWithServiceAttribute("*.Feature.*");
            serviceCollection.AddClassesWithServiceAttribute("*.Foundation.*");
            serviceCollection.AddClassesWithServiceAttribute("*.Web");
        }
    }
}