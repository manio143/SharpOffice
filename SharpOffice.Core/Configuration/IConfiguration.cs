using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOffice.Core.Configuration
{
    public interface IConfiguration
    {
        T GetProperty<T>(string propertyName);
        void SetProperty<T>(string propertyName, T propertyValue);
        IEnumerable<KeyValuePair<string, object>> GetAllProperties();
    }
}
