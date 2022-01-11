using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ufht_UI.Models;

namespace ufht_UI.DialogWindow
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsManager _settingsManager;
        private Settings _settings;

        public SettingsWindow(SettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            _settings = _settingsManager.UserSettings;

            Application.Current.Resources["OpacityText"] = ((int)(_settings.Opacity * 100)).ToString();
            Application.Current.Resources["StartingHeightText"] = ((int)_settings.DefaultSizeY).ToString();
            Application.Current.Resources["StartingWidthText"] = ((int)_settings.DefaultSizeX).ToString();

            InitializeComponent();
        }

        private void OpacityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = (TextBox) sender;
            Trace.WriteLine($"---------------------------------\n{text.Text}\n------------------------------------");
        }
        private void OpacityTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textbox = (TextBox) sender;

            Regex regex = new Regex(@"^(?:\d{1}){1,3}$");
            Trace.WriteLine("LENGTH " + textbox.Text.Length);
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void StartingHeightTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (StartingWidthTextBox != null)
            {
                StartingWidthTextBox.Text = textbox.Text;
            }
        }

        private void StartingHeightTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textbox = (TextBox)sender;

            Regex regex = new Regex(@"^(?:\d{1})$");
            e.Handled = !regex.IsMatch(e.Text);
            
        }

        private void StartingWidthTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (StartingHeightTextBox != null)
            {
                StartingHeightTextBox.Text = textbox.Text;
            }
        }

        private void StartingWidthTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textbox = (TextBox)sender;

            Regex regex = new Regex(@"^(?:\d{1})$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            Double opacity, height, width;

            if (Double.TryParse(OpacityTextBox.Text, out opacity) )
            {
                _settings.Opacity = opacity/100.0;
            }

            if (Double.TryParse(StartingHeightTextBox.Text, out height))
            {
                _settings.DefaultSizeY = height;
                _settings.DefaultSizeX = height;
            }

            if (Double.TryParse(StartingWidthTextBox.Text, out width))
            {
                _settings.DefaultSizeX = height;
                _settings.DefaultSizeY = height;
            }

            _settingsManager.SaveSettings();

            Close();
        }

    }
}
