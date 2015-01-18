namespace MCMV3
{
	#region

	using System;
	using System.Linq;
	using System.Windows;
	using System.Windows.Input;
	using KMCCC.Authentication;
	using KMCCC.Launcher;
	using Version = KMCCC.Launcher.Version;

	#endregion

	/// <summary>
	///     MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			var last = Config.LastVersion;
			var versions = App.Core.GetVersions().ToArray();
			ListVersions.ItemsSource = versions;
			if (versions.Count(ver => ver.Id == last) > 0)
			{
				ListVersions.SelectedItem = versions.First(ver => ver.Id == last);
			}
			else if (versions.Any())
			{
				ListVersions.SelectedItem = versions[0];
			}
			App.Core.GameExit += OnExit;
			App.Core.GameLog += OnLog;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void Btn_launch_Click(object sender, RoutedEventArgs e)
		{
			var ver = (Version) ListVersions.SelectedItem;
			Config.LastVersion = ver.Id;
			var result = App.Core.Launch(new LaunchOptions
			{
				Version = ver,
				MaxMemory = Config.MaxMemory,
				Authenticator = (Config.Authenticator == "Yggdrasil")
					? new YggdrasilLogin(Config.UserName, Config.Password, true)
					: ((IAuthenticator) new OfflineAuthenticator(Config.UserName)),
				Mode = (Config.LaunchMode == "BMCL") ? (LaunchMode) LaunchMode.BmclMode : ((Config.LaunchMode == "MCLauncher") ? LaunchMode.MCLauncher : null)
			}, args => args.AdvencedArguments.Add(Config.AdvancedArguments));
			if (!result.Success)
			{
				MessageBox.Show(result.ErrorMessage, result.ErrorType.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
				switch (result.ErrorType)
				{
					case ErrorType.NoJAVA:
					case ErrorType.AuthenticationFailed:
						new ConfigWindow {Owner = this}.ShowDialog();
						break;
				}
			}
			else
			{
				Hide();
			}
		}

		private static void OnLog(LaunchHandle handle, string line)
		{
			Logger.Log(line);
		}

		private void OnExit(LaunchHandle handle, int code)
		{
			Dispatcher.Invoke((Action<int>) OnGameExit, code);
		}

		private void OnGameExit(int code)
		{
			if (code == 0)
			{
				Close();
			}
			else
			{
				MessageBox.Show("Minecraft已经崩溃，详见mcm.log");
				Close();
			}
		}

		private void Btn_config_Click(object sender, RoutedEventArgs e)
		{
			new ConfigWindow {Owner = this}.ShowDialog();
		}
	}
}