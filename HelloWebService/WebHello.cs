using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Topshelf;
using Topshelf.Logging;

using TopshelfHost = Topshelf.Host;
using AspNetHost = Microsoft.Extensions.Hosting.Host;

namespace HelloService
{
    public class WebHello
        : ServiceControl
    {
        #region Private members
        
        private LogWriter _log;
        
        #endregion

        #region Private members

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            AspNetHost.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        #endregion

        #region Public methods
        
        /// <summary>
        /// Запуск сервиса. 
        /// </summary>
        public bool Start
            (
                HostControl hostControl
            )
        {
            _log = HostLogger.Get<WebHello>();
            _log.Info (nameof(WebHello) + "::" + nameof (Start));
            
            CreateHostBuilder(new string[0]).Build().Run();
            
            return true;
        }
        
        /// <summary>
        /// Остановка сервиса. 
        /// </summary>
        public bool Stop
            (
                HostControl hostControl
            )
        {
            _log.Info (nameof(WebHello) + "::" + nameof (Stop));
            
            return true;
        }
        
        #endregion
    }
}
