using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DryIoc;
using SharpOffice.Core.Attributes;
using SharpOffice.Core.Configuration;
using SharpOffice.Core.Window;

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
            
            RegisterProviders();

            foreach (var assembly in assemblies)
            {
                if (assembly.GetName().Name == applicationAssemblyName)
                    RegisterApplication(assembly);

                RegisterConfigurations(assembly);
                RegisterMenus(assembly);
                //TODO: register types that may be plugged in (like file formats, data formats, etc.)
            }
        }

        private static void RegisterMenus(Assembly assembly)
        {
            var menuProvider = _container.Resolve<IMenuProvider>();
            var menuComposers = assembly.GetTypes().Where(t => typeof (IMenuComposer).IsAssignableFrom(t));
            foreach (var menuComposerType in menuComposers)
            {
                var menuComposer = (IMenuComposer) menuComposerType.GetConstructor(new Type[0]).Invoke(new object[0]);
                menuComposer.Setup(menuProvider);
            }
        }

        private static void RegisterConfigurations(Assembly assembly)
        {
            var configurations = assembly.GetTypes().Where(t => typeof(IConfiguration).IsAssignableFrom(t));
            foreach (var type in configurations)
                _container.Register(typeof(IConfiguration), type);
        }

        private static void RegisterProviders()
        {
            var commonAssembly = Assembly.Load(new AssemblyName("SharpOffice.Common"));
            RegisterConfigurationProvider(commonAssembly);
            RegisterMenuProvider(commonAssembly);
        }

        private static void RegisterConfigurationProvider(Assembly commonAssembly)
        {
            Type configurationProvider = commonAssembly
                .GetTypes()
                .First(t => t.Name == "ConfigurationProvider");
            _container.Register(typeof(IConfigurationProvider), configurationProvider, Reuse.Singleton);
        }

        private static void RegisterMenuProvider(Assembly commonAssembly)
        {
            Type menuProvider = commonAssembly
                .GetTypes()
                .First(t => t.Name == "MenuProvider");
            _container.Register(typeof(IMenuProvider), menuProvider, Reuse.Singleton);
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
            List<Assembly> list = new List<Assembly>();
            foreach (var assemblyFile in assemblyFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(assemblyFile);
                    if (assembly.GetCustomAttribute<CoreAssemblyAttribute>() == null &&
                        (assembly.GetCustomAttribute<ApplicationAssemblyAttribute>() != null ||
                         assembly.GetCustomAttribute<PluginAssemblyAttribute>() != null))
                        list.Add(assembly);
                }
                catch (BadImageFormatException)
                {
                    continue;
                }
            }
            return list.ToArray();
        }

        public static void Dispose()
        {
            _container.Dispose();
        }
    }
}
