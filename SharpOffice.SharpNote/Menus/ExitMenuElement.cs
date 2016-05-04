using SharpOffice.Core.Window;

namespace SharpOffice.SharpNote.Menus
{
    public class ExitMenuElement : SingleMenuItem
    {
        public override void Command(object sender, System.EventArgs args)
        {
            
        }

        public ExitMenuElement() : base("E_xit")
        {
        }
    }
}