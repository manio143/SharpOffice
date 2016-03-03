namespace SharpOffice.Core.Data
{
    public interface IData
    {
        IMetadata Metadata { get; }
        INode RootNode { get; }
        IFileAccess FileAccess { get; }
    }
}