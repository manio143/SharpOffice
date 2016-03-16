using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DryIoc;
using SharpOffice.Core.Attributes;

namespace SharpOffice.Core
{
    /// <summary>
    /// Provides access to the IoC container and registers all neccesary types for the application to run.
    /// </summary>
    public static class ContainerWrapper
    {
        private static Container _container;
        public static Container GetContainer()
        {
            return _container;
        }

        public static void Initialize(string applicationAssemblyName)
        {
            _container = new Container();
            AutoRegister(applicationAssemblyName);
        }

        private static void AutoRegister(string applicationAssemblyName)
        {
            Assembly[] assemblies = GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.FullName == applicationAssemblyName)
                {
                    RegisterApplication(assembly);
                    RegisterMainWindow(assembly);
                }
                //TODO: register types that may be plugged in (like file formats, data formats, etc.)
            }
        }

        private static void RegisterMainWindow(Assembly assembly)
        {
            Type mainWindowDefinitionType =
                assembly.GetTypes()
                    .First(t => t.GetCustomAttribute<WindowAttribute>().WindowType == WindowType.MainWindow);
            Type iWindowDefinition =
                Assembly.Load(new AssemblyName("SharpOffice.Window"))
                    .GetTypes()
                    .First(t => t.Name == "IWindowDefinition");
            _container.Register(iWindowDefinition, mainWindowDefinitionType, serviceKey: "MainWindow");
        }

        private static void RegisterApplication(Assembly assembly)
        {
            Type applicationType = assembly.GetTypes().First(t => typeof(IApplication).IsAssignableFrom(t));
            _container.Register(typeof(IApplication), applicationType);
        }

        private static Assembly[] GetAssemblies()
        {
            string path = Environment.CurrentDirectory;
            string[] assemblyFiles = Directory.GetFiles(path, "*.DLL");
            return assemblyFiles.Select(Assembly.LoadFrom)
                .Where(assembly => !assembly.GetCustomAttributes<CoreAssemblyAttribute>().Any())
                .ToArray();
        }

        public static void Dispose()
        {
            _container.Dispose();
        }
    }
}
