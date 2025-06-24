using System.Windows.Controls;
using System.Windows;

namespace Logik.MultiAiCoder.VS2022.UI
{
    public partial class MultiAiControl : UserControl
    {
        public MultiAiControl()
        {
            InitializeComponent();
        }

        public async void UpdateContext(string filePath, string content)
        {
            var dispatcher = new Engine.PromptDispatcher();
            await dispatcher.UpdateContextAsync(filePath, content);
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new ConfigurationWindow();
            wnd.ShowDialog();
        }
    }
}
