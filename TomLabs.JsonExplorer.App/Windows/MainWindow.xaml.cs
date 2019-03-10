using System.Windows;
using TomLabs.JsonExplorer.App.ViewModels;

namespace TomLabs.JsonExplorer.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowViewModel();
		}
	}
}