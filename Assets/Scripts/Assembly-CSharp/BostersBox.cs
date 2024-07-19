using UnityEngine;
using UnityEngine.UI;

public class BostersBox : MonoBehaviour
{
	public Text Booster1;

	public Text Booster2;

	public Text Booster3;

	public Text Booster4;

	public Text Booster5;

	public Text Booster6;

	public Text Booster7;

	public Text Booster8;

	public GameObject b1buy;

	public GameObject b2buy;

	public GameObject b3buy;

	public GameObject b4buy;

	public GameObject b5buy;

	public GameObject b6buy;

	public GameObject b7buy;

	public GameObject b8buy;

	public static BostersBox instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		Invoke("updateBoostersLabels", 2f);
	}

	private void Update()
	{
	}

	public void updateBoostersLabels()
	{
		Booster1.text = CoreData.instance.GetRowBreaker().ToString();
		Booster2.text = CoreData.instance.GetColumnBreaker().ToString();
		Booster3.text = CoreData.instance.GetRainbowBreaker().ToString();
		Booster4.text = CoreData.instance.GetOvenBreaker().ToString();
		Booster5.text = CoreData.instance.GetSingleBreaker().ToString();
		Booster6.text = CoreData.instance.GetBeginBombBreaker().ToString();
		Booster7.text = CoreData.instance.GetBeginRainbow().ToString();
		Booster8.text = CoreData.instance.GetBeginFiveMoves().ToString();
		if (15 <= CoreData.instance.GetPlayerCoin())
		{
			b1buy.SetActive(true);
			b2buy.SetActive(true);
		}
		else
		{
			b1buy.SetActive(false);
			b2buy.SetActive(false);
		}
		if (20 <= CoreData.instance.GetPlayerCoin())
		{
			b3buy.SetActive(true);
			b4buy.SetActive(true);
		}
		else
		{
			b3buy.SetActive(false);
			b4buy.SetActive(false);
		}
		if (15 <= CoreData.instance.GetPlayerCoin())
		{
			b1buy.SetActive(true);
			b2buy.SetActive(true);
			b7buy.SetActive(true);
		}
		else
		{
			b1buy.SetActive(false);
			b2buy.SetActive(false);
			b7buy.SetActive(false);
		}
		if (8 <= CoreData.instance.GetPlayerCoin())
		{
			b5buy.SetActive(true);
			b6buy.SetActive(true);
		}
		else
		{
			b5buy.SetActive(false);
			b6buy.SetActive(false);
		}
		if (6 <= CoreData.instance.GetPlayerCoin())
		{
			b8buy.SetActive(true);
		}
		else
		{
			b8buy.SetActive(false);
		}
	}

	public void b1()
	{
		if (15 <= CoreData.instance.GetPlayerCoin())
		{
			CoreData.instance.SaveRowBreaker(++CoreData.instance.rowBreaker);
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= 15);
			Booster1.text = CoreData.instance.GetRowBreaker().ToString();
			AudioManager.instance.CoinPayAudio();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			updateBoostersLabels();
		}
	}

	public void b2()
	{
		if (15 <= CoreData.instance.GetPlayerCoin())
		{
			CoreData.instance.SaveColumnBreaker(++CoreData.instance.columnBreaker);
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= 15);
			Booster2.text = CoreData.instance.GetColumnBreaker().ToString();
			AudioManager.instance.CoinPayAudio();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			updateBoostersLabels();
		}
	}

	public void b3()
	{
		if (20 <= CoreData.instance.GetPlayerCoin())
		{
			CoreData.instance.SaveRainbowBreaker(++CoreData.instance.rainbowBreaker);
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= 20);
			Booster3.text = CoreData.instance.GetRainbowBreaker().ToString();
			AudioManager.instance.CoinPayAudio();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			updateBoostersLabels();
		}
	}

	public void b4()
	{
		if (20 <= CoreData.instance.GetPlayerCoin())
		{
			CoreData.instance.SaveOvenBreaker(++CoreData.instance.ovenBreaker);
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= 20);
			Booster4.text = CoreData.instance.GetOvenBreaker().ToString();
			AudioManager.instance.CoinPayAudio();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			updateBoostersLabels();
		}
	}

	public void b5()
	{
		if (8 <= CoreData.instance.GetPlayerCoin())
		{
			CoreData.instance.SaveSingleBreaker(++CoreData.instance.singleBreaker);
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= 8);
			Booster5.text = CoreData.instance.GetSingleBreaker().ToString();
			AudioManager.instance.CoinPayAudio();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			updateBoostersLabels();
		}
	}

	public void b6()
	{
		if (8 <= CoreData.instance.GetPlayerCoin())
		{
			CoreData.instance.SaveBeginBombBreaker(++CoreData.instance.beginBombBreaker);
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= 8);
			Booster6.text = CoreData.instance.GetBeginBombBreaker().ToString();
			AudioManager.instance.CoinPayAudio();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			updateBoostersLabels();
		}
	}

	public void b7()
	{
		if (20 <= CoreData.instance.GetPlayerCoin())
		{
			CoreData.instance.SaveBeginRainbow(++CoreData.instance.beginRainbow);
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= 20);
			Booster7.text = CoreData.instance.GetBeginRainbow().ToString();
			AudioManager.instance.CoinPayAudio();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			updateBoostersLabels();
		}
	}

	public void b8()
	{
		if (6 <= CoreData.instance.GetPlayerCoin())
		{
			CoreData.instance.SaveBeginFiveMoves(++CoreData.instance.beginFiveMoves);
			CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= 6);
			Booster8.text = CoreData.instance.GetBeginFiveMoves().ToString();
			AudioManager.instance.CoinPayAudio();
			GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
			updateBoostersLabels();
		}
	}
}
