using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zenject
{
	[NoReflectionBaking]
	public class ConventionBindInfo
	{
		private readonly List<Func<Type, bool>> _typeFilters = new List<Func<Type, bool>>();

		private readonly List<Func<Assembly, bool>> _assemblyFilters = new List<Func<Assembly, bool>>();

		private static Dictionary<Assembly, Type[]> _assemblyTypeCache = new Dictionary<Assembly, Type[]>();

		public void AddAssemblyFilter(Func<Assembly, bool> predicate)
		{
			_assemblyFilters.Add(predicate);
		}

		public void AddTypeFilter(Func<Type, bool> predicate)
		{
			_typeFilters.Add(predicate);
		}

		private IEnumerable<Assembly> GetAllAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}

		private bool ShouldIncludeAssembly(Assembly assembly)
		{
			return _assemblyFilters.All((Func<Assembly, bool> predicate) => predicate(assembly));
		}

		private bool ShouldIncludeType(Type type)
		{
			return _typeFilters.All((Func<Type, bool> predicate) => predicate(type));
		}

		private Type[] GetTypes(Assembly assembly)
		{
			if (!_assemblyTypeCache.TryGetValue(assembly, out var types))
			{
				types = assembly.GetTypes();
				_assemblyTypeCache[assembly] = types;
			}
			return types;
		}

		public List<Type> ResolveTypes()
		{
			return GetAllAssemblies().Where(ShouldIncludeAssembly).SelectMany((Assembly assembly) => GetTypes(assembly)).Where(ShouldIncludeType)
				.ToList();
		}
	}
}
