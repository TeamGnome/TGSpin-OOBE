using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TeamGnome.Oobe
{
    /// <summary>
    /// Interaction logic for InitialPage.xaml
    /// </summary>
    public partial class InitialPage : Page
    {
        readonly MainWindow _mainWindowHandle = Application.Current.Windows[0] as MainWindow;

        public InitialPage()
        {
            InitializeComponent();
        }

        public async Task WaitBeforeSwitch()
        {
            await Task.Delay(2800);
        }

        private async void InitialPageGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard initialAnimation = (Storyboard)FindResource("InitialAnimation");
            initialAnimation.Begin();

            await WaitBeforeSwitch();
            _mainWindowHandle.NavigatePage("COMPUTERNAME");
        }
    }
}
