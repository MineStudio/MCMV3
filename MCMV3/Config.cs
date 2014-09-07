using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LitJson;

namespace MCMV3
{
	public class Config
	{
		private static Config instance;

		public static String JAVAPath { get { return instance._mJAVAPath; } set { instance._mJAVAPath = value; Save(); } }

		public static String LastVersion { get { return instance._mLastVersion; } set { instance._mLastVersion = value; Save(); } }

		public static String UserName { get { return instance._mUserName; } set { instance._mUserName = value; Save(); } }

		public static String Authenticator { get { return instance._mAuthenticator; } set { instance._mAuthenticator = value; Save(); } }

		public static String Password { get { return instance._mPassword; } set { instance._mPassword = value; Save(); } }

		public static int MaxMemory { get { return instance._mMaxMemory; } set { instance._mMaxMemory = value; Save(); } }

		public static String AdvancedArguments { get { return instance._mAdvancedArguments; } set { instance._mAdvancedArguments = value; Save(); } }

		public static String LaunchMode { get { return instance._mLaunchMode; } set { instance._mLaunchMode = value; Save(); } }

		static Config()
		{
			Load();
		}

		[JsonPropertyName("JAVAPath")]
		public String _mJAVAPath;

		[JsonPropertyName("LastVersion")]
		public String _mLastVersion;

		[JsonPropertyName("UserName")]
		public String _mUserName;

		[JsonPropertyName("Password")]
		public String _mPassword;

		[JsonPropertyName("Authenticator")]
		public String _mAuthenticator;

		[JsonPropertyName("MaxMemory")]
		public int _mMaxMemory;

		[JsonPropertyName("AdvancedArguments")]
		public String _mAdvancedArguments;

		[JsonPropertyName("LaunchMode")]
		public String _mLaunchMode;

		public static void Save()
		{
			try
			{
				File.WriteAllText("mcm.cfg", JsonMapper.ToJson(instance));
			}
			catch { }
		}

		public static void Load()
		{
			try
			{
				instance = JsonMapper.ToObject<Config>(File.ReadAllText("mcm.cfg"));
			}
			catch { instance = new Config(); }
		}
	}
}
