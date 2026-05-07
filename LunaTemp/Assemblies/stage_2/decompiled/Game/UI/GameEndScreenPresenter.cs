using System;
using Game.Gameplay;
using Game.Playworks;
using Zenject;

namespace Game.UI
{
	public sealed class GameEndScreenPresenter : IInitializable, IDisposable
	{
		public const string WinScreenKey = "WinScreen";

		public const string LoseScreenKey = "LoseScreen";

		private readonly GameplayManager gameplayManager;

		private readonly PlayworksService playworksService;

		private readonly GameEndScreenViewComponent winScreenView;

		private readonly GameEndScreenViewComponent loseScreenView;

		private void OnGameCompleted(GameplayManager manager, GameResult result)
		{
			if (result == GameResult.Win)
			{
				winScreenView.Show();
			}
			else
			{
				loseScreenView.Show();
			}
		}

		private void OnExitButtonClicked()
		{
			playworksService.GoToGameInstallation();
		}

		public GameEndScreenPresenter(GameplayManager gameplayManager, PlayworksService playworksService, [Inject(Id = "WinScreen")] GameEndScreenViewComponent winScreenView, [Inject(Id = "LoseScreen")] GameEndScreenViewComponent loseScreenView)
		{
			this.gameplayManager = gameplayManager;
			this.playworksService = playworksService;
			this.winScreenView = winScreenView;
			this.loseScreenView = loseScreenView;
			gameplayManager.GameCompleted += OnGameCompleted;
			winScreenView.ExitButtonClicked += OnExitButtonClicked;
			loseScreenView.ExitButtonClicked += OnExitButtonClicked;
		}

		public void Initialize()
		{
			winScreenView.Hide();
			loseScreenView.Hide();
		}

		public void Dispose()
		{
			if (gameplayManager != null)
			{
				gameplayManager.GameCompleted -= OnGameCompleted;
			}
			if (winScreenView != null)
			{
				winScreenView.ExitButtonClicked -= OnExitButtonClicked;
			}
			if (loseScreenView != null)
			{
				loseScreenView.ExitButtonClicked -= OnExitButtonClicked;
			}
		}
	}
}
