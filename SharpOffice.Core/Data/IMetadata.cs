using System;

namespace SharpOffice.Core.Data
{
    public interface IMetadata
    {
        string FileName { get; }

        DateTime CreatedOn { get; }
        DateTime ModifiedOn { get; }

        bool IsReadOnly { get; }
    }
}