using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToyWinPopup : MonoBehaviour
{
	public static ToyWinPopup instance;

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
		Invoke("RandomGitHesapla", 2f);
	}

	public void RandomGitHesapla()
	{
		int num = Random.Range(0, 2);
		Debug.Log(num);
		if (num == 0)
		{
			updatetoys();
			StartCoroutine(randomGift());
		}
		else
		{
			base.gameObject.SetActive(false);
			StopCoroutine(randomGift());
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
		noiseValues = new float[10];
		for (int i = 0; i < noiseValues.Length; i++)
		{
			noiseValues[i] = Random.Range(1, 31);
			Debug.Log(noiseValues[i]);
			if (noiseValues[i] == (float)ToyNumber)
			{
				Gift = true;
				yield return new WaitForSeconds(0.5f);
				Addone.SetActive(true);
				yield return new WaitForSeconds(0.5f);
				AudioManager.instance.CollectibleExplodeAudio();
				yield return new WaitForSeconds(0.5f);
				Debug.Log("Gift " + noiseValues[i]);
				yield return new WaitForSeconds(0.5f);
				Configuration.instance.gift1 = ToyNumber;
				Configuration.instance.giftsayisi++;
				yield return new WaitForSeconds(0.5f);
				NewToyBildir();
			}
			yield return new WaitForEndOfFrame();
		}
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
		GetComponent<Animator>().Play("toyzipla");
		NewToyPopupText.text = "New " + giftsayisi + " Toys! ";
		yield return new WaitForSeconds(3f);
	}

	public void closeNewToy()
	{
		NewToyPopup.gameObject.SetActive(false);
	}
}
