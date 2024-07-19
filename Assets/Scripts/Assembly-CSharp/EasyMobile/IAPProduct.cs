using System;
using UnityEngine;

namespace EasyMobile
{
	[Serializable]
	public class IAPProduct
	{
		[Serializable]
		public class StoreSpecificId
		{
			public IAPStore store;

			public string id;
		}

		[SerializeField]
		private string _name;

		[SerializeField]
		private IAPProductType _type;

		[SerializeField]
		private string _id;

		[SerializeField]
		private string _price;

		[SerializeField]
		private string _description;

		[SerializeField]
		private StoreSpecificId[] _storeSpecificIds;

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public string Id
		{
			get
			{
				return _id;
			}
		}

		public IAPProductType Type
		{
			get
			{
				return _type;
			}
		}

		public string Price
		{
			get
			{
				return _price;
			}
		}

		public string Description
		{
			get
			{
				return _description;
			}
		}

		public StoreSpecificId[] StoreSpecificIds
		{
			get
			{
				return _storeSpecificIds;
			}
		}
	}
}
