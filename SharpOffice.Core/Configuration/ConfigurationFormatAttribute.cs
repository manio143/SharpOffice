using System;

namespace SharpOffice.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ConfigurationFormatAttribute : Attribute
    {
        private readonly Type _configurationFormatType;

        public ConfigurationFormatAttribute(Type configurationFormatType)
        {
            _configurationFormatType = configurationFormatType;
        }

        public Type GetFormatType()
        {
            return _configurationFormatType;
        }
    }
}