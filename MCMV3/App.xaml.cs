namespace MCMV3
{
	#region

	using System.Reflection;
	using System.Threading;
	using System.Windows;
	using KMCCC.Launcher;

	#endregion

	/// <summary>
	///     App.xaml 的交互逻辑
	/// </summary>
	public partial class App
	{
		public static LauncherCore Core = LauncherCore.Create();
		private MainWindow _mainWindow;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Reporter.SetClientName("MCMV3 @" + Assembly.GetExecutingAssembly().GetName().Version);
			_mainWindow = new MainWindow();
			_mainWindow.Show();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			Thread.Sleep(1000);
			Logger.End();
		}
	}
}