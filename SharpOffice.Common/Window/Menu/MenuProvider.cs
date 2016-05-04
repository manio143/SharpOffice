using System;
using System.Collections.Generic;
using System.Linq;
using SharpOffice.Core.Window;

namespace SharpOffice.Common.Window.Menu
{
    public class MenuProvider : IMenuProvider
    {
        public IEnumerable<IMenuElement> TopLevelMenus { get { return _menu.Values; } }
        private readonly Dictionary<Type, IMenuElement> _menu = new Dictionary<Type, IMenuElement>();

        public T GetOrCreateMenu<T>() where T : IMenuElement, new()
        {
            if (!_menu.ContainsKey(typeof (T)))
                _menu[typeof (T)] = new T();
            return (T)_menu[typeof (T)];
        }
    }
}