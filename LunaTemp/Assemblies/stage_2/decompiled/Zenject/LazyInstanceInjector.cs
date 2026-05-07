using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class LazyInstanceInjector
	{
		private readonly DiContainer _container;

		private readonly HashSet<object> _instancesToInject = new HashSet<object>();

		public IEnumerable<object> Instances => _instancesToInject;

		public LazyInstanceInjector(DiContainer container)
		{
			_container = container;
		}

		public void AddInstance(object instance)
		{
			_instancesToInject.Add(instance);
		}

		public void AddInstances(IEnumerable<object> instances)
		{
			_instancesToInject.UnionWith(instances);
		}

		public void LazyInject(object instance)
		{
			if (_instancesToInject.Remove(instance))
			{
				_container.Inject(instance);
			}
		}

		public void LazyInjectAll()
		{
			List<object> tempList = new List<object>();
			while (!_instancesToInject.IsEmpty())
			{
				tempList.Clear();
				tempList.AddRange(_instancesToInject);
				foreach (object instance in tempList)
				{
					LazyInject(instance);
				}
			}
		}
	}
}
