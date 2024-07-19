using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class toy : MonoBehaviour
{
	public static toy instance;

	public int ToyNumber;

	public GameObject Label;

	public Text LabelText;

	public GameObject GetGiftButton;

	public GameObject Addone;

	public GameObject GiftAmountPopup;

	public Text GiftAmountPopupText;

	public GameObject ToyBoxTarget;

	public Text ToyBoxTargetText;

	public int TargetToyAmount;

	public int GiftGemsAmount;

	public int Odul;

	public GameObject NewToyPopup;

	public Text NewToyPopupText;

	public int CurrentToy;

	public Sprite[] ToySprite;

	public bool GiftButton;

	public bool Gift;

	public GameObject toyimagelocked;

	public GameObject toyimage;

	public SpriteRenderer current_ToySprite;

	public SpriteRenderer current_ToySpriteLocked;

	private float[] noiseValues;

	public GAME_STATE state;

	private void changeToySprite()
	{
		current_ToySpriteLocked.sprite = ToySprite[ToyNumber];
		current_ToySprite.sprite = ToySprite[ToyNumber];
	}

	private void Start()
	{
		instance = this;
		Gift = false;
		if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
		{
			Invoke("RandomGitHesapla", 1f);
		}
		else
		{
			Invoke("RandomGitHesapla", 2f);
		}
	}

	public void RandomGitHesapla()
	{
		if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
		{
			updatetoys();
			StartCoroutine(randomGift());
		}
		else if (state == GAME_STATE.PREPARING_LEVEL)
		{
			updatetoys();
			StartCoroutine(randomGift());
		}
	}

	public void updatetoys()
	{
		changeToySprite();
		LabelText.text = string.Empty + PlayerPrefs.GetInt("toy" + ToyNumber) + " /" + TargetToyAmount;
		CurrentToy = PlayerPrefs.GetInt("toy" + ToyNumber);
		if (CurrentToy >= TargetToyAmount)
		{
			GiftButton = true;
		}
		if (CurrentToy > 0)
		{
			toyimagelocked.SetActive(false);
			toyimage.SetActive(true);
		}
		if (GiftButton)
		{
			GetGiftButton.SetActive(true);
			Label.SetActive(false);
		}
		else
		{
			GetGiftButton.SetActive(false);
			Label.SetActive(true);
		}
	}

	public void buy()
	{
		PlayerPrefs.SetInt("toy" + ToyNumber, CurrentToy + 1);
		PlayerPrefs.Save();
		LabelText.text = string.Empty + PlayerPrefs.GetInt("toy" + ToyNumber) + " /" + TargetToyAmount;
		AudioManager.instance.CollectibleExplodeAudio();
		Gift = false;
		Addone.SetActive(false);
		updatetoys();
		closeNewToy();
		Configuration.instance.giftsayisi = 0;
	}

	public void getgifts()
	{
		PlayerPrefs.SetInt("toy" + ToyNumber, 0);
		PlayerPrefs.Save();
		LabelText.text = string.Empty + PlayerPrefs.GetInt("toy" + ToyNumber) + " /" + TargetToyAmount;
		CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += GiftGemsAmount);
		GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		AudioManager.instance.CoinPayAudio();
		GiftButton = false;
		showgiftamount();
		updatetoys();
	}

	public void Giftlerisec()
	{
		StartCoroutine(randomGift());
	}

	private IEnumerator randomGift()
	{
		int seed = Random.Range(1, 31);
		if (seed == ToyNumber)
		{
			Gift = true;
			yield return new WaitForSeconds(0.2f);
			Addone.SetActive(true);
			yield return new WaitForSeconds(0.2f);
			Debug.Log("Gift " + seed);
			yield return new WaitForSeconds(0.2f);
			Configuration.instance.gift1 = ToyNumber;
			yield return new WaitForSeconds(0.2f);
			Configuration.instance.giftsayisi++;
			yield return new WaitForSeconds(0.2f);
			AudioManager.instance.CollectibleExplodeAudio();
			NewToyBildir();
		}
		else if (!Gift)
		{
			if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
			{
				base.gameObject.SetActive(false);
			}
			else
			{
				base.gameObject.SetActive(true);
			}
		}
		yield return new WaitForEndOfFrame();
	}

	public void showgiftamount()
	{
		StartCoroutine(giftamount());
	}

	private IEnumerator giftamount()
	{
		GiftAmountPopup.gameObject.SetActive(true);
		GiftAmountPopupText.text = "+" + GiftGemsAmount;
		yield return new WaitForSeconds(3f);
		GiftAmountPopup.gameObject.SetActive(false);
	}

	public void ToyButton()
	{
		if (Gift)
		{
			buy();
			return;
		}
		AudioManager.instance.ButtonClickAudio();
		StartCoroutine(ToyBoxTargettext());
	}

	private IEnumerator ToyBoxTargettext()
	{
		ToyBoxTarget.gameObject.SetActive(true);
		ToyBoxTargetText.text = "Collect " + TargetToyAmount + " toys and earn " + GiftGemsAmount + " gems";
		yield return new WaitForSeconds(3f);
		ToyBoxTarget.gameObject.SetActive(false);
	}

	public void NewToyBildir()
	{
		StartCoroutine(NewToygoster());
	}

	private IEnumerator NewToygoster()
	{
		int giftsayisi = Configuration.instance.giftsayisi;
		NewToyPopup.gameObject.SetActive(true);
		NewToyPopupText.text = "New " + giftsayisi + " Toys! ";
		yield return new WaitForSeconds(3f);
	}

	public void closeNewToy()
	{
		NewToyPopup.gameObject.SetActive(false);
	}
}
