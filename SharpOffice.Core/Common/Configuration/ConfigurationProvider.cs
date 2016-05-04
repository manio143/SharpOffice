using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SharpOffice.Core.Configuration;
using SharpOffice.Core.Formats;

namespace SharpOffice.Common.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly List<IConfiguration> _configurations;
        private readonly IConfigurationFormat _defaultConfigurationFormat;

        public ConfigurationProvider(IConfigurationFormat defaultFormat, IEnumerable<IConfiguration> configurations)
        {
            _configurations = configurations.ToList();
            _defaultConfigurationFormat = defaultFormat;

            LoadAll();
        }

        private void LoadAll()
        {
            for (int i = 0; i < _configurations.Count; i++)
            {
                var configuration = _configurations[i];
                var fileName = GetFileName(configuration);
                var format = GetFormat(configuration);
                if (File.Exists(fileName))
                    _configurations[i] = format.ReadConfiguration(configuration.GetType(), File.OpenRead(fileName));
            }
        }

        private IConfigurationFormat GetFormat(IConfiguration configuration)
        {
            var formatAttribute = configuration.GetType().GetCustomAttribute<ConfigurationFormatAttribute>();
            if (formatAttribute == null)
                return _defaultConfigurationFormat;
            return (IConfigurationFormat) Activator.CreateInstance(formatAttribute.GetFormatType());
        }

        private string GetFileName(IConfiguration configuration)
        {
            var sourceAttribute = configuration.GetType().GetCustomAttribute<ConfigurationSourceAttribute>();
            if (sourceAttribute == null)
                return configuration.GetType().Name.ToLower() + ".config";
            return sourceAttribute.GetSourcePath();
        }

        public T GetConfiguration<T>() where T : IConfiguration
        {
            return (T)_configurations.FirstOrDefault(cfg => cfg is T);
        }

        public void SaveChanges(IConfiguration config)
        {
            var fileName = GetFileName(config);
            var format = GetFormat(config);
            format.WriteConfiguration(config, File.OpenWrite(fileName));
            UpdateLocalList(config);
        }

        private void UpdateLocalList(IConfiguration config)
        {
            var configuration = _configurations.FirstOrDefault(cfg => cfg.GetType() == config.GetType());
            if (configuration != null)
                _configurations.Remove(configuration);
            _configurations.Add(config);
        }
    }
}