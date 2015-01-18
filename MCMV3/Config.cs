namespace MCMV3
{
	#region

	using System;
	using System.IO;
	using LitJson;

	#endregion

	public class Config
	{
		private static Config _instance;
		[JsonPropertyName("AdvancedArguments")] public String _mAdvancedArguments;
		[JsonPropertyName("Authenticator")] public String _mAuthenticator;
		[JsonPropertyName("JAVAPath")] public String _mJavaPath;

		[JsonPropertyName("LastVersion")] public String _mLastVersion;
		[JsonPropertyName("LaunchMode")] public String _mLaunchMode;
		[JsonPropertyName("MaxMemory")] public int _mMaxMemory;
		[JsonPropertyName("Password")] public String _mPassword;
		[JsonPropertyName("UserName")] public String _mUserName;

		static Config()
		{
			Load();
		}

		public static String JavPath
		{
			get { return _instance._mJavaPath; }
			set
			{
				_instance._mJavaPath = value;
				Save();
			}
		}

		public static String LastVersion
		{
			get { return _instance._mLastVersion; }
			set
			{
				_instance._mLastVersion = value;
				Save();
			}
		}

		public static String UserName
		{
			get { return _instance._mUserName; }
			set
			{
				_instance._mUserName = value;
				Save();
			}
		}

		public static String Authenticator
		{
			get { return _instance._mAuthenticator; }
			set
			{
				_instance._mAuthenticator = value;
				Save();
			}
		}

		public static String Password
		{
			get { return _instance._mPassword; }
			set
			{
				_instance._mPassword = value;
				Save();
			}
		}

		public static int MaxMemory
		{
			get { return _instance._mMaxMemory; }
			set
			{
				_instance._mMaxMemory = value;
				Save();
			}
		}

		public static String AdvancedArguments
		{
			get { return _instance._mAdvancedArguments; }
			set
			{
				_instance._mAdvancedArguments = value;
				Save();
			}
		}

		public static String LaunchMode
		{
			get { return _instance._mLaunchMode; }
			set
			{
				_instance._mLaunchMode = value;
				Save();
			}
		}

		public static void Save()
		{
			try
			{
				File.WriteAllText("mcm.cfg", JsonMapper.ToJson(_instance));
			}
			catch
			{
			}
		}

		public static void Load()
		{
			try
			{
				_instance = JsonMapper.ToObject<Config>(File.ReadAllText("mcm.cfg"));
			}
			catch
			{
				_instance = new Config();
			}
		}
	}
}