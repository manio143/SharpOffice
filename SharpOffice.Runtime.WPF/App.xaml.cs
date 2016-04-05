using System;
using System.Windows;
using DryIoc;
using SharpOffice.Core;

namespace SharpOffice.Window.Runtime.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Name of the Assembly containing IApplication definition is required.");
                return 1;
            }

            ContainerWrapper.Initialize(args[0]); //args[0] = applicationAssemblyName
            var container = ContainerWrapper.GetContainer();
            InitializeApplication(container);

            var application = new App();
            application.InitializeComponent();
            application.Run();

            ContainerWrapper.Dispose();
            return 0;
        }

        private static void InitializeApplication(Container container)
        {
            var application = container.Resolve<IApplication>();
            application.RegisterCustomTypes(container);
        }
    }
}
