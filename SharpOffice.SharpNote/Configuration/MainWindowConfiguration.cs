using SharpOffice.Common.Configuration;
using SharpOffice.Core.Configuration;

namespace SharpOffice.SharpNote.Configuration
{
    [ConfigurationSource("sharpnote-mainwindow.config")]
    [ConfigurationFormat(typeof(YamlConfigurationFormat))]
    public class MainWindowConfiguration : PropertyBasedConfiguration
    {

    }
}