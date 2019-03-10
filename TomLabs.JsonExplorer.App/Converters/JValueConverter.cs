using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Windows.Data;

namespace TomLabs.JsonExplorer.App.Converters
{
	public sealed class JValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var jval = value as JValue;
			if (jval != null)
			{
				switch (jval.Type)
				{
					case JTokenType.String:
						return "\"" + jval.Value + "\"";

					case JTokenType.Null:
						return "null";
				}
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
		}
	}
}