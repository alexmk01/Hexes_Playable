using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree;
using Zenject.Internal;

namespace Zenject
{
	[NoReflectionBaking]
	public class InjectContext : IDisposable
	{
		private BindingId _bindingId;

		private Type _objectType;

		private InjectContext _parentContext;

		private object _objectInstance;

		private string _memberName;

		private bool _optional;

		private InjectSources _sourceType;

		private object _fallBackValue;

		private object _concreteIdentifier;

		private DiContainer _container;

		public BindingId BindingId => _bindingId;

		public Type ObjectType
		{
			get
			{
				return _objectType;
			}
			set
			{
				_objectType = value;
			}
		}

		public InjectContext ParentContext
		{
			get
			{
				return _parentContext;
			}
			set
			{
				_parentContext = value;
			}
		}

		public object ObjectInstance
		{
			get
			{
				return _objectInstance;
			}
			set
			{
				_objectInstance = value;
			}
		}

		public object Identifier
		{
			get
			{
				return _bindingId.Identifier;
			}
			set
			{
				_bindingId.Identifier = value;
			}
		}

		public string MemberName
		{
			get
			{
				return _memberName;
			}
			set
			{
				_memberName = value;
			}
		}

		public Type MemberType
		{
			get
			{
				return _bindingId.Type;
			}
			set
			{
				_bindingId.Type = value;
			}
		}

		public bool Optional
		{
			get
			{
				return _optional;
			}
			set
			{
				_optional = value;
			}
		}

		public InjectSources SourceType
		{
			get
			{
				return _sourceType;
			}
			set
			{
				_sourceType = value;
			}
		}

		public object ConcreteIdentifier
		{
			get
			{
				return _concreteIdentifier;
			}
			set
			{
				_concreteIdentifier = value;
			}
		}

		public object FallBackValue
		{
			get
			{
				return _fallBackValue;
			}
			set
			{
				_fallBackValue = value;
			}
		}

		public DiContainer Container
		{
			get
			{
				return _container;
			}
			set
			{
				_container = value;
			}
		}

		public IEnumerable<InjectContext> ParentContexts
		{
			get
			{
				if (ParentContext == null)
				{
					yield break;
				}
				yield return ParentContext;
				foreach (InjectContext parentContext in ParentContext.ParentContexts)
				{
					yield return parentContext;
				}
			}
		}

		public IEnumerable<InjectContext> ParentContextsAndSelf
		{
			get
			{
				yield return this;
				foreach (InjectContext parentContext in ParentContexts)
				{
					yield return parentContext;
				}
			}
		}

		public IEnumerable<Type> AllObjectTypes
		{
			get
			{
				foreach (InjectContext context in ParentContextsAndSelf)
				{
					if (context.ObjectType != null)
					{
						yield return context.ObjectType;
					}
				}
			}
		}

		public InjectContext()
		{
			_bindingId = default(BindingId);
			Reset();
		}

		public InjectContext(DiContainer container, Type memberType)
			: this()
		{
			Container = container;
			MemberType = memberType;
		}

		public InjectContext(DiContainer container, Type memberType, object identifier)
			: this(container, memberType)
		{
			Identifier = identifier;
		}

		public InjectContext(DiContainer container, Type memberType, object identifier, bool optional)
			: this(container, memberType, identifier)
		{
			Optional = optional;
		}

		public void Dispose()
		{
			ZenPools.DespawnInjectContext(this);
		}

		public void Reset()
		{
			_objectType = null;
			_parentContext = null;
			_objectInstance = null;
			_memberName = "";
			_optional = false;
			_sourceType = InjectSources.Any;
			_fallBackValue = null;
			_container = null;
			_bindingId.Type = null;
			_bindingId.Identifier = null;
		}

		public InjectContext CreateSubContext(Type memberType)
		{
			return CreateSubContext(memberType, null);
		}

		public InjectContext CreateSubContext(Type memberType, object identifier)
		{
			InjectContext subContext = new InjectContext();
			subContext.ParentContext = this;
			subContext.Identifier = identifier;
			subContext.MemberType = memberType;
			subContext.ConcreteIdentifier = null;
			subContext.MemberName = "";
			subContext.FallBackValue = null;
			subContext.ObjectType = ObjectType;
			subContext.ObjectInstance = ObjectInstance;
			subContext.Optional = Optional;
			subContext.SourceType = SourceType;
			subContext.Container = Container;
			return subContext;
		}

		public InjectContext Clone()
		{
			InjectContext clone = new InjectContext();
			clone.ObjectType = ObjectType;
			clone.ParentContext = ParentContext;
			clone.ConcreteIdentifier = ConcreteIdentifier;
			clone.ObjectInstance = ObjectInstance;
			clone.Identifier = Identifier;
			clone.MemberType = MemberType;
			clone.MemberName = MemberName;
			clone.Optional = Optional;
			clone.SourceType = SourceType;
			clone.FallBackValue = FallBackValue;
			clone.Container = Container;
			return clone;
		}

		public string GetObjectGraphString()
		{
			StringBuilder result = new StringBuilder();
			foreach (InjectContext context in ParentContextsAndSelf.Reverse())
			{
				if (!(context.ObjectType == null))
				{
					result.AppendLine(context.ObjectType.PrettyName());
				}
			}
			return result.ToString();
		}
	}
}
