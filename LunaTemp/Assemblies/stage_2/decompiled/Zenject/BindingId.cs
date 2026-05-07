using System;
using System.Diagnostics;
using ModestTree;

namespace Zenject
{
	[DebuggerStepThrough]
	public struct BindingId : IEquatable<BindingId>
	{
		private Type _type;

		private object _identifier;

		public Type Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		public object Identifier
		{
			get
			{
				return _identifier;
			}
			set
			{
				_identifier = value;
			}
		}

		public BindingId(Type type, object identifier)
		{
			_type = type;
			_identifier = identifier;
		}

		public override string ToString()
		{
			if (_identifier == null)
			{
				return _type.PrettyName();
			}
			return "{0} (ID: {1})".Fmt(_type, _identifier);
		}

		public override int GetHashCode()
		{
			int hash = 17;
			hash = hash * 29 + _type.GetHashCode();
			return hash * 29 + ((_identifier != null) ? _identifier.GetHashCode() : 0);
		}

		public override bool Equals(object other)
		{
			if (other is BindingId otherId)
			{
				return otherId == this;
			}
			return false;
		}

		public bool Equals(BindingId that)
		{
			return this == that;
		}

		public static bool operator ==(BindingId left, BindingId right)
		{
			return left.Type == right.Type && object.Equals(left.Identifier, right.Identifier);
		}

		public static bool operator !=(BindingId left, BindingId right)
		{
			return !left.Equals(right);
		}
	}
}
