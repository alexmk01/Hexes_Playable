namespace Zenject
{
	public static class StaticContext
	{
		private static DiContainer _container;

		public static bool HasContainer => _container != null;

		public static DiContainer Container
		{
			get
			{
				if (_container == null)
				{
					_container = new DiContainer();
				}
				return _container;
			}
		}

		public static void Clear()
		{
			_container = null;
		}
	}
}
