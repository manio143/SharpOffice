using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SharpOffice.Core.Data;

namespace SharpOffice.Core.Formats
{
    public interface IFileFormat
    {
        IDataConverter Converter { get; }

        void WriteData(IData data, Stream stream);
        IData ReadData(Stream stream);
    }
}