namespace Zenject
{
	[NoReflectionBaking]
	public class NonLazyBinder : IfNotBoundBinder
	{
		public NonLazyBinder(BindInfo bindInfo)
			: base(bindInfo)
		{
		}

		public IfNotBoundBinder NonLazy()
		{
			base.BindInfo.NonLazy = true;
			return this;
		}

		public IfNotBoundBinder Lazy()
		{
			base.BindInfo.NonLazy = false;
			return this;
		}
	}
}
