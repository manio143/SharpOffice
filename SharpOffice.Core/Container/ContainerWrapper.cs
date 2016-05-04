using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SharpOffice.Core.Attributes;

namespace SharpOffice.Core.Container
{
    /// <summary>
    /// Provides access to the IoC container and registers all neccesary types for the application to run.
    /// </summary>
    public static class ContainerWrapper
    {
        private static DryIoc.Container _container;
        private static Assembly _applicationAssembly;

        public static DryIoc.Container GetContainer()
        {
            return _container;
        }

        public static void Initialize(string applicationAssemblyName)
        {
            _container = new DryIoc.Container();
            AutoRegister(applicationAssemblyName);
        }

        private static void AutoRegister(string applicationAssemblyName)
        {
            Assembly[] assemblies = GetAssemblies(applicationAssemblyName);

            List<IRegistrationModule> modules = new List<IRegistrationModule>();
            foreach (var assembly in assemblies.Concat(new[] {typeof (IRegistrationModule).Assembly})) //include SharpOffice.Core
                foreach (var type in assembly.GetTypes().Where(t => typeof (IRegistrationModule).IsAssignableFrom(t)))
                {
                    IRegistrationModule module;
                    var noParams = type.GetConstructor(new Type[0]);
                    var appParam = type.GetConstructor(new[] {typeof (Assembly)});
                    var allParam = type.GetConstructor(new[] {typeof (Assembly[])});
                    if (allParam != null)
                        module = (IRegistrationModule) Activator.CreateInstance(type, new object[] {assemblies});
                    else if (appParam != null)
                        module = (IRegistrationModule) Activator.CreateInstance(type, _applicationAssembly);
                    else if (noParams != null)
                        module = (IRegistrationModule) Activator.CreateInstance(type);
                    else
                        throw new MethodAccessException(String.Format("{0} has no valid constructor.", type.Name));
                    modules.Add(module);
                }
            foreach (var module in modules)
                module.Register(_container);
        }


        /// <summary>
        /// Get all assemblies that are not Core and are either Applications or Plugins.
        /// </summary>
        /// <returns>Array of such assemblies.</returns>
        private static Assembly[] GetAssemblies(string applicationName)
        {
            string path = Environment.CurrentDirectory;
            string[] assemblyFiles = Directory.GetFiles(path, "*.DLL");
            List<Assembly> apps = new List<Assembly>();
            List<Assembly> plugins = new List<Assembly>();
            foreach (var assemblyFile in assemblyFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(assemblyFile);

                    if (assembly.GetCustomAttribute<CoreAssemblyAttribute>() != null)
                        continue;

                    if (assembly.GetCustomAttribute<ApplicationAssemblyAttribute>() != null)
                        apps.Add(assembly);
                    else if (assembly.GetCustomAttribute<PluginAssemblyAttribute>() != null)
                        plugins.Add(assembly);
                }
                catch (BadImageFormatException)
                {
                    continue;
                }
            }
            _applicationAssembly = apps.First(a => a.GetName().Name == applicationName);
            plugins.Add(_applicationAssembly);
            return plugins.ToArray();
        }

        public static void Dispose()
        {
            _container.Dispose();
        }
    }
}
