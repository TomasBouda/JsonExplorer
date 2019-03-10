using TomLabs.WPFTools.Core.ViewModels.Base;

namespace TomLabs.JsonExplorer.App.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public JsonViewModel JsonViewModel { get; set; } = new JsonViewModel();

		public MainWindowViewModel()
		{
		}
	}
}