using UnityEngine;

public class StarGiftsPopup : MonoBehaviour
{
	public PopupOpener stargiftPopup;

	public GameObject gift1;

	public GameObject gift2;

	public GameObject gift3;

	public GameObject gift4;

	public GameObject gift5;

	public GameObject gift6;

	public void stargifts()
	{
		if (!GameObject.Find("Stargift(Clone)"))
		{
			stargiftPopup.OpenPopup();
		}
	}

	public void ButtonClickAudio()
	{
		AudioManager.instance.ButtonClickAudio();
	}

	public void ButtonPayAudio()
	{
		AudioManager.instance.giftsound();
	}

	public void g1()
	{
		Debug.Log("gift1 alindi");
		CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 5);
		GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		gift1.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatestargiftPopup();
	}

	public void g2()
	{
		Debug.Log("gift2 alindi");
		CoreData.instance.SaveColumnBreaker(++CoreData.instance.columnBreaker);
		gift2.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatestargiftPopup();
	}

	public void g3()
	{
		Debug.Log("gift3 alindi");
		CoreData.instance.SaveBeginRainbow(++CoreData.instance.beginRainbow);
		gift3.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatestargiftPopup();
	}

	public void g4()
	{
		Debug.Log("gift4 alindi");
		CoreData.instance.SaveSingleBreaker(++CoreData.instance.singleBreaker);
		gift4.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatestargiftPopup();
	}

	public void g5()
	{
		Debug.Log("gift5 alindi");
		CoreData.instance.SaveRainbowBreaker(++CoreData.instance.rainbowBreaker);
		gift5.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatestargiftPopup();
	}

	public void g6()
	{
		Debug.Log("gift6 alindi");
		CoreData.instance.SaveOvenBreaker(++CoreData.instance.ovenBreaker);
		gift6.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatestargiftPopup();
	}

	public void UpdatestargiftPopup()
	{
		if (CoreData.instance.giftAmount >= 3)
		{
			GetComponent<Popup>().Close();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateStarsButon();
			CoreData.instance.SavePlayerStars(0);
			CoreData.instance.SaveGiftAmount(0);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateStarsAmountLabel();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateStarProgressBar();
			AudioManager.instance.CoinAddAudio();
		}
	}
}
