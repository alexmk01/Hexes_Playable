using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
	public static class HexExtensions
	{
		public static Vector3 GetCenter(this HexComponent hex)
		{
			return hex.Bounds.center;
		}

		public static Vector3 GetTopPoint(this HexComponent hex)
		{
			Bounds bounds = hex.Bounds;
			return new Vector3(bounds.center.x, bounds.center.y + bounds.extents.y, bounds.center.z);
		}

		public static Vector3 GetLocalCenter(this HexComponent hex)
		{
			Vector3 center = hex.GetCenter();
			Transform parent = hex.transform.parent;
			return (parent != null) ? parent.InverseTransformPoint(center) : center;
		}

		public static Vector3 GetLocalTopPoint(this HexComponent hex)
		{
			Vector3 topPoint = hex.GetTopPoint();
			Transform parent = hex.transform.parent;
			return (parent != null) ? parent.InverseTransformPoint(topPoint) : topPoint;
		}

		public static int GetTopHexCount(this HexStackComponent hexStack)
		{
			int count = 0;
			if (!hexStack.IsEmpty)
			{
				IReadOnlyList<HexComponent> hexes = hexStack.Hexes;
				int topHexType = hexStack.TopHex.HexType;
				for (int i = 0; i < hexes.Count; i++)
				{
					if (hexes[i].HexType == topHexType)
					{
						count++;
					}
				}
			}
			return count;
		}
	}
}
