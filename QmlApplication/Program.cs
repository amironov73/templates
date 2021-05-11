// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

using System;
using System.IO;

using Qml.Net;
using Qml.Net.Runtimes;

namespace HelloQml
{
    public class NetObject
    {
        public string SomeUsefulText { get; set; }
            = "В чащах Юга жил-был цитрус, но фальшивый экземпляр!";

        public void HandleClick()
        {
            Console.WriteLine("Нас нажали!");
        }
    }
    
    internal static class Program
    {
        private static string RuntimeTargetToString(RuntimeTarget target)
        {
            switch (target)
            {
                case RuntimeTarget.LinuxX64:
                    return "linux-x64";
                
                case RuntimeTarget.OSX64:
                    return "osx-x64";
                
                case RuntimeTarget.Windows64:
                    return "win-x64";
                
                case RuntimeTarget.Unsupported:
                    throw new Exception("Unsupported target");
                
                default:
                    throw new Exception($"Unknown target {target}");
            }
        }

        static void PrepareRuntime()
        {
            var qtVersion = QmlNetConfig.QtBuildVersion;
            var runtimeTarget = RuntimeManager.GetCurrentRuntimeTarget();
            var runtimeVersion = qtVersion + "-" + RuntimeTargetToString(runtimeTarget);
            var runtimePath = Path.Combine
                (
                    AppContext.BaseDirectory,
                    runtimeVersion
                );

            if (!Directory.Exists(runtimePath))
            {
                Directory.CreateDirectory(runtimePath);
                RuntimeManager.DownloadRuntimeToDirectory
                    (
                        qtVersion, 
                        runtimeTarget, 
                        runtimePath
                    );
            }
            
            RuntimeManager.ConfigureRuntimeDirectory (runtimePath);
        }
        
        [STAThread]
        static int Main(string[] args)
        {
            PrepareRuntime();
            
            using var app = new QGuiApplication(args);
            using var engine = new QQmlApplicationEngine();

            Qml.Net.Qml.RegisterType<NetObject>("app");
            
            engine.Load("Main.qml");
            
            return app.Exec();
        }
    }
}
