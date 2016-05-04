using SharpOffice.Core.Data;

namespace SharpOffice.Common.Data
{
    public class TextData : IData
    {
        private IMetadata _metadata;
        private INode _rootNode;

        public TextData(IMetadata metadata, INode rootNode)
        {
            _metadata = metadata;
            _rootNode = rootNode;
        }

        public TextData(IMetadata metadata) : this(metadata, new PlainTextNode()) { }

        public IMetadata Metadata
        {
            get { return _metadata; }
        }

        public INode RootNode
        {
            get { return _rootNode; }
        }

        public IFileAccess FileAccess
        {
            get { return null; }
        }
    }
}