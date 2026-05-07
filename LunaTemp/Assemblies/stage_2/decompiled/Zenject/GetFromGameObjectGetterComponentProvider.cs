using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class GetFromGameObjectGetterComponentProvider : IProvider
	{
		private readonly Func<InjectContext, GameObject> _gameObjectGetter;

		private readonly Type _componentType;

		private readonly bool _matchSingle;

		public bool IsCached => false;

		public bool TypeVariesBasedOnMemberType => false;

		public GetFromGameObjectGetterComponentProvider(Type componentType, Func<InjectContext, GameObject> gameObjectGetter, bool matchSingle)
		{
			_componentType = componentType;
			_matchSingle = matchSingle;
			_gameObjectGetter = gameObjectGetter;
		}

		public Type GetInstanceType(InjectContext context)
		{
			return _componentType;
		}

		public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsNotNull(context);
			injectAction = null;
			if (context.Container.IsValidating)
			{
				buffer.Add(new ValidationMarker(_componentType));
				return;
			}
			GameObject gameObject = _gameObjectGetter(context);
			if (_matchSingle)
			{
				Component match = gameObject.GetComponent(_componentType);
				Assert.IsNotNull(match, "Could not find component with type '{0}' on game object '{1}'", _componentType, gameObject.name);
				buffer.Add(match);
			}
			else
			{
				Component[] allComponents = gameObject.GetComponents(_componentType);
				Assert.That(allComponents.Length >= 1, "Expected to find at least one component with type '{0}' on prefab '{1}'", _componentType, gameObject.name);
				buffer.AllocFreeAddRange(allComponents);
			}
		}
	}
}
