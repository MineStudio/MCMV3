using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using KMCCC.Launcher;

namespace MCMV3
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		private MainWindow mainWindow;

		public static LauncherCore core = LauncherCore.Create(LauncherCoreCreationOption.Create());

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			mainWindow = new MainWindow();
			mainWindow.Show();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			Thread.Sleep(1000);
			Logger.End();
		}
	}
}
