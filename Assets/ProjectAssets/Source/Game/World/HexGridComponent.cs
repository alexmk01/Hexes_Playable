using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.Mathf;

namespace Game.World
{
    public sealed class HexGridComponent : MonoBehaviour
    {
        private static readonly float Sqrt3 = Sqrt(3f);
        
        public static readonly Vector2Int[] NeighbourOffsets = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(1, -1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(-1, 1),
            new Vector2Int(0, 1)
        };

        public static int GetDistance(Vector2Int hex0, Vector2Int hex1)
        {
            return (Abs(hex0.x - hex1.x) + Abs(hex0.x + hex0.y - hex1.x - hex1.y) + Abs(hex0.x - hex1.y)) / 2;
        }

        public struct CircleHexAreaEnumerator : IEnumerator<Vector2Int>
        {
            public readonly Vector2Int Current => current;
            readonly object IEnumerator.Current => Current;

            private readonly int radius;
            private int q;
            private int r;
            private int rMin;
            private int rMax;
            private Vector2Int current;
            
            public CircleHexAreaEnumerator(int radius) : this()
            {
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
                    rMin = Max(-radius, -q - radius);
                    rMax = Min(radius, -q + radius);
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
                current = default;
            }

            public readonly void Dispose() { }
        }

        public readonly struct CircleHexAreaEnumerable : IEnumerable<Vector2Int>
        {
            private readonly int radius;

            public CircleHexAreaEnumerable(int radius)
            {
                this.radius = radius;
            }

            public readonly CircleHexAreaEnumerator GetEnumerator() => new CircleHexAreaEnumerator(radius);
            IEnumerator<Vector2Int> IEnumerable<Vector2Int>.GetEnumerator() => GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        
        public struct HexNeighboursEnumerator : IEnumerator<Vector2Int>
        {
            private readonly Vector2Int hex;
            private readonly int maxDistance;
            private int index;
            private Vector2Int current;

            public HexNeighboursEnumerator(Vector2Int hex, int maxDistance)
            {
                this.hex = hex;
                this.maxDistance = maxDistance;
                index = -1;
                current = default;
            }

            public readonly Vector2Int Current => current;
            readonly object IEnumerator.Current => Current;

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
                current = default;
            }
            
            public readonly void Dispose() { }
        }

        public readonly struct HexNeighboursEnumerable : IEnumerable<Vector2Int>
        {
            private readonly Vector2Int hex;
            private readonly int maxDistance;

            public HexNeighboursEnumerable(Vector2Int hex, int maxDistance)
            {
                this.hex = hex;
                this.maxDistance = maxDistance;
            }

            public readonly HexNeighboursEnumerator GetEnumerator() => new HexNeighboursEnumerator(hex, maxDistance);
            IEnumerator<Vector2Int> IEnumerable<Vector2Int>.GetEnumerator() => GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        
        public Plane GridPlane { get;  private set; }

        [SerializeField]
        private float size = 1f;
        
        [SerializeField]
        private int gridRadius = 3;
        
        private readonly Dictionary<Vector2Int, object> gridItems = new Dictionary<Vector2Int, object>(32);

        private void OnEnable()
        {
            GridPlane = new Plane(Vector3.up, transform.position);
        }

        public Vector3 HexToWorld(Vector2Int hex)
        {
            float x = 3f / 2f * hex.x * size;
            float z = (Sqrt3 / 2f * hex.x + Sqrt3 * hex.y) * size;
            return transform.position + new Vector3(x, 0f, z);
        }

        public Vector2Int WorldToHex(Vector3 worldPos)
        {
            Vector3 localPos = worldPos - transform.position;
            float px = localPos.x / size;
            float pz = localPos.z / size;
            float q = 2f / 3f * px;
            float r = -1f / 3f * px + Sqrt3 / 3f * pz;
            float s = -q - r;
            int qRound = RoundToInt(q);
            int rRound = RoundToInt(r);
            int sRound = RoundToInt(s);
            float qDiff = Abs(qRound - q);
            float rDiff = Abs(rRound - r);
            float sDiff = Abs(sRound - s);
            if (qDiff > rDiff && qDiff > sDiff) qRound = -rRound - sRound;
            else if (rDiff > sDiff) rRound = -qRound - sRound;
            return new Vector2Int(qRound, rRound);
        }

        public bool IsValidHex(Vector2Int hex) => GetDistance(Vector2Int.zero, hex) <= gridRadius;
        public bool TryGetItem(Vector2Int hex, out object item) => gridItems.TryGetValue(hex, out item);
        public bool IsBusyHex(Vector2Int hex) => gridItems.ContainsKey(hex);
        public bool CanBeAddedTo(Vector2Int hex) => IsValidHex(hex) && !IsBusyHex(hex);

        public bool TryAddItem(Vector2Int hex, object item)
        {
            Assert.IsNotNull(item);

            if (CanBeAddedTo(hex))
            {
                gridItems.Add(hex, item);
                return true;
            }

            return false;
        }
        
        public bool RemoveItem(Vector2Int hex) => gridItems.Remove(hex);
        public CircleHexAreaEnumerable GetAllHexes() => new CircleHexAreaEnumerable(gridRadius);
        public HexNeighboursEnumerable GetNeighbours(Vector2Int hex) => new HexNeighboursEnumerable(hex, gridRadius);

#if UNITY_EDITOR
        private static readonly Quaternion HandlesRotation = Quaternion.AngleAxis(90f, Vector3.right);

        private static GUIStyle coordinatesLabelStyle;

        private static void DrawHex(Vector3 center, float size)
        {
            Span<Vector3> corners = stackalloc Vector3[7];

            for (int i = 0; i <= 6; i++)
            {
                float angle = Deg2Rad * 60f * i;
                corners[i] = center + new Vector3(size * Cos(angle), 0f, size * Sin(angle));
            }

            for (int i = 0; i < 6; i++)
            {
                Gizmos.DrawLine(corners[i], corners[i + 1]);
            }
        }

        [Space, SerializeField]
        private bool drawCells = true;

        [SerializeField]
        private bool drawCenters = true;

        [SerializeField]
        private bool drawCoordinates = false;

        [SerializeField]
        private Color cellColor = Color.green;

        [SerializeField]
        private Color centerColor = Color.blue;

        [SerializeField]
        private Color busyCenterColor = Color.red;
        
        private void OnDrawGizmos()
        {
            if (!drawCells && !drawCenters && !drawCoordinates)
            {
                return;
            }

            coordinatesLabelStyle ??= new()
            {
                fontSize = 12,
                normal = new GUIStyleState { textColor = Color.white }
            };
            
            foreach (Vector2Int hex in GetAllHexes())
            {
                Vector3 center = HexToWorld(hex);
                
                if (drawCells)
                {
                    Gizmos.color = cellColor;
                    DrawHex(center, size);
                }
                
                if (drawCenters)
                {
                    UnityEditor.Handles.color = IsBusyHex(hex) ? busyCenterColor : centerColor;
                    UnityEditor.Handles.CircleHandleCap(0, center, HandlesRotation, size * 0.1f, EventType.Repaint);
                }
                
                if (drawCoordinates)
                {
                    int distance = GetDistance(Vector2Int.zero, hex);
                    UnityEditor.Handles.Label(center + Vector3.up * 0.1f, $"({hex.x}, {hex.y}, d: {distance})", coordinatesLabelStyle);
                }
            }
        }
#endif
    }
}
