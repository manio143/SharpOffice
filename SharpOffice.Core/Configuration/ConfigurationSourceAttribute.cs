using System;

namespace SharpOffice.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ConfigurationSourceAttribute : Attribute
    {
        private readonly string _sourcePath;

        public ConfigurationSourceAttribute(string sourcePath)
        {
            _sourcePath = sourcePath;
        }

        public string GetSourcePath()
        {
            return _sourcePath;
        }
    }
}