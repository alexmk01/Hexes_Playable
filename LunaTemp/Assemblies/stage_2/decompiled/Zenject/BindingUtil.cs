using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject
{
	internal static class BindingUtil
	{
		public static void AssertIsValidPrefab(UnityEngine.Object prefab)
		{
			Assert.That(!ZenUtilInternal.IsNull(prefab), "Received null prefab during bind command");
		}

		public static void AssertIsValidGameObject(GameObject gameObject)
		{
			Assert.That(!ZenUtilInternal.IsNull(gameObject), "Received null game object during bind command");
		}

		public static void AssertIsNotComponent(IEnumerable<Type> types)
		{
			foreach (Type type in types)
			{
				AssertIsNotComponent(type);
			}
		}

		public static void AssertIsNotComponent<T>()
		{
			AssertIsNotComponent(typeof(T));
		}

		public static void AssertIsNotComponent(Type type)
		{
			Assert.That(!type.DerivesFrom(typeof(Component)), "Invalid type given during bind command.  Expected type '{0}' to NOT derive from UnityEngine.Component", type);
		}

		public static void AssertDerivesFromUnityObject(IEnumerable<Type> types)
		{
			foreach (Type type in types)
			{
				AssertDerivesFromUnityObject(type);
			}
		}

		public static void AssertDerivesFromUnityObject<T>()
		{
			AssertDerivesFromUnityObject(typeof(T));
		}

		public static void AssertDerivesFromUnityObject(Type type)
		{
			Assert.That(type.DerivesFrom<UnityEngine.Object>(), "Invalid type given during bind command.  Expected type '{0}' to derive from UnityEngine.Object", type);
		}

		public static void AssertTypesAreNotComponents(IEnumerable<Type> types)
		{
			foreach (Type type in types)
			{
				AssertIsNotComponent(type);
			}
		}

		public static void AssertIsValidResourcePath(string resourcePath)
		{
			Assert.That(!string.IsNullOrEmpty(resourcePath), "Null or empty resource path provided");
		}

		public static void AssertIsInterfaceOrScriptableObject(IEnumerable<Type> types)
		{
			foreach (Type type in types)
			{
				AssertIsInterfaceOrScriptableObject(type);
			}
		}

		public static void AssertIsInterfaceOrScriptableObject<T>()
		{
			AssertIsInterfaceOrScriptableObject(typeof(T));
		}

		public static void AssertIsInterfaceOrScriptableObject(Type type)
		{
			Assert.That(type.DerivesFrom(typeof(ScriptableObject)) || type.IsInterface(), "Invalid type given during bind command.  Expected type '{0}' to either derive from UnityEngine.ScriptableObject or be an interface", type);
		}

		public static void AssertIsInterfaceOrComponent(IEnumerable<Type> types)
		{
			foreach (Type type in types)
			{
				AssertIsInterfaceOrComponent(type);
			}
		}

		public static void AssertIsInterfaceOrComponent<T>()
		{
			AssertIsInterfaceOrComponent(typeof(T));
		}

		public static void AssertIsInterfaceOrComponent(Type type)
		{
			Assert.That(type.DerivesFrom(typeof(Component)) || type.IsInterface(), "Invalid type given during bind command.  Expected type '{0}' to either derive from UnityEngine.Component or be an interface", type);
		}

		public static void AssertIsComponent(IEnumerable<Type> types)
		{
			foreach (Type type in types)
			{
				AssertIsComponent(type);
			}
		}

		public static void AssertIsComponent<T>()
		{
			AssertIsComponent(typeof(T));
		}

		public static void AssertIsComponent(Type type)
		{
			Assert.That(type.DerivesFrom(typeof(Component)), "Invalid type given during bind command.  Expected type '{0}' to derive from UnityEngine.Component", type);
		}

		public static void AssertTypesAreNotAbstract(IEnumerable<Type> types)
		{
			foreach (Type type in types)
			{
				AssertIsNotAbstract(type);
			}
		}

		public static void AssertIsNotAbstract(IEnumerable<Type> types)
		{
			foreach (Type type in types)
			{
				AssertIsNotAbstract(type);
			}
		}

		public static void AssertIsNotAbstract<T>()
		{
			AssertIsNotAbstract(typeof(T));
		}

		public static void AssertIsNotAbstract(Type type)
		{
			Assert.That(!type.IsAbstract(), "Invalid type given during bind command.  Expected type '{0}' to not be abstract.", type);
		}

		public static void AssertIsDerivedFromType(Type concreteType, Type parentType)
		{
			Assert.That(parentType.IsOpenGenericType() == concreteType.IsOpenGenericType(), "Invalid type given during bind command.  Expected type '{0}' and type '{1}' to both either be open generic types or not open generic types", parentType, concreteType);
			if (parentType.IsOpenGenericType())
			{
				Assert.That(concreteType.IsOpenGenericType());
				Assert.That(TypeExtensions.IsAssignableToGenericType(concreteType, parentType), "Invalid type given during bind command.  Expected open generic type '{0}' to derive from open generic type '{1}'", concreteType, parentType);
			}
			else
			{
				Assert.That(concreteType.DerivesFromOrEqual(parentType), "Invalid type given during bind command.  Expected type '{0}' to derive from type '{1}'", concreteType, parentType);
			}
		}

		public static void AssertConcreteTypeListIsNotEmpty(IEnumerable<Type> concreteTypes)
		{
			Assert.That(concreteTypes.Count() >= 1, "Must supply at least one concrete type to the current binding");
		}

		public static void AssertIsDerivedFromTypes(IEnumerable<Type> concreteTypes, IEnumerable<Type> parentTypes, InvalidBindResponses invalidBindResponse)
		{
			if (invalidBindResponse == InvalidBindResponses.Assert)
			{
				AssertIsDerivedFromTypes(concreteTypes, parentTypes);
			}
			else
			{
				Assert.IsEqual(invalidBindResponse, InvalidBindResponses.Skip);
			}
		}

		public static void AssertIsDerivedFromTypes(IEnumerable<Type> concreteTypes, IEnumerable<Type> parentTypes)
		{
			foreach (Type concreteType in concreteTypes)
			{
				AssertIsDerivedFromTypes(concreteType, parentTypes);
			}
		}

		public static void AssertIsDerivedFromTypes(Type concreteType, IEnumerable<Type> parentTypes)
		{
			foreach (Type parentType in parentTypes)
			{
				AssertIsDerivedFromType(concreteType, parentType);
			}
		}

		public static void AssertInstanceDerivesFromOrEqual(object instance, IEnumerable<Type> parentTypes)
		{
			if (ZenUtilInternal.IsNull(instance))
			{
				return;
			}
			foreach (Type baseType in parentTypes)
			{
				AssertInstanceDerivesFromOrEqual(instance, baseType);
			}
		}

		public static void AssertInstanceDerivesFromOrEqual(object instance, Type baseType)
		{
			if (!ZenUtilInternal.IsNull(instance))
			{
				Assert.That(instance.GetType().DerivesFromOrEqual(baseType), "Invalid type given during bind command.  Expected type '{0}' to derive from type '{1}'", instance.GetType(), baseType);
			}
		}

		public static IProvider CreateCachedProvider(IProvider creator)
		{
			if (creator.TypeVariesBasedOnMemberType)
			{
				return new CachedOpenTypeProvider(creator);
			}
			return new CachedProvider(creator);
		}
	}
}
