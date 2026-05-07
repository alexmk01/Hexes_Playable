using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;

namespace Zenject
{
	public class GuiRenderableManager
	{
		private class RenderableInfo
		{
			public IGuiRenderable Renderable;

			public int Priority;

			public RenderableInfo(IGuiRenderable renderable, int priority)
			{
				Renderable = renderable;
				Priority = priority;
			}
		}

		private List<RenderableInfo> _renderables;

		public GuiRenderableManager([Inject(Optional = true, Source = InjectSources.Local)] List<IGuiRenderable> renderables, [Inject(Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> priorities)
		{
			_renderables = new List<RenderableInfo>();
			foreach (IGuiRenderable renderable in renderables)
			{
				List<int> matches = (from x in priorities
					where renderable.GetType().DerivesFromOrEqual(x.First)
					select x.Second).ToList();
				int priority = ((!matches.IsEmpty()) ? matches.Distinct().Single() : 0);
				_renderables.Add(new RenderableInfo(renderable, priority));
			}
			_renderables = _renderables.OrderBy((RenderableInfo x) => x.Priority).ToList();
		}
	}
}
