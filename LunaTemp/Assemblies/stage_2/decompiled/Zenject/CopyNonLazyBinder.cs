using System.Collections.Generic;

namespace Zenject
{
	[NoReflectionBaking]
	public class CopyNonLazyBinder : NonLazyBinder
	{
		private List<BindInfo> _secondaryBindInfos;

		public CopyNonLazyBinder(BindInfo bindInfo)
			: base(bindInfo)
		{
		}

		internal void AddSecondaryCopyBindInfo(BindInfo bindInfo)
		{
			if (_secondaryBindInfos == null)
			{
				_secondaryBindInfos = new List<BindInfo>();
			}
			_secondaryBindInfos.Add(bindInfo);
		}

		public NonLazyBinder CopyIntoAllSubContainers()
		{
			SetInheritanceMethod(BindingInheritanceMethods.CopyIntoAll);
			return this;
		}

		public NonLazyBinder CopyIntoDirectSubContainers()
		{
			SetInheritanceMethod(BindingInheritanceMethods.CopyDirectOnly);
			return this;
		}

		public NonLazyBinder MoveIntoAllSubContainers()
		{
			SetInheritanceMethod(BindingInheritanceMethods.MoveIntoAll);
			return this;
		}

		public NonLazyBinder MoveIntoDirectSubContainers()
		{
			SetInheritanceMethod(BindingInheritanceMethods.MoveDirectOnly);
			return this;
		}

		private void SetInheritanceMethod(BindingInheritanceMethods method)
		{
			base.BindInfo.BindingInheritanceMethod = method;
			if (_secondaryBindInfos == null)
			{
				return;
			}
			foreach (BindInfo secondaryBindInfo in _secondaryBindInfos)
			{
				secondaryBindInfo.BindingInheritanceMethod = method;
			}
		}
	}
}
