using System;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class GameObjectCreationParameters
	{
		public static readonly GameObjectCreationParameters Default = new GameObjectCreationParameters();

		public string Name { get; set; }

		public string GroupName { get; set; }

		public Transform ParentTransform { get; set; }

		public Func<InjectContext, Transform> ParentTransformGetter { get; set; }

		public Vector3? Position { get; set; }

		public Quaternion? Rotation { get; set; }

		public override int GetHashCode()
		{
			int hash = 17;
			hash = hash * 29 + ((Name != null) ? Name.GetHashCode() : 0);
			hash = hash * 29 + ((GroupName != null) ? GroupName.GetHashCode() : 0);
			hash = hash * 29 + ((!(ParentTransform == null)) ? ParentTransform.GetHashCode() : 0);
			hash = hash * 29 + ((ParentTransformGetter != null) ? ParentTransformGetter.GetHashCode() : 0);
			hash = hash * 29 + (Position.HasValue ? Position.Value.GetHashCode() : 0);
			return hash * 29 + (Rotation.HasValue ? Rotation.Value.GetHashCode() : 0);
		}

		public override bool Equals(object other)
		{
			if (other is GameObjectCreationParameters)
			{
				GameObjectCreationParameters otherId = (GameObjectCreationParameters)other;
				return otherId == this;
			}
			return false;
		}

		public bool Equals(GameObjectCreationParameters that)
		{
			return this == that;
		}

		public static bool operator ==(GameObjectCreationParameters left, GameObjectCreationParameters right)
		{
			return object.Equals(left.Name, right.Name) && object.Equals(left.GroupName, right.GroupName);
		}

		public static bool operator !=(GameObjectCreationParameters left, GameObjectCreationParameters right)
		{
			return !left.Equals(right);
		}
	}
}
