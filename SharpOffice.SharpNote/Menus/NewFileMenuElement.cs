using System;
using SharpOffice.Core.Window;

namespace SharpOffice.SharpNote.Menus
{
    public class NewFileMenuElement : IMenuElement
    {
        public string Label { get { return "_Source file"; } }
        public bool Enabled { get { return true; } }
        public bool? Checked { get { return null; } }
        public Menu SubMenu { get { return null; }}

        public void Command(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}