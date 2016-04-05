using System;
using SharpOffice.Core.Window;

namespace SharpOffice.SharpNote.Menus
{
    public class NewNoteMenuElement : SingleMenuItem
    {
        public NewNoteMenuElement() : base("_Note")
        {
        }

        public override void Command(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}