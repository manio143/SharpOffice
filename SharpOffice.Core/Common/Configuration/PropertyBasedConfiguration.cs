using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharpOffice.Core.Configuration;

namespace SharpOffice.Common.Configuration
{
    public abstract class PropertyBasedConfiguration : IConfiguration
    {
        private PropertyInfo getPropertyInfo(string propertyName)
        {
            var property = GetType().GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException(String.Format("This configuration has no '{0}' property.", propertyName));
            return property;
        }

        public T GetProperty<T>(string propertyName)
        {
            var property = getPropertyInfo(propertyName);
            try
            {
                return (T)property.GetValue(this);
            }
            catch (NullReferenceException nrex)
            { throw new InvalidCastException("Can't cast null on to a value type.", nrex); }
        }

        public void SetProperty<T>(string propertyName, T propertyValue)
        {
            var property = getPropertyInfo(propertyName);
            try
            {
                property.SetValue(this, propertyValue);
            }
            catch (ArgumentException aex)
            {
                throw new InvalidCastException(String.Format("Object of type '{0}' cannot be converted to type '{1}'.",
                  typeof(T), property.GetGetMethod().ReturnType));
            }

        }

        public IEnumerable<KeyValuePair<string, object>> GetAllProperties()
        {
            return GetType().GetProperties().Select(propertyInfo =>
                new KeyValuePair<string, object>(propertyInfo.Name, propertyInfo.GetValue(this))).ToList();
        }
    }
}