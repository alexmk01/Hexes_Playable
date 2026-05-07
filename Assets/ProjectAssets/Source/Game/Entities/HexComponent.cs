using System;
using UnityEngine;

namespace Game.Entities
{
    public sealed class HexComponent : MonoBehaviour
    {
        private static readonly int ColorPropertyId = Shader.PropertyToID("_BaseColor");

        public Bounds Bounds
        {
            get
            {
                return new Bounds(transform.position + boundsCenterOffset, boundsSize);
            }
        }

        public Color Color
        {
            get => color;
            set
            {
                if (color == value)
                {
                    return;
                }

                if (renderer != null)
                {
                    materialPropertyBlock.SetColor(ColorPropertyId, value);
                    renderer.SetPropertyBlock(materialPropertyBlock);
                }

                color = value;
            }
        }
        
        public Renderer Renderer => renderer;
        
        [SerializeField]
        private Vector3 boundsCenterOffset;

        [SerializeField]
        private Vector3 boundsSize;
        
        [NonSerialized]
        public int HexType;

        new private Renderer renderer;
        private Color color;
        private MaterialPropertyBlock materialPropertyBlock;

        private void Reset()
        {
            if (TryGetComponent(out Renderer renderer))
            {
                Bounds bounds = renderer.bounds;
                boundsCenterOffset = bounds.center - transform.position;
                boundsSize = bounds.size;
            }
        }
        
        private void Awake()
        {
            if (TryGetComponent(out renderer))
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