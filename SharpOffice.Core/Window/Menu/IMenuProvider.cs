using System.Collections.Generic;

namespace SharpOffice.Core.Window
{
    public interface IMenuProvider
    {
        IEnumerable<IMenuElement> TopLevelMenus { get; }

        T GetOrCreateMenu<T>() where T : IMenuElement, new();
    }
}