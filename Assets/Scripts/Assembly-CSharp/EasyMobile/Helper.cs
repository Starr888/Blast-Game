using System.Collections;
using UnityEngine;

namespace EasyMobile
{
	[AddComponentMenu("")]
	public class Helper : MonoBehaviour
	{
		private static Helper _instance;

		public static Helper Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameObject("EM_Helper").AddComponent<Helper>();
				}
				return _instance;
			}
		}

		private void OnDestroy()
		{
			if (_instance == this)
			{
				_instance = null;
			}
		}

		public static void DestroyProxy()
		{
			if (_instance != null)
			{
				Object.Destroy(_instance.gameObject);
				_instance = null;
			}
		}

		public static void StaticStartCoroutine(IEnumerator routine)
		{
			Instance.StartCoroutine(routine);
		}

		public static void StaticStopCoroutine(IEnumerator routine)
		{
			Instance.StopCoroutine(routine);
		}
	}
}
