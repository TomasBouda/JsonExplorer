using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using TomLabs.JsonExplorer.App.ViewModels.Json;
using TomLabs.WPFTools.Core.Commands;
using TomLabs.WPFTools.Core.ViewModels.Base;

namespace TomLabs.JsonExplorer.App.ViewModels
{
	public class JsonViewModel : BaseViewModel
	{
		public string Title { get; set; }

		public ObservableCollection<JTWrapper> Json { get; set; }

		public ICommand OpenJsonFileCmd { get; set; }
		public ICommand OpenSerilogFileCmd { get; set; }
		public ICommand ExpandAllCmd { get; set; }
		public ICommand CollapseAllCmd { get; set; }

		public JsonViewModel()
		{
			OpenJsonFileCmd = new RelayCommand(() => OpenFileDialog());
			OpenSerilogFileCmd = new RelayCommand(() => OpenFileDialog(true));
			ExpandAllCmd = new RelayCommand(ExpandAll);
			CollapseAllCmd = new RelayCommand(CollapseAll);

			Test();
		}

		private void Test()
		{
			var client = new WebClient { Proxy = null };
			client.DownloadStringCompleted += delegate (object sender, DownloadStringCompletedEventArgs args)
			{
				LoadJson(args.Result);
			};

			// Choose 1
			//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/posts"));
			//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/comments"));
			//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/albums"));
			//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/photos"));
			//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/todos"));			client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/users"));
		}

		public void OpenFileDialog(bool serilog = false)
		{
			using (var ofd = new CommonOpenFileDialog()
			{
				IsFolderPicker = false,
				InitialDirectory = "\\",
				EnsureFileExists = true,
				EnsurePathExists = true,
			})
			{
				ofd.Filters.Add(new CommonFileDialogFilter("Json", serilog ? "*.*" : "*.json"));
				if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
				{
					OpenFile(ofd.FileName, serilog);
				}
			}
		}

		private void ExpandAll()
		{
			ExpandOrCollapse(Json.First(), true);
		}

		private void CollapseAll()
		{
			ExpandOrCollapse(Json.First(), false);
		}

		private void ExpandOrCollapse(JTWrapper jTWrapper, bool expand)
		{
			jTWrapper.IsExpanded = expand;
			foreach (var child in jTWrapper.Children)
			{
				ExpandOrCollapse(child, expand);
			}
		}

		public void OpenFile(string filePath, bool serilog = false)
		{
			if (serilog)
			{
				LoadSerilogJson(File.ReadAllText(filePath));
			}
			else
			{
				LoadJson(File.ReadAllText(filePath));
			}

			Title = Path.GetFileName(filePath);
		}

		private void LoadSerilogJson(string serilogJson)
		{
			var json = "[" + serilogJson.Replace($"}}{Environment.NewLine}", $"}},{Environment.NewLine}") + "]";
			LoadJson(json);
		}

		private void LoadJson(string json)
		{
			try
			{
				var token = JToken.Parse(json);

				if (token != null)
				{
					Json = new ObservableCollection<JTWrapper>();
					Json.Add(new JTWrapper(token));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not open the JSON string:\r\n" + ex.Message);
			}
		}
	}
}