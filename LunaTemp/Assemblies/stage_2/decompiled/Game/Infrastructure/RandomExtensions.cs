using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Infrastructure
{
	public static class RandomExtensions
	{
		public static bool TryGetRandomWeightedItem<T>(this IList<T> items, Func<T, float> weightSelector, float weightSum, out int itemIndex)
		{
			if (items.Count == 0 || weightSum <= 0f)
			{
				itemIndex = -1;
				return false;
			}
			float randomValue = UnityEngine.Random.Range(0f, weightSum);
			float cumulativeWeight = 0f;
			for (int i = 0; i < items.Count; i++)
			{
				cumulativeWeight += weightSelector(items[i]);
				if (randomValue < cumulativeWeight)
				{
					itemIndex = i;
					return true;
				}
			}
			itemIndex = items.Count - 1;
			return true;
		}
	}
}
