using System;
using System.Collections;
using UnityEngine;

namespace UI.Pagination
{
	public static class PagedRectTimer
	{
		private static void EditorUpdate()
		{
		}

		public static void DelayedCall(float delay, Action action, MonoBehaviour actionTarget)
		{
			if (Application.isPlaying && actionTarget.gameObject.activeInHierarchy)
			{
				actionTarget.StartCoroutine(_DelayedCall(delay, action));
			}
		}

		private static IEnumerator _DelayedCall(float delay, Action action)
		{
			yield return new WaitForSeconds(delay);
			action();
		}
	}
}
