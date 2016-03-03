using SharpOffice.Core.Data;

namespace SharpOffice.Common.Data
{
    public class PlainTextNode : INode
    {
        public PlainTextNode(IValue value)
        {
            Value = value;
        }

        public PlainTextNode() : this(new StringValue()) { }

        public INode GetParent()
        {
            return null;
        }

        public System.Collections.Generic.IEnumerable<INode> GetChildren()
        {
            return new INode[0];
        }

        public System.Collections.Generic.IEnumerable<IAttribute> GetAttributes()
        {
            throw new System.NotImplementedException();
        }

        public IValue Value { get; set; }
    }
}