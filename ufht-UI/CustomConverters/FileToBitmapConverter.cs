using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ufht_UI.CustomConverters
{
    public class FileToBitmapConverter : IValueConverter
    {
        private static readonly Dictionary<string, BitmapImage> _locations =
            new Dictionary<string, BitmapImage>();
        private double _playerIconOpacity;
        private bool OnHuntMapLocation;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string filename))
            {
                return null;
            }

            if (!File.Exists(filename))
            {
                _playerIconOpacity = 0;
                filename = "./Images//Icons/086139_hr1.png"; //change this to error image
            }
            else
            {
                _playerIconOpacity = 0.7;
            }

            Application.Current.Resources["_playerIconOpacity"] = _playerIconOpacity;

            if (!_locations.ContainsKey(filename))
            {
                _locations.Add(filename,
                    new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{filename}",
                        UriKind.Absolute)));
            }

            return _locations[filename];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}