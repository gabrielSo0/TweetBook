using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tweetbook.Installers
{
    public static class InstallerExtensions
    {

        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {

            // Creating instances of all my installer classes that implement the IInstaller interface
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x => 
                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            // With all the instances like DbInstaller, MvcInstaller in this collection of installer, I can call iterater and call the InstallServices methods
            // that will execute the code that is inside this methods.
            installers.ForEach(installer => installer.InstallServices(configuration, services));
        }
        
    }
}