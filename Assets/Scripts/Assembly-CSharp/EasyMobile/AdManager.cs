using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace EasyMobile
{
	[AddComponentMenu("")]
	public class AdManager : MonoBehaviour
	{
		private const string UNITYADS_REWARDED_ZONE_ID = "rewardedVideo";

		private static float lastInterstitialAdLoadTimestamp;

		private static float lastRewardedAdLoadTimestamp;

		private static List<BannerAdNetwork> activeBannerAdNetworks;

		private const string AD_REMOVE_STATUS_PPKEY = "EM_REMOVE_ADS";

		private const int AD_ENABLED = 1;

		private const int AD_DISABLED = -1;

		private static BannerView admobBannerView;

		private static InterstitialAd admobInterstitial;

		private static RewardBasedVideoAd admobRewardedAd;

		private static bool isAdMobInterstitialClosed;

		private static bool isAdMobRewardedAdPlaying;

		private static bool isAdMobRewardedAdClosed;

		private static bool isAdMobRewardedAdCompleted;

		private static IEnumerator autoLoadAdsCoroutine;

		private static bool isAutoLoadDefaultAds;

		public static AdManager Instance { get; private set; }

		public static event Action<InterstitialAdNetwork, AdLocation> InterstitialAdCompleted;

		public static event Action<RewardedAdNetwork, AdLocation> RewardedAdCompleted;

		public static event Action AdsRemoved;

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

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void Start()
		{
			isAutoLoadDefaultAds = EM_Settings.Advertising.IsAutoLoadDefaultAds;
			if (!isAutoLoadDefaultAds)
			{
			}
		}

		private void Update()
		{
			if (isAutoLoadDefaultAds != EM_Settings.Advertising.IsAutoLoadDefaultAds)
			{
				SetAutoLoadDefaultAds(EM_Settings.Advertising.IsAutoLoadDefaultAds);
			}
			if (isAdMobInterstitialClosed)
			{
				isAdMobInterstitialClosed = false;
				AdManager.InterstitialAdCompleted(InterstitialAdNetwork.AdMob, AdLocation.Default);
			}
			if (isAdMobRewardedAdClosed)
			{
				isAdMobRewardedAdClosed = false;
				isAdMobRewardedAdPlaying = false;
				if (isAdMobRewardedAdCompleted)
				{
					isAdMobRewardedAdCompleted = false;
					AdManager.RewardedAdCompleted(RewardedAdNetwork.AdMob, AdLocation.Default);
				}
			}
		}

		public static bool IsAutoLoadDefaultAds()
		{
			return EM_Settings.Advertising.IsAutoLoadDefaultAds;
		}

		public static void SetAutoLoadDefaultAds(bool isAutoLoad)
		{
			isAutoLoadDefaultAds = isAutoLoad;
			EM_Settings.Advertising.IsAutoLoadDefaultAds = isAutoLoad;
			if (!isAutoLoad)
			{
				if (autoLoadAdsCoroutine != null)
				{
					Instance.StopCoroutine(autoLoadAdsCoroutine);
					autoLoadAdsCoroutine = null;
				}
			}
			else if (autoLoadAdsCoroutine != null)
			{
			}
		}

		public static void ShowBannerAd(BannerAdPosition position)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				ShowBannerAd(EM_Settings.Advertising.AndroidDefaultAdNetworks.bannerAdNetwork, position, BannerAdSize.SmartBanner);
				break;
			case RuntimePlatform.IPhonePlayer:
				ShowBannerAd(EM_Settings.Advertising.IosDefaultAdNetworks.bannerAdNetwork, position, BannerAdSize.SmartBanner);
				break;
			}
		}

		public static void ShowBannerAd(BannerAdPosition position, BannerAdSize size)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				ShowBannerAd(EM_Settings.Advertising.AndroidDefaultAdNetworks.bannerAdNetwork, position, size);
				break;
			case RuntimePlatform.IPhonePlayer:
				ShowBannerAd(EM_Settings.Advertising.IosDefaultAdNetworks.bannerAdNetwork, position, size);
				break;
			}
		}

		public static void ShowBannerAd(BannerAdNetwork adNetwork, BannerAdPosition position, BannerAdSize size)
		{
			if (IsAdRemoved())
			{
				Debug.Log("ShowBannerAd FAILED: Ads were removed.");
				return;
			}
			switch ((AdNetwork)adNetwork)
			{
			case AdNetwork.AdMob:
				if (admobBannerView == null)
				{
					admobBannerView = new BannerView(EM_Settings.Advertising.AdMobIds.bannerAdId, size.ToAdMobAdSize(), position.ToAdMobAdPosition());
					if (admobBannerView != null)
					{
						admobBannerView.OnAdLoaded += HandleAdMobBannerAdLoaded;
						admobBannerView.OnAdFailedToLoad += HandleAdMobBannerAdFailedToLoad;
						admobBannerView.OnAdOpening += HandleAdMobBannerAdOpened;
						admobBannerView.OnAdClosed += HandleAdMobBannerAdClosed;
						admobBannerView.OnAdLeavingApplication += HandleAdMobBannerAdLeftApplication;
						admobBannerView.LoadAd(CreateAdMobAdRequest());
					}
				}
				if (admobBannerView != null)
				{
					admobBannerView.Show();
					if (!activeBannerAdNetworks.Contains(adNetwork))
					{
						activeBannerAdNetworks.Add(adNetwork);
					}
				}
				break;
			case AdNetwork.Heyzap:
				Debug.LogError("SDK missing. Please import Heyzap plugin.");
				break;
			}
		}

		public static void HideBannerAd()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				HideBannerAd(EM_Settings.Advertising.AndroidDefaultAdNetworks.bannerAdNetwork);
				break;
			case RuntimePlatform.IPhonePlayer:
				HideBannerAd(EM_Settings.Advertising.IosDefaultAdNetworks.bannerAdNetwork);
				break;
			}
		}

		public static void HideBannerAd(BannerAdNetwork adNetwork)
		{
			switch ((AdNetwork)adNetwork)
			{
			case AdNetwork.AdMob:
				if (admobBannerView != null)
				{
					admobBannerView.Hide();
					if (activeBannerAdNetworks.Contains(adNetwork))
					{
						activeBannerAdNetworks.Remove(adNetwork);
					}
				}
				break;
			case AdNetwork.Heyzap:
				Debug.LogError("SDK missing. Please import Heyzap plugin.");
				break;
			}
		}

		public static void DestroyBannerAd()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				DestroyBannerAd(EM_Settings.Advertising.AndroidDefaultAdNetworks.bannerAdNetwork);
				break;
			case RuntimePlatform.IPhonePlayer:
				DestroyBannerAd(EM_Settings.Advertising.IosDefaultAdNetworks.bannerAdNetwork);
				break;
			}
		}

		public static void DestroyBannerAd(BannerAdNetwork adNetwork)
		{
			switch ((AdNetwork)adNetwork)
			{
			case AdNetwork.AdMob:
				if (admobBannerView != null)
				{
					admobBannerView.Destroy();
					admobBannerView = null;
					if (activeBannerAdNetworks.Contains(adNetwork))
					{
						activeBannerAdNetworks.Remove(adNetwork);
					}
				}
				break;
			case AdNetwork.Heyzap:
				Debug.LogError("SDK missing. Please import Heyzap plugin.");
				break;
			}
		}

		public static bool IsShowingBannerAd()
		{
			return activeBannerAdNetworks.Count > 0;
		}

		public static BannerAdNetwork[] GetActiveBannerAdNetworks()
		{
			return activeBannerAdNetworks.ToArray();
		}

		public static void LoadInterstitialAd()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				LoadInterstitialAd(EM_Settings.Advertising.AndroidDefaultAdNetworks.interstitialAdNetwork, AdLocation.Default);
				break;
			case RuntimePlatform.IPhonePlayer:
				LoadInterstitialAd(EM_Settings.Advertising.IosDefaultAdNetworks.interstitialAdNetwork, AdLocation.Default);
				break;
			}
		}

		public static void LoadInterstitialAd(InterstitialAdNetwork adNetwork, AdLocation location)
		{
			if (IsAdRemoved())
			{
				return;
			}
			switch ((AdNetwork)adNetwork)
			{
			case AdNetwork.AdColony:
				Debug.LogError("SDK missing. Please import AdColony plugin.");
				break;
			case AdNetwork.AdMob:
				if (admobInterstitial != null)
				{
					admobInterstitial.Destroy();
					admobInterstitial = null;
				}
				admobInterstitial = new InterstitialAd(EM_Settings.Advertising.AdMobIds.interstitialAdId);
				if (admobInterstitial != null)
				{
					admobInterstitial.OnAdLoaded += HandleAdMobInterstitialLoaded;
					admobInterstitial.OnAdFailedToLoad += HandleAdMobInterstitialFailedToLoad;
					admobInterstitial.OnAdOpening += HandleAdMobInterstitialOpened;
					admobInterstitial.OnAdClosed += HandleAdMobInterstitialClosed;
					admobInterstitial.OnAdLeavingApplication += HandleAdMobInterstitialLeftApplication;
					admobInterstitial.LoadAd(CreateAdMobAdRequest());
				}
				break;
			case AdNetwork.Heyzap:
				Debug.LogError("SDK missing. Please import Heyzap plugin.");
				break;
			case AdNetwork.UnityAds:
				Debug.LogError("SDK missing. Please enable Unity Ads service.");
				break;
			case AdNetwork.Chartboost:
				break;
			}
		}

		public static bool IsInterstitialAdReady()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return IsInterstitialAdReady(EM_Settings.Advertising.AndroidDefaultAdNetworks.interstitialAdNetwork, AdLocation.Default);
			case RuntimePlatform.IPhonePlayer:
				return IsInterstitialAdReady(EM_Settings.Advertising.IosDefaultAdNetworks.interstitialAdNetwork, AdLocation.Default);
			default:
				return false;
			}
		}

		public static bool IsInterstitialAdReady(InterstitialAdNetwork adNetwork, AdLocation location)
		{
			if (IsAdRemoved())
			{
				return false;
			}
			switch ((AdNetwork)adNetwork)
			{
			case AdNetwork.AdColony:
				return false;
			case AdNetwork.AdMob:
				return admobInterstitial != null && admobInterstitial.IsLoaded();
			case AdNetwork.Heyzap:
				return false;
			case AdNetwork.UnityAds:
				return false;
			default:
				return false;
			}
		}

		public static void ShowInterstitialAd()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				ShowInterstitialAd(EM_Settings.Advertising.AndroidDefaultAdNetworks.interstitialAdNetwork, AdLocation.Default);
				break;
			case RuntimePlatform.IPhonePlayer:
				ShowInterstitialAd(EM_Settings.Advertising.IosDefaultAdNetworks.interstitialAdNetwork, AdLocation.Default);
				break;
			}
		}

		public static void ShowInterstitialAd(InterstitialAdNetwork adNetwork, AdLocation location)
		{
			if (IsAdRemoved())
			{
				Debug.Log("ShowInterstitialAd FAILED: Ads were removed.");
				return;
			}
			if (!IsInterstitialAdReady(adNetwork, location))
			{
				Debug.Log("ShowInterstitialAd FAILED: Interstitial ad is not loaded.");
				return;
			}
			switch ((AdNetwork)adNetwork)
			{
			case AdNetwork.AdColony:
				Debug.LogError("SDK missing. Please import AdColony plugin.");
				break;
			case AdNetwork.AdMob:
				admobInterstitial.Show();
				break;
			case AdNetwork.Heyzap:
				Debug.LogError("SDK missing. Please import Heyzap plugin.");
				break;
			case AdNetwork.UnityAds:
				Debug.LogError("SDK missing. Please enable Unity Ads service.");
				break;
			case AdNetwork.Chartboost:
				break;
			}
		}

		public static void LoadRewardedAd()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				LoadRewardedAd(EM_Settings.Advertising.AndroidDefaultAdNetworks.rewardedAdNetwork, AdLocation.Default);
				break;
			case RuntimePlatform.IPhonePlayer:
				LoadRewardedAd(EM_Settings.Advertising.IosDefaultAdNetworks.rewardedAdNetwork, AdLocation.Default);
				break;
			}
		}

		public static void LoadRewardedAd(RewardedAdNetwork adNetwork, AdLocation location)
		{
			switch ((AdNetwork)adNetwork)
			{
			case AdNetwork.AdColony:
				Debug.LogError("SDK missing. Please import AdColony plugin.");
				break;
			case AdNetwork.AdMob:
				if (!isAdMobRewardedAdPlaying)
				{
					if (admobRewardedAd == null)
					{
						admobRewardedAd = RewardBasedVideoAd.Instance;
						admobRewardedAd.OnAdLoaded += HandleAdMobRewardBasedVideoLoaded;
						admobRewardedAd.OnAdFailedToLoad += HandleAdMobRewardBasedVideoFailedToLoad;
						admobRewardedAd.OnAdOpening += HandleAdMobRewardBasedVideoOpened;
						admobRewardedAd.OnAdStarted += HandleAdMobRewardBasedVideoStarted;
						admobRewardedAd.OnAdRewarded += HandleAdMobRewardBasedVideoRewarded;
						admobRewardedAd.OnAdClosed += HandleAdMobRewardBasedVideoClosed;
						admobRewardedAd.OnAdLeavingApplication += HandleAdMobRewardBasedVideoLeftApplication;
					}
					admobRewardedAd.LoadAd(CreateAdMobAdRequest(), EM_Settings.Advertising.AdMobIds.rewardedAdId);
				}
				break;
			case AdNetwork.Heyzap:
				Debug.LogError("SDK missing. Please import Heyzap plugin.");
				break;
			case AdNetwork.UnityAds:
				Debug.LogError("SDK missing. Please enable Unity Ads service.");
				break;
			case AdNetwork.Chartboost:
				break;
			}
		}

		public static bool IsRewardedAdReady()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return IsRewardedAdReady(EM_Settings.Advertising.AndroidDefaultAdNetworks.rewardedAdNetwork, AdLocation.Default);
			case RuntimePlatform.IPhonePlayer:
				return IsRewardedAdReady(EM_Settings.Advertising.IosDefaultAdNetworks.rewardedAdNetwork, AdLocation.Default);
			default:
				return false;
			}
		}

		public static bool IsRewardedAdReady(RewardedAdNetwork adNetwork, AdLocation location)
		{
			switch ((AdNetwork)adNetwork)
			{
			case AdNetwork.AdColony:
				return false;
			case AdNetwork.AdMob:
				return admobRewardedAd != null && admobRewardedAd.IsLoaded();
			case AdNetwork.Heyzap:
				return false;
			case AdNetwork.UnityAds:
				return false;
			default:
				return false;
			}
		}

		public static void ShowRewardedAd()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				ShowRewardedAd(EM_Settings.Advertising.AndroidDefaultAdNetworks.rewardedAdNetwork, AdLocation.Default);
				break;
			case RuntimePlatform.IPhonePlayer:
				ShowRewardedAd(EM_Settings.Advertising.IosDefaultAdNetworks.rewardedAdNetwork, AdLocation.Default);
				break;
			}
		}

		public static void ShowRewardedAd(RewardedAdNetwork adNetwork, AdLocation location)
		{
			if (!IsRewardedAdReady(adNetwork, location))
			{
				Debug.Log("ShowRewardedAd FAILED: Rewarded ad is not loaded.");
				return;
			}
			switch ((AdNetwork)adNetwork)
			{
			case AdNetwork.AdColony:
				Debug.LogError("SDK missing. Please import AdColony plugin.");
				break;
			case AdNetwork.AdMob:
				isAdMobRewardedAdPlaying = true;
				admobRewardedAd.Show();
				break;
			case AdNetwork.Heyzap:
				Debug.LogError("SDK missing. Please import Heyzap plugin.");
				break;
			case AdNetwork.UnityAds:
				Debug.LogError("SDK missing. Please enable Unity Ads service.");
				break;
			case AdNetwork.Chartboost:
				break;
			}
		}

		public static bool IsAdRemoved()
		{
			return PlayerPrefs.GetInt("EM_REMOVE_ADS", 1) == -1;
		}

		public static void RemoveAds()
		{
			Debug.Log("******* REMOVING ADS... *******");
			DestroyBannerAd();
			PlayerPrefs.SetInt("EM_REMOVE_ADS", -1);
			PlayerPrefs.Save();
			AdManager.AdsRemoved();
		}

		public static void ResetRemoveAds()
		{
			Debug.Log("******* RESET REMOVE ADS STATUS... *******");
			PlayerPrefs.SetInt("EM_REMOVE_ADS", 1);
			PlayerPrefs.Save();
		}

		private static AdRequest CreateAdMobAdRequest()
		{
			AdRequest.Builder builder = new AdRequest.Builder();
			if (EM_Settings.Advertising.AdMobEnableTestMode)
			{
				builder.AddTestDevice("SIMULATOR");
				for (int i = 0; i < EM_Settings.Advertising.AdMobTestDeviceIds.Length; i++)
				{
					builder.AddTestDevice(EM_Settings.Advertising.AdMobTestDeviceIds[i]);
				}
			}
			return builder.Build();
		}

		private static void HandleAdMobBannerAdLoaded(object sender, EventArgs args)
		{
			Debug.Log("AdMob banner ad was loaded successfully.");
		}

		private static void HandleAdMobBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
			Debug.Log("AdMob banner ad failed to load.");
		}

		private static void HandleAdMobBannerAdOpened(object sender, EventArgs args)
		{
			Debug.Log("AdMob banner ad was clicked.");
		}

		private static void HandleAdMobBannerAdClosed(object sender, EventArgs args)
		{
			Debug.Log("AdMob banner ad was closed.");
		}

		private static void HandleAdMobBannerAdLeftApplication(object sender, EventArgs args)
		{
			Debug.Log("HandleAdMobBannerAdLeftApplication event received");
		}

		private static void HandleAdMobInterstitialLoaded(object sender, EventArgs args)
		{
			Debug.Log("AdMob interstitial ad was loaded successfully.");
		}

		private static void HandleAdMobInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
			Debug.Log("AdMob interstitial ad failed to load.");
		}

		private static void HandleAdMobInterstitialOpened(object sender, EventArgs args)
		{
			Debug.Log("AdMob interstitial ad was clicked.");
		}

		private static void HandleAdMobInterstitialClosed(object sender, EventArgs args)
		{
			Debug.Log("AdMob interstitial ad was closed.");
			isAdMobInterstitialClosed = true;
		}

		private static void HandleAdMobInterstitialLeftApplication(object sender, EventArgs args)
		{
			Debug.Log("HandleAdMobInterstitialLeftApplication event received");
		}

		private static void HandleAdMobRewardBasedVideoLoaded(object sender, EventArgs args)
		{
			Debug.Log("AdMob rewarded video ad was loaded successfully.");
		}

		private static void HandleAdMobRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
			Debug.Log("AdMob rewarded video ad failed to load.");
		}

		private static void HandleAdMobRewardBasedVideoOpened(object sender, EventArgs args)
		{
			Debug.Log("AdMob rewarded video ad was clicked.");
		}

		private static void HandleAdMobRewardBasedVideoStarted(object sender, EventArgs args)
		{
			Debug.Log("AdMob rewarded video ad has started.");
		}

		private static void HandleAdMobRewardBasedVideoClosed(object sender, EventArgs args)
		{
			Debug.Log("AdMob rewarded video ad was closed.");
			isAdMobRewardedAdClosed = true;
		}

		private static void HandleAdMobRewardBasedVideoRewarded(object sender, Reward args)
		{
			Debug.Log("AdMob rewarded video ad was completed.");
			isAdMobRewardedAdCompleted = true;
		}

		private static void HandleAdMobRewardBasedVideoLeftApplication(object sender, EventArgs args)
		{
			Debug.Log("HandleRewardBasedVideoLeftApplication event received");
		}

		static AdManager()
		{
			AdManager.InterstitialAdCompleted = delegate
			{
			};
			AdManager.RewardedAdCompleted = delegate
			{
			};
			AdManager.AdsRemoved = delegate
			{
			};
			lastInterstitialAdLoadTimestamp = -1000f;
			lastRewardedAdLoadTimestamp = -1000f;
			activeBannerAdNetworks = new List<BannerAdNetwork>();
		}
	}
}
