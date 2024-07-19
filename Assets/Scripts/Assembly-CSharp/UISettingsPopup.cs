using EasyMobile;
using UnityEngine;

public class UISettingsPopup : MonoBehaviour
{
	public SceneTransition toMap;

	public void GoToMap()
	{
		AudioManager.instance.ButtonClickAudio();
		toMap.PerformTransition();
		if (AdManager.IsRewardedAdReady())
		{
			AdManager.ShowInterstitialAd();
			Debug.Log("REKLAM");
		}
	}

	public void Replay()
	{
		AudioManager.instance.ButtonClickAudio();
		Configuration.instance.autoPopup = StageLoader.instance.Stage;
		toMap.PerformTransition();
		if (AdManager.IsRewardedAdReady())
		{
			AdManager.ShowInterstitialAd();
			Debug.Log("REKLAM");
		}
	}

	public void ButtonClickAudio()
	{
		AudioManager.instance.ButtonClickAudio();
	}

	public void CloseButtonClick()
	{
		UISettings.isclick = true;
		AudioManager.instance.ButtonClickAudio();
		PlayerPrefs.SetInt("canvas", 1);
		if ((bool)GameObject.Find("Board"))
		{
			GameObject.Find("Board").GetComponent<itemGrid>().state = GAME_STATE.WAITING_USER_SWAP;
		}
	}
}
