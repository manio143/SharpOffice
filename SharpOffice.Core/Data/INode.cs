using System.Collections.Generic;

namespace SharpOffice.Core.Data
{
    public interface INode
    {
        INode GetParent();
        IList<INode> GetChildren();

        ICollection<IAttribute> GetAttributeCollection();

        IValue Value { get; set; }
    }
}