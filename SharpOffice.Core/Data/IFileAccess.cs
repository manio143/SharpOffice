using System.IO;

namespace SharpOffice.Core.Data
{
    public interface IFileAccess
    {
        Stream this[string name] { get; }
        Stream GetFile(string name);
    }
}