using System.Collections.Generic;
using System.Security.Policy;
using SharpOffice.Core.Data;

namespace SharpOffice.Common.Data
{
    public class PlainTextNode : INode
    {
        protected readonly INode ParentNode;
        protected readonly ICollection<IAttribute> AttributeCollection; 

        public PlainTextNode(IValue value, INode parentNode = null)
        {
            Value = value;
            ParentNode = parentNode;
            AttributeCollection = new List<IAttribute>();
        }

        public PlainTextNode(INode parentNode = null) : this(new StringValue(), parentNode) { }

        public INode GetParent()
        {
            return ParentNode;
        }

        public IList<INode> GetChildren()
        {
            return new INode[0];
        }

        public ICollection<IAttribute> GetAttributeCollection()
        {
            return AttributeCollection;
        }

        public IValue Value { get; set; }
    }
}