using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class CachedProvider : IProvider
	{
		private readonly IProvider _creator;

		private List<object> _instances;

		private readonly object _locker = new object();

		private bool _isCreatingInstance;

		public bool IsCached => true;

		public bool TypeVariesBasedOnMemberType
		{
			get
			{
				throw Assert.CreateException();
			}
		}

		public int NumInstances
		{
			get
			{
				lock (_locker)
				{
					return (_instances != null) ? _instances.Count : 0;
				}
			}
		}

		public CachedProvider(IProvider creator)
		{
			_creator = creator;
		}

		public void ClearCache()
		{
			lock (_locker)
			{
				_instances = null;
			}
		}

		public Type GetInstanceType(InjectContext context)
		{
			return _creator.GetInstanceType(context);
		}

		public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsNotNull(context);
			lock (_locker)
			{
				if (_instances != null)
				{
					injectAction = null;
					buffer.AllocFreeAddRange(_instances);
					return;
				}
				if (_isCreatingInstance)
				{
					Type instanceType = _creator.GetInstanceType(context);
					throw Assert.CreateException("Found circular dependency when creating type '{0}'. Object graph:\n {1}{2}\n", instanceType, context.GetObjectGraphString(), instanceType);
				}
				_isCreatingInstance = true;
				List<object> instances = new List<object>();
				_creator.GetAllInstancesWithInjectSplit(context, args, out injectAction, instances);
				Assert.IsNotNull(instances);
				_instances = instances;
				_isCreatingInstance = false;
				buffer.AllocFreeAddRange(instances);
			}
		}
	}
}
