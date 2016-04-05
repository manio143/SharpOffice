using System.Collections.Generic;

namespace SharpOffice.Core.Window
{
    public interface IMenuProvider
    {
        IEnumerable<IMenuElement> TopLevelMenus { get; }
    }
}