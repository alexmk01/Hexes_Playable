using System;

namespace Zenject
{
	[Serializable]
	public class MemoryPoolSettings
	{
		public int InitialSize;

		public int MaxSize;

		public PoolExpandMethods ExpandMethod;

		public static readonly MemoryPoolSettings Default = new MemoryPoolSettings();

		public MemoryPoolSettings()
		{
			InitialSize = 0;
			MaxSize = int.MaxValue;
			ExpandMethod = PoolExpandMethods.OneAtATime;
		}

		public MemoryPoolSettings(int initialSize, int maxSize, PoolExpandMethods expandMethod)
		{
			InitialSize = initialSize;
			MaxSize = maxSize;
			ExpandMethod = expandMethod;
		}
	}
}
