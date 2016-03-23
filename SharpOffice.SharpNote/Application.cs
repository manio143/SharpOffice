using SharpOffice.Common.Configuration;
using SharpOffice.Core;
using SharpOffice.Core.Formats;
using DryIoc;

namespace SharpOffice.SharpNote
{
    public class Application : IApplication
    {
        public string Name
        {
            get { return "SharpNote"; }
        }

        public void RegisterCustomTypes(DryIoc.Container container)
        {
            container.Register<IConfigurationFormat, YamlConfigurationFormat>();
        }
    }
}
