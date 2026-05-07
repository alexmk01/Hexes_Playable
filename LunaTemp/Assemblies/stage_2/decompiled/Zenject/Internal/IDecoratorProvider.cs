using System.Collections.Generic;

namespace Zenject.Internal
{
	public interface IDecoratorProvider
	{
		void GetAllInstances(IProvider provider, InjectContext context, List<object> buffer);
	}
}
