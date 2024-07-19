using UnityEngine;
using UnityEngine.UI;

public class UI_Level : MonoBehaviour
{
	public Text levelText;

	public Image star1;

	public Image star2;

	public Image star3;

	public Image tick1;

	public Image tick2;

	public Image tick3;

	public Image add1;

	public Image add2;

	public Image add3;

	public Text number1;

	public Text number2;

	public Text number3;

	public GameObject fuze;

	public GameObject bomba;

	public GameObject carpi;

	public GameObject cubic;

	public GameObject lego;

	public GameObject kilit;

	public GameObject buz;

	public GameObject baloncuk;

	public GameObject cikolata;

	public GameObject top;

	public GameObject genel;

	public GameObject oyuncak;

	public Text targetText;

	public SceneTransition transition;

	public PopupOpener beginBooster1Popup;

	public PopupOpener beginBooster2Popup;

	public PopupOpener beginBooster3Popup;

	public bool avaialbe1;

	public bool avaialbe2;

	public bool avaialbe3;

	public Image booster1;

	public Image booster2;

	public Image booster3;

	public GameObject locked1;

	public GameObject locked2;

	public GameObject locked3;

	public Text lockedText1;

	public Text lockedText2;

	public Text lockedText3;

	public SceneTransition toMap;

	public GameObject StarBoxBildirim;

	public GameObject ScoreBoxBildirim;

	public GameObject probs1;

	public GameObject probs2;

	public GameObject probs3;

	public GAME_STATE state;

	private void Start()
	{
		levelText.text = "Level " + StageLoader.instance.Stage;
		if (CoreData.instance.GetOpendedLevel() == 1)
		{
			PlayButtonClick();
		}
		if (StageLoader.instance.Stage == 10)
		{
			probs1.gameObject.SetActive(true);
		}
		if (StageLoader.instance.Stage == 20)
		{
			probs2.gameObject.SetActive(true);
		}
		if (StageLoader.instance.Stage == 23)
		{
			probs3.gameObject.SetActive(true);
		}
		Configuration.instance.beginFiveMoves = false;
		Configuration.instance.beginRainbow = false;
		Configuration.instance.beginBombBreaker = false;
		switch (CoreData.instance.GetLevelStar(StageLoader.instance.Stage))
		{
		case 1:
			star1.gameObject.SetActive(true);
			star2.gameObject.SetActive(false);
			star3.gameObject.SetActive(false);
			break;
		case 2:
			star1.gameObject.SetActive(true);
			star2.gameObject.SetActive(true);
			star3.gameObject.SetActive(false);
			break;
		case 3:
			star1.gameObject.SetActive(true);
			star2.gameObject.SetActive(true);
			star3.gameObject.SetActive(true);
			break;
		default:
			star1.gameObject.SetActive(false);
			star2.gameObject.SetActive(false);
			star3.gameObject.SetActive(false);
			break;
		}
		if (StageLoader.instance.doll > 0)
		{
			string text = "doll_" + StageLoader.instance.doll + "_4";
		}
		else
		{
			string text = "cake_1_4";
		}
		targetText.text = StageLoader.instance.targetlbl;
		Invoke("Sandiklar", 0.5f);
		if (StageLoader.instance.Stage == 1 || StageLoader.instance.Stage == 2)
		{
			genel.gameObject.SetActive(false);
			fuze.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 3)
		{
			genel.gameObject.SetActive(false);
			bomba.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 4)
		{
			genel.gameObject.SetActive(false);
			carpi.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 5)
		{
			genel.gameObject.SetActive(false);
			cubic.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 6 || StageLoader.instance.Stage == 7 || StageLoader.instance.Stage == 8)
		{
			genel.gameObject.SetActive(false);
			oyuncak.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 9 || StageLoader.instance.Stage == 10 || StageLoader.instance.Stage == 11)
		{
			genel.gameObject.SetActive(false);
			buz.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 15 || StageLoader.instance.Stage == 16 || StageLoader.instance.Stage == 17)
		{
			genel.gameObject.SetActive(false);
			baloncuk.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 18 || StageLoader.instance.Stage == 19 || StageLoader.instance.Stage == 20)
		{
			genel.gameObject.SetActive(false);
			cikolata.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 27 || StageLoader.instance.Stage == 28)
		{
			genel.gameObject.SetActive(false);
			top.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 29 || StageLoader.instance.Stage == 30 || StageLoader.instance.Stage == 31)
		{
			genel.gameObject.SetActive(false);
			lego.gameObject.SetActive(true);
		}
		else if (StageLoader.instance.Stage == 40 || StageLoader.instance.Stage == 41)
		{
			genel.gameObject.SetActive(false);
			kilit.gameObject.SetActive(true);
		}
		else
		{
			switch (Random.Range(0, 5))
			{
			case 0:
				genel.gameObject.SetActive(false);
				fuze.gameObject.SetActive(true);
				break;
			case 1:
				genel.gameObject.SetActive(false);
				bomba.gameObject.SetActive(true);
				break;
			case 2:
				genel.gameObject.SetActive(false);
				carpi.gameObject.SetActive(true);
				break;
			case 3:
				genel.gameObject.SetActive(false);
				cubic.gameObject.SetActive(true);
				break;
			case 4:
				genel.gameObject.SetActive(false);
				genel.gameObject.SetActive(true);
				break;
			}
		}
		for (int i = 1; i <= 3; i++)
		{
			int num = 0;
			Image image = null;
			Image image2 = null;
			Text text2 = null;
			bool flag = false;
			Image image3 = null;
			GameObject gameObject = null;
			Text text3 = null;
			switch (i)
			{
			case 1:
				num = CoreData.instance.beginFiveMoves;
				image = tick1;
				image2 = add1;
				text2 = number1;
				avaialbe1 = ((StageLoader.instance.Stage >= Configuration.instance.beginFiveMovesLevel) ? true : false);
				flag = avaialbe1;
				image3 = booster1;
				gameObject = locked1;
				text3 = lockedText1;
				break;
			case 2:
				num = CoreData.instance.beginRainbow;
				image = tick2;
				image2 = add2;
				text2 = number2;
				avaialbe2 = ((StageLoader.instance.Stage >= Configuration.instance.beginRainbowLevel) ? true : false);
				flag = avaialbe2;
				image3 = booster2;
				gameObject = locked2;
				text3 = lockedText2;
				break;
			case 3:
				num = CoreData.instance.beginBombBreaker;
				image = tick3;
				image2 = add3;
				text2 = number3;
				avaialbe3 = ((StageLoader.instance.Stage >= Configuration.instance.beginBombBreakerLevel) ? true : false);
				flag = avaialbe3;
				image3 = booster3;
				gameObject = locked3;
				text3 = lockedText3;
				break;
			}
			if (flag)
			{
				if (num > 0)
				{
					text2.text = num.ToString();
					image2.gameObject.SetActive(false);
					image.gameObject.SetActive(false);
				}
				else
				{
					text2.text = "0";
					image2.gameObject.SetActive(true);
					image.gameObject.SetActive(false);
				}
				continue;
			}
			text2.text = "0";
			text2.gameObject.transform.parent.gameObject.SetActive(false);
			image2.gameObject.SetActive(false);
			image.gameObject.SetActive(false);
			image3.gameObject.SetActive(false);
			gameObject.SetActive(true);
			switch (i)
			{
			case 1:
				text3.text = "Require\nLevel " + Configuration.instance.beginFiveMovesLevel;
				break;
			case 2:
				text3.text = "Require\nLevel " + Configuration.instance.beginRainbowLevel;
				break;
			case 3:
				text3.text = "Require\nLevel " + Configuration.instance.beginBombBreakerLevel;
				break;
			}
		}
		if (CoreData.instance.GetOpendedLevel() == 4 && state == GAME_STATE.PREPARING_LEVEL)
		{
			GetComponent<SceneTransition2>().PerformTransition();
		}
	}

	public void Sandiklar()
	{
		if (state == GAME_STATE.PREPARING_LEVEL)
		{
			if (CoreData.instance.playerPuan >= 75000)
			{
				ScoreBoxBildirim.SetActive(true);
				AudioManager.instance.giftbuton();
			}
			else
			{
				ScoreBoxBildirim.SetActive(false);
			}
			if (CoreData.instance.playerStars >= 25)
			{
				StarBoxBildirim.SetActive(true);
				AudioManager.instance.giftbuton();
			}
			else
			{
				StarBoxBildirim.SetActive(false);
			}
		}
	}

	public void PlayButtonClick()
	{
		AudioManager.instance.ButtonClickAudio();
		Configuration.instance.giftsayisi = 0;
		if (Configuration.instance.life > 0)
		{
			GameObject.Find("LifeBar").GetComponent<Life>().ReduceLife(1);
			transition.PerformTransition();
			GoogleAnalyticsV4.instance.LogScreen("Play level " + StageLoader.instance.Stage);
		}
		else if (state == GAME_STATE.OPENING_POPUP)
		{
			GameObject.Find("MapScene").GetComponent<MapScene>().LifeButtonClick();
			GoogleAnalyticsV4.instance.LogScreen("Life bitti level " + StageLoader.instance.Stage);
		}
		else if (state == GAME_STATE.PREPARING_LEVEL)
		{
			GetComponent<SceneTransition2>().PerformTransition();
			GoogleAnalyticsV4.instance.LogScreen("Life bitti level " + StageLoader.instance.Stage);
		}
	}

	public void ButtonClickAudio()
	{
		AudioManager.instance.ButtonClickAudio();
	}

	public void GoToMap()
	{
		AudioManager.instance.ButtonClickAudio();
		toMap.PerformTransition();
	}

	public void levelplayclose()
	{
		GetComponent<SceneTransition2>().PerformTransition();
	}

	public void BeginBoosterClick(int booster)
	{
		bool flag = false;
		switch (booster)
		{
		case 1:
			flag = avaialbe1;
			break;
		case 2:
			flag = avaialbe2;
			break;
		case 3:
			flag = avaialbe3;
			break;
		}
		if (!flag)
		{
			return;
		}
		AudioManager.instance.ButtonClickAudio();
		int num = 0;
		switch (booster)
		{
		case 1:
			num = CoreData.instance.beginFiveMoves;
			break;
		case 2:
			num = CoreData.instance.beginRainbow;
			break;
		case 3:
			num = CoreData.instance.beginBombBreaker;
			break;
		}
		if (num > 0)
		{
			switch (booster)
			{
			case 1:
				if (!Configuration.instance.beginFiveMoves)
				{
					tick1.gameObject.SetActive(true);
					number1.gameObject.SetActive(false);
					Configuration.instance.beginFiveMoves = true;
					probs1.gameObject.SetActive(false);
				}
				else
				{
					tick1.gameObject.SetActive(false);
					number1.gameObject.SetActive(true);
					Configuration.instance.beginFiveMoves = false;
				}
				break;
			case 2:
				if (!Configuration.instance.beginRainbow)
				{
					tick2.gameObject.SetActive(true);
					number2.gameObject.SetActive(false);
					Configuration.instance.beginRainbow = true;
					probs2.gameObject.SetActive(false);
				}
				else
				{
					tick2.gameObject.SetActive(false);
					number2.gameObject.SetActive(true);
					Configuration.instance.beginRainbow = false;
				}
				break;
			case 3:
				if (!Configuration.instance.beginBombBreaker)
				{
					tick3.gameObject.SetActive(true);
					number3.gameObject.SetActive(false);
					Configuration.instance.beginBombBreaker = true;
					probs3.gameObject.SetActive(false);
				}
				else
				{
					tick3.gameObject.SetActive(false);
					number3.gameObject.SetActive(true);
					Configuration.instance.beginBombBreaker = false;
				}
				break;
			}
		}
		else
		{
			switch (booster)
			{
			case 1:
				beginBooster1Popup.OpenPopup();
				break;
			case 2:
				beginBooster2Popup.OpenPopup();
				break;
			case 3:
				beginBooster3Popup.OpenPopup();
				break;
			}
		}
	}
}
