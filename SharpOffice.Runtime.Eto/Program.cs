using System;
using DryIoc;
using Eto.Forms;
using NLog;
using SharpOffice.Core;
using SharpOffice.Core.Container;
using SharpOffice.Core.Window;
using Container = DryIoc.Container;

namespace SharpOffice.Runtime.Eto
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetLogger("Runtime.Eto.Main");

        public static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Name of the Assembly containing IApplication definition is required.");
                Logger.Error("Running without an application specified.");
                return 1;
            }

            ContainerWrapper.Initialize(args[0]); //args[0] = applicationAssemblyName
            var container = ContainerWrapper.GetContainer();
            var app = InitializeApplication(container);
            var menuProvider = container.Resolve<IMenuProvider>();
            var menuComposers = container.ResolveMany<IMenuComposer>();

            var etoApplication = new Application();

            Logger.Debug("Creating MainWindow...");
            var window = new MainWindow(app, menuProvider, menuComposers);
            window.Initialize();

            Logger.Debug("Starting application...");
            etoApplication.Run(window);

            Logger.Debug("Stopping application...");
            ContainerWrapper.Dispose();
            return 0;
        }

        private static IApplication InitializeApplication(Container container)
        {
            var application = container.Resolve<IApplication>();
            application.RegisterCustomTypes(container);
            return application;
        }
    }
}