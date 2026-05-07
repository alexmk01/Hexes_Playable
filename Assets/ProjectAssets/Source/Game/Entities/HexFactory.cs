using Game.Data;
using UnityEngine;

namespace Game.Entities
{
    public sealed class HexFactory
    {
        //private readonly IObjectResolver diContainer;
        private readonly HexDatabase hexDatabase;
        private readonly GameObject hexPrefab;
        
        public HexFactory(HexDatabase hexDatabase, GameObject hexPrefab)
        {
            //this.diContainer = diContainer;
            this.hexDatabase = hexDatabase;
            this.hexPrefab = hexPrefab;
        }

        public HexComponent CreateHex(int hexType)
        {
            if (!hexDatabase.TryGetHexData(hexType, out HexData hexData))
            {
                Debug.LogError($"Hex type {hexType} not found in database.");
                return null;
            }

            GameObject hexObject = Object.Instantiate(hexPrefab);

            if (!hexObject.TryGetComponent(out HexComponent hexComponent))
            {
                hexComponent = hexObject.AddComponent<HexComponent>();
            }
            
            hexComponent.name = $"Hex_{hexType}";
            hexComponent.HexType = hexType;
            hexComponent.Color = hexData.Color;
            //diContainer.Inject(hexComponent);
            return hexComponent;
        }
    }
}