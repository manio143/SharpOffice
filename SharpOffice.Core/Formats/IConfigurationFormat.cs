using System;
using SharpOffice.Core.Configuration;
using System.IO;

namespace SharpOffice.Core.Formats
{
    public interface IConfigurationFormat
    {
        void WriteConfiguration(IConfiguration data, Stream stream);
        T ReadConfiguration<T>(Stream stream) where T : IConfiguration, new();
        IConfiguration ReadConfiguration(Type configurationType, Stream stream);
    }
}