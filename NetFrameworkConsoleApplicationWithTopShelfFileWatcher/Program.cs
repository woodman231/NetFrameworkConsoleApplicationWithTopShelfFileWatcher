using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Topshelf;

namespace NetFrameworkConsoleApplicationWithTopShelfFileWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureService();
        }

        static void ConfigureService()
        {
            ServiceConfig serviceConfig = GetServiceConfig();

            HostFactory.Run(serviceOptions =>
            {
                serviceOptions.Service<ExampleFilesystemWatcher>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(() => new ExampleFilesystemWatcher(serviceConfig));
                    serviceInstance.WhenStarted(execute => execute.Start());
                    serviceInstance.WhenStopped(execute => execute.Stop());
                    serviceInstance.WhenPaused(execute => execute.Pause());
                    serviceInstance.WhenContinued(execute => execute.Continue());
                });

                serviceOptions.RunAsNetworkService();

                serviceOptions.SetServiceName(serviceConfig.ServiceName);
                serviceOptions.SetDisplayName(serviceConfig.ServiceDisplayName);
                serviceOptions.SetDescription(serviceConfig.ServiceDescription);

                serviceOptions.StartAutomatically();
            });
        }

        static ServiceConfig GetServiceConfig()
        {
            return new ServiceConfig
            {
                ServiceName = ConfigurationManager.AppSettings["ServiceName"],
                ServiceDisplayName = ConfigurationManager.AppSettings["ServiceDisplayName"],
                ServiceDescription = ConfigurationManager.AppSettings["ServiceDescription"],
                FolderToWatch = ConfigurationManager.AppSettings["FolderToWatch"]
            };
        }

    }
}
