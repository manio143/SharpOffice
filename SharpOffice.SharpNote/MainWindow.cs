using Xwt;

namespace SharpOffice.SharpNote
{
    public class MainWindow : Xwt.Window
    {
        public MainWindow()
        {
            //DEMO
            Title = "Xwt Demo Application";
            Width = 500;
            Height = 400;
            Closed += (sender, args) => Xwt.Application.Exit();
        }
    }
}
