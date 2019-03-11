using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TomLabs.JsonExplorer.App.ViewModels.Json;

namespace TomLabs.JsonExplorer.App.Converters
{
	public sealed class JTWrapperCollectionToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var jtWrapperColleciton = value as ObservableCollection<JTWrapper>;
			if (jtWrapperColleciton == null || jtWrapperColleciton.Count == 0)
				return "{ }";

			return string.Join(", ", jtWrapperColleciton.Select(j => j.JToken?.ToString() ?? ""));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
		}
	}
}