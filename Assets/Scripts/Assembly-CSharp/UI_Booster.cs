using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Booster : MonoBehaviour
{
	public BOOSTER_TYPE booster;

	public Text cost1;

	public Text cost2;

	public bool clicking;

	public PopupOpener shopPopup;

	private void Start()
	{
		switch (booster)
		{
		case BOOSTER_TYPE.BEGIN_FIVE_MOVES:
			cost1.text = Configuration.instance.beginFiveMovesCost1.ToString();
			cost2.text = Configuration.instance.beginFiveMovesCost2.ToString();
			break;
		case BOOSTER_TYPE.BEGIN_RAINBOW_BREAKER:
			cost1.text = Configuration.instance.beginRainbowCost1.ToString();
			cost2.text = Configuration.instance.beginRainbowCost2.ToString();
			break;
		case BOOSTER_TYPE.BEGIN_BOMB_BREAKER:
			cost1.text = Configuration.instance.beginBombBreakerCost1.ToString();
			cost2.text = Configuration.instance.beginBombBreakerCost2.ToString();
			break;
		case BOOSTER_TYPE.SINGLE_BREAKER:
			cost1.text = Configuration.instance.singleBreakerCost1.ToString();
			cost2.text = Configuration.instance.singleBreakerCost2.ToString();
			break;
		case BOOSTER_TYPE.ROW_BREAKER:
			cost1.text = Configuration.instance.rowBreakerCost1.ToString();
			cost2.text = Configuration.instance.rowBreakerCost2.ToString();
			break;
		case BOOSTER_TYPE.COLUMN_BREAKER:
			cost1.text = Configuration.instance.columnBreakerCost1.ToString();
			cost2.text = Configuration.instance.columnBreakerCost2.ToString();
			break;
		case BOOSTER_TYPE.RAINBOW_BREAKER:
			cost1.text = Configuration.instance.rainbowBreakerCost1.ToString();
			cost2.text = Configuration.instance.rainbowBreakerCost2.ToString();
			break;
		case BOOSTER_TYPE.OVEN_BREAKER:
			cost1.text = Configuration.instance.ovenBreakerCost1.ToString();
			cost2.text = Configuration.instance.ovenBreakerCost2.ToString();
			break;
		}
	}

	public void BuyButtonClick(int package)
	{
		if (clicking)
		{
			return;
		}
		clicking = true;
		StartCoroutine(ResetButtonClick());
		int num = 0;
		int number = 0;
		switch (package)
		{
		case 1:
			switch (booster)
			{
			case BOOSTER_TYPE.BEGIN_FIVE_MOVES:
				num = Configuration.instance.beginFiveMovesCost1;
				number = Configuration.instance.package1Amount;
				break;
			case BOOSTER_TYPE.BEGIN_RAINBOW_BREAKER:
				num = Configuration.instance.beginRainbowCost1;
				number = Configuration.instance.package1Amount;
				break;
			case BOOSTER_TYPE.BEGIN_BOMB_BREAKER:
				num = Configuration.instance.beginBombBreakerCost1;
				number = Configuration.instance.package1Amount;
				break;
			case BOOSTER_TYPE.SINGLE_BREAKER:
				num = Configuration.instance.singleBreakerCost1;
				number = Configuration.instance.package1Amount;
				break;
			case BOOSTER_TYPE.ROW_BREAKER:
				num = Configuration.instance.rowBreakerCost1;
				number = Configuration.instance.package1Amount;
				break;
			case BOOSTER_TYPE.COLUMN_BREAKER:
				num = Configuration.instance.columnBreakerCost1;
				number = Configuration.instance.package1Amount;
				break;
			case BOOSTER_TYPE.RAINBOW_BREAKER:
				num = Configuration.instance.rainbowBreakerCost1;
				number = Configuration.instance.package1Amount;
				break;
			case BOOSTER_TYPE.OVEN_BREAKER:
				num = Configuration.instance.ovenBreakerCost1;
				number = Configuration.instance.package1Amount;
				break;
			}
			break;
		case 2:
			switch (booster)
			{
			case BOOSTER_TYPE.BEGIN_FIVE_MOVES:
				num = Configuration.instance.beginFiveMovesCost2;
				number = Configuration.instance.package2Amount;
				break;
			case BOOSTER_TYPE.BEGIN_RAINBOW_BREAKER:
				num = Configuration.instance.beginRainbowCost2;
				number = Configuration.instance.package2Amount;
				break;
			case BOOSTER_TYPE.BEGIN_BOMB_BREAKER:
				num = Configuration.instance.beginBombBreakerCost2;
				number = Configuration.instance.package2Amount;
				break;
			case BOOSTER_TYPE.SINGLE_BREAKER:
				num = Configuration.instance.singleBreakerCost2;
				number = Configuration.instance.package2Amount;
				break;
			case BOOSTER_TYPE.ROW_BREAKER:
				num = Configuration.instance.rowBreakerCost2;
				number = Configuration.instance.package2Amount;
				break;
			case BOOSTER_TYPE.COLUMN_BREAKER:
				num = Configuration.instance.columnBreakerCost2;
				number = Configuration.instance.package2Amount;
				break;
			case BOOSTER_TYPE.RAINBOW_BREAKER:
				num = Configuration.instance.rainbowBreakerCost2;
				number = Configuration.instance.package2Amount;
				break;
			case BOOSTER_TYPE.OVEN_BREAKER:
				num = Configuration.instance.ovenBreakerCost2;
				number = Configuration.instance.package2Amount;
				break;
			}
			break;
		}
		if (num <= CoreData.instance.GetPlayerCoin())
		{
			CoreData.instance.SavePlayerCoin(CoreData.instance.GetPlayerCoin() - num);
			AudioManager.instance.CoinPayAudio();
			switch (booster)
			{
			case BOOSTER_TYPE.BEGIN_FIVE_MOVES:
				CoreData.instance.SaveBeginFiveMoves(number);
				break;
			case BOOSTER_TYPE.BEGIN_RAINBOW_BREAKER:
				CoreData.instance.SaveBeginRainbow(number);
				break;
			case BOOSTER_TYPE.BEGIN_BOMB_BREAKER:
				CoreData.instance.SaveBeginBombBreaker(number);
				break;
			case BOOSTER_TYPE.SINGLE_BREAKER:
				CoreData.instance.SaveSingleBreaker(number);
				break;
			case BOOSTER_TYPE.ROW_BREAKER:
				CoreData.instance.SaveRowBreaker(number);
				break;
			case BOOSTER_TYPE.COLUMN_BREAKER:
				CoreData.instance.SaveColumnBreaker(number);
				break;
			case BOOSTER_TYPE.RAINBOW_BREAKER:
				CoreData.instance.SaveRainbowBreaker(number);
				break;
			case BOOSTER_TYPE.OVEN_BREAKER:
				CoreData.instance.SaveOvenBreaker(number);
				break;
			}
			if (booster == BOOSTER_TYPE.BEGIN_FIVE_MOVES || booster == BOOSTER_TYPE.BEGIN_RAINBOW_BREAKER || booster == BOOSTER_TYPE.BEGIN_BOMB_BREAKER)
			{
				if ((bool)GameObject.Find("LevelPopup(Clone)"))
				{
					GameObject gameObject = GameObject.Find("LevelPopup(Clone)");
					switch (booster)
					{
					case BOOSTER_TYPE.BEGIN_FIVE_MOVES:
						gameObject.GetComponent<UI_Level>().number1.text = number.ToString();
						gameObject.GetComponent<UI_Level>().add1.gameObject.SetActive(false);
						gameObject.GetComponent<UI_Level>().BeginBoosterClick(1);
						break;
					case BOOSTER_TYPE.BEGIN_RAINBOW_BREAKER:
						gameObject.GetComponent<UI_Level>().number2.text = number.ToString();
						gameObject.GetComponent<UI_Level>().add2.gameObject.SetActive(false);
						gameObject.GetComponent<UI_Level>().BeginBoosterClick(2);
						break;
					case BOOSTER_TYPE.BEGIN_BOMB_BREAKER:
						gameObject.GetComponent<UI_Level>().number3.text = number.ToString();
						gameObject.GetComponent<UI_Level>().add3.gameObject.SetActive(false);
						gameObject.GetComponent<UI_Level>().BeginBoosterClick(3);
						break;
					}
				}
				switch (booster)
				{
				case BOOSTER_TYPE.BEGIN_FIVE_MOVES:
					if ((bool)GameObject.Find("BeginBooster1Popup(Clone)"))
					{
						GameObject.Find("BeginBooster1Popup(Clone)").GetComponent<Popup>().Close();
					}
					break;
				case BOOSTER_TYPE.BEGIN_RAINBOW_BREAKER:
					if ((bool)GameObject.Find("BeginBooster2Popup(Clone)"))
					{
						GameObject.Find("BeginBooster2Popup(Clone)").GetComponent<Popup>().Close();
					}
					break;
				case BOOSTER_TYPE.BEGIN_BOMB_BREAKER:
					if ((bool)GameObject.Find("BeginBooster3Popup(Clone)"))
					{
						GameObject.Find("BeginBooster3Popup(Clone)").GetComponent<Popup>().Close();
					}
					break;
				}
				GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
				return;
			}
			switch (booster)
			{
			case BOOSTER_TYPE.SINGLE_BREAKER:
				Booster.instance.singleAmount.text = number.ToString();
				Booster.instance.ActiveBooster(BOOSTER_TYPE.SINGLE_BREAKER);
				break;
			case BOOSTER_TYPE.ROW_BREAKER:
				Booster.instance.rowAmount.text = number.ToString();
				Booster.instance.ActiveBooster(BOOSTER_TYPE.ROW_BREAKER);
				break;
			case BOOSTER_TYPE.COLUMN_BREAKER:
				Booster.instance.columnAmount.text = number.ToString();
				Booster.instance.ActiveBooster(BOOSTER_TYPE.COLUMN_BREAKER);
				break;
			case BOOSTER_TYPE.RAINBOW_BREAKER:
				Booster.instance.rainbowAmount.text = number.ToString();
				Booster.instance.ActiveBooster(BOOSTER_TYPE.RAINBOW_BREAKER);
				break;
			case BOOSTER_TYPE.OVEN_BREAKER:
				Booster.instance.ovenAmount.text = number.ToString();
				Booster.instance.ActiveBooster(BOOSTER_TYPE.OVEN_BREAKER);
				break;
			}
			switch (booster)
			{
			case BOOSTER_TYPE.SINGLE_BREAKER:
				if ((bool)GameObject.Find("SingleBoosterPopup(Clone)"))
				{
					GameObject.Find("SingleBoosterPopup(Clone)").GetComponent<Popup>().Close();
					GameObject.Find("Board").GetComponent<itemGrid>().state = GAME_STATE.WAITING_USER_SWAP;
				}
				break;
			case BOOSTER_TYPE.ROW_BREAKER:
				if ((bool)GameObject.Find("RowBoosterPopup(Clone)"))
				{
					GameObject.Find("RowBoosterPopup(Clone)").GetComponent<Popup>().Close();
					GameObject.Find("Board").GetComponent<itemGrid>().state = GAME_STATE.WAITING_USER_SWAP;
				}
				break;
			case BOOSTER_TYPE.COLUMN_BREAKER:
				if ((bool)GameObject.Find("ColumnBoosterPopup(Clone)"))
				{
					GameObject.Find("ColumnBoosterPopup(Clone)").GetComponent<Popup>().Close();
					GameObject.Find("Board").GetComponent<itemGrid>().state = GAME_STATE.WAITING_USER_SWAP;
				}
				break;
			case BOOSTER_TYPE.RAINBOW_BREAKER:
				if ((bool)GameObject.Find("RainbowBoosterPopup(Clone)"))
				{
					GameObject.Find("RainbowBoosterPopup(Clone)").GetComponent<Popup>().Close();
					GameObject.Find("Board").GetComponent<itemGrid>().state = GAME_STATE.WAITING_USER_SWAP;
				}
				break;
			case BOOSTER_TYPE.OVEN_BREAKER:
				if ((bool)GameObject.Find("OvenBoosterPopup(Clone)"))
				{
					GameObject.Find("OvenBoosterPopup(Clone)").GetComponent<Popup>().Close();
					GameObject.Find("Board").GetComponent<itemGrid>().state = GAME_STATE.WAITING_USER_SWAP;
				}
				break;
			}
		}
		else
		{
			shopPopup.OpenPopup();
		}
	}

	private IEnumerator ResetButtonClick()
	{
		yield return new WaitForSeconds(1f);
		clicking = false;
	}

	public void ButtonClickAudio()
	{
		AudioManager.instance.ButtonClickAudio();
	}

	public void CloseButtonClick()
	{
		AudioManager.instance.ButtonClickAudio();
		GameObject.Find("Board").GetComponent<itemGrid>().state = GAME_STATE.WAITING_USER_SWAP;
	}
}
