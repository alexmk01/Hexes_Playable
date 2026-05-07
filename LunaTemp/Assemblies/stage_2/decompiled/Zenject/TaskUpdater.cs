using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ModestTree;

namespace Zenject
{
	[DebuggerStepThrough]
	public abstract class TaskUpdater<TTask>
	{
		private class TaskInfo
		{
			public TTask Task;

			public int Priority;

			public bool IsRemoved;

			public TaskInfo(TTask task, int priority)
			{
				Task = task;
				Priority = priority;
			}
		}

		private readonly LinkedList<TaskInfo> _tasks = new LinkedList<TaskInfo>();

		private readonly List<TaskInfo> _queuedTasks = new List<TaskInfo>();

		private IEnumerable<TaskInfo> AllTasks => ActiveTasks.Concat(_queuedTasks);

		private IEnumerable<TaskInfo> ActiveTasks => _tasks;

		public void AddTask(TTask task, int priority)
		{
			AddTaskInternal(task, priority);
		}

		private void AddTaskInternal(TTask task, int priority)
		{
			Assert.That(!AllTasks.Select((TaskInfo x) => x.Task).ContainsItem(task), "Duplicate task added to DependencyRoot with name '" + task.GetType().FullName + "'");
			_queuedTasks.Add(new TaskInfo(task, priority));
		}

		public void RemoveTask(TTask task)
		{
			TaskInfo info = AllTasks.Where((TaskInfo x) => (object)x.Task == (object)task).SingleOrDefault();
			Assert.IsNotNull(info, "Tried to remove a task not added to DependencyRoot, task = " + task.GetType().Name);
			Assert.That(!info.IsRemoved, "Tried to remove task twice, task = " + task.GetType().Name);
			info.IsRemoved = true;
		}

		public void OnFrameStart()
		{
			AddQueuedTasks();
		}

		public void UpdateAll()
		{
			UpdateRange(int.MinValue, int.MaxValue);
		}

		public void UpdateRange(int minPriority, int maxPriority)
		{
			LinkedListNode<TaskInfo> node = _tasks.First;
			while (node != null)
			{
				LinkedListNode<TaskInfo> next = node.Next;
				TaskInfo taskInfo = node.Value;
				if (!taskInfo.IsRemoved && taskInfo.Priority >= minPriority && (maxPriority == int.MaxValue || taskInfo.Priority < maxPriority))
				{
					UpdateItem(taskInfo.Task);
				}
				node = next;
			}
			ClearRemovedTasks(_tasks);
		}

		private void ClearRemovedTasks(LinkedList<TaskInfo> tasks)
		{
			LinkedListNode<TaskInfo> node = tasks.First;
			while (node != null)
			{
				LinkedListNode<TaskInfo> next = node.Next;
				TaskInfo info = node.Value;
				if (info.IsRemoved)
				{
					tasks.Remove(node);
				}
				node = next;
			}
		}

		private void AddQueuedTasks()
		{
			for (int i = 0; i < _queuedTasks.Count; i++)
			{
				TaskInfo task = _queuedTasks[i];
				if (!task.IsRemoved)
				{
					InsertTaskSorted(task);
				}
			}
			_queuedTasks.Clear();
		}

		private void InsertTaskSorted(TaskInfo task)
		{
			for (LinkedListNode<TaskInfo> current = _tasks.First; current != null; current = current.Next)
			{
				if (current.Value.Priority > task.Priority)
				{
					_tasks.AddBefore(current, task);
					return;
				}
			}
			_tasks.AddLast(task);
		}

		protected abstract void UpdateItem(TTask task);
	}
}
