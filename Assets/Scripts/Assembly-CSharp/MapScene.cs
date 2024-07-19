using System;
using System.Collections;
using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class MapScene : MonoBehaviour
{
	public static MapScene instance;

	public GoogleAnalyticsV4 googleAnalytics;

	public PopupOpener levelPopup;

	public Text coinText;

	public Text starText;

	public Text sandikText;

	public Text puanText;

	public PopupOpener shopPopup;

	public PopupOpener biberPopup;

	public GameObject levels;

	public GameObject scrollContent;

	public PopupOpener lifePopup;

	public PopupOpener starGiftPopup;

	public PopupOpener puanGiftPopup;

	public GameObject starGift;

	public GameObject puanGift;

	public GameObject starshake;

	public GameObject puanshake;

	public Image starprogress;

	public Image scoreprogress;

	public Text toplamScore;

	public GameObject starinfo;

	public GameObject scoreinfo;

	public Text scoreinfotext;

	public Text starinfotext;

	public float staramount;

	public float scoreamount;

	private float canvasHeight;

	public GameObject _avatar;

	private Texture def_Texture;

	public GameObject PromoWindow;

	public GameObject PromoWindow2;

	private bool wheel;

	public GameObject MoreGames;

	private void Awake()
	{
		def_Texture = _avatar.GetComponent<MeshRenderer>().material.mainTexture;
	}

	private void Start()
	{
		UpdateCoinAmountLabel();
		googleAnalytics.LogScreen("Map");
		if (CoreData.instance.GetOpendedLevel() == 1)
		{
			StartCoroutine(OpenLevelPopup());
		}
		AdManager.HideBannerAd();
		AdManager.LoadRewardedAd();
		canvasHeight = (float)Screen.height / (float)Screen.width * 720f;
		if (Configuration.instance.autoPopup > 0 && Configuration.instance.autoPopup <= Configuration.instance.maxLevel)
		{
			StartCoroutine(OpenLevelPopup());
		}
		Invoke("yildizhesapla", 0.5f);
		Invoke("Sandiklar", 0.6f);
		Invoke("pozisyon", 2f);
		Invoke("promocheck", 2.2f);
		Invoke("ShowMoreGames", 0.2f);
	}

	private void Update()
	{
		float num = canvasHeight / 2f - TargetPosition().y;
		float y = scrollContent.GetComponent<RectTransform>().localPosition.y;
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if ((bool)GameObject.Find("LevelPopup(Clone)"))
			{
				GameObject.Find("LevelPopup(Clone)").GetComponent<Popup>().Close();
			}
			else
			{
				Application.Quit();
			}
		}
	}

	public void ShowMoreGames()
	{
		if (Configuration.instance.passLevelCounter == 0 && CoreData.instance.GetOpendedLevel() > 1)
		{
			MoreGames.SetActive(true);
		}
		else
		{
			MoreGames.SetActive(false);
		}
	}

	public void pozisyon()
	{
		Vector3 zero = Vector3.zero;
		zero = ((StageLoader.instance.Stage != 0) ? levels.transform.GetChild(StageLoader.instance.Stage).GetComponent<RectTransform>().localPosition : TargetPosition());
		scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0f, canvasHeight / 2f - zero.y, 0f);
	}

	public void promocheck()
	{
		if (Configuration.instance.promoTimer >= 2f)
		{
			openpromowindow();
			AudioManager.instance.giftbuton();
		}
		if (Configuration.instance.promoTimer2 >= 4f)
		{
			openpromowindow2();
			AudioManager.instance.giftbuton();
		}
	}

	public void yildizhesapla()
	{
		coinText.text = CoreData.instance.GetPlayerCoin().ToString();
		toplamScore.text = CoreData.instance.GetToplamScore().ToString();
		int num = (CoreData.instance.GetOpendedLevel() - 1) * 3;
		int num2 = 0;
		for (int i = 1; i <= CoreData.instance.GetOpendedLevel(); i++)
		{
			num2 += CoreData.instance.GetLevelStar(i);
		}
		starText.text = num2 + "/" + num;
	}

	public void stargifts()
	{
		if (!GameObject.Find("Stargift(Clone)"))
		{
			starGiftPopup.OpenPopup();
		}
	}

	public void Puangifts()
	{
		if (!GameObject.Find("Puangift(Clone)"))
		{
			puanGiftPopup.OpenPopup();
		}
	}

	public void ButtonClickAudio()
	{
		AudioManager.instance.ButtonClickAudio();
	}

	private IEnumerator OpenLevelPopup()
	{
		yield return new WaitForSeconds(0.2f);
		if (CoreData.instance.GetOpendedLevel() == 1)
		{
			StageLoader.instance.Stage = 1;
		}
		else
		{
			StageLoader.instance.Stage = Configuration.instance.autoPopup;
		}
		StageLoader.instance.LoadLevel();
		Configuration.instance.autoPopup = 0;
		levelPopup.OpenPopup();
	}

	public void openpromowindow()
	{
		PromoWindow.SetActive(true);
		GoogleAnalyticsV4.instance.LogScreen("openpromowindow " + CoreData.instance.openedLevel);
	}

	public void openpromowindow2()
	{
		PromoWindow2.SetActive(true);
		GoogleAnalyticsV4.instance.LogScreen("openpromowindow2 " + CoreData.instance.openedLevel);
	}

	public void closepromo()
	{
		PromoWindow.SetActive(false);
		GoogleAnalyticsV4.instance.LogScreen("closepromo " + CoreData.instance.openedLevel);
		PlayerPrefs.SetString(Configuration.promo_date, DateTime.Now.ToString());
		PlayerPrefs.Save();
	}

	public void closepromo2()
	{
		PromoWindow2.SetActive(false);
		GoogleAnalyticsV4.instance.LogScreen("closepromo2 " + CoreData.instance.openedLevel);
		PlayerPrefs.SetString(Configuration.promo_date2, DateTime.Now.ToString());
		PlayerPrefs.Save();
	}

	public void CoinButtonClick()
	{
		if (!GameObject.Find("ShopPopupMap(Clone)"))
		{
			shopPopup.OpenPopup();
		}
	}

	public void Bildirimetikla()
	{
		if ((bool)GameObject.Find("LevelPlay(Clone)"))
		{
			GameObject.Find("LevelPlay(Clone)").GetComponent<Popup>().Close();
		}
		AudioManager.instance.GingerbreadExplodeAudio();
	}

	public void LifeButtonClick()
	{
		if (!GameObject.Find("LifePopup(Clone)") && !GameObject.Find("ShopPopupMap(Clone)"))
		{
			lifePopup.OpenPopup();
		}
	}

	public void hepsiniac()
	{
		CoreData.instance.SaveOpendedLevel(399);
	}

	public void hepsinsil()
	{
		PlayerPrefs.DeleteAll();
	}

	public void FoundTargetButtonClick()
	{
		AudioManager.instance.ButtonClickAudio();
		StartCoroutine(ScrollContent(new Vector3(0f, canvasHeight / 2f - TargetPosition().y, 0f)));
		Settings.instance.CloseMenu();
		GoogleAnalyticsV4.instance.LogScreen("Found Target Buton");
	}

	private IEnumerator ScrollContent(Vector3 target)
	{
		if (target.y > 0f)
		{
			target.y = 0f;
		}
		Vector3 from = scrollContent.GetComponent<RectTransform>().localPosition;
		float step = Time.fixedDeltaTime;
		float t = 0f;
		while (t <= 1f)
		{
			t += step;
			scrollContent.GetComponent<RectTransform>().localPosition = Vector3.Lerp(from, target, t);
			yield return new WaitForFixedUpdate();
		}
		scrollContent.GetComponent<RectTransform>().localPosition = target;
	}

	private Vector3 TargetPosition()
	{
		Vector3 result = Vector3.zero;
		foreach (Transform item in levels.transform)
		{
			if (item.gameObject.GetComponent<UILevel>().status == MAP_LEVEL_STATUS.CURRENT)
			{
				result = item.gameObject.GetComponent<RectTransform>().localPosition;
				break;
			}
		}
		return result;
	}

	public void UpdateCoinAmountLabel()
	{
		coinText.text = CoreData.instance.GetPlayerCoin().ToString();
	}

	public void UpdateStarsAmountLabel()
	{
		sandikText.text = CoreData.instance.GetPlayerStars().ToString() + "/" + staramount;
	}

	public void UpdatePuanAmountLabel()
	{
		puanText.text = CoreData.instance.playerPuan.ToString() + "/" + scoreamount;
	}

	public void Showleaderboard()
	{
		GoogleAnalyticsV4.instance.LogScreen("LeaderBoard");
		if (GameServiceManager.IsInitialized())
		{
			GameServiceManager.ShowLeaderboardUI();
		}
		else
		{
			GameServiceManager.Init();
		}
	}

	public void Showachievement()
	{
		GoogleAnalyticsV4.instance.LogScreen("Achievement");
		if (GameServiceManager.IsInitialized())
		{
			GameServiceManager.ShowAchievementsUI();
		}
		else
		{
			GameServiceManager.Init();
		}
	}

	public void UpdateStarsButon()
	{
		starGift.SetActive(false);
		starshake.SetActive(false);
		AudioManager.instance.giftsound();
	}

	public void UpdatePuanButon()
	{
		puanGift.SetActive(false);
		puanshake.SetActive(false);
		AudioManager.instance.giftsound();
	}

	public void UpdateStarProgressBar()
	{
		starprogress.fillAmount = 0f;
	}

	public void UpdatePuanProgressBar()
	{
		scoreprogress.fillAmount = 0f;
	}

	public void UpdateProgressBar()
	{
		starprogress.fillAmount = (float)CoreData.instance.playerStars / staramount;
		scoreprogress.fillAmount = (float)CoreData.instance.playerPuan / scoreamount;
	}

	public void odullureklamgoster()
	{
		if (AdManager.IsRewardedAdReady())
		{
			AdManager.ShowRewardedAd();
		}
		else
		{
			AdManager.LoadRewardedAd();
		}
	}

	private void OnEnable()
	{
		AdManager.RewardedAdCompleted += RewardedAdCompletedHandler;
	}

	private void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
	{
	}

	private void OnDisable()
	{
		AdManager.RewardedAdCompleted -= RewardedAdCompletedHandler;
	}

	public void Sandiklar()
	{
		UpdateProgressBar();
		puanText.text = CoreData.instance.playerPuan.ToString() + "/" + scoreamount;
		if ((float)CoreData.instance.playerPuan >= scoreamount)
		{
			puanGift.SetActive(true);
			puanshake.SetActive(true);
			AudioManager.instance.giftbuton();
		}
		else
		{
			puanGift.SetActive(false);
			puanshake.SetActive(false);
		}
		sandikText.text = CoreData.instance.GetPlayerStars().ToString() + "/" + staramount;
		if ((float)CoreData.instance.playerStars >= staramount)
		{
			starGift.SetActive(true);
			starshake.SetActive(true);
			AudioManager.instance.giftbuton();
		}
		else
		{
			starGift.SetActive(false);
			starshake.SetActive(false);
		}
	}

	public void showstarinfopopup()
	{
		GoogleAnalyticsV4.instance.LogScreen("star info");
		StartCoroutine(starinfopopup());
	}

	public void showscoreinfopopup()
	{
		GoogleAnalyticsV4.instance.LogScreen("score info");
		StartCoroutine(scoreinfopopup());
	}

	private IEnumerator starinfopopup()
	{
		starinfo.gameObject.SetActive(true);
		starinfotext.text = "Collect " + staramount + " stars to get the Star Toy Box!";
		yield return new WaitForSeconds(3f);
		starinfo.gameObject.SetActive(false);
	}

	private IEnumerator scoreinfopopup()
	{
		scoreinfo.gameObject.SetActive(true);
		scoreinfotext.text = "Collect " + scoreamount + " score to get the Score Toy Box!";
		yield return new WaitForSeconds(3f);
		scoreinfo.gameObject.SetActive(false);
	}
}
