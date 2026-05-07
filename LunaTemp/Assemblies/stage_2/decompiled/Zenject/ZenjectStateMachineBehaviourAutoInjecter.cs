using ModestTree;
using UnityEngine;

namespace Zenject
{
	public class ZenjectStateMachineBehaviourAutoInjecter : MonoBehaviour
	{
		private DiContainer _container;

		private Animator _animator;

		[Inject]
		public void Construct(DiContainer container)
		{
			_container = container;
			_animator = GetComponent<Animator>();
			Assert.IsNotNull(_animator);
		}

		public void Start()
		{
			if (!(_animator != null))
			{
				return;
			}
			StateMachineBehaviour[] behaviours = _animator.GetBehaviours<StateMachineBehaviour>();
			if (behaviours != null)
			{
				StateMachineBehaviour[] array = behaviours;
				foreach (StateMachineBehaviour behaviour in array)
				{
					_container.Inject(behaviour);
				}
			}
		}
	}
}
