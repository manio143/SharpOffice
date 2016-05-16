using System;
using System.IO;
using System.Text;
using NLog;
using SharpOffice.Core.Configuration;
using SharpOffice.Core.Formats;
using YamlDotNet.Serialization;

namespace SharpOffice.Common.Configuration
{
    public class YamlConfigurationFormat : IConfigurationFormat
    {
        private static readonly Logger Logger = LogManager.GetLogger("YamlConfigFormat");

        public void WriteConfiguration(IConfiguration data, Stream stream)
        {
            if(!(data is PropertyBasedConfiguration))
                throw new InvalidOperationException(
                    "The YamlConfigurationFormat doesn't support IConfiguration classes that do not derive from PropertyBasedConfiguration.");

            Logger.Debug("Writing {0} in YAML to a stream...", data.GetType().Name);
            using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true) {AutoFlush = true})
                new Serializer().Serialize(writer, data);
        }

        public T ReadConfiguration<T>(Stream stream) where T : IConfiguration, new()
        {
            if (!typeof(T).IsSubclassOf(typeof(PropertyBasedConfiguration)))
                throw new InvalidOperationException(
                    "The YamlConfigurationFormat doesn't support IConfiguration classes that do not derive from PropertyBasedConfiguration.");

            Logger.Debug("Reading {0} in YAML from a stream...", typeof(T));
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