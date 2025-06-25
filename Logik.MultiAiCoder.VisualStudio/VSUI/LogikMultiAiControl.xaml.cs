using System.Windows.Controls;
using System.Windows;
using Logik.MultiAiCoder.Engine;
using Logik.MultiAiCoder.VisualStudio.UIControls;

namespace Logik.MultiAiCoder.VisualStudio.VSUI
{
    public partial class LogikMultiAiControl : UserControl
    {
        private readonly PromptDispatcher _dispatcher = new PromptDispatcher();

        public LogikMultiAiControl()
        {
            InitializeComponent();
        }

        public async void UpdateContext(string filePath, string content)
        {
            await _dispatcher.UpdateContextAsync(filePath, content);
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new ConfigurationWindow();
            wnd.ShowDialog();
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            ResultsTab.Items.Clear();
            var providers = new System.Collections.Generic.List<string>();
            if (OpenAiToggle.IsChecked == true) providers.Add("OpenAI-gpt-4o");
            if (ClaudeToggle.IsChecked == true) providers.Add("Claude-3-opus");
            if (GeminiToggle.IsChecked == true) providers.Add("Gemini-1.5-pro");
            if (AzureToggle.IsChecked == true) providers.Add("AzureOpenAI-gpt-4o");

            var results = await _dispatcher.DispatchAsync(PromptBox.Text, providers);
            foreach (var r in results)
            {
                var tab = new TabItem { Header = $"{r.Provider} ({r.Model})" };
                tab.Content = new TextBox { Text = r.Content, IsReadOnly = true };
                ResultsTab.Items.Add(tab);
            }
        }
    }
}
