using Windows.UI.Xaml.Controls;

namespace Hexapet
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();

            Movements.InitializeHats();

            Controllers.XboxJoystickInit();
        }
    }
}
