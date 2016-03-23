using System.ComponentModel;

namespace SharpOffice.Core.Configuration
{
    public interface IConfigurationProvider
    {
        T GetConfiguration<T>() where T : IConfiguration;
        void SaveChanges(IConfiguration config);
    }
}