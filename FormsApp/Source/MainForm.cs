// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

/* MainForm.cs -- главная форма приложения
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System;
using System.Threading;
using System.Windows.Forms;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Timer = System.Windows.Forms.Timer;

#endregion

#nullable enable

namespace FormsApp
{
    /// <summary>
    /// Главная форма приложения
    /// </summary>
    public partial class MainForm 
        : Form,
        IMainForm
    {
        private readonly IHost _host;
        private readonly Timer _timer;
        
        public MainForm
            (
                IHost host
            )
        {
            _host = host;
            
            InitializeComponent();

            _timer = new Timer
            {
                Interval = 10,
                Enabled = true
            };
            _timer.Tick += Timer_OnTick;
        }

        private async void Timer_OnTick
            (
                object? sender, 
                EventArgs e
            )
        {
            _timer.Enabled = false;

            // получаем доступ к конфигурации приложения
            var configuration = _host.Services.GetService<IConfiguration>();
            
            // получаем данные из конфигурации
            var personData = new PersonData();
            configuration.Bind("PersonData", personData);

            var greeter = _host.Services.GetRequiredService<IGreeter>();
            await greeter.GreetAsync
                (
                    personData.Name!,
                    personData.Age!,
                    CancellationToken.None
                );
        }
    }
}
