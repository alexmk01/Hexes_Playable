namespace Zenject
{
	public class TickablesTaskUpdater : TaskUpdater<ITickable>
	{
		protected override void UpdateItem(ITickable task)
		{
			task.Tick();
		}
	}
}
