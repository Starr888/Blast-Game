using UnityEngine;

public class moregames : MonoBehaviour
{
	public PopupOpener biberPopup;

	public static moregames instance;

	public void MoregameButtonClick()
	{
		if (!GameObject.Find("Moregames(Clone)"))
		{
			biberPopup.OpenPopup();
			GoogleAnalyticsV4.instance.LogScreen("More Games buton");
		}
	}

	public void drawgame()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.drawdotgame";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("DRAW GAME");
	}

	public void nab()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.ttban";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("BBTAN");
	}

	public void wow()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.carkifelek.en";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("WOW");
	}

	public void crazyrush()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.crazyrush";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("Crazy Rush");
	}

	public void fcm()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.fruitcandymonsters";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("FCM");
	}

	public void bibergames()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/dev?id=7250183505735303936";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("BIBER GAMES");
	}

	public void rateus()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxblast";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("RATE US");
		PlayerPrefs.SetInt("Rated", 1);
	}

	public void ButtonClickAudio()
	{
		AudioManager.instance.ButtonClickAudio();
	}

	public void farmgame()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.farmpartytime";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("FARM");
	}

	public void cubegame()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.cubeblock";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("CUBE");
	}

	public void crazyblast()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxcrazycubes";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("CRAZY BLAST");
	}

	public void TBB()
	{
		string empty = string.Empty;
		empty = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxblast";
		Application.OpenURL(empty);
		GoogleAnalyticsV4.instance.LogScreen("TBB");
	}
}
