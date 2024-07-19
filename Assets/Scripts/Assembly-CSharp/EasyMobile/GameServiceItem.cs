using System;
using UnityEngine;

namespace EasyMobile
{
	[Serializable]
	public class GameServiceItem
	{
		[SerializeField]
		private string _name;

		[SerializeField]
		private string _iosId;

		[SerializeField]
		private string _androidId;

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public string IOSId
		{
			get
			{
				return _iosId;
			}
		}

		public string AndroidId
		{
			get
			{
				return _androidId;
			}
		}

		public string Id
		{
			get
			{
				return _androidId;
			}
		}

		public GameServiceItem(string name, string iosId, string androidId)
		{
			_name = name;
			_iosId = iosId;
			_androidId = androidId;
		}
	}
}
