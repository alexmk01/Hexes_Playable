namespace Zenject
{
	public class FixedTickablesTaskUpdater : TaskUpdater<IFixedTickable>
	{
		protected override void UpdateItem(IFixedTickable task)
		{
			task.FixedTick();
		}
	}
}
