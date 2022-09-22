using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TomLabs.WPFTools.Core.Commands;
using TomLabs.WPFTools.Core.ViewModels.Base;

namespace TomLabs.JsonExplorer.App.ViewModels.Json
{
	public class JTWrapper : BaseViewModel
	{
		private static JTWrapper ROOT { get; set; }

		private JTWrapper Parent { get; set; }

		public int Level { get; private set; } = 0;

		public JToken JToken { get; private set; }

		public JTokenType Type => JToken.Type;

		public Type SystemType => JToken.GetType();

		public string Name => (JToken as JProperty)?.Name ?? "";

		public bool ShowInSummary { get; set; }

		public ObservableCollection<JTWrapper> Children { get; set; }

		public JTWrapper FirstChild => Children.FirstOrDefault();

		public ObservableCollection<JTWrapper> FirstChildChildren => FirstChild?.Children;

		public ObservableCollection<JTWrapper> ChildrenInSummary { get; set; } = new ObservableCollection<JTWrapper>();

		public bool IsExpanded { get; set; }

		public ICommand AddToSummary { get; set; }
		public ICommand RemoveFromSummary { get; set; }

		public JTWrapper()
		{
			AddToSummary = new RelayCommand(() => SetShowInSummary(true));
			RemoveFromSummary = new RelayCommand(() => SetShowInSummary(false));
		}

		public JTWrapper(JToken jToken, JTWrapper parent = null, int level = 0) : this()
		{
			JToken = jToken;
			Parent = parent;
			Level = level;
			if (Level == 0 && Parent == null)
			{
				ROOT = this;
			}

			Children = new ObservableCollection<JTWrapper>(JToken.Children().Select(j => new JTWrapper(j, this, Level + 1)));
		}

		private JTWrapper GetParent(int level)
		{
			var parent = Parent;
			while (parent.Level != level)
			{
				parent = parent.Parent;
			}

			return parent;
		}

		private void SetShowInSummary(bool value)
		{
			if (JToken is JProperty prop)
			{
				ROOT?.ReloadSummary(prop.Name, value, Level);
			}
		}

		public void ReloadSummary(string propName, bool value, int targetLevel)
		{
			var targetChildren = Children.ToList();
			while (!targetChildren.Any(c => c.Level == targetLevel))
			{
				targetChildren = targetChildren.Where(c => Children.Count > 0).SelectMany(c => c.Children).ToList();
			}

			foreach (var targetChild in targetChildren.Where(j => j.Level == targetLevel && j.ShowInSummary != value && j.SystemType == typeof(JProperty) && ((JProperty)j.JToken).Name == propName))
			{
				targetChild.ShowInSummary = value;

				if (targetChild.Parent?.Parent?.SystemType == typeof(JProperty))
				{
					// Skip to parent -> parent because we render JProperty's first child
					targetChild.Parent.Parent.ChildrenInSummary = new ObservableCollection<JTWrapper>(targetChild.Parent.Children.Where(j => j.ShowInSummary));
				}
				else
				{
					targetChild.Parent.ChildrenInSummary = new ObservableCollection<JTWrapper>(targetChild.Parent.Children.Where(j => j.ShowInSummary));
				}
			}
		}
	}
}