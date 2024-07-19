using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyMobile
{
	[AddComponentMenu("")]
	public class NotificationManager : MonoBehaviour
	{
		public static NotificationManager Instance { get; private set; }

		public static event Action<string, string, Dictionary<string, object>, bool> NotificationOpened;

		private void Awake()
		{
			if (Instance != null)
			{
				UnityEngine.Object.Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

		private void Start()
		{
			if (EM_Settings.Notification.IsAutoInit)
			{
				StartCoroutine(CRAutoInit(EM_Settings.Notification.AutoInitDelay));
			}
		}

		private IEnumerator CRAutoInit(float delay)
		{
			yield return new WaitForSeconds(delay);
			Init();
		}

		public static void Init()
		{
			Debug.LogError("SDK missing. Please import OneSignal plugin for Unity.");
		}
	}
}
