// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedParameter.Local

/* Program.cs -- точка входа в программу
 * Ars Magna project, http://arsmagna.ru
 * -------------------------------------------------------
 */

#region Using directives

using System;

using QuartzShelf.Jobs;

using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.Logging;

#endregion

namespace QuartzShelf
{
    /// <summary>
    /// Точка входа в программу.
    /// </summary>
    internal sealed class Program
    {
        #region Private members
        
        /// <summary>
        /// Конфигурация сервиса средствами Topshelf.
        /// </summary>
        private static void ConfigureService 
            (
                HostConfigurator configurator
            )
        {
            configurator.ApplyCommandLine();

            var service = configurator.Service<DummyService>();
            service.SetDescription("My Dummy service for learning");
            service.SetDisplayName("Dummy Service");
            service.SetServiceName("DummyService");

            service.StartAutomaticallyDelayed();
            service.RunAsLocalSystem();
            service.EnableShutdown();

            service.UseNLog();

            // Необязательная настройка восстановления после сбоев
            service.EnableServiceRecovery(recovery =>
                {
                    recovery.RestartService(1); 
                    
                });

            // Реакция на исключение
            service.OnException(exception =>
                {
                    var log = HostLogger.Get<DummyService>();
                    log.ErrorFormat($"Exception {exception}");
                });
        }

        /// <summary>
        /// Однократный ручной запуск заданий.
        /// </summary>
        private static int RunJobsManually
            (
                string[] args
            )
        {
            string selectedJobName = null;

            if (args.Length > 1)
            {
                selectedJobName = args[1];
            }
            
            HostLogger.UseLogger
                (
                    new NLogLogWriterFactory.NLogHostLoggerConfigurator()
                );
                    
            try
            {
                JobController.RunJobsManually(selectedJobName);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Конфигурирование и запуск сервиса.
        /// </summary>
        private static int ConfigureAndRunService
            (
                string[] args
            )
        {
            var result = HostFactory.Run(ConfigureService);

            return (int) result;
        }
        
        #endregion
        
        #region Program entry point
        
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        public static int Main(string[] args)
        {
            if (args.Length != 0)
            {
                if (args[0].ToLowerInvariant() == "run")
                {
                    return RunJobsManually(args);
                }
            }

            return ConfigureAndRunService(args);
        }
        
        #endregion
    }
}