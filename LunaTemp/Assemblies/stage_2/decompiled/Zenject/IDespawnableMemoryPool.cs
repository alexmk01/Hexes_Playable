namespace Zenject
{
	public interface IDespawnableMemoryPool<TValue> : IMemoryPool
	{
		void Despawn(TValue item);
	}
}
