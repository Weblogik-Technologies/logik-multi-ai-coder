using System.Windows;
using Logik.MultiAiCoder.Engine;
using Logik.MultiAiCoder.VisualStudio.UIControls;

namespace Logik.MultiAiCoder.VisualStudio.UI
{
    public partial class AddPromptWindow : Window
    {
        public PromptConfiguration Configuration { get; private set; } = new PromptConfiguration();

        public AddPromptWindow()
        {
            InitializeComponent();
        }

        private void KeyBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var valid = !string.IsNullOrWhiteSpace(KeyBox.Text);
            KeyStatus.Text = valid ? "✓" : "✗";
            KeyStatus.Foreground = valid ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Configuration.Provider = ProviderBox.Text;
            Configuration.Model = ModelBox.Text;
            Configuration.ApiKey = KeyBox.Text;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
