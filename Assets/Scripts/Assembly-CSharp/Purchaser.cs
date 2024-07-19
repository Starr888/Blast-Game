using System;
using EasyMobile;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{
	private static IStoreController m_StoreController;

	private static IExtensionProvider m_StoreExtensionProvider;

	public static string kProductIDConsumable_p1 = "com.bibergames.toyboxblast.60coins";

	public static string kProductIDConsumable_p2 = "com.bibergames.toyboxblast.128coins";

	public static string kProductIDConsumable_p3 = "com.bibergames.toyboxblast.218coins";

	public static string kProductIDConsumable_p4 = "com.bibergames.toyboxblast.400coins";

	public static string kProductIDConsumable_p5 = "com.bibergames.toyboxblast.600coins";

	public static string kProductIDConsumable_p7 = "com.bibergames.toyboxblast.1200coins";

	public static string kProductIDConsumable_p10 = "com.bibergames.toyboxblast.2500coins";

	public static string kProductIDConsumable_p11 = "com.bibergames.toyboxblast.5200coins";

	public static string kProductIDConsumable_p12 = "com.bibergames.toyboxblast.12000coins";

	public static string kProductIDConsumable_p8 = "com.bibergames.toyboxblast.promo1200coins";

	public static string kProductIDConsumable_p9 = "com.bibergames.toyboxblast.promo600coins";

	public static string kProductIDConsumable_pack1 = "com.bibergames.toyboxblast.pack1";

	public static string kProductIDConsumable_pack2 = "com.bibergames.toyboxblast.pack2";

	public static string kProductIDConsumable_pack3 = "com.bibergames.toyboxblast.pack3";

	public static string kProductIDConsumable_pack4 = "com.bibergames.toyboxblast.pack4";

	public static string kProductIDNonConsumable_p6 = "removeads";

	private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

	private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

	private void Start()
	{
		if (m_StoreController == null)
		{
			InitializePurchasing();
		}
	}

	public void InitializePurchasing()
	{
		if (!IsInitialized())
		{
			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			configurationBuilder.AddProduct(kProductIDConsumable_p1, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p2, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p3, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p4, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p5, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p7, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p8, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p9, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p10, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p11, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_p12, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_pack1, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_pack2, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_pack3, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDConsumable_pack4, ProductType.Consumable);
			configurationBuilder.AddProduct(kProductIDNonConsumable_p6, ProductType.NonConsumable);
			UnityPurchasing.Initialize(this, configurationBuilder);
		}
	}

	private bool IsInitialized()
	{
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	public void p1()
	{
		BuyProductID(kProductIDConsumable_p1);
	}

	public void p2()
	{
		BuyProductID(kProductIDConsumable_p2);
	}

	public void p3()
	{
		BuyProductID(kProductIDConsumable_p3);
	}

	public void p4()
	{
		BuyProductID(kProductIDConsumable_p4);
	}

	public void p5()
	{
		BuyProductID(kProductIDConsumable_p5);
	}

	public void p6()
	{
		BuyProductID(kProductIDNonConsumable_p6);
	}

	public void p7()
	{
		BuyProductID(kProductIDConsumable_p7);
	}

	public void p8()
	{
		BuyProductID(kProductIDConsumable_p8);
	}

	public void p9()
	{
		BuyProductID(kProductIDConsumable_p9);
	}

	public void p10()
	{
		BuyProductID(kProductIDConsumable_p10);
	}

	public void p11()
	{
		BuyProductID(kProductIDConsumable_p11);
	}

	public void p12()
	{
		BuyProductID(kProductIDConsumable_p12);
	}

	public void pack1()
	{
		BuyProductID(kProductIDConsumable_pack1);
	}

	public void pack2()
	{
		BuyProductID(kProductIDConsumable_pack2);
	}

	public void pack3()
	{
		BuyProductID(kProductIDConsumable_pack3);
	}

	public void pack4()
	{
		BuyProductID(kProductIDConsumable_pack4);
	}

	private void BuyProductID(string productId)
	{
		if (IsInitialized())
		{
			Product product = m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				m_StoreController.InitiatePurchase(product);
			}
			else
			{
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public void RestorePurchases()
	{
		if (!IsInitialized())
		{
			Debug.Log("RestorePurchases FAIL. Not initialized.");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			Debug.Log("RestorePurchases started ...");
			IAppleExtensions extension = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			extension.RestoreTransactions(delegate(bool result)
			{
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		else
		{
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Debug.Log("OnInitialized: PASS");
		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p1, StringComparison.Ordinal))
		{
			Debug.Log("product1 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 60);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p2, StringComparison.Ordinal))
		{
			Debug.Log("product2 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 128);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p3, StringComparison.Ordinal))
		{
			Debug.Log("product3 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 218);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p4, StringComparison.Ordinal))
		{
			Debug.Log("product4 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 400);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p5, StringComparison.Ordinal))
		{
			Debug.Log("product5 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 600);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable_p6, StringComparison.Ordinal))
		{
			Debug.Log("remove ads alindi");
			AdManager.RemoveAds();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p7, StringComparison.Ordinal))
		{
			Debug.Log("product7 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 1200);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p8, StringComparison.Ordinal))
		{
			Debug.Log("product8 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 1200);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			PlayerPrefs.SetString(Configuration.promo_date, DateTime.Now.ToString());
			PlayerPrefs.Save();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p9, StringComparison.Ordinal))
		{
			Debug.Log("product9 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 600);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			PlayerPrefs.SetString(Configuration.promo_date2, DateTime.Now.ToString());
			PlayerPrefs.Save();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p10, StringComparison.Ordinal))
		{
			Debug.Log("product10 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 2500);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p11, StringComparison.Ordinal))
		{
			Debug.Log("product11 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 5200);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_p12, StringComparison.Ordinal))
		{
			Debug.Log("product12 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 12000);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_pack1, StringComparison.Ordinal))
		{
			Debug.Log("pack1 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 100);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			CoreData.instance.SaveRowBreaker(++CoreData.instance.rowBreaker);
			CoreData.instance.SaveColumnBreaker(++CoreData.instance.columnBreaker);
			CoreData.instance.SaveRainbowBreaker(++CoreData.instance.rainbowBreaker);
			CoreData.instance.SaveOvenBreaker(++CoreData.instance.ovenBreaker);
			CoreData.instance.SaveSingleBreaker(++CoreData.instance.singleBreaker);
			CoreData.instance.SaveBeginBombBreaker(++CoreData.instance.beginBombBreaker);
			CoreData.instance.SaveBeginRainbow(++CoreData.instance.beginRainbow);
			CoreData.instance.SaveBeginFiveMoves(++CoreData.instance.beginFiveMoves);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_pack2, StringComparison.Ordinal))
		{
			Debug.Log("pack2 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 750);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			CoreData.instance.SaveRowBreaker(CoreData.instance.rowBreaker += 3);
			CoreData.instance.SaveColumnBreaker(CoreData.instance.columnBreaker += 3);
			CoreData.instance.SaveRainbowBreaker(CoreData.instance.rainbowBreaker += 3);
			CoreData.instance.SaveOvenBreaker(CoreData.instance.ovenBreaker += 3);
			CoreData.instance.SaveSingleBreaker(CoreData.instance.singleBreaker += 3);
			CoreData.instance.SaveBeginBombBreaker(CoreData.instance.beginBombBreaker += 3);
			CoreData.instance.SaveBeginRainbow(CoreData.instance.beginRainbow += 3);
			CoreData.instance.SaveBeginFiveMoves(CoreData.instance.beginFiveMoves += 3);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_pack3, StringComparison.Ordinal))
		{
			Debug.Log("pack3 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 2000);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			CoreData.instance.SaveRowBreaker(CoreData.instance.rowBreaker += 8);
			CoreData.instance.SaveColumnBreaker(CoreData.instance.columnBreaker += 8);
			CoreData.instance.SaveRainbowBreaker(CoreData.instance.rainbowBreaker += 8);
			CoreData.instance.SaveOvenBreaker(CoreData.instance.ovenBreaker += 8);
			CoreData.instance.SaveSingleBreaker(CoreData.instance.singleBreaker += 8);
			CoreData.instance.SaveBeginBombBreaker(CoreData.instance.beginBombBreaker += 8);
			CoreData.instance.SaveBeginRainbow(CoreData.instance.beginRainbow += 8);
			CoreData.instance.SaveBeginFiveMoves(CoreData.instance.beginFiveMoves += 8);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, kProductIDConsumable_pack4, StringComparison.Ordinal))
		{
			Debug.Log("pack4 alindi");
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 5000);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			CoreData.instance.SaveRowBreaker(CoreData.instance.rowBreaker += 20);
			CoreData.instance.SaveColumnBreaker(CoreData.instance.columnBreaker += 20);
			CoreData.instance.SaveRainbowBreaker(CoreData.instance.rainbowBreaker += 20);
			CoreData.instance.SaveOvenBreaker(CoreData.instance.ovenBreaker += 20);
			CoreData.instance.SaveSingleBreaker(CoreData.instance.singleBreaker += 20);
			CoreData.instance.SaveBeginBombBreaker(CoreData.instance.beginBombBreaker += 20);
			CoreData.instance.SaveBeginRainbow(CoreData.instance.beginRainbow += 20);
			CoreData.instance.SaveBeginFiveMoves(CoreData.instance.beginFiveMoves += 20);
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}
