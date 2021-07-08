using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.Logging;

namespace HelloService
{
    public class Program
    {
        /// <summary>
        /// Конфигурация сервиса средствами Topshelf.
        /// </summary>
        private static void ConfigureService 
            (
                HostConfigurator configurator
            )
        {
            configurator.ApplyCommandLine();

            var service = configurator.Service<WebHello>();
            service.SetDescription("ASP.NET Hello service");
            service.SetDisplayName("Web Hello Service");
            service.SetServiceName("WebHelloService");

            service.StartAutomaticallyDelayed();
            service.RunAsLocalSystem();
            service.EnableShutdown();

            // Необязательная настройка восстановления после сбоев
            service.EnableServiceRecovery(recovery =>
            {
                recovery.RestartService(1); 
                    
            });

            // Реакция на исключение
            service.OnException(exception =>
            {
                var log = HostLogger.Get<WebHello>();
                log.ErrorFormat($"Exception {exception}");
            });
        }
        
        public static int Main(string[] args)
        {
            var result = HostFactory.Run(ConfigureService);

            return (int) result;
        }

    }
}
