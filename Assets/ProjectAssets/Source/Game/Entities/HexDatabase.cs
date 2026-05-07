using System.Collections.Generic;
using Game.Data;
using UnityEngine;

namespace Game.Entities
{
    public sealed class HexDatabase
    {
        public int HexTypeCount => hexesData.Length;

        private readonly HexData[] hexesData;

        public HexDatabase(HexData[] hexesData)
        {
            this.hexesData = hexesData;
        }
        
        public bool TryGetHexData(int hexType, out HexData hexData)
        {
            if (hexType < 0 || hexType >= hexesData.Length)
            {
                hexData = null;
                return false;
            }
            
            hexData = hexesData[hexType];
            return true;
        }

        public bool TryGetHexData(string hexName, out HexData hexData)
        {
            if (!string.IsNullOrEmpty(hexName))
            {
                for (int i = 0; i < hexesData.Length; i++)
                {
                    if (string.Equals(hexesData[i].Name, hexName, System.StringComparison.OrdinalIgnoreCase))
                    {
                        hexData = hexesData[i];
                        return true;
                    }
                }
            }
            
            hexData = null;
            return false;
        }

        public void GetHexTypes(List<int> hexTypes)
        {
            hexTypes.Clear();

            for (int i = 0; i < hexesData.Length; i++)
            {
                hexTypes.Add(i);
            }
        }
        
        public int GetRandomHexType() => Random.Range(0, hexesData.Length);
    }
}