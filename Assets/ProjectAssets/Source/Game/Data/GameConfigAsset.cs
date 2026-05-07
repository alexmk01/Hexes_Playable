using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu]
    public sealed class GameConfigAsset : ScriptableObject
    {
        public float HexStackSpawnDensity
        {
            get => hexStackSpawnDensity;
            set => hexStackSpawnDensity = Mathf.Clamp01(value);
        }
        
        [LunaPlaygroundField]
        public int MaxHexStackSize = 10;
        
        [Space]
        public GameObject HexPrefab;
        [LunaPlaygroundField]
        public float HexMoveActionDuration = 0.2f;
        [LunaPlaygroundField]
        public float MinHexMoveActionDuration = 0.05f;
        [LunaPlaygroundField]
        public float HexDesctructionActionDuration = 0.2f;
        [LunaPlaygroundField]
        public float MinHexDesctructionActionDuration = 0.05f;
        [LunaPlaygroundField]
        public int StartPlayerHexStackCount = 3;
        public string PlayerHexStackLayerName = "PlayerHexStack";
        public float PlayerStackSpawnActionDuration = 0.3f;
        public float PlayerHexStackDragHeight = 2f;
        
        [Space]
        [SerializeField, Range(0f, 1f), LunaPlaygroundField]
        private float hexStackSpawnDensity = 0.3f;
        public GameObject GroundPrefab;
        public GameObject GameFieldHexPrefab;
        public Color GameFieldHexHighlightColor = Color.yellow;

        [Space]
        public float StackingAnimationJumpHeight = 1.5f;
        public float HexHighlightDuration = 0.2f;

        [Space]
        public GameObject HexDestructionEffectPrefab;

        [Space]
        public HexData[] HexesData;
    }
}
