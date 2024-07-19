using UnityEngine;

namespace EasyMobile
{
	public class EM_PrefabManager : MonoBehaviour
	{
		public static EM_PrefabManager Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null)
			{
				Object.Destroy(base.gameObject);
				return;
			}
			Instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
			SetLogEnabled(true);
		}

		private void SetLogEnabled(bool isEnabled)
		{
			Debug.unityLogger.logEnabled = isEnabled;
		}

		private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}
	}
}
