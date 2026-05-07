using System;
using System.Text.RegularExpressions;

namespace Zenject
{
	[NoReflectionBaking]
	public class ProfileBlock : IDisposable
	{
		public static Regex ProfilePattern { get; set; }

		private ProfileBlock(string sampleName, bool rootBlock)
		{
		}

		private ProfileBlock(string sampleName)
			: this(sampleName, false)
		{
		}

		public static ProfileBlock Start()
		{
			return null;
		}

		public static ProfileBlock Start(string sampleNameFormat, object obj1, object obj2)
		{
			return null;
		}

		public static ProfileBlock Start(string sampleNameFormat, object obj)
		{
			return null;
		}

		public static ProfileBlock Start(string sampleName)
		{
			return null;
		}

		public void Dispose()
		{
		}
	}
}
