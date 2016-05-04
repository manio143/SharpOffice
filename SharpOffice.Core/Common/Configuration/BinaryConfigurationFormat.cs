using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOffice.Core.Configuration;
using SharpOffice.Core.Formats;

namespace SharpOffice.Common.Configuration
{
    public class BinaryConfigurationFormat : IConfigurationFormat
    {
        public void WriteConfiguration(IConfiguration data, Stream stream)
        {
            var writer = new BinaryWriter(stream);
            var formatter = new BinaryFormatter();
            foreach (var keyValuePair in data.GetAllProperties())
            {
                writer.Write(keyValuePair.Key);
                formatter.Serialize(stream, keyValuePair.Value);
            }
        }

        public T ReadConfiguration<T>(Stream stream) where T : IConfiguration, new()
        {
            var data = new List<KeyValuePair<string, object>>();
            var reader = new BinaryReader(stream);
            var formatter = new BinaryFormatter();
            var config = new T();
            try
            {
                while (true) 
                    data.Add(new KeyValuePair<string, object>(reader.ReadString(), formatter.Deserialize(stream)));
            }
            catch(EndOfStreamException)
            {
            }
            foreach (var keyValuePair in data)
                config.SetProperty(keyValuePair.Key, keyValuePair.Value);
            return config;
        }


        public IConfiguration ReadConfiguration(System.Type configurationType, Stream stream)
        {
            return (IConfiguration)
                GetType()
                .GetMethod("ReadConfiguration", new[] {typeof (Stream)})
                .MakeGenericMethod(configurationType)
                .Invoke(this, new[] {stream});
        }
    }
}