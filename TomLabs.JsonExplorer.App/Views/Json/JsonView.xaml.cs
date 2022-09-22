using System.Windows;
using System.Windows.Controls;
using TomLabs.JsonExplorer.App.ViewModels;

namespace TomLabs.JsonExplorer.App.Views.Json
{
	/// <summary>
	/// Interaction logic for JsonView.xaml
	/// </summary>
	public partial class JsonView : UserControl
	{
		public JsonView()
		{
			InitializeComponent();
		}

		private void Grid_Drop(object sender, System.Windows.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				// Note that you can have more than one file.
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

				var vm = DataContext as JsonViewModel;
				vm.OpenFile(files[0], true);
			}
		}
	}
}