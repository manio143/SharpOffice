using SharpOffice.Core.Data;
using System.IO;
using SharpOffice.Core.Formats;

namespace SharpOffice.Common.IO
{
    public static class File
    {
        public static void WriteData(this Stream stream, IData data, IFileFormat fileFormat)
        {
            fileFormat.WriteData(data, stream);
        }

        public static IData ReadData(this Stream stream, IFileFormat fileFormat)
        {
            return fileFormat.ReadData(stream);
        }
    }
}
