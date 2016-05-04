using System;
using DryIoc;
using Eto.Forms;
using SharpOffice.Core;
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
            InitializeApplication(container);

            new Application().Run(new Dialog(){Title = "First run."});

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