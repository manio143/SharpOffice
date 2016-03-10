using System;
using SharpOffice.Core;
using SharpOffice.Core.Configuration;
using SharpOffice.Window;
using DryIoc;
using Xwt;
using Xwt.GtkBackend;

namespace SharpOffice
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            InitializeApplication();
            ContainerWrapper.Initialize();

            var container = ContainerWrapper.GetContainer();
            var windowDefinition = GetWindowDefinition(container);

            using (var window = windowDefinition.Window)
            {
                window.Show();
                Application.Run();
            }

            ContainerWrapper.Dispose();
        }

        public static void InitializeApplication()
        {
            if (Platform.IsWindows)
                Application.Initialize(ToolkitType.Wpf);
            else if (Platform.IsMac)
                Application.Initialize(ToolkitType.Cocoa);
            else
                Application.Initialize(ToolkitType.Gtk);
        }

        private static IWindowDefinition GetWindowDefinition(Container container)
        {
            var windowDefinition = container.Resolve<IWindowDefinition>(serviceKey: "MainWindow");
            windowDefinition.Configure(container.Resolve<IConfiguration>(serviceKey: "MainWindowConfiguration"));
            return windowDefinition;
        }
    }
}
