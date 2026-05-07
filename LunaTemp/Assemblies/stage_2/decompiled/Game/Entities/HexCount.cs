namespace Game.Entities
{
	public struct HexCount
	{
		public readonly int HexType;

		public int Count;

		public HexCount(int hexType, int count)
		{
			HexType = hexType;
			Count = count;
		}
	}
}
