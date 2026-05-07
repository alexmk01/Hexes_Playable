namespace Zenject
{
	[NoReflectionBaking]
	public class NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder : TransformScopeConcreteIdArgConditionCopyNonLazyBinder
	{
		public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(BindInfo bindInfo, GameObjectCreationParameters gameObjectInfo)
			: base(bindInfo, gameObjectInfo)
		{
		}

		public TransformScopeConcreteIdArgConditionCopyNonLazyBinder WithGameObjectName(string gameObjectName)
		{
			base.GameObjectInfo.Name = gameObjectName;
			return this;
		}
	}
}
