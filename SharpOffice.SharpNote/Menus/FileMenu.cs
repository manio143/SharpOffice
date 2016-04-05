using System;
using SharpOffice.Core.Window;

namespace SharpOffice.SharpNote.Menus
{
    [TopLevelMenu]
    public class FileMenu : IMenuElement
    {
        public string Label { get { return "_File"; } }
        public bool Enabled { get { return true; }}
        public bool? Checked { get { return null; } }

        private readonly Menu _subMenu = new Menu();
        public Menu SubMenu
        {
            get { return _subMenu; }
        }

        public void Command(object sender, EventArgs args)
        {
            throw new InvalidOperationException();
        }
    }
}