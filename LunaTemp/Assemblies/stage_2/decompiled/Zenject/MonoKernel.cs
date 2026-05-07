using ModestTree;
using UnityEngine;

namespace Zenject
{
	public abstract class MonoKernel : MonoBehaviour
	{
		[InjectLocal]
		private TickableManager _tickableManager = null;

		[InjectLocal]
		private InitializableManager _initializableManager = null;

		[InjectLocal]
		private DisposableManager _disposablesManager = null;

		private bool _hasInitialized;

		private bool _isDestroyed;

		protected bool IsDestroyed => _isDestroyed;

		public virtual void Start()
		{
			Initialize();
		}

		public void Initialize()
		{
			if (!_hasInitialized)
			{
				_hasInitialized = true;
				_initializableManager.Initialize();
			}
		}

		public virtual void Update()
		{
			if (_tickableManager != null)
			{
				_tickableManager.Update();
			}
		}

		public virtual void FixedUpdate()
		{
			if (_tickableManager != null)
			{
				_tickableManager.FixedUpdate();
			}
		}

		public virtual void LateUpdate()
		{
			if (_tickableManager != null)
			{
				_tickableManager.LateUpdate();
			}
		}

		public virtual void OnDestroy()
		{
			if (_disposablesManager != null)
			{
				Assert.That(!_isDestroyed);
				_isDestroyed = true;
				_disposablesManager.Dispose();
				_disposablesManager.LateDispose();
			}
		}
	}
}
