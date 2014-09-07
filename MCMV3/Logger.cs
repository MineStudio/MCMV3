using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCMV3
{
	public static class Logger
	{
		static Logger()
		{
			tw = File.CreateText("mcm.log");
		}

		private static TextWriter tw;

		public static void Log(String log)
		{
			lock (tw)
			{
				tw.WriteLine(log);
			}
		}

		public static void End()
		{
			lock (tw)
			{
				tw.Flush();
				tw.Dispose();
			}
		}
	}
}
