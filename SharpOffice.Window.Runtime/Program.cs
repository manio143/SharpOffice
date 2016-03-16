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
        private static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Name of the Assembly containing IApplication definition is required.");
                return 1;
            }

            string applicationAssemblyName = args[0];

            InitializeXwtApplication();
            ContainerWrapper.Initialize(applicationAssemblyName);

            var container = ContainerWrapper.GetContainer();
            
            InitializeApplication(container);
            var windowDefinition = GetWindowDefinition(container);

            using (var window = windowDefinition.Window)
            {
                window.Show();
                Application.Run();
            }

            ContainerWrapper.Dispose();

            return 0;
        }

        public static void InitializeXwtApplication()
        {
            if (Platform.IsWindows)
                Application.Initialize(ToolkitType.Wpf);
            else if (Platform.IsMac)
                Application.Initialize(ToolkitType.Cocoa);
            else
                Application.Initialize(ToolkitType.Gtk);
        }

        private static void InitializeApplication(Container container)
        {
            var application = container.Resolve<IApplication>();
            application.RegisterCustomTypes(container);
        }

        private static IWindowDefinition GetWindowDefinition(Container container)
        {
            var windowDefinition = container.Resolve<IWindowDefinition>(serviceKey: "MainWindow");
            windowDefinition.Configure(container.Resolve<IConfiguration>(serviceKey: "MainWindowConfiguration"));
            return windowDefinition;
        }
    }
}
