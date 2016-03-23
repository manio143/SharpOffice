using System;
using System.IO;
using System.Text;
using SharpOffice.Core.Configuration;
using SharpOffice.Core.Formats;
using YamlDotNet.Serialization;

namespace SharpOffice.Common.Configuration
{
    public class YamlConfigurationFormat : IConfigurationFormat
    {
        public void WriteConfiguration(IConfiguration data, Stream stream)
        {
            if(!(data is PropertyBasedConfiguration))
                throw new InvalidOperationException(
                    "The YamlConfigurationFormat doesn't support IConfiguration classes that do not derive from PropertyBasedConfiguration.");
            using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true) {AutoFlush = true})
                new Serializer().Serialize(writer, data);
        }

        public T ReadConfiguration<T>(Stream stream) where T : IConfiguration, new()
        {
            if (!typeof(T).IsSubclassOf(typeof(PropertyBasedConfiguration)))
                throw new InvalidOperationException(
                    "The YamlConfigurationFormat doesn't support IConfiguration classes that do not derive from PropertyBasedConfiguration.");
            return new Deserializer().Deserialize<T>(new StreamReader(stream));
        }

        public IConfiguration ReadConfiguration(Type configurationType, Stream stream)
        {
            return (IConfiguration)
                GetType()
                .GetMethod("ReadConfiguration", new[] { typeof(Stream) })
                .MakeGenericMethod(configurationType)
                .Invoke(this, new[] { stream });
        }
    }
}