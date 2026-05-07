using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.World
{
	public sealed class HexGridComponent : MonoBehaviour
	{
		public struct CircleHexAreaEnumerator : IEnumerator<Vector2Int>, IEnumerator, IDisposable
		{
			private readonly int radius;

			private int q;

			private int r;

			private int rMin;

			private int rMax;

			private Vector2Int current;

			public Vector2Int Current
			{
				
				get
				{
					return current;
				}
			}

			object IEnumerator.Current
			{
				
				get
				{
					return Current;
				}
			}

			public CircleHexAreaEnumerator(int radius)
			{
				this = default(CircleHexAreaEnumerator);
				this.radius = radius;
				Reset();
			}

			public bool MoveNext()
			{
				if (r <= rMax)
				{
					current = new Vector2Int(q, r);
					r++;
					return true;
				}
				q++;
				if (q <= radius)
				{
					rMin = Mathf.Max(-radius, -q - radius);
					rMax = Mathf.Min(radius, -q + radius);
					r = rMin;
					current = new Vector2Int(q, r);
					r++;
					return true;
				}
				return false;
			}

			public void Reset()
			{
				rMin = -radius;
				rMax = radius;
				q = -radius;
				r = 0;
				current = default(Vector2Int);
			}

			
			public void Dispose()
			{
			}
		}

		
		public struct CircleHexAreaEnumerable : IEnumerable<Vector2Int>, IEnumerable
		{
			private readonly int radius;

			public CircleHexAreaEnumerable(int radius)
			{
				this.radius = radius;
			}

			public CircleHexAreaEnumerator GetEnumerator()
			{
				return new CircleHexAreaEnumerator(radius);
			}

			IEnumerator<Vector2Int> IEnumerable<Vector2Int>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		public struct HexNeighboursEnumerator : IEnumerator<Vector2Int>, IEnumerator, IDisposable
		{
			private readonly Vector2Int hex;

			private readonly int maxDistance;

			private int index;

			private Vector2Int current;

			public Vector2Int Current
			{
				
				get
				{
					return current;
				}
			}

			object IEnumerator.Current
			{
				
				get
				{
					return Current;
				}
			}

			public HexNeighboursEnumerator(Vector2Int hex, int maxDistance)
			{
				this.hex = hex;
				this.maxDistance = maxDistance;
				index = -1;
				current = default(Vector2Int);
			}

			public bool MoveNext()
			{
				index++;
				if (index < NeighbourOffsets.Length)
				{
					current.x = hex.x + NeighbourOffsets[index].x;
					current.y = hex.y + NeighbourOffsets[index].y;
					return true;
				}
				return false;
			}

			public void Reset()
			{
				index = -1;
				current = default(Vector2Int);
			}

			
			public void Dispose()
			{
			}
		}

		
		public struct HexNeighboursEnumerable : IEnumerable<Vector2Int>, IEnumerable
		{
			private readonly Vector2Int hex;

			private readonly int maxDistance;

			public HexNeighboursEnumerable(Vector2Int hex, int maxDistance)
			{
				this.hex = hex;
				this.maxDistance = maxDistance;
			}

			public HexNeighboursEnumerator GetEnumerator()
			{
				return new HexNeighboursEnumerator(hex, maxDistance);
			}

			IEnumerator<Vector2Int> IEnumerable<Vector2Int>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		private static readonly float Sqrt3 = Mathf.Sqrt(3f);

		public static readonly Vector2Int[] NeighbourOffsets = new Vector2Int[6]
		{
			new Vector2Int(1, 0),
			new Vector2Int(1, -1),
			new Vector2Int(0, -1),
			new Vector2Int(-1, 0),
			new Vector2Int(-1, 1),
			new Vector2Int(0, 1)
		};

		[SerializeField]
		private float size = 1f;

		[SerializeField]
		private int gridRadius = 3;

		private readonly Dictionary<Vector2Int, object> gridItems = new Dictionary<Vector2Int, object>(32);

		public Plane GridPlane { get; private set; }

		public static int GetDistance(Vector2Int hex0, Vector2Int hex1)
		{
			return (Mathf.Abs(hex0.x - hex1.x) + Mathf.Abs(hex0.x + hex0.y - hex1.x - hex1.y) + Mathf.Abs(hex0.x - hex1.y)) / 2;
		}

		private void OnEnable()
		{
			GridPlane = new Plane(Vector3.up, base.transform.position);
		}

		public Vector3 HexToWorld(Vector2Int hex)
		{
			float x = 1.5f * (float)hex.x * size;
			float z = (Sqrt3 / 2f * (float)hex.x + Sqrt3 * (float)hex.y) * size;
			return base.transform.position + new Vector3(x, 0f, z);
		}

		public Vector2Int WorldToHex(Vector3 worldPos)
		{
			Vector3 localPos = worldPos - base.transform.position;
			float px = localPos.x / size;
			float pz = localPos.z / size;
			float q = 2f / 3f * px;
			float r = -1f / 3f * px + Sqrt3 / 3f * pz;
			float s = 0f - q - r;
			int qRound = Mathf.RoundToInt(q);
			int rRound = Mathf.RoundToInt(r);
			int sRound = Mathf.RoundToInt(s);
			float qDiff = Mathf.Abs((float)qRound - q);
			float rDiff = Mathf.Abs((float)rRound - r);
			float sDiff = Mathf.Abs((float)sRound - s);
			if (qDiff > rDiff && qDiff > sDiff)
			{
				qRound = -rRound - sRound;
			}
			else if (rDiff > sDiff)
			{
				rRound = -qRound - sRound;
			}
			return new Vector2Int(qRound, rRound);
		}

		public bool IsValidHex(Vector2Int hex)
		{
			return GetDistance(Vector2Int.zero, hex) <= gridRadius;
		}

		public bool TryGetItem(Vector2Int hex, out object item)
		{
			return gridItems.TryGetValue(hex, out item);
		}

		public bool IsBusyHex(Vector2Int hex)
		{
			return gridItems.ContainsKey(hex);
		}

		public bool CanBeAddedTo(Vector2Int hex)
		{
			return IsValidHex(hex) && !IsBusyHex(hex);
		}

		public bool TryAddItem(Vector2Int hex, object item)
		{
			if (CanBeAddedTo(hex))
			{
				gridItems.Add(hex, item);
				return true;
			}
			return false;
		}

		public bool RemoveItem(Vector2Int hex)
		{
			return gridItems.Remove(hex);
		}

		public CircleHexAreaEnumerable GetAllHexes()
		{
			return new CircleHexAreaEnumerable(gridRadius);
		}

		public HexNeighboursEnumerable GetNeighbours(Vector2Int hex)
		{
			return new HexNeighboursEnumerable(hex, gridRadius);
		}
	}
}
