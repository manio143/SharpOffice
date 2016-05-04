using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DryIoc;
using SharpOffice.Core.Configuration;

namespace SharpOffice.Core.Container
{
    public class ConfigurationsRegistrationModule : IRegistrationModule
    {
        private readonly List<Type> _configurations;

        public ConfigurationsRegistrationModule(Assembly[] assemblies)
        {
            _configurations = new List<Type>();

            foreach (var assembly in assemblies)
                _configurations.AddRange(assembly.GetTypes().Where(t => typeof (IConfiguration).IsAssignableFrom(t)));
        }

        public void Register(DryIoc.Container container)
        {
            foreach (var configuration in _configurations)
                container.Register(typeof (IConfiguration), configuration);
        }
    }
}