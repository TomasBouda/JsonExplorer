using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TomLabs.JsonExplorer.App.ViewModels.Json;

namespace TomLabs.JsonExplorer.App.Converters
{
	public sealed class JArrayLengthConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var jtWrapper = value as JTWrapper;
			if (jtWrapper == null)
				throw new Exception("Wrong type for this converter");

			switch (jtWrapper.Type)
			{
				case JTokenType.Array:
					var arrayLen = jtWrapper.Children.Count();
					return string.Format("[{0}]", arrayLen);

				case JTokenType.Property:
					var propertyArrayLen = jtWrapper.Children.FirstOrDefault().Children.Count();
					return string.Format("[{0}]", propertyArrayLen);

				default:
					throw new Exception("Type should be JProperty or JArray");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
		}
	}
}