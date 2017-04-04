using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TeamGnome.Oobe
{
    /// <summary>
    ///     Interaction logic for ComputerNamePage.xaml
    /// </summary>
    public partial class ComputerNamePage : Page
    {
        private readonly char[] _illegalNameCharacters =
        {
            '\\',
            '/',
            ':',
            '*',
            '?',
            '\"',
            '<',
            '>',
            '|'
        };

        private readonly MainWindow _mainWindowHandle = Application.Current.Windows[0] as MainWindow;

        public ComputerNamePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NextButton.IsEnabled = false;
            if (string.IsNullOrEmpty(ComputerNameText.Text))
            {
                ShowWarning("No fields can be left empty.");
                return;
            }

            foreach (char t in _illegalNameCharacters)
            {
                if (ComputerNameText.Text.Contains(t))
                {
                    ShowWarning($"The name you entered contains one or more invalid characters.{Environment.NewLine}Invalid characters: \\ / : * ? \" < > |");
                    return;
                }
            }

#if !DEBUG
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "wmic",
                Arguments = $"wmic computersystem where caption='{Environment.GetEnvironmentVariable("COMPUTERNAME")}' call rename {ComputerNameText.Text}",
                CreateNoWindow = true
            };

            Process p = Process.Start(psi);
            p.WaitForExit();
#endif

            _mainWindowHandle.NavigatePage("CREATEUSER");
        }

        private void ShowWarning(string inText)
        {
            WarningText.Text = inText;
            WarningText.Visibility = Visibility.Visible;
            NextButton.IsEnabled = true;
        }
    }
}