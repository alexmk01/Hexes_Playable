using JetBrains.Annotations;
using Zenject.Internal;

namespace Zenject
{
	[MeansImplicitUse(ImplicitUseKindFlags.Assign)]
	public abstract class InjectAttributeBase : PreserveAttribute
	{
		public bool Optional { get; set; }

		public object Id { get; set; }

		public InjectSources Source { get; set; }
	}
}
