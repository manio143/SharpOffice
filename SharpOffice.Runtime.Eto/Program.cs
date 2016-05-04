using System;
using DryIoc;
using Eto.Forms;
using SharpOffice.Core;
using SharpOffice.Core.Window;
using Container = DryIoc.Container;

namespace SharpOffice.Runtime.Eto
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Name of the Assembly containing IApplication definition is required.");
                return 1;
            }

            ContainerWrapper.Initialize(args[0]); //args[0] = applicationAssemblyName
            var container = ContainerWrapper.GetContainer();
            var app = InitializeApplication(container);
            var menuProvider = container.Resolve<IMenuProvider>();

            var etoApplication = new Application();

            var window = new MainWindow(app, menuProvider);
            window.Initialize();

            etoApplication.Run(window);

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