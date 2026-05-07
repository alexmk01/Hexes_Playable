using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Game.Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Entities
{
	public sealed class HexStackComponent : MonoBehaviour
	{
		public struct TopHexesEnumerator : IEnumerator<HexComponent>, IEnumerator, IDisposable
		{
			private readonly List<HexComponent> hexes;

			private readonly int topHexType;

			private readonly int count;

			private int index;

			private int endIndex;

			private HexComponent current;

			public HexComponent Current
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

			public TopHexesEnumerator(HexStackComponent stack, int count)
			{
				this = default(TopHexesEnumerator);
				hexes = stack.hexes;
				this.count = count;
				topHexType = stack.TopHex.HexType;
				Reset();
			}

			public bool MoveNext()
			{
				index--;
				if (index >= endIndex)
				{
					HexComponent next = hexes[index];
					if (next.HexType == topHexType)
					{
						current = next;
						return true;
					}
				}
				return false;
			}

			public void Reset()
			{
				index = hexes.Count;
				endIndex = index - count;
				current = null;
			}

			
			public void Dispose()
			{
			}
		}

		public struct HexTypesEnumerator : IEnumerator<int>, IEnumerator, IDisposable
		{
			private readonly List<HexComponent> hexes;

			private int hexIndex;

			private int currentHexType;

			public int Current
			{
				
				get
				{
					return currentHexType;
				}
			}

			object IEnumerator.Current
			{
				
				get
				{
					return Current;
				}
			}

			public HexTypesEnumerator(HexStackComponent stack)
			{
				this = default(HexTypesEnumerator);
				hexes = stack.hexes;
				Reset();
			}

			public bool MoveNext()
			{
				while (hexIndex < hexes.Count)
				{
					int hexType = hexes[hexIndex++].HexType;
					if (hexType != currentHexType)
					{
						currentHexType = hexType;
						return true;
					}
				}
				return false;
			}

			public void Reset()
			{
				hexIndex = 0;
				currentHexType = -1;
			}

			
			public void Dispose()
			{
			}
		}

		
		public struct TopHexesEnumerable : IEnumerable<HexComponent>, IEnumerable
		{
			private readonly HexStackComponent stack;

			private readonly int count;

			public TopHexesEnumerable(HexStackComponent stack, int count)
			{
				this.stack = stack;
				this.count = count;
			}

			public TopHexesEnumerator GetEnumerator()
			{
				return new TopHexesEnumerator(stack, count);
			}

			IEnumerator<HexComponent> IEnumerable<HexComponent>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		
		public struct HexTypesEnumerable : IEnumerable<int>, IEnumerable
		{
			private readonly HexStackComponent stack;

			public HexTypesEnumerable(HexStackComponent stack)
			{
				this.stack = stack;
			}

			public HexTypesEnumerator GetEnumerator()
			{
				return new HexTypesEnumerator(stack);
			}

			IEnumerator<int> IEnumerable<int>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		private readonly List<HexComponent> hexes = new List<HexComponent>(16);

		private HexFactory hexFactory;

		private new BoxCollider collider;

		private bool isInteractive = true;

		private bool isBlocked;

		public IReadOnlyList<HexComponent> Hexes => hexes;

		public bool IsEmpty => hexes.Count == 0;

		public HexComponent TopHex
		{
			get
			{
				object result;
				if (hexes.Count == 0)
				{
					result = null;
				}
				else
				{
					List<HexComponent> list = hexes;
					result = list[list.Count - 1];
				}
				return (HexComponent)result;
			}
		}

		public bool IsBlocked
		{
			get
			{
				return isBlocked;
			}
			set
			{
				isBlocked = value;
			}
		}

		[Inject]
		private void Construct(HexFactory hexFactory)
		{
			this.hexFactory = hexFactory;
		}

		private void UpdateCollider()
		{
			if (!(collider == null))
			{
				if (hexes.Count == 0)
				{
					collider.size = Vector3.zero;
					collider.center = Vector3.zero;
				}
				else
				{
					List<HexComponent> list = hexes;
					float stackHeight = list[list.Count - 1].GetLocalTopPoint().y;
					List<HexComponent> list2 = hexes;
					Vector3 stackSize = list2[list2.Count - 1].Bounds.size;
					collider.size = new Vector3(stackSize.x, stackHeight, stackSize.z);
					collider.center = new Vector3(0f, stackHeight * 0.5f, 0f);
				}
				collider.enabled = isInteractive;
			}
		}

		private void StackHexes(int index)
		{
			if (hexes.Count != 0)
			{
				float positionY = ((index > 0) ? hexes[index - 1].GetLocalCenter().y : (0f - hexes[0].Bounds.extents.y));
				for (int i = index; i < hexes.Count; i++)
				{
					HexComponent hex = hexes[i];
					positionY += hex.Bounds.size.y;
					hex.transform.localPosition = new Vector3(0f, positionY, 0f);
				}
			}
		}

		private void AddHexes(List<HexComponent> hexesToAdd, int hexType)
		{
			for (int i = 0; i < hexesToAdd.Count; i++)
			{
				HexComponent hex = hexesToAdd[i];
				hex.transform.parent = base.transform;
			}
			int insertIndex = -1;
			for (int j = hexes.Count - 1; j >= 0; j--)
			{
				if (hexes[j].HexType == hexType)
				{
					insertIndex = j + 1;
					break;
				}
			}
			if (insertIndex < 0)
			{
				insertIndex = hexes.Count;
				hexes.AddRange(hexesToAdd);
			}
			else
			{
				hexes.InsertRange(insertIndex, hexesToAdd);
			}
			StackHexes(insertIndex);
		}

		private void RemoveHexesWithType(int hexType, List<HexComponent> removedHexes)
		{
			bool updateStack = false;
			for (int i = hexes.Count - 1; i >= 0; i--)
			{
				if (hexes[i].HexType == hexType)
				{
					HexComponent hex = hexes[i];
					hex.transform.parent = null;
					hexes.RemoveAt(i);
					removedHexes.Add(hex);
					updateStack = true;
				}
			}
			if (updateStack)
			{
				StackHexes();
			}
		}

		private void RemoveHexes(int count, List<HexComponent> removedHexes)
		{
			int endIndex = hexes.Count - count;
			for (int i = hexes.Count - 1; i >= endIndex; i--)
			{
				HexComponent hex = hexes[i];
				hex.transform.parent = null;
				hexes.RemoveAt(i);
				removedHexes.Add(hex);
			}
			StackHexes();
		}

		public void AddCollider()
		{
			if (!TryGetComponent<BoxCollider>(out collider))
			{
				collider = base.gameObject.AddComponent<BoxCollider>();
			}
		}

		public void TrySetInteractive(bool isInteractive)
		{
			if (collider != null)
			{
				collider.enabled = isInteractive;
				this.isInteractive = isInteractive;
			}
		}

		public TopHexesEnumerable GetTopHexes(int count)
		{
			return new TopHexesEnumerable(this, count);
		}

		public TopHexesEnumerable GetTopHexes()
		{
			return new TopHexesEnumerable(this, hexes.Count);
		}

		public HexTypesEnumerable GetHexTypes()
		{
			return new HexTypesEnumerable(this);
		}

		public void AddHexes(params HexCount[] newHexes)
		{
			if (isBlocked)
			{
				return;
			}
			List<HexComponent> hexesToAdd;
			using (ListPool<HexComponent>.Get(out hexesToAdd))
			{
				for (int i = 0; i < newHexes.Length; i++)
				{
					int count = newHexes[i].Count;
					int hexType = newHexes[i].HexType;
					for (int j = 0; j < count; j++)
					{
						hexesToAdd.Add(hexFactory.CreateHex(hexType));
					}
					AddHexes(hexesToAdd, hexType);
					hexesToAdd.Clear();
				}
			}
			UpdateCollider();
		}

		public void MoveHexes(int hexType, HexStackComponent otherStack, List<HexComponent> movedHexes = null)
		{
			movedHexes?.Clear();
			if (isBlocked)
			{
				return;
			}
			List<HexComponent> removedHexes;
			using (ListPool<HexComponent>.Get(out removedHexes))
			{
				RemoveHexesWithType(hexType, removedHexes);
				if (removedHexes.Count != 0)
				{
					otherStack.AddHexes(removedHexes, hexType);
					movedHexes?.AddRange(removedHexes);
					UpdateCollider();
					otherStack.UpdateCollider();
				}
			}
		}

		public void StackHexes()
		{
			StackHexes(0);
		}

		public void DestroyHexes(int count)
		{
			if (isBlocked)
			{
				return;
			}
			if (count > hexes.Count)
			{
				count = hexes.Count;
			}
			List<HexComponent> removedHexes;
			using (ListPool<HexComponent>.Get(out removedHexes))
			{
				RemoveHexes(count, removedHexes);
				for (int i = 0; i < removedHexes.Count; i++)
				{
					UnityEngine.Object.Destroy(removedHexes[i].gameObject);
				}
				UpdateCollider();
			}
		}

		public void DestroyAllHexes()
		{
			DestroyHexes(hexes.Count);
		}
	}
}
