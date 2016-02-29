using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
            using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true) { AutoFlush = true })
            {
                new Serializer().Serialize(writer, data.GetAllProperties().Select(
                    kvp => new Pair<string, Pair<string, object>>(
                        kvp.Key, new Pair<string, object>(kvp.Value.GetType().ToString(), kvp.Value))));
            }
        }

        public T ReadConfiguration<T>(Stream stream) where T : IConfiguration, new()
        {
            var data = new Deserializer().Deserialize<IEnumerable<Pair<string, Pair<string, object>>>>(new StreamReader(stream));
            var config = new T();
            foreach (var keyValuePair in data)
            {
                var type = Type.GetType(keyValuePair.Value.Key);
                object value = null;
                try
                {
                    if (!type.IsInstanceOfType(keyValuePair.Value.Value))
                        value = TypeDescriptor.GetConverter(type)
                            .ConvertFromInvariantString((string) keyValuePair.Value.Value);
                    else
                        value = keyValuePair.Value.Value;
                }
                catch (InvalidCastException)
                {
                    value = TypeDescriptor.GetConverter(type).IsValid(keyValuePair.Value.Value)
                        ? TypeDescriptor.GetConverter(type).ConvertTo(keyValuePair.Value.Value, type)
                        : keyValuePair.Value.Value;
                }
                catch (NullReferenceException)
                {
                    value = keyValuePair.Value.Value;
                }
                config.SetProperty(keyValuePair.Key, value);
            }
            return config;
        }

        [Serializable]
        private class Pair<TKey, TValue>
        {
            public Pair() { }

            public Pair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public TKey Key { get; set; }
            public TValue Value { get; set; }
        }
    }
}