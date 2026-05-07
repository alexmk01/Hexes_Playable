using UnityEngine;
using Zenject;

namespace Game.Entities
{
    public sealed class HexStackFactory
    {
        private readonly DiContainer diContainer;
        private readonly int playerStackLayer;
    
        public HexStackFactory(DiContainer diContainer, int playerStackLayer)
        {
            this.diContainer = diContainer;
            this.playerStackLayer = playerStackLayer;
        }
    
        public HexStackComponent CreateStack(Vector3 position, bool isPlayerStack)
        {
            var stackObject = new GameObject("HexStack");
            stackObject.transform.position = position;
            HexStackComponent stackComponent = stackObject.AddComponent<HexStackComponent>();
            
            if (isPlayerStack)
            {
                stackObject.layer = playerStackLayer;
                stackComponent.AddCollider();
            }

            diContainer.Inject(stackComponent);
            return stackComponent;
        }
    }
}