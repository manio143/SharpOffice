using System;
using System.Linq;
using System.Reflection;
using DryIoc;

namespace SharpOffice.Core.Container
{
    public class ApplicationRegistrationModule : IRegistrationModule
    {
        private readonly Type _applicationType;

        public ApplicationRegistrationModule(Assembly applicationAssembly)
        {
            _applicationType = applicationAssembly.GetTypes().First(t => typeof(IApplication).IsAssignableFrom(t));
        }

        public void Register(DryIoc.Container container)
        {
            container.Register(typeof(IApplication), _applicationType);
        }
    }
}