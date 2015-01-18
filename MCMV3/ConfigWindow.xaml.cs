namespace MCMV3
{
	#region

	using System;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Globalization;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;

	#endregion

	/// <summary>
	///     ConfigWindow.xaml 的交互逻辑
	/// </summary>
	public partial class ConfigWindow
	{
		public ConfigWindow()
		{
			InitializeComponent();
			switch (Config.Authenticator)
			{
				case "Yggdrasil":
					Authenticator.SelectedIndex = 0;
					break;
				case "Offline":
					Authenticator.SelectedIndex = 1;
					break;
			}
			switch (Config.LaunchMode)
			{
				case "Default":
					LaunchMode.SelectedIndex = 0;
					break;
				case "BMCL":
					LaunchMode.SelectedIndex = 1;
					break;
				case "MCLauncher":
					LaunchMode.SelectedIndex = 2;
					break;
			}
			UserName.Text = Config.UserName;
			Password.Password = Config.Password;
			MaxMem.Text = Config.MaxMemory.ToString(CultureInfo.InvariantCulture);
			AdvArg.Text = Config.AdvancedArguments;
		}

		private void Label_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Process.Start("http://github.com/MineStudio");
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (Authenticator.SelectedItem == null)
			{
				MessageBox.Show("请选择验证方式");
				e.Cancel = true;
				return;
			}
			switch ((String) ((dynamic) Authenticator.SelectedItem).Tag)
			{
				case "Yggdrasil":
					if (String.IsNullOrWhiteSpace(UserName.Text))
					{
						MessageBox.Show("请输入Email");
						e.Cancel = true;
						return;
					}
					if (String.IsNullOrWhiteSpace(Password.Password))
					{
						MessageBox.Show("请输入密码");
						e.Cancel = true;
						return;
					}
					Config.Authenticator = "Yggdrasil";
					Config.UserName = UserName.Text;
					Config.Password = Password.Password;
					break;
				case "Offline":
					if (String.IsNullOrWhiteSpace(UserName.Text))
					{
						MessageBox.Show("请输入用户名");
						e.Cancel = true;
						return;
					}
					Config.Authenticator = "Offline";
					Config.UserName = UserName.Text;
					break;
				default:
					e.Cancel = true;
					return;
			}
			if (LaunchMode.SelectedIndex == -1)
			{
				MessageBox.Show("请选择启动模式");
				e.Cancel = true;
				return;
			}
			Config.LaunchMode = (String) ((dynamic) LaunchMode.SelectedItem).Tag;
			int maxMem;
			if (!int.TryParse(MaxMem.Text, out maxMem))
			{
				MessageBox.Show("请输入正确的最大内存");
				e.Cancel = true;
				return;
			}
			Config.MaxMemory = maxMem;
			Config.AdvancedArguments = AdvArg.Text;
		}

		private void Authenticator_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			switch ((String) ((dynamic) Authenticator.SelectedItem).Tag)
			{
				case "Yggdrasil":
					Password.IsEnabled = true;
					break;
				case "Offline":
					Password.IsEnabled = false;
					break;
			}
		}
	}
}