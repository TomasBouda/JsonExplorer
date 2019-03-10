using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TomLabs.WPFTools.Core.ViewModels.Base;

namespace TomLabs.JsonExplorer.App.ViewModels.Json
{
	public class JTWrapper : BaseViewModel
	{
		public JToken JToken { get; private set; }

		public JTokenType Type => JToken.Type;

		public Type TokenType => JToken.GetType();

		public bool ShowInSummary { get; set; }

		public ObservableCollection<JTWrapper> Children => new ObservableCollection<JTWrapper>(JToken.Children().Select(j => new JTWrapper(j)));

		public ObservableCollection<JTWrapper> FirstChild => Children.First().Children;

		public JTWrapper(JToken jToken)
		{
			JToken = jToken;
		}

		//public override string ToString() => JToken.ToString().Replace(Environment.NewLine, "");
	}
}