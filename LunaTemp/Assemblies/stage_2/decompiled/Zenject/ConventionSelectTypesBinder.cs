using System;

namespace Zenject
{
	[NoReflectionBaking]
	public class ConventionSelectTypesBinder
	{
		private readonly ConventionBindInfo _bindInfo;

		public ConventionSelectTypesBinder(ConventionBindInfo bindInfo)
		{
			_bindInfo = bindInfo;
		}

		private ConventionFilterTypesBinder CreateNextBinder()
		{
			return new ConventionFilterTypesBinder(_bindInfo);
		}

		public ConventionFilterTypesBinder AllTypes()
		{
			return CreateNextBinder();
		}

		public ConventionFilterTypesBinder AllClasses()
		{
			_bindInfo.AddTypeFilter((Type t) => t.IsClass);
			return CreateNextBinder();
		}

		public ConventionFilterTypesBinder AllNonAbstractClasses()
		{
			_bindInfo.AddTypeFilter((Type t) => t.IsClass && !t.IsAbstract);
			return CreateNextBinder();
		}

		public ConventionFilterTypesBinder AllAbstractClasses()
		{
			_bindInfo.AddTypeFilter((Type t) => t.IsClass && t.IsAbstract);
			return CreateNextBinder();
		}

		public ConventionFilterTypesBinder AllInterfaces()
		{
			_bindInfo.AddTypeFilter((Type t) => t.IsInterface);
			return CreateNextBinder();
		}
	}
}
