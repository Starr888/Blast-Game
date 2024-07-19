using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
	public static Booster instance;

	[Header("Board")]
	public itemGrid board;

	[Header("Booster")]
	public GameObject singleBooster;

	public GameObject rowBooster;

	public GameObject columnBooster;

	public GameObject rainbowBooster;

	public GameObject ovenBooster;

	[Header("Active")]
	public GameObject singleActive;

	public GameObject rowActive;

	public GameObject columnActive;

	public GameObject rainbowActive;

	public GameObject ovenActive;

	[Header("Amount")]
	public Text singleAmount;

	public Text rowAmount;

	public Text columnAmount;

	public Text rainbowAmount;

	public Text ovenAmount;

	[Header("Popup")]
	public PopupOpener singleBoosterPopup;

	public PopupOpener rowBoosterPopup;

	public PopupOpener columnBoosterPopup;

	public PopupOpener rainbowBoosterPopup;

	public PopupOpener ovenBoosterPopup;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		singleBooster.SetActive(true);
		rowBooster.SetActive(true);
		columnBooster.SetActive(true);
		rainbowBooster.SetActive(true);
		ovenBooster.SetActive(true);
		if (StageLoader.instance.Stage >= 7)
		{
			singleBooster.SetActive(false);
			singleAmount.text = CoreData.instance.GetSingleBreaker().ToString();
		}
		if (StageLoader.instance.Stage >= 12)
		{
			rowBooster.SetActive(false);
			rowAmount.text = CoreData.instance.GetRowBreaker().ToString();
		}
		if (StageLoader.instance.Stage >= 15)
		{
			columnBooster.SetActive(false);
			columnAmount.text = CoreData.instance.GetColumnBreaker().ToString();
		}
		if (StageLoader.instance.Stage >= 18)
		{
			rainbowBooster.SetActive(false);
			rainbowAmount.text = CoreData.instance.GetRainbowBreaker().ToString();
		}
		if (StageLoader.instance.Stage >= 25)
		{
			ovenBooster.SetActive(false);
			ovenAmount.text = CoreData.instance.GetOvenBreaker().ToString();
		}
	}

	public void SingleBoosterClick()
	{
		Debug.Log("Click on single booster");
		if (board.state == GAME_STATE.WAITING_USER_SWAP && !board.lockSwap)
		{
			AudioManager.instance.ButtonClickAudio();
			board.dropTime = 1;
			if (CoreData.instance.GetSingleBreaker() <= 0)
			{
				ShowPopup(BOOSTER_TYPE.SINGLE_BREAKER);
			}
			else if (board.booster == BOOSTER_TYPE.NONE)
			{
				ActiveBooster(BOOSTER_TYPE.SINGLE_BREAKER);
			}
			else
			{
				CancelBooster(BOOSTER_TYPE.SINGLE_BREAKER);
			}
		}
	}

	public void RowBoosterClick()
	{
		if (board.state == GAME_STATE.WAITING_USER_SWAP && !board.lockSwap)
		{
			AudioManager.instance.ButtonClickAudio();
			board.dropTime = 1;
			if (CoreData.instance.GetRowBreaker() <= 0)
			{
				ShowPopup(BOOSTER_TYPE.ROW_BREAKER);
			}
			else if (board.booster == BOOSTER_TYPE.NONE)
			{
				ActiveBooster(BOOSTER_TYPE.ROW_BREAKER);
			}
			else
			{
				CancelBooster(BOOSTER_TYPE.ROW_BREAKER);
			}
		}
	}

	public void ColumnBoosterClick()
	{
		if (board.state == GAME_STATE.WAITING_USER_SWAP && !board.lockSwap)
		{
			AudioManager.instance.ButtonClickAudio();
			board.dropTime = 1;
			if (CoreData.instance.GetColumnBreaker() <= 0)
			{
				ShowPopup(BOOSTER_TYPE.COLUMN_BREAKER);
			}
			else if (board.booster == BOOSTER_TYPE.NONE)
			{
				ActiveBooster(BOOSTER_TYPE.COLUMN_BREAKER);
			}
			else
			{
				CancelBooster(BOOSTER_TYPE.COLUMN_BREAKER);
			}
		}
	}

	public void RainbowBoosterClick()
	{
		if (board.state == GAME_STATE.WAITING_USER_SWAP && !board.lockSwap)
		{
			AudioManager.instance.ButtonClickAudio();
			board.dropTime = 1;
			if (CoreData.instance.GetRainbowBreaker() <= 0)
			{
				ShowPopup(BOOSTER_TYPE.RAINBOW_BREAKER);
			}
			else if (board.booster == BOOSTER_TYPE.NONE)
			{
				ActiveBooster(BOOSTER_TYPE.RAINBOW_BREAKER);
			}
			else
			{
				CancelBooster(BOOSTER_TYPE.RAINBOW_BREAKER);
			}
		}
	}

	public void OvenBoosterClick()
	{
		if (board.state == GAME_STATE.WAITING_USER_SWAP && !board.lockSwap)
		{
			AudioManager.instance.ButtonClickAudio();
			board.dropTime = 0;
			if (CoreData.instance.GetOvenBreaker() <= 0)
			{
				ShowPopup(BOOSTER_TYPE.OVEN_BREAKER);
			}
			else if (board.booster == BOOSTER_TYPE.NONE)
			{
				ActiveBooster(BOOSTER_TYPE.OVEN_BREAKER);
			}
			else
			{
				CancelBooster(BOOSTER_TYPE.OVEN_BREAKER);
			}
		}
	}

	public void BoosterComplete()
	{
		if (board.booster == BOOSTER_TYPE.SINGLE_BREAKER)
		{
			CancelBooster(BOOSTER_TYPE.SINGLE_BREAKER);
			if (CoreData.instance.GetSingleBreaker() > 0)
			{
				int number = CoreData.instance.GetSingleBreaker() - 1;
				CoreData.instance.SaveSingleBreaker(number);
				singleAmount.text = number.ToString();
			}
		}
		else if (board.booster == BOOSTER_TYPE.ROW_BREAKER)
		{
			CancelBooster(BOOSTER_TYPE.ROW_BREAKER);
			if (CoreData.instance.GetRowBreaker() > 0)
			{
				int number2 = CoreData.instance.GetRowBreaker() - 1;
				CoreData.instance.SaveRowBreaker(number2);
				rowAmount.text = number2.ToString();
			}
		}
		else if (board.booster == BOOSTER_TYPE.COLUMN_BREAKER)
		{
			CancelBooster(BOOSTER_TYPE.COLUMN_BREAKER);
			if (CoreData.instance.GetColumnBreaker() > 0)
			{
				int number3 = CoreData.instance.GetColumnBreaker() - 1;
				CoreData.instance.SaveColumnBreaker(number3);
				columnAmount.text = number3.ToString();
			}
		}
		else if (board.booster == BOOSTER_TYPE.RAINBOW_BREAKER)
		{
			CancelBooster(BOOSTER_TYPE.RAINBOW_BREAKER);
			if (CoreData.instance.GetRainbowBreaker() > 0)
			{
				int number4 = CoreData.instance.GetRainbowBreaker() - 1;
				CoreData.instance.SaveRainbowBreaker(number4);
				rainbowAmount.text = number4.ToString();
			}
		}
		else if (board.booster == BOOSTER_TYPE.OVEN_BREAKER)
		{
			CancelBooster(BOOSTER_TYPE.OVEN_BREAKER);
			if (CoreData.instance.GetOvenBreaker() > 0)
			{
				int number5 = CoreData.instance.GetOvenBreaker() - 1;
				CoreData.instance.SaveOvenBreaker(number5);
				ovenAmount.text = number5.ToString();
			}
		}
	}

	public void ShowPopup(BOOSTER_TYPE check)
	{
		switch (check)
		{
		case BOOSTER_TYPE.SINGLE_BREAKER:
			board.state = GAME_STATE.OPENING_POPUP;
			singleBoosterPopup.OpenPopup();
			break;
		case BOOSTER_TYPE.ROW_BREAKER:
			board.state = GAME_STATE.OPENING_POPUP;
			rowBoosterPopup.OpenPopup();
			break;
		case BOOSTER_TYPE.COLUMN_BREAKER:
			board.state = GAME_STATE.OPENING_POPUP;
			columnBoosterPopup.OpenPopup();
			break;
		case BOOSTER_TYPE.RAINBOW_BREAKER:
			board.state = GAME_STATE.OPENING_POPUP;
			rainbowBoosterPopup.OpenPopup();
			break;
		case BOOSTER_TYPE.OVEN_BREAKER:
			board.state = GAME_STATE.OPENING_POPUP;
			ovenBoosterPopup.OpenPopup();
			break;
		}
	}

	public void ActiveBooster(BOOSTER_TYPE check)
	{
		switch (check)
		{
		case BOOSTER_TYPE.SINGLE_BREAKER:
			board.booster = BOOSTER_TYPE.SINGLE_BREAKER;
			singleActive.SetActive(true);
			rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			break;
		case BOOSTER_TYPE.ROW_BREAKER:
			board.booster = BOOSTER_TYPE.ROW_BREAKER;
			rowActive.SetActive(true);
			singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			break;
		case BOOSTER_TYPE.COLUMN_BREAKER:
			board.booster = BOOSTER_TYPE.COLUMN_BREAKER;
			columnActive.SetActive(true);
			singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			break;
		case BOOSTER_TYPE.RAINBOW_BREAKER:
			board.booster = BOOSTER_TYPE.RAINBOW_BREAKER;
			rainbowActive.SetActive(true);
			singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			break;
		case BOOSTER_TYPE.OVEN_BREAKER:
			board.booster = BOOSTER_TYPE.OVEN_BREAKER;
			ovenActive.SetActive(true);
			singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
			break;
		}
	}

	public void CancelBooster(BOOSTER_TYPE check)
	{
		board.booster = BOOSTER_TYPE.NONE;
		switch (check)
		{
		case BOOSTER_TYPE.SINGLE_BREAKER:
			singleActive.SetActive(false);
			rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			break;
		case BOOSTER_TYPE.ROW_BREAKER:
			rowActive.SetActive(false);
			singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			break;
		case BOOSTER_TYPE.COLUMN_BREAKER:
			columnActive.SetActive(false);
			singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			break;
		case BOOSTER_TYPE.RAINBOW_BREAKER:
			rainbowActive.SetActive(false);
			singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			break;
		case BOOSTER_TYPE.OVEN_BREAKER:
			ovenActive.SetActive(false);
			singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
			break;
		}
	}
}
