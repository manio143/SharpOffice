using DryIoc;
using SharpOffice.Common.Configuration;
using SharpOffice.Core.Configuration;

namespace SharpOffice.Core.Container
{
    public class ConfigurationProviderRegistrationModule : IRegistrationModule
    {
        public void Register(DryIoc.Container container)
        {
            container.Register<IConfigurationProvider, ConfigurationProvider>(Reuse.Singleton);
        }
    }
}