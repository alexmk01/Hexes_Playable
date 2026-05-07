using System;
using UnityEngine;

namespace Game.Entities
{
	public sealed class HexComponent : MonoBehaviour
	{
		private static readonly int ColorPropertyId = Shader.PropertyToID("_BaseColor");

		[SerializeField]
		private Vector3 boundsCenterOffset;

		[SerializeField]
		private Vector3 boundsSize;

		[NonSerialized]
		public int HexType;

		private new Renderer renderer;

		private Color color;

		private MaterialPropertyBlock materialPropertyBlock;

		public Bounds Bounds => new Bounds(base.transform.position + boundsCenterOffset, boundsSize);

		public Color Color
		{
			get
			{
				return color;
			}
			set
			{
				if (!(color == value))
				{
					if (renderer != null)
					{
						materialPropertyBlock.SetColor(ColorPropertyId, value);
						renderer.SetPropertyBlock(materialPropertyBlock);
					}
					color = value;
				}
			}
		}

		public Renderer Renderer => renderer;

		private void Reset()
		{
			if (TryGetComponent<Renderer>(out var renderer))
			{
				Bounds bounds = renderer.bounds;
				boundsCenterOffset = bounds.center - base.transform.position;
				boundsSize = bounds.size;
			}
		}

		private void Awake()
		{
			if (TryGetComponent<Renderer>(out renderer))
			{
				color = renderer.sharedMaterial.GetColor(ColorPropertyId);
				materialPropertyBlock = new MaterialPropertyBlock();
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Bounds bounds = Bounds;
			Gizmos.DrawWireCube(bounds.center, bounds.size);
		}
	}
}
