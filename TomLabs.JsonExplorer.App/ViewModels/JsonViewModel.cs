using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using TomLabs.JsonExplorer.App.ViewModels.Json;
using TomLabs.WPFTools.Core.ViewModels.Base;

namespace TomLabs.JsonExplorer.App.ViewModels
{
	public class JsonViewModel : BaseViewModel
	{
		public ObservableCollection<JTWrapper> Json { get; set; } = new ObservableCollection<JTWrapper>();

		public JsonViewModel()
		{
			var children = new List<JToken>();
			try
			{
				var client = new WebClient { Proxy = null };
				client.DownloadStringCompleted += delegate (object sender, DownloadStringCompletedEventArgs args)
				{
				};

				var serilog = "[" + File.ReadAllText(@"C:\Data\WORK\FC\Source\DailyTasks\bin\Debug\log-2019030711.json").Replace($"}}{Environment.NewLine}", $"}},{Environment.NewLine}") + "]";

				var token = JToken.Parse(serilog);

				if (token != null)
				{
					Json.Add(new JTWrapper(token));
				}

				// Choose 1
				//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/posts"));
				//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/comments"));
				//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/albums"));
				//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/photos"));
				//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/todos"));
				//client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/users"));
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not open the JSON string:\r\n" + ex.Message);
			}
		}
	}
}