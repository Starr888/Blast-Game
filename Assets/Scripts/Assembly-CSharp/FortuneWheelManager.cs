using System;
using System.Collections;
using System.Linq;
using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheelManager : MonoBehaviour
{
	[Header("Game Objects for some elements")]
	public Button PaidTurnButton;

	public Button FreeTurnButton;

	public GameObject Circle;

	public Text DeltaCoinsText;

	public Text CurrentCoinsText;

	public GameObject NextTurnTimerWrapper;

	public Text NextFreeTurnTimerText;

	[Header("How much currency one paid turn costs")]
	public int TurnCost = 10;

	private bool _isStarted;

	[Header("Params for each sector")]
	public FortuneWheelSector[] Sectors;

	private float _finalAngle;

	private float _startAngle;

	private float _currentLerpRotationTime;

	private int PlayerCoins;

	private int _previousCoinsAmount;

	[Header("Time Between Two Free Turns")]
	public int TimerMaxHours;

	[Range(0f, 59f)]
	public int TimerMaxMinutes;

	[Range(0f, 59f)]
	public int TimerMaxSeconds = 10;

	private int _timerRemainingHours;

	private int _timerRemainingMinutes;

	private int _timerRemainingSeconds;

	private DateTime _nextFreeTurnTime;

	private const string LAST_FREE_TURN_TIME_NAME = "LastFreeTurnTimeTicks";

	[Header("Can players turn the wheel for currency?")]
	public bool IsPaidTurnEnabled = true;

	[Header("Can players turn the wheel for FREE from time to time?")]
	public bool IsFreeTurnEnabled = true;

	private bool _isFreeTurnAvailable;

	private FortuneWheelSector _finalSector;

	public GameObject WinWindow;

	public GameObject GiftWheel;

	public GameObject WheelInfo;

	public bool wheel;

	public bool reward = true;

	private void Awake()
	{
		FortuneWheelSector[] sectors = Sectors;
		foreach (FortuneWheelSector fortuneWheelSector in sectors)
		{
			if (fortuneWheelSector.ValueTextObject != null)
			{
				fortuneWheelSector.ValueTextObject.GetComponent<Text>().text = fortuneWheelSector.RewardValue.ToString();
			}
		}
		if (IsFreeTurnEnabled)
		{
			SetNextFreeTime();
			if (!PlayerPrefs.HasKey("LastFreeTurnTimeTicks"))
			{
				PlayerPrefs.SetString("LastFreeTurnTimeTicks", DateTime.Now.Ticks.ToString());
			}
		}
		else
		{
			NextTurnTimerWrapper.gameObject.SetActive(false);
		}
		WinWindow.gameObject.SetActive(false);
		GiftWheel.SetActive(false);
		reward = true;
	}

	private void TurnWheelForFree()
	{
		TurnWheel(true);
		AudioManager.instance.GingerbreadExplodeAudio();
	}

	private void TurnWheelForCoins()
	{
		TurnWheel(false);
	}

	private void TurnWheel(bool isFree)
	{
		_currentLerpRotationTime = 0f;
		int[] array = new int[Sectors.Length];
		for (int i = 1; i <= Sectors.Length; i++)
		{
			array[i - 1] = 360 / Sectors.Length * i;
		}
		double num = UnityEngine.Random.Range(1, Sectors.Sum((FortuneWheelSector sector) => sector.Probability));
		int num2 = 0;
		int num3 = array[0];
		_finalSector = Sectors[0];
		for (int j = 0; j < Sectors.Length; j++)
		{
			num2 += Sectors[j].Probability;
			if (num <= (double)num2)
			{
				num3 = array[j];
				_finalSector = Sectors[j];
				break;
			}
		}
		int num4 = 5;
		_finalAngle = num4 * 360 + num3;
		_isStarted = true;
		int playerCoin = CoreData.instance.GetPlayerCoin();
		_previousCoinsAmount = playerCoin;
		if (!isFree)
		{
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= TurnCost);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			AudioManager.instance.CoinPayAudio();
			DeltaCoinsText.text = string.Format("-{0}", TurnCost);
			DeltaCoinsText.gameObject.SetActive(true);
			StartCoroutine(HideCoinsDelta());
			StartCoroutine(UpdateCoinsAmount());
		}
		else
		{
			PlayerPrefs.SetString("LastFreeTurnTimeTicks", DateTime.Now.Ticks.ToString());
			SetNextFreeTime();
		}
	}

	public void TurnWheelButtonClick()
	{
		if (_isFreeTurnAvailable)
		{
			if (AdManager.IsRewardedAdReady())
			{
				AdManager.ShowRewardedAd();
				wheel = true;
				reward = false;
			}
			else if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				WheelInfo.SetActive(true);
				AudioManager.instance.ButtonClickAudio();
				GoogleAnalyticsV4.instance.LogScreen("Free Spin No Connect ");
			}
			else if (reward)
			{
				TurnWheelForFree();
				GoogleAnalyticsV4.instance.LogScreen("Free Spin No Reward");
				AdManager.LoadRewardedAd();
				wheel = false;
			}
			else
			{
				Debug.Log("odul kapatıldı");
				AdManager.LoadRewardedAd();
				AudioManager.instance.ButtonClickAudio();
				GoogleAnalyticsV4.instance.LogScreen("Free Spin odul kapatıldı ");
			}
		}
		else if (IsPaidTurnEnabled)
		{
			int playerCoin = CoreData.instance.GetPlayerCoin();
			if (playerCoin >= TurnCost)
			{
				TurnWheelForCoins();
				GoogleAnalyticsV4.instance.LogScreen("Buy Spin ");
			}
		}
	}

	public void TurnWheelbuyButtonClick()
	{
		int playerCoin = CoreData.instance.GetPlayerCoin();
		if (playerCoin >= TurnCost)
		{
			TurnWheelForCoins();
			GoogleAnalyticsV4.instance.LogScreen("Buy Spin ");
		}
	}

	public void closeWheelInfo()
	{
		WheelInfo.SetActive(false);
		AudioManager.instance.ButtonClickAudio();
	}

	public void ShowGiftWheel()
	{
		GiftWheel.SetActive(true);
		AudioManager.instance.ButtonClickAudio();
	}

	public void HideGiftWheel()
	{
		GiftWheel.SetActive(false);
		AudioManager.instance.ButtonClickAudio();
	}

	public void BoosterDikeyfirca()
	{
		CoreData.instance.SaveColumnBreaker(++CoreData.instance.columnBreaker);
		BostersBox.instance.updateBoostersLabels();
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterBomb()
	{
		CoreData.instance.SaveBeginBombBreaker(++CoreData.instance.beginBombBreaker);
		BostersBox.instance.updateBoostersLabels();
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterFive()
	{
		CoreData.instance.SaveBeginFiveMoves(++CoreData.instance.beginFiveMoves);
		BostersBox.instance.updateBoostersLabels();
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterMala()
	{
		CoreData.instance.SaveSingleBreaker(++CoreData.instance.singleBreaker);
		BostersBox.instance.updateBoostersLabels();
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterLife3()
	{
		GameObject.Find("LifeBar").GetComponent<Life>().AddLife(3);
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterYatayfirca()
	{
		CoreData.instance.SaveRowBreaker(++CoreData.instance.rowBreaker);
		BostersBox.instance.updateBoostersLabels();
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterRainbow()
	{
		CoreData.instance.SaveRainbowBreaker(++CoreData.instance.rainbowBreaker);
		BostersBox.instance.updateBoostersLabels();
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterLife2()
	{
		GameObject.Find("LifeBar").GetComponent<Life>().AddLife(3);
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterBeginRainbow()
	{
		CoreData.instance.SaveBeginRainbow(++CoreData.instance.beginRainbow);
		BostersBox.instance.updateBoostersLabels();
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterGems10()
	{
		CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 15);
		GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterLife1()
	{
		GameObject.Find("LifeBar").GetComponent<Life>().AddLife(1);
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void BoosterEl()
	{
		CoreData.instance.SaveOvenBreaker(++CoreData.instance.ovenBreaker);
		BostersBox.instance.updateBoostersLabels();
		AudioManager.instance.giftbuton();
		WinWindow.gameObject.SetActive(true);
	}

	public void HideWinWindow()
	{
		WinWindow.gameObject.SetActive(false);
	}

	public void SetNextFreeTime()
	{
		_timerRemainingHours = TimerMaxHours;
		_timerRemainingMinutes = TimerMaxMinutes;
		_timerRemainingSeconds = TimerMaxSeconds;
		DateTime dateTime = new DateTime(Convert.ToInt64(PlayerPrefs.GetString("LastFreeTurnTimeTicks", DateTime.Now.Ticks.ToString())));
		_nextFreeTurnTime = dateTime.AddHours(TimerMaxHours).AddMinutes(TimerMaxMinutes).AddSeconds(TimerMaxSeconds);
		_isFreeTurnAvailable = false;
		FreeTurnButton.gameObject.SetActive(false);
	}

	private void ShowTurnButtons()
	{
		if (_isFreeTurnAvailable)
		{
			ShowFreeTurnButton();
			EnableFreeTurnButton();
			return;
		}
		int playerCoin = CoreData.instance.GetPlayerCoin();
		if (!IsPaidTurnEnabled)
		{
			ShowFreeTurnButton();
			DisableFreeTurnButton();
			return;
		}
		ShowPaidTurnButton();
		if (_isStarted || playerCoin < TurnCost)
		{
			DisablePaidTurnButton();
		}
		else
		{
			EnablePaidTurnButton();
		}
	}

	private void OnEnable()
	{
		AdManager.RewardedAdCompleted += RewardedAdCompletedHandler;
	}

	private void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
	{
		if (wheel)
		{
			TurnWheelForFree();
			GoogleAnalyticsV4.instance.LogScreen("Free Spin ");
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			AdManager.LoadRewardedAd();
			wheel = false;
			reward = true;
		}
		else
		{
			Debug.Log("Rewarded ad has completed. The user should be rewarded now.");
			Debug.Log("odul alindi");
			CoreData.instance.SavePlayerCoin(++CoreData.instance.playerCoin);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			AudioManager.instance.CoinPayAudio();
			AdManager.LoadRewardedAd();
		}
	}

	private void OnDisable()
	{
		AdManager.RewardedAdCompleted -= RewardedAdCompletedHandler;
	}

	private void Update()
	{
		ShowTurnButtons();
		if (IsFreeTurnEnabled)
		{
			UpdateFreeTurnTimer();
		}
		if (_isStarted)
		{
			float num = 4f;
			_currentLerpRotationTime += Time.deltaTime;
			if (_currentLerpRotationTime > num || Circle.transform.eulerAngles.z == _finalAngle)
			{
				_currentLerpRotationTime = num;
				_isStarted = false;
				_startAngle = _finalAngle % 360f;
				_finalSector.RewardCallback.Invoke();
				StartCoroutine(HideCoinsDelta());
			}
			else
			{
				float num2 = _currentLerpRotationTime / num;
				num2 = num2 * num2 * num2 * (num2 * (6f * num2 - 15f) + 10f);
				float z = Mathf.Lerp(_startAngle, _finalAngle, num2);
				Circle.transform.eulerAngles = new Vector3(0f, 0f, z);
			}
		}
	}

	public void RewardCoins(int awardCoins)
	{
		int playerCoin = CoreData.instance.GetPlayerCoin();
		playerCoin += awardCoins;
		DeltaCoinsText.text = string.Format("+{0}", awardCoins);
		DeltaCoinsText.gameObject.SetActive(true);
		StartCoroutine(UpdateCoinsAmount());
	}

	private IEnumerator HideCoinsDelta()
	{
		yield return new WaitForSeconds(1f);
		DeltaCoinsText.gameObject.SetActive(false);
	}

	private IEnumerator UpdateCoinsAmount()
	{
		int PlayerCoins = CoreData.instance.GetPlayerCoin();
		float elapsedTime = 0f;
		while (elapsedTime < 0.5f)
		{
			CurrentCoinsText.text = Mathf.Floor(Mathf.Lerp(_previousCoinsAmount, PlayerCoins, elapsedTime / 0.5f)).ToString();
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		_previousCoinsAmount = PlayerCoins;
		CurrentCoinsText.text = PlayerCoins.ToString();
	}

	private void UpdateFreeTurnTimer()
	{
		if (!_isFreeTurnAvailable)
		{
			_timerRemainingHours = (_nextFreeTurnTime - DateTime.Now).Hours;
			_timerRemainingMinutes = (_nextFreeTurnTime - DateTime.Now).Minutes;
			_timerRemainingSeconds = (_nextFreeTurnTime - DateTime.Now).Seconds;
			if (_timerRemainingHours <= 0 && _timerRemainingMinutes <= 0 && _timerRemainingSeconds <= 0)
			{
				NextFreeTurnTimerText.text = "Ready!";
				_isFreeTurnAvailable = true;
			}
			else
			{
				NextFreeTurnTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", _timerRemainingHours, _timerRemainingMinutes, _timerRemainingSeconds);
				_isFreeTurnAvailable = false;
			}
		}
	}

	private void EnableButton(Button button)
	{
		button.interactable = true;
	}

	private void DisableButton(Button button)
	{
		button.interactable = false;
	}

	private void EnableFreeTurnButton()
	{
		EnableButton(FreeTurnButton);
	}

	private void DisableFreeTurnButton()
	{
		DisableButton(FreeTurnButton);
	}

	private void EnablePaidTurnButton()
	{
		EnableButton(PaidTurnButton);
	}

	private void DisablePaidTurnButton()
	{
		DisableButton(PaidTurnButton);
	}

	private void ShowFreeTurnButton()
	{
		FreeTurnButton.gameObject.SetActive(true);
		PaidTurnButton.gameObject.SetActive(true);
	}

	private void ShowPaidTurnButton()
	{
		PaidTurnButton.gameObject.SetActive(true);
		FreeTurnButton.gameObject.SetActive(false);
	}

	public void ResetTimer()
	{
		PlayerPrefs.DeleteKey("LastFreeTurnTimeTicks");
	}
}
