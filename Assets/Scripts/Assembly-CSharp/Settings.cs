using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	public Image MenuButtonImage;

	public Sprite MenuOpenedSprite;

	public Sprite MenuClosedSprite;

	public GameObject SettingContent;

	private bool isMenuOpened;

	private bool isGameMenuOpened;

	public static Settings instance;

	private void Start()
	{
		instance = this;
	}

	public void OnMenuButtonPressed()
	{
		if (InputManager.instance.canInput(1f))
		{
			AudioManager.instance.PlayButtonClickSound();
			if (!isMenuOpened)
			{
				OpenMenu();
			}
			else
			{
				CloseMenu();
			}
		}
	}

	public void OnGameMenuButtonPressed()
	{
		if (InputManager.instance.canInput(1f))
		{
			AudioManager.instance.PlayButtonClickSound();
			if (!isGameMenuOpened)
			{
				OpenGameMenu();
			}
			else
			{
				CloseGameMenu();
			}
		}
	}

	private void OpenMenu()
	{
		isMenuOpened = true;
		MenuButtonImage.sprite = MenuOpenedSprite;
		GetComponent<Animator>().Play("Open-Settings");
	}

	private void OpenGameMenu()
	{
		isGameMenuOpened = true;
		MenuButtonImage.sprite = MenuOpenedSprite;
		GetComponent<Animator>().Play("gameopen");
	}

	public void CloseMenu()
	{
		isMenuOpened = false;
		MenuButtonImage.sprite = MenuClosedSprite;
		GetComponent<Animator>().Play("Close-Settings");
	}

	public void CloseGameMenu()
	{
		isGameMenuOpened = false;
		MenuButtonImage.sprite = MenuClosedSprite;
		GetComponent<Animator>().Play("gameclose");
	}

	public void quitgamebutton()
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

	public void OnLeaderBoardButtonPressed()
	{
		if (InputManager.instance.canInput())
		{
			AudioManager.instance.PlayButtonClickSound();
			CloseMenu();
			if (GameServiceManager.IsInitialized())
			{
				GameServiceManager.ShowLeaderboardUI();
			}
			else
			{
				GameServiceManager.Init();
			}
		}
	}

	public void OnAchievementsButtonPressed()
	{
		if (InputManager.instance.canInput())
		{
			AudioManager.instance.PlayButtonClickSound();
			Debug.Log("Achievement stuff goes here..");
			CloseMenu();
			if (GameServiceManager.IsInitialized())
			{
				GameServiceManager.ShowAchievementsUI();
			}
			else
			{
				GameServiceManager.Init();
			}
		}
	}

	public void MoregameButtonClick()
	{
		if (InputManager.instance.canInput())
		{
			AudioManager.instance.PlayButtonClickSound();
			string empty = string.Empty;
			empty = "https://play.google.com/store/apps/dev?id=7250183505735303936";
			Application.OpenURL(empty);
			CloseMenu();
		}
	}

	public void feedback()
	{
		string text = "Please send us your advise: ";
		Application.OpenURL("mailto:info@bibergames.com?subject=ToyBoxPartyTime Feedback&body=" + text);
	}

	public void RateUs()
	{
		if (InputManager.instance.canInput())
		{
			AudioManager.instance.PlayButtonClickSound();
			string empty = string.Empty;
			empty = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxblast";
			Application.OpenURL(empty);
			PlayerPrefs.SetInt("Rated", 1);
			CloseMenu();
		}
	}

	public void RateUsCrazy()
	{
		if (InputManager.instance.canInput())
		{
			AudioManager.instance.PlayButtonClickSound();
			string empty = string.Empty;
			empty = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxcrazycubes";
			Application.OpenURL(empty);
			PlayerPrefs.SetInt("Rated", 1);
			CloseMenu();
		}
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}
}
