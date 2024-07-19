using UnityEngine;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
	public class AdvertisingDemo : MonoBehaviour
	{
		public GameObject curtain;

		public GameObject isAutoLoadInfo;

		public GameObject isAdRemovedInfo;

		public Text defaultBannerAdNetwork;

		public Text defaultInterstitialAdNetwork;

		public Text defaultRewardedAdNetwork;

		public GameObject isInterstitialAdReadyInfo;

		public GameObject isRewardedAdReadyInfo;

		public DemoUtils demoUtils;

		private void OnEnable()
		{
			AdManager.RewardedAdCompleted += AdManager_RewardedAdCompleted;
			AdManager.InterstitialAdCompleted += AdManager_InterstitialAdCompleted;
		}

		private void OnDisable()
		{
			AdManager.RewardedAdCompleted -= AdManager_RewardedAdCompleted;
			AdManager.InterstitialAdCompleted -= AdManager_InterstitialAdCompleted;
		}

		private void AdManager_InterstitialAdCompleted(InterstitialAdNetwork arg1, AdLocation arg2)
		{
			MobileNativeUI.Alert("Interstitial Ad Completed", "Interstitial ad has been closed.");
		}

		private void AdManager_RewardedAdCompleted(RewardedAdNetwork arg1, AdLocation arg2)
		{
			MobileNativeUI.Alert("Rewarded Ad Completed", "The rewarded ad has completed, this is when you should reward the user.");
		}

		private void Start()
		{
			curtain.SetActive(!EM_Settings.IsAdModuleEnable);
			AdSettings.DefaultAdNetworks defaultAdNetworks = new AdSettings.DefaultAdNetworks(BannerAdNetwork.None, InterstitialAdNetwork.None, RewardedAdNetwork.None);
			defaultAdNetworks = EM_Settings.Advertising.AndroidDefaultAdNetworks;
			defaultBannerAdNetwork.text = "Default banner ad network: " + defaultAdNetworks.bannerAdNetwork;
			defaultInterstitialAdNetwork.text = "Default interstitial ad network: " + defaultAdNetworks.interstitialAdNetwork;
			defaultRewardedAdNetwork.text = "Default rewarded ad network: " + defaultAdNetworks.rewardedAdNetwork;
		}

		private void Update()
		{
			if (AdManager.IsAutoLoadDefaultAds())
			{
				demoUtils.DisplayBool(isAutoLoadInfo, true, "Auto load default ads: ON");
			}
			else
			{
				demoUtils.DisplayBool(isAutoLoadInfo, false, "Auto load default ads: OFF");
			}
			if (AdManager.IsAdRemoved())
			{
				demoUtils.DisplayBool(isAdRemovedInfo, false, "Ads were removed");
			}
			else
			{
				demoUtils.DisplayBool(isAdRemovedInfo, true, "Ads are enabled");
			}
			if (AdManager.IsInterstitialAdReady())
			{
				demoUtils.DisplayBool(isInterstitialAdReadyInfo, true, "isInterstitialAdReady: TRUE");
			}
			else
			{
				demoUtils.DisplayBool(isInterstitialAdReadyInfo, false, "isInterstitialAdReady: FALSE");
			}
			if (AdManager.IsRewardedAdReady())
			{
				demoUtils.DisplayBool(isRewardedAdReadyInfo, true, "isRewardedAdReady: TRUE");
			}
			else
			{
				demoUtils.DisplayBool(isRewardedAdReadyInfo, false, "isRewardedAdReady: FALSE");
			}
		}

		public void ShowBannerAd()
		{
			if (AdManager.IsAdRemoved())
			{
				MobileNativeUI.Alert("Alert", "Ads were removed.");
			}
			else
			{
				AdManager.ShowBannerAd(BannerAdPosition.Bottom);
			}
		}

		public void HideBannerAd()
		{
			AdManager.HideBannerAd();
		}

		public void LoadInterstitialAd()
		{
			if (AdManager.IsAutoLoadDefaultAds())
			{
				MobileNativeUI.Alert("Alert", "autoLoadDefaultAds is currently enabled. Ads will be loaded automatically in background without you having to do anything.");
			}
			AdManager.LoadInterstitialAd();
		}

		public void ShowInterstitialAd()
		{
			if (AdManager.IsAdRemoved())
			{
				MobileNativeUI.Alert("Alert", "Ads were removed.");
			}
			else if (AdManager.IsInterstitialAdReady())
			{
				AdManager.ShowInterstitialAd();
			}
			else
			{
				MobileNativeUI.Alert("Alert", "Interstitial ad is not loaded.");
			}
		}

		public void LoadRewardedAd()
		{
			if (AdManager.IsAutoLoadDefaultAds())
			{
				MobileNativeUI.Alert("Alert", "autoLoadDefaultAds is currently enabled. Ads will be loaded automatically in background without you having to do anything.");
			}
			AdManager.LoadRewardedAd();
		}

		public void ShowRewardedAd()
		{
			if (AdManager.IsRewardedAdReady())
			{
				AdManager.ShowRewardedAd();
			}
			else
			{
				MobileNativeUI.Alert("Alert", "Rewarded ad is not loaded.");
			}
		}

		public void RemoveAds()
		{
			AdManager.RemoveAds();
			MobileNativeUI.Alert("Alert", "Ads were removed. Future ads won't be shown except rewarded ads.");
		}

		public void ResetRemoveAds()
		{
			AdManager.ResetRemoveAds();
			MobileNativeUI.Alert("Alert", "Remove Ads status was reset. Ads will be shown normally.");
		}
	}
}
