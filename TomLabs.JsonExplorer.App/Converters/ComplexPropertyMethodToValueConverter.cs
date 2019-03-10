using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TomLabs.JsonExplorer.App.ViewModels.Json;

namespace TomLabs.JsonExplorer.App.Converters
{
	// This converter is only used by JProperty tokens whose Value is Array/Object
	internal class ComplexPropertyMethodToValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return null;

			var jtWrapper = value as JTWrapper;
			return jtWrapper.Children.First().Children;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
		}
	}
}