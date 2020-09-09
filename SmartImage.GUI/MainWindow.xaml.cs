using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using Path = System.IO.Path;

namespace SmartImage.GUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void InstallExe_Click(object sender, RoutedEventArgs e)
		{
			var sz = "https://api.github.com/repos/Decimation/SmartImage/releases";
			var get = Util.Get(sz);
			var ja = JArray.Parse(get);

			var first = ja[0];

			var tagName = first["tag_name"];
			var url = first["html_url"];
			var publish = first["published_at"];


			var assets = first["assets"];
			var dlurl = assets[0]["browser_download_url"].ToString();

			var dest = RuntimeInfo.Folder;
			Log.Text += dest + "\n";
			new WebClient().DownloadFile(dlurl,Path.Combine(dest,RuntimeInfo.EXE));


			Log.Text += dlurl;
		}

		private void LocateExe_Click(object sender, RoutedEventArgs e)
		{
			var exe = RuntimeInfo.LocateExe();

			if (exe== null) {
				var dlg = new OpenFileDialog();
				System.Windows.Forms.DialogResult result = dlg.ShowDialog(this.GetIWin32Window());

				ExecutableLocation.Text = dlg.FileName;
			}
			else {
				ExecutableLocation.Text = exe;
			}
			
			
			
		}

		private void ExecutableLocation_TextChanged(object sender, TextChangedEventArgs e)
		{
			
		}
	}
}
