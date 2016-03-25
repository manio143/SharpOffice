using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DryIoc;
using SharpOffice.Core.Attributes;
using SharpOffice.Core.Configuration;

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
                if (assembly.GetName().Name == applicationAssemblyName)
                {
                    RegisterApplication(assembly);
                    RegisterMainWindow(assembly);
                }
                RegisterConfigurations(assembly);
                //TODO: register types that may be plugged in (like file formats, data formats, etc.)
            }
            RegisterConfigurationProvider();
        }

        private static void RegisterConfigurations(Assembly assembly)
        {
            var configurations = assembly.GetTypes().Where(t => typeof (IConfiguration).IsAssignableFrom(t));
            foreach (var type in configurations)
            _container.Register(typeof(IConfiguration), type);
        }

        private static void RegisterConfigurationProvider()
        {
            Type configurationProvider = Assembly.Load(new AssemblyName("SharpOffice.Common"))
                .GetTypes()
                .First(t => t.Name == "ConfigurationProvider");
            _container.Register(typeof(IConfigurationProvider), configurationProvider, Reuse.Singleton);
        }

        private static void RegisterMainWindow(Assembly assembly)
        {
            Type mainWindowDefinitionType =
                assembly.GetTypes()
                    .First(t =>
                    {
                        var attribute = t.GetCustomAttribute<WindowAttribute>();
                        return attribute != null && attribute.WindowType == WindowType.MainWindow;
                    });
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

        /// <summary>
        /// Get all assemblies that are not Core and are either Applications or Plugins.
        /// </summary>
        /// <returns>Array of such assemblies.</returns>
        private static Assembly[] GetAssemblies()
        {
            string path = Environment.CurrentDirectory;
            string[] assemblyFiles = Directory.GetFiles(path, "*.DLL");
            return assemblyFiles.Select(Assembly.LoadFrom)
                .Where(assembly => assembly.GetCustomAttribute<CoreAssemblyAttribute>() == null
                                    && (assembly.GetCustomAttribute<ApplicationAssemblyAttribute>() != null
                                        || assembly.GetCustomAttribute<PluginAssemblyAttribute>() != null))
                .ToArray();
        }

        public static void Dispose()
        {
            _container.Dispose();
        }
    }
}
