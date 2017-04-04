using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace TeamGnome.Oobe
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double calcdWidth = ContentGrid.ActualWidth / ContentGrid.ActualHeight * 0.13;
            ContentGrid.ColumnDefinitions[0].Width = new GridLength(calcdWidth, GridUnitType.Star);
            ContentGrid.ColumnDefinitions[2].Width = ContentGrid.ColumnDefinitions[0].Width;
            NavigatePage("INITIALPAGE");
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            Storyboard frameAnimation = (Storyboard)FindResource("FrameEffect");
            frameAnimation.Begin();
        }

        public void NavigatePage(string pageName)
        {
            pageName = pageName.ToUpper();
            switch (pageName)
            {
                case "INITIALPAGE":
                    ContentFrame.Content = new InitialPage();
                    break;
                case "COMPUTERNAME":
                    ContentFrame.Content = new ComputerNamePage();
                    break;
                case "CREATEUSER":
                    ContentFrame.Content = new CreateUserPage();
                    break;
            }
        }
    }
}