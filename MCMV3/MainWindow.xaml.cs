using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KMCCC.Authentication;
using KMCCC.Launcher;

namespace MCMV3
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			var vers = App.core.GetVersions();
			var last = Config.LastVersion;
			List_ver.ItemsSource = vers;
			if (vers.Count(ver => ver.Id == last) > 0)
			{
				List_ver.SelectedItem = vers.First(ver => ver.Id == last);
			}
			else if (vers.Length > 0)
			{
				List_ver.SelectedItem = vers[0];
			}
			App.core.GameExit += this.OnExit;
			App.core.GameLog += this.OnLog;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void Btn_launch_Click(object sender, RoutedEventArgs e)
		{
			var ver = (Version)List_ver.SelectedItem;
			Config.LastVersion = ver.Id;
			var result = App.core.Launch(new LaunchOptions
			{
				Version = ver,
				MaxMemory = Config.MaxMemory,
				Authenticator = (Config.Authenticator == "Yggdrasil") ?
				((IAuthenticator)new YggdrasilLogin(Config.UserName, Config.Password, true)) : ((IAuthenticator)new OfflineAuthenticator(Config.UserName)),
				Mode = (Config.LaunchMode=="BMCL")?LaunchMode.BMCL:((Config.LaunchMode=="MCLauncher")?LaunchMode.MCLauncher:LaunchMode.Own)
			}, args => { args.AdvencedArguments.Add(Config.AdvancedArguments); });
			if (!result.Success)
			{
				MessageBox.Show(result.ErrorMessage, result.ErrorType.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
				switch (result.ErrorType)
				{
					case ErrorType.NoJAVA:
					case ErrorType.AuthenticationFailed:
						new ConfigWindow { Owner = this }.ShowDialog();
						break;
					default:

						break;
				}
			}
			else
			{
				this.Hide();
			}
		}

		private void OnLog(LaunchHandle handle, string line)
		{
			Logger.Log(line);
		}

		private void OnExit(LaunchHandle handle, int code)
		{
			this.Dispatcher.Invoke((System.Action<int>)this.onExit, code);

		}

		private void onExit(int code)
		{
			if (code == 0)
			{
				this.Close();
			}
			else
			{
				MessageBox.Show("Minecraft已经崩溃，详见mcm.log");
				this.Close();
			}
		}

		private void Btn_config_Click(object sender, RoutedEventArgs e)
		{
			new ConfigWindow { Owner = this }.ShowDialog();
		}
	}
}
