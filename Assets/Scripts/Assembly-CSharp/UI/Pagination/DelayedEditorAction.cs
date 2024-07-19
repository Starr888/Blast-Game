using System;
using UnityEngine;

namespace UI.Pagination
{
	internal class DelayedEditorAction
	{
		internal double TimeToExecute;

		internal Action Action;

		internal MonoBehaviour ActionTarget;

		public DelayedEditorAction(double timeToExecute, Action action, MonoBehaviour actionTarget)
		{
			TimeToExecute = timeToExecute;
			Action = action;
			ActionTarget = actionTarget;
		}
	}
}
