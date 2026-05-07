using Luna.Unity;
using Zenject;

namespace Game.Playworks
{
    public sealed class PlayworksService : IInitializable
    {
        public void SetGameEnded() => LifeCycle.GameEnded();
        public void GoToGameInstallation() => Playable.InstallFullGame();
        
        void IInitializable.Initialize() { }
    }
}