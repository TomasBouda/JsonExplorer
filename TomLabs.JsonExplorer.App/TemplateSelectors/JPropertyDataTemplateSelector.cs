using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Controls;
using TomLabs.JsonExplorer.App.ViewModels.Json;

namespace TomLabs.JsonExplorer.App.TemplateSelectors
{
	public sealed class JPropertyDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate PrimitivePropertyTemplate { get; set; }
		public DataTemplate ComplexPropertyTemplate { get; set; }
		public DataTemplate ArrayPropertyTemplate { get; set; }
		public DataTemplate ObjectPropertyTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item == null)
				return null;

			var frameworkElement = container as FrameworkElement;
			if (frameworkElement == null)
				return null;

			var jtWrapper = item as JTWrapper;
			if (jtWrapper.TokenType == typeof(JProperty))
			{
				var jProperty = jtWrapper.JToken as JProperty;
				switch (jProperty.Value.Type)
				{
					case JTokenType.Object:
						return ObjectPropertyTemplate;

					case JTokenType.Array:
						return ArrayPropertyTemplate;

					default:
						return PrimitivePropertyTemplate;
				}
			}

			var key = new DataTemplateKey(jtWrapper.TokenType);
			return frameworkElement.FindResource(key) as DataTemplate;
		}
	}
}