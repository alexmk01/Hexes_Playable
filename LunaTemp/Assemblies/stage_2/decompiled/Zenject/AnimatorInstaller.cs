using UnityEngine;

namespace Zenject
{
	public class AnimatorInstaller : Installer<Animator, AnimatorInstaller>
	{
		private readonly Animator _animator;

		public AnimatorInstaller(Animator animator)
		{
			_animator = animator;
		}

		public override void InstallBindings()
		{
			base.Container.Bind<AnimatorIkHandlerManager>().FromNewComponentOn(_animator.gameObject);
			base.Container.Bind<AnimatorIkHandlerManager>().FromNewComponentOn(_animator.gameObject);
		}
	}
}
