using System.Collections.Generic;
using System.IO;
using SharpOffice.Core.Configuration;
using SharpOffice.Core.Formats;
using YamlDotNet.Serialization;

namespace SharpOffice.Common.Configuration
{
    public class YamlConfigurationFormat : IConfigurationFormat
    {
        public void WriteConfiguration(IConfiguration data, Stream stream)
        {
            new Serializer().Serialize(new StreamWriter(stream), data.GetAllProperties());
        }

        public T ReadConfiguration<T>(Stream stream) where T:IConfiguration, new()
        {
            var data =
                new Deserializer().Deserialize<IEnumerable<KeyValuePair<string, object>>>(
                    new StreamReader(stream));
            var config = new T();
            foreach (var keyValuePair in data)
                config.SetProperty(keyValuePair.Key, keyValuePair.Value);
            return config;
        }
    }
}