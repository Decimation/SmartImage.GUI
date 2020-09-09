using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartImage.GUI
{
	public static class MyWpfExtensions
	{
		public static System.Windows.Forms.IWin32Window GetIWin32Window(this System.Windows.Media.Visual visual)
		{
			var source = System.Windows.PresentationSource.FromVisual(visual) as System.Windows.Interop.HwndSource;
			System.Windows.Forms.IWin32Window win = new OldWindow(source.Handle);
			return win;
		}

		private class OldWindow : System.Windows.Forms.IWin32Window
		{
			private readonly System.IntPtr _handle;
			public OldWindow(System.IntPtr handle)
			{
				_handle = handle;
			}

			#region IWin32Window Members
			System.IntPtr System.Windows.Forms.IWin32Window.Handle
			{
				get { return _handle; }
			}
			#endregion
		}
	}
	internal static class RuntimeInfo
	{
		public const string EXE = "SmartImage.exe";


		internal static string LocateExe()
		{
			var path = Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.User).Split(";");

			string dir = path.FirstOrDefault(s => File.Exists(Path.Combine(s, EXE)));
			
			if (!String.IsNullOrWhiteSpace(dir))
			{
				return Path.Combine(dir, EXE);
			}

			return null;

		}

		internal static string Folder
		{
			get
			{
				var commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
				var configFolder = Path.Combine(commonAppData, "SmartImage.GUI");

				
				if (!Directory.Exists(configFolder)) {
					Directory.CreateDirectory(configFolder);
				}

				return configFolder;
			}
		}
	}
}
