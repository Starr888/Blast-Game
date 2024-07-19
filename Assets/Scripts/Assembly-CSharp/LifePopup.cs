using UnityEngine;
using UnityEngine.UI;

public class LifePopup : MonoBehaviour
{
	public Text lifeRemain;

	public Text recoveryCost;

	public GameObject recoveryButton;

	private int cost;

	private void Start()
	{
		if (Configuration.instance.life < Configuration.instance.maxLife)
		{
			recoveryButton.SetActive(true);
			lifeRemain.text = "Life: " + Configuration.instance.life + "/" + Configuration.instance.maxLife;
			cost = Configuration.instance.recoveryCostPerLife * (Configuration.instance.maxLife - Configuration.instance.life);
			recoveryCost.text = cost.ToString();
		}
		else
		{
			lifeRemain.text = "Life: " + Configuration.instance.maxLife + "/" + Configuration.instance.maxLife;
			recoveryButton.SetActive(false);
			recoveryCost.gameObject.transform.parent.gameObject.SetActive(false);
		}
	}

	public void updatelife()
	{
		if (Configuration.instance.life < Configuration.instance.maxLife)
		{
			recoveryButton.SetActive(true);
			lifeRemain.text = "Life: " + Configuration.instance.life + "/" + Configuration.instance.maxLife;
			cost = Configuration.instance.recoveryCostPerLife * (Configuration.instance.maxLife - Configuration.instance.life);
			recoveryCost.text = cost.ToString();
		}
		else
		{
			lifeRemain.text = "Life: " + Configuration.instance.maxLife + "/" + Configuration.instance.maxLife;
			recoveryButton.SetActive(false);
			recoveryCost.gameObject.transform.parent.gameObject.SetActive(false);
		}
	}

	public void ButtonClickAudio()
	{
		AudioManager.instance.ButtonClickAudio();
	}

	public void RecoveryButtonClick()
	{
		if (CoreData.instance.playerCoin < cost)
		{
			GameObject.Find("MapScene").GetComponent<MapScene>().CoinButtonClick();
			return;
		}
		CoreData.instance.SavePlayerCoin(CoreData.instance.GetPlayerCoin() - cost);
		AudioManager.instance.CoinPayAudio();
		GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
		GameObject.Find("LifeBar").GetComponent<Life>().AddLife(Configuration.instance.maxLife);
		lifeRemain.text = "Life: " + Configuration.instance.maxLife + "/" + Configuration.instance.maxLife;
		recoveryButton.SetActive(false);
		recoveryCost.gameObject.transform.parent.gameObject.SetActive(false);
	}
}
