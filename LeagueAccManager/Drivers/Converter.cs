using System;
using System.Globalization;
using System.Windows.Data;

namespace LeagueAccManager
{
    public class TagOrRegionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var account = value as Account;
            if (account == null)
                return string.Empty;

            return string.IsNullOrEmpty(account.CustomTag) ? account.Region : account.CustomTag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}