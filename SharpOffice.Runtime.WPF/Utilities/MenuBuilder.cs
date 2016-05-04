using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using SharpOffice.Core.Window;
using WPFMenu = System.Windows.Controls.Menu;

namespace SharpOffice.Window.Runtime.WPF.Utilities
{
    public class MenuBuilder
    {
        private readonly IMenuProvider _menuProvider;
        public WPFMenu RootMenu { get; private set; }

        public MenuBuilder(IMenuProvider menuProvider)
        {
            _menuProvider = menuProvider;
        }

        private void BuildObjectTree()
        {
            RootMenu = new WPFMenu();
            WalkMenuTree(RootMenu.Items, _menuProvider.TopLevelMenus, CreateNewMenuItem);
        }

        private void CreateNewMenuItem(IMenuElement menuElement, ItemCollection menu)
        {
            if (menuElement is MenuSeparator)
                menu.Add(new Separator());
            else
            {
                MenuItem menuItem = new MenuItem
                {
                    Header = menuElement.Label,
                    IsCheckable = menuElement.Checked != null,
                    IsChecked = menuElement.Checked ?? false,
                    IsEnabled = menuElement.Enabled, 
                    Icon = IconFactory.GetIconFromAttribute(menuElement),
                    Tag = menuElement //binds this MenuItem object with IMenuElement for later update possibility
                };
                if (menuElement.SubMenu != null)
                    WalkMenuTree(menuItem.Items, menuElement.SubMenu.Items, CreateNewMenuItem);
                else
                    menuItem.Click += menuElement.Command;
                menu.Add(menuItem);
            }
        }

        public void WalkMenuTree<T>(ItemCollection menu, IEnumerable<T> elements, Action<T, ItemCollection> action)
        {
            foreach (var element in elements)
            {
                action(element, menu);
            }
        }

        public void BuildMenu()
        {
            if (RootMenu == null)
                BuildObjectTree();
        }
    }
}