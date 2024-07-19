using System.Collections;
using System.Collections.Generic;
using System.Text;
using SgLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
	public class InAppPurchaseDemo : MonoBehaviour
	{
		public bool logProductLocalizedData;

		public GameObject curtain;

		public GameObject scrollableListPrefab;

		public GameObject isInitInfo;

		public Text ownedProductsInfo;

		public Text selectedProductInfo;

		public GameObject receiptViewer;

		public DemoUtils demoUtils;

		private IAPProduct selectedProduct;

		private List<IAPProduct> ownedProducts = new List<IAPProduct>();

		private void OnEnable()
		{
			IAPManager.PurchaseCompleted += IAPManager_PurchaseCompleted;
			IAPManager.PurchaseFailed += IAPManager_PurchaseFailed;
			IAPManager.RestoreCompleted += IAPManager_RestoreCompleted;
		}

		private void OnDisable()
		{
			IAPManager.PurchaseCompleted -= IAPManager_PurchaseCompleted;
			IAPManager.PurchaseFailed -= IAPManager_PurchaseFailed;
			IAPManager.RestoreCompleted -= IAPManager_RestoreCompleted;
		}

		private void IAPManager_PurchaseCompleted(IAPProduct product)
		{
			if (!ownedProducts.Contains(product))
			{
				ownedProducts.Add(product);
			}
			MobileNativeUI.Alert("Purchased Completed", "The purchase of product " + product.Name + " has completed successfully. This is when you should grant the buyer digital goods.");
		}

		private void IAPManager_PurchaseFailed(IAPProduct product)
		{
			MobileNativeUI.Alert("Purchased Failed", "The purchase of product " + product.Name + " has failed.");
		}

		private void IAPManager_RestoreCompleted()
		{
			MobileNativeUI.Alert("Restore Completed", "Your purchases have been restored successfully.");
		}

		private void Start()
		{
			receiptViewer.SetActive(false);
			curtain.SetActive(!EM_Settings.IsIAPModuleEnable);
			if (logProductLocalizedData)
			{
			}
			StartCoroutine(CheckOwnedProducts());
		}

		private void Update()
		{
			ownedProductsInfo.text = "All purchased products will be listed here.";
			if (IAPManager.IsInitialized())
			{
				demoUtils.DisplayBool(isInitInfo, true, "isInitialized: TRUE");
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = false;
				for (int i = 0; i < ownedProducts.Count; i++)
				{
					IAPProduct iAPProduct = ownedProducts[i];
					if (!flag)
					{
						flag = true;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(iAPProduct.Name);
				}
				string text = stringBuilder.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					ownedProductsInfo.text = text;
				}
			}
			else
			{
				demoUtils.DisplayBool(isInitInfo, false, "isInitialized: FALSE");
			}
		}

		public void SelectProduct()
		{
			IAPProduct[] products = EM_Settings.InAppPurchasing.Products;
			if (products == null || products.Length == 0)
			{
				MobileNativeUI.Alert("Alert", "You don't have any IAP product. Please go to Window > Easy Mobile > Settings and add some.");
				selectedProduct = null;
				return;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			IAPProduct[] array = products;
			foreach (IAPProduct iAPProduct in array)
			{
				dictionary.Add(iAPProduct.Name, iAPProduct.Type.ToString());
			}
			ScrollableList scrollableList = ScrollableList.Create(scrollableListPrefab, "PRODUCTS", dictionary);
			scrollableList.ItemSelected += OnItemSelected;
		}

		public void Purchase()
		{
			if (selectedProduct != null)
			{
				IAPManager.Purchase(selectedProduct.Name);
			}
			else
			{
				MobileNativeUI.Alert("Alert", "Please select a product.");
			}
		}

		public void ViewReceipt()
		{
			if (selectedProduct == null)
			{
				MobileNativeUI.Alert("Alert", "Please select a product.");
			}
			else
			{
				MobileNativeUI.Alert("Alert", "Please enable Unity IAP service.");
			}
		}

		public void ShowReceiptViewer(string receiptContent)
		{
			receiptViewer.transform.Find("Content/ReceiptText").GetComponent<Text>().text = receiptContent;
			receiptViewer.SetActive(true);
		}

		public void HideReceiptViewer()
		{
			receiptViewer.SetActive(false);
		}

		public void RestorePurchases()
		{
			IAPManager.RestorePurchases();
		}

		private void OnItemSelected(ScrollableList list, string title, string subtitle)
		{
			list.ItemSelected -= OnItemSelected;
			selectedProduct = IAPManager.GetIAPProductByName(title);
			selectedProductInfo.text = "Selected product: " + selectedProduct.Name + " (" + selectedProduct.Type.ToString() + ")";
		}

		private IEnumerator CheckOwnedProducts()
		{
			if (!IAPManager.IsInitialized())
			{
				yield return new WaitForSeconds(0.5f);
			}
			IAPProduct[] products = EM_Settings.InAppPurchasing.Products;
			if (products == null || products.Length <= 0)
			{
				yield break;
			}
			foreach (IAPProduct iAPProduct in products)
			{
				if (IAPManager.IsProductOwned(iAPProduct.Name) && !ownedProducts.Contains(iAPProduct))
				{
					ownedProducts.Add(iAPProduct);
				}
			}
		}
	}
}
