using System;
using UnityEngine;

namespace EasyMobile
{
	[Serializable]
	public class GameServiceSettings
	{
		[SerializeField]
		private bool _gpgsDebugLog;

		[SerializeField]
		private bool _autoInit = true;

		[SerializeField]
		private float _autoInitDelay;

		[SerializeField]
		private int _androidMaxLoginRequests = 3;

		[SerializeField]
		private Leaderboard[] _leaderboards;

		[SerializeField]
		private Achievement[] _achievements;

		[SerializeField]
		private string _androidXmlResources = string.Empty;

		public bool IsGPGSDebug
		{
			get
			{
				return _gpgsDebugLog;
			}
			set
			{
				_gpgsDebugLog = value;
			}
		}

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

		public int AndroidMaxLoginRequests
		{
			get
			{
				return _androidMaxLoginRequests;
			}
		}

		public Leaderboard[] Leaderboards
		{
			get
			{
				return _leaderboards;
			}
		}

		public Achievement[] Achievements
		{
			get
			{
				return _achievements;
			}
		}
	}
}
