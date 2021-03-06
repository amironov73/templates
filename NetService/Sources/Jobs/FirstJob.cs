﻿// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global

/* FirstJob.cs -- задание, выводящее на экран значение счетчика
 * Ars Magna project, http://arsmagna.ru
 * -------------------------------------------------------
 */

#region Using directives

using System;
using System.Threading.Tasks;

using Topshelf.Logging;

using Quartz;

#endregion

namespace QuartzShelf.Jobs
{
    /// <summary>
    /// Задание, выводящее на экран значение счетчика.
    /// </summary>
    [DisallowConcurrentExecution]
    public sealed class FirstJob 
        : IJob
    {
        #region Private members
        
        private static readonly LogWriter _log = HostLogger.Get<FirstJob> ();
        private static int _counter;
        
        #endregion
        
        #region Public members
        
        /// <summary>
        /// Метод вызывается планировщиком.
        /// </summary>
        public async Task Execute
            (
                IJobExecutionContext context
            )
        {
            ++_counter;
            await Console.Out.WriteLineAsync($"FirstJob: {_counter}");
            _log.InfoFormat("FirstJob: {0}", _counter);
        }
        
        #endregion
    }
}