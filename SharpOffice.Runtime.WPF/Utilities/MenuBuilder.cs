using System;
using System.Collections.Generic;
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
            //TODO: walk through MenuElements and generate WPFMenuItems tree
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
                    menuItem.Command = new Command(menuElement.Command, menuItem);
                menu.Add(menuItem);
            }
        }

        public void UpdateMenu()
        {
            if (RootMenu == null)
                BuildObjectTree();
            //TODO: update changes in the menu
        }

        public void WalkMenuTree(ItemCollection menu, IEnumerable<IMenuElement> elements, Action<IMenuElement, ItemCollection> action)
        {
            foreach (var menuElement in elements)
            {
                action(menuElement, menu);
            }
        }
    }
}