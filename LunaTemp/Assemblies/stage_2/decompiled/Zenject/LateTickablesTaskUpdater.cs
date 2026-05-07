namespace Zenject
{
	public class LateTickablesTaskUpdater : TaskUpdater<ILateTickable>
	{
		protected override void UpdateItem(ILateTickable task)
		{
			task.LateTick();
		}
	}
}
