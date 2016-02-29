using System;
using System.Collections.Generic;
using SharpOffice.Core.Configuration;

namespace SharpOffice.Common.Tests.Configuration
{
    public class TestConfiguration : IConfiguration
    {
        private Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public Dictionary<string, object> Dictionary
        {
            get { return dictionary; }
            set { dictionary = value; }
        }

        public T GetProperty<T>(string propertyName)
        {
            object property = dictionary[propertyName];
            if (!(property is T))
                throw new ArgumentException("type T");
            return (T)property;
        }

        public void SetProperty<T>(string propertyName, T propertyValue)
        {
            object property;
            if (dictionary.TryGetValue(propertyName, out property))
            {
                if (!(property is T))
                    throw new ArgumentException("type T");
                dictionary[propertyName] = propertyValue;
            }
            else
                dictionary.Add(propertyName, propertyValue);
        }

        public IEnumerable<KeyValuePair<string, object>> GetAllProperties()
        {
            return dictionary;
        }
    }
}