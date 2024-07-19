using System;
using UnityEngine;

namespace EasyMobile
{
	[AddComponentMenu("")]
	public class IAPManager : MonoBehaviour
	{
		public static IAPManager Instance { get; private set; }

		public static event Action InitializeSucceeded;

		public static event Action InitializeFailed;

		public static event Action<IAPProduct> PurchaseCompleted;

		public static event Action<IAPProduct> PurchaseFailed;

		public static event Action RestoreCompleted;

		public static event Action RestoreFailed;

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
		}

		public static void InitializePurchasing()
		{
			Debug.Log("InitializePurchasing FAILED: IAP module is not enabled.");
		}

		public static bool IsInitialized()
		{
			return false;
		}

		public static void Purchase(IAPProduct product)
		{
			if (product != null && product.Id != null)
			{
				PurchaseWithId(product.Id);
			}
			else
			{
				Debug.Log("Purchase FAILED: Either the product or its id is invalid.");
			}
		}

		public static void Purchase(string productName)
		{
			IAPProduct iAPProductByName = GetIAPProductByName(productName);
			if (iAPProductByName != null && iAPProductByName.Id != null)
			{
				PurchaseWithId(iAPProductByName.Id);
			}
			else
			{
				Debug.Log("PurchaseWithName FAILED: Not found product with name: " + productName + " or its id is invalid.");
			}
		}

		public static void PurchaseWithId(string productId)
		{
			Debug.Log("PurchaseWithId FAILED: IAP module is not enabled.");
		}

		public static void RestorePurchases()
		{
			Debug.Log("RestorePurchases FAILED: IAP module is not enabled.");
		}

		public static bool IsProductOwned(string productName)
		{
			Debug.Log("IsProductOwned FAILED: IAP module is not enabled.");
			return false;
		}

		public static void RefreshAppleAppReceipt(Action<string> successCallback, Action errorCallback)
		{
			Debug.Log("RefreshAppleAppReceipt FAILED: IAP module is not enabled.");
		}

		public static IAPProduct GetIAPProductByName(string productName)
		{
			IAPProduct[] products = EM_Settings.InAppPurchasing.Products;
			foreach (IAPProduct iAPProduct in products)
			{
				if (iAPProduct.Name.Equals(productName))
				{
					return iAPProduct;
				}
			}
			return null;
		}

		public static IAPProduct GetIAPProductById(string productId)
		{
			IAPProduct[] products = EM_Settings.InAppPurchasing.Products;
			foreach (IAPProduct iAPProduct in products)
			{
				if (iAPProduct.Id.Equals(productId))
				{
					return iAPProduct;
				}
			}
			return null;
		}
	}
}
