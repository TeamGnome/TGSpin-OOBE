using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace TeamGnome.Oobe
{
    /// <summary>
    ///     Interaction logic for CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : Page
    {
        public CreateUserPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NextButton.IsEnabled = false;
            if (string.IsNullOrEmpty(UserText.Text))
            {
                ShowWarning("You must enter a username.");
                return;
            }
            if (PassText.Password != PassConfirmText.Password)
            {
                ShowWarning("The passwords you entered do not match.");
                return;
            }

#if !DEBUG
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "net",
                Arguments = $"user {UserText.Text} {PassText.Password} /add",
                CreateNoWindow = true
            };

            Process p = Process.Start(psi);
            p.WaitForExit();

            psi.Arguments = $"localgroup Administrators {UserText.Text} /add";

            p = Process.Start(psi);
            p.WaitForExit();
#endif

            Application.Current.Shutdown();
        }

        private void ShowWarning(string inText)
        {
            WarningText.Text = inText;
            WarningText.Visibility = Visibility.Visible;
            NextButton.IsEnabled = true;
        }
    }
}