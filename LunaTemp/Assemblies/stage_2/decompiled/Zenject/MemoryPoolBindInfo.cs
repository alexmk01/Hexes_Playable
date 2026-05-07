namespace Zenject
{
	[NoReflectionBaking]
	public class MemoryPoolBindInfo
	{
		public PoolExpandMethods ExpandMethod { get; set; }

		public int InitialSize { get; set; }

		public int MaxSize { get; set; }

		public MemoryPoolBindInfo()
		{
			ExpandMethod = PoolExpandMethods.OneAtATime;
			MaxSize = int.MaxValue;
		}
	}
}
