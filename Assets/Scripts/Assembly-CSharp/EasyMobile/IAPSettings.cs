using System;
using UnityEngine;

namespace EasyMobile
{
	[Serializable]
	public class IAPSettings
	{
		[SerializeField]
		private IAPAndroidStore _targetAndroidStore;

		[SerializeField]
		private bool _validateAppleReceipt = true;

		[SerializeField]
		private bool _validateGooglePlayReceipt = true;

		[SerializeField]
		private IAPProduct[] _products;

		public IAPAndroidStore TargetAndroidStore
		{
			get
			{
				return _targetAndroidStore;
			}
		}

		public bool IsValidateAppleReceipt
		{
			get
			{
				return _validateAppleReceipt;
			}
		}

		public bool IsValidateGooglePlayReceipt
		{
			get
			{
				return _validateGooglePlayReceipt;
			}
		}

		public IAPProduct[] Products
		{
			get
			{
				return _products;
			}
		}
	}
}
