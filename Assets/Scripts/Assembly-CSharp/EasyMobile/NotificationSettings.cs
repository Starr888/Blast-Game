using System;
using UnityEngine;

namespace EasyMobile
{
	[Serializable]
	public class NotificationSettings
	{
		[SerializeField]
		private bool _autoInit = true;

		[SerializeField]
		private float _autoInitDelay;

		[SerializeField]
		private string _oneSignalAppId;

		public bool IsAutoInit
		{
			get
			{
				return _autoInit;
			}
		}

		public float AutoInitDelay
		{
			get
			{
				return _autoInitDelay;
			}
		}

		public string OneSignalAppId
		{
			get
			{
				return _oneSignalAppId;
			}
		}
	}
}
