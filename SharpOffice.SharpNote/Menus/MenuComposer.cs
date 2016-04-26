using SharpOffice.Core.Window;

namespace SharpOffice.SharpNote.Menus
{
    public class MenuComposer : IMenuComposer
    {
        private IMenuProvider _menuProvider;
        public void Setup(IMenuProvider menuProvider)
        {
            _menuProvider = menuProvider;
            FileMenuSetup();
        }

        private void FileMenuSetup()
        {
            var file = _menuProvider.GetOrCreateMenu<FileMenu>();

            var newMenu = new NewSubMenu();
            newMenu.SubMenu.AddMultiple(new IMenuElement[]
            {
                new NewFileMenuElement(),
                new NewNoteMenuElement()
            });

            file.SubMenu.AddMultiple(new IMenuElement[]
            {
                newMenu,
                new OpenMenuElement(),
                new MenuSeparator(), 
                new ExitMenuElement()
            });
        }
    }
}