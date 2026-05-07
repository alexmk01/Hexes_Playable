using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
	public class AnimatorMoveHandlerManager : MonoBehaviour
	{
		private List<IAnimatorMoveHandler> _handlers;

		[Inject]
		public void Construct([Inject(Source = InjectSources.Local)] List<IAnimatorMoveHandler> handlers)
		{
			_handlers = handlers;
		}

		public void OnAnimatorMove()
		{
			foreach (IAnimatorMoveHandler handler in _handlers)
			{
				handler.OnAnimatorMove();
			}
		}
	}
}
