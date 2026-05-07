namespace Zenject
{
	public interface IInstaller
	{
		bool IsEnabled { get; }

		void InstallBindings();
	}
}
