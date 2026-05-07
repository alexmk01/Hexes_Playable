using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
	public class AnimatorIkHandlerManager : MonoBehaviour
	{
		private List<IAnimatorIkHandler> _handlers;

		[Inject]
		public void Construct([Inject(Source = InjectSources.Local)] List<IAnimatorIkHandler> handlers)
		{
			_handlers = handlers;
		}

		public void OnAnimatorIk()
		{
			foreach (IAnimatorIkHandler handler in _handlers)
			{
				handler.OnAnimatorIk();
			}
		}
	}
}
