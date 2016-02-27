using SharpOffice.Core.Configuration;
using System.IO;

namespace SharpOffice.Core.Formats
{
    public interface IConfigurationFormat
    {
        void WriteConfiguration(IConfiguration data, Stream stream);
        IConfiguration ReadConfiguration(Stream stream);
    }
}