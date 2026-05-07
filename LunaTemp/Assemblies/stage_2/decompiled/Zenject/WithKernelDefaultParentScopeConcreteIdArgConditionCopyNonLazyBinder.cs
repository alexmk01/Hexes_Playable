namespace Zenject
{
	[NoReflectionBaking]
	public class WithKernelDefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder : DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder
	{
		public WithKernelDefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(SubContainerCreatorBindInfo subContainerBindInfo, BindInfo bindInfo)
			: base(subContainerBindInfo, bindInfo)
		{
		}

		public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder WithKernel()
		{
			base.SubContainerCreatorBindInfo.CreateKernel = true;
			return this;
		}

		public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder WithKernel<TKernel>() where TKernel : Kernel
		{
			base.SubContainerCreatorBindInfo.CreateKernel = true;
			base.SubContainerCreatorBindInfo.KernelType = typeof(TKernel);
			return this;
		}
	}
}
