using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class UILosePopup : MonoBehaviour
{
	public SceneTransition toMap;

	public Text coinText;

	public Text skipCost;

	public PopupOpener morePopup;

	public PopupOpener shopPopup;

	private void Start()
	{
		coinText.text = CoreData.instance.GetPlayerCoin().ToString();
		skipCost.text = Configuration.instance.skipLevelCost.ToString();
		GoogleAnalyticsV4.instance.LogScreen("lose popup " + StageLoader.instance.Stage);
	}

	public void MoreClick()
	{
		AudioManager.instance.ButtonClickAudio();
		morePopup.OpenPopup();
		GetComponent<Popup>().Close();
		AudioManager.instance.PopupLoseAudio();
	}

	public void LevelPlayClose()
	{
		GetComponent<SceneTransition2>().PerformTransition();
		AudioManager.instance.ButtonClickAudio();
	}

	public void ExitButtonClick()
	{
		GoogleAnalyticsV4.instance.LogScreen("lose popup exit " + StageLoader.instance.Stage);
		AudioManager.instance.ButtonClickAudio();
		toMap.PerformTransition();
	}

	public void ReplayButtonClick()
	{
		GoogleAnalyticsV4.instance.LogScreen("lose popup replay " + StageLoader.instance.Stage);
		AudioManager.instance.ButtonClickAudio();
		Configuration.instance.autoPopup = StageLoader.instance.Stage;
		toMap.PerformTransition();
	}

	public void SkipButtonClick()
	{
		GoogleAnalyticsV4.instance.LogScreen("lose popup skip " + StageLoader.instance.Stage);
		AudioManager.instance.ButtonClickAudio();
		int skipLevelCost = Configuration.instance.skipLevelCost;
		if (skipLevelCost <= CoreData.instance.playerCoin)
		{
			AudioManager.instance.CoinPayAudio();
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin - skipLevelCost);
			itemGrid component = GameObject.Find("Board").GetComponent<itemGrid>();
			if ((bool)component)
			{
				component.SaveLevelInfo();
			}
			Configuration.instance.autoPopup = StageLoader.instance.Stage + 1;
			toMap.PerformTransition();
		}
		else
		{
			shopPopup.OpenPopup();
		}
	}

	public void KeepButtonClick()
	{
		GoogleAnalyticsV4.instance.LogScreen("keep buton " + StageLoader.instance.Stage);
		AudioManager.instance.ButtonClickAudio();
		int keepPlayingCost = Configuration.instance.keepPlayingCost;
		if (keepPlayingCost <= CoreData.instance.playerCoin)
		{
			AudioManager.instance.CoinPayAudio();
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin - keepPlayingCost);
			itemGrid component = GameObject.Find("Board").GetComponent<itemGrid>();
			if ((bool)component)
			{
				component.moveLeft = 10;
				component.UITop.Set5Moves();
				component.state = GAME_STATE.WAITING_USER_SWAP;
				component.checkHintCall = 0;
				component.Hint();
			}
			GameObject gameObject = GameObject.Find("LosePopup(Clone)");
			if ((bool)gameObject)
			{
				gameObject.GetComponent<Popup>().Close();
			}
		}
		else
		{
			shopPopup.OpenPopup();
		}
	}

	public void RewardKeepButtonClick()
	{
		AudioManager.instance.ButtonClickAudio();
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
		GoogleAnalyticsV4.instance.LogScreen("Reward Ads keep buton " + StageLoader.instance.Stage);
		itemGrid component = GameObject.Find("Board").GetComponent<itemGrid>();
		if ((bool)component)
		{
			component.moveLeft = 3;
			component.UITop.Set2Moves();
			component.state = GAME_STATE.WAITING_USER_SWAP;
			component.checkHintCall = 0;
			component.Hint();
		}
		GameObject gameObject = GameObject.Find("LosePopup(Clone)");
		if ((bool)gameObject)
		{
			gameObject.GetComponent<Popup>().Close();
		}
		AdManager.LoadRewardedAd();
	}

	private void OnDisable()
	{
		AdManager.RewardedAdCompleted -= RewardedAdCompletedHandler;
	}
}
