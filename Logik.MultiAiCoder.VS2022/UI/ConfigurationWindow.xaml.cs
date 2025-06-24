using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Logik.MultiAiCoder.VS2022.UI
{
    public partial class ConfigurationWindow : Window
    {
        private readonly List<Engine.PromptConfiguration> _configs;

        public ConfigurationWindow()
        {
            InitializeComponent();
            _configs = Engine.PromptConfigurationStore.Load();
            ConfigGrid.ItemsSource = _configs;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddPromptWindow();
            if (dlg.ShowDialog() == true)
            {
                if (_configs.Any(c => c.Provider == dlg.Configuration.Provider && c.Model == dlg.Configuration.Model))
                {
                    MessageBox.Show("Configuration already exists");
                }
                else
                {
                    dlg.Configuration.Order = _configs.Count;
                    _configs.Add(dlg.Configuration);
                    ConfigGrid.Items.Refresh();
                }
            }
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            var idx = ConfigGrid.SelectedIndex;
            if (idx > 0)
            {
                var item = _configs[idx];
                _configs.RemoveAt(idx);
                _configs.Insert(idx - 1, item);
                ConfigGrid.Items.Refresh();
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            var idx = ConfigGrid.SelectedIndex;
            if (idx >= 0 && idx < _configs.Count - 1)
            {
                var item = _configs[idx];
                _configs.RemoveAt(idx);
                _configs.Insert(idx + 1, item);
                ConfigGrid.Items.Refresh();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            for (int i = 0; i < _configs.Count; i++)
            {
                _configs[i].Order = i;
            }
            Engine.PromptConfigurationStore.Save(_configs);
        }
    }
}
