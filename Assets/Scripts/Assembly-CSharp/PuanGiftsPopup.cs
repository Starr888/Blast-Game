using UnityEngine;

public class PuanGiftsPopup : MonoBehaviour
{
	public PopupOpener puangiftPopup;

	public GameObject puan1;

	public GameObject puan2;

	public GameObject puan3;

	public GameObject puan4;

	public GameObject puan5;

	public GameObject puan6;

	public void Puangifts()
	{
		if (!GameObject.Find("Puangift(Clone)"))
		{
			puangiftPopup.OpenPopup();
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

	public void p1()
	{
		Debug.Log("gift1 alindi");
		CoreData.instance.SaveRowBreaker(++CoreData.instance.rowBreaker);
		puan1.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatepuangiftPopup();
	}

	public void p2()
	{
		Debug.Log("gift2 alindi");
		CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 5);
		GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		puan2.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatepuangiftPopup();
	}

	public void p3()
	{
		Debug.Log("gift3 alindi");
		CoreData.instance.SaveBeginBombBreaker(++CoreData.instance.beginBombBreaker);
		puan3.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatepuangiftPopup();
	}

	public void p4()
	{
		Debug.Log("gift4 alindi");
		CoreData.instance.SaveBeginFiveMoves(++CoreData.instance.beginFiveMoves);
		puan4.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatepuangiftPopup();
	}

	public void p5()
	{
		Debug.Log("gift5 alindi");
		GameObject.Find("LifeBar").GetComponent<Life>().AddLife(Configuration.instance.maxLife);
		puan5.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatepuangiftPopup();
	}

	public void p6()
	{
		Debug.Log("gift6 alindi");
		CoreData.instance.SaveBeginRainbow(++CoreData.instance.beginRainbow);
		puan6.SetActive(false);
		CoreData.instance.SaveGiftAmount(++CoreData.instance.giftAmount);
		UpdatepuangiftPopup();
	}

	public void UpdatepuangiftPopup()
	{
		if (CoreData.instance.giftAmount >= 3)
		{
			GetComponent<Popup>().Close();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdatePuanButon();
			CoreData.instance.SavePlayerPuan(0);
			CoreData.instance.SaveGiftAmount(0);
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdatePuanAmountLabel();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdatePuanProgressBar();
			AudioManager.instance.CoinAddAudio();
		}
	}
}
