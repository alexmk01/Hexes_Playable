namespace Game.Infrastructure
{
	public static class CollectionExtensions
	{
		public static T[] CreateArray<T>(int length) where T : struct
		{
			return new T[length];
		}
	}
}
