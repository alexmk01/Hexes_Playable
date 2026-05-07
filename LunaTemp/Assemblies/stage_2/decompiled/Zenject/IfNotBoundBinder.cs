namespace Zenject
{
	[NoReflectionBaking]
	public class IfNotBoundBinder
	{
		public BindInfo BindInfo { get; private set; }

		public IfNotBoundBinder(BindInfo bindInfo)
		{
			BindInfo = bindInfo;
		}

		public void IfNotBound()
		{
			BindInfo.OnlyBindIfNotBound = true;
		}
	}
}
