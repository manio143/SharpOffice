using SharpOffice.Core.Data;
using System.IO;
using SharpOffice.Core.Formats;

namespace SharpOffice.Core.IO
{
    public static class File
    {
        public static DataContainer Open(string fileName, IFileFormat format)
        {
            return Open(System.IO.File.OpenRead(fileName), format);
        }

        public static DataContainer Open(Stream stream, IFileFormat format)
        {
            return format.ReadData(stream);
        }

        public static void Save(string fileName, DataContainer data, IFileFormat format)
        {
            Save(System.IO.File.OpenWrite(fileName), data, format);
        }

        public static void Save(Stream stream, DataContainer data, IFileFormat format)
        {
            format.WriteData(data, stream);
        }
    }
}
