using System.Collections.Generic;

namespace SharpOffice.Core.Data
{
    public interface INode
    {
        INode GetParent();
        IEnumerable<INode> GetChildren();
        IEnumerable<IAttribute> GetAttributes();

        IValue Value { get; set; }
    }
}