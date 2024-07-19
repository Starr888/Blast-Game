using UnityEngine;

public class ToyBoxGift : MonoBehaviour
{
	public PopupOpener ToyBoxgiftPopup;

	public GameObject gift1;

	public GameObject gift2;

	public GameObject gift3;

	public GameObject gift4;

	public GameObject gift5;

	public GameObject gift6;

	public bool gift1alindi;

	public bool gift2alindi;

	public bool gift3alindi;

	public bool gift4alindi;

	public bool gift5alindi;

	public bool gift6alindi;

	private bool IsGiftsCompleted()
	{
		if (!gift1alindi && !gift2alindi && !gift3alindi && !gift4alindi && !gift5alindi && !gift6alindi)
		{
			return true;
		}
		return false;
	}

	public void levelgifts()
	{
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
		CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 15);
		gift1.SetActive(false);
		gift1alindi = true;
		UpdatelevelgiftPopup();
	}

	public void g2()
	{
		Debug.Log("gift2 alindi");
		CoreData.instance.SaveColumnBreaker(++CoreData.instance.columnBreaker);
		gift2.SetActive(false);
		gift2alindi = true;
		UpdatelevelgiftPopup();
	}

	public void g3()
	{
		Debug.Log("gift3 alindi");
		CoreData.instance.SaveBeginRainbow(++CoreData.instance.beginRainbow);
		gift3.SetActive(false);
		gift3alindi = true;
		UpdatelevelgiftPopup();
	}

	public void g4()
	{
		Debug.Log("gift4 alindi");
		CoreData.instance.SaveSingleBreaker(++CoreData.instance.singleBreaker);
		gift4.SetActive(false);
		gift4alindi = true;
		UpdatelevelgiftPopup();
	}

	public void g5()
	{
		Debug.Log("gift5 alindi");
		CoreData.instance.SaveRainbowBreaker(++CoreData.instance.rainbowBreaker);
		gift5.SetActive(false);
		gift5alindi = true;
		UpdatelevelgiftPopup();
	}

	public void g6()
	{
		Debug.Log("gift6 alindi");
		CoreData.instance.SaveOvenBreaker(++CoreData.instance.ovenBreaker);
		gift6.SetActive(false);
		gift6alindi = true;
		UpdatelevelgiftPopup();
	}

	public void UpdatelevelgiftPopup()
	{
		int opendedLevel = CoreData.instance.GetOpendedLevel();
		PlayerPrefs.SetInt("levelgift alindi" + opendedLevel, 1);
		if (gift1alindi && gift2alindi && gift3alindi && gift4alindi && gift5alindi && gift6alindi)
		{
			base.gameObject.SetActive(false);
			AudioManager.instance.CoinAddAudio();
		}
	}
}
