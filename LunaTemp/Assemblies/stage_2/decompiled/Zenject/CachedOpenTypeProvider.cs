using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class CachedOpenTypeProvider : IProvider
	{
		private readonly IProvider _creator;

		private readonly Dictionary<Type, CachedProvider> _providerMap = new Dictionary<Type, CachedProvider>();

		public bool IsCached => true;

		public bool TypeVariesBasedOnMemberType
		{
			get
			{
				throw Assert.CreateException();
			}
		}

		public int NumInstances => _providerMap.Values.Select((CachedProvider x) => x.NumInstances).Sum();

		public CachedOpenTypeProvider(IProvider creator)
		{
			Assert.That(creator.TypeVariesBasedOnMemberType);
			_creator = creator;
		}

		public void ClearCache()
		{
			_providerMap.Clear();
		}

		public Type GetInstanceType(InjectContext context)
		{
			return _creator.GetInstanceType(context);
		}

		public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
		{
			Assert.IsNotNull(context);
			if (!_providerMap.TryGetValue(context.MemberType, out var provider))
			{
				provider = new CachedProvider(_creator);
				_providerMap.Add(context.MemberType, provider);
			}
			provider.GetAllInstancesWithInjectSplit(context, args, out injectAction, buffer);
		}
	}
}
