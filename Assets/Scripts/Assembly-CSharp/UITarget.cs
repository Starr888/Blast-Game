using UnityEngine;
using UnityEngine.UI;

public class UITarget : MonoBehaviour
{
	public Image target1;

	public Text target1Amount;

	public Image target1Tick;

	public Image target2;

	public Text target2Amount;

	public Image target2Tick;

	public Image target3;

	public Text target3Amount;

	public Image target3Tick;

	public Image target4;

	public Text target4Amount;

	public Image target4Tick;

	private void Start()
	{
		for (int i = 1; i <= 4; i++)
		{
			Image image = null;
			Text text = null;
			Image image2 = null;
			GameObject gameObject = null;
			TARGET_TYPE tARGET_TYPE = TARGET_TYPE.NONE;
			int num = 0;
			int num2 = 0;
			switch (i)
			{
			case 1:
				image = target1;
				text = target1Amount;
				image2 = target1Tick;
				tARGET_TYPE = StageLoader.instance.target1Type;
				num = StageLoader.instance.target1Amount;
				num2 = StageLoader.instance.target1Color;
				break;
			case 2:
				image = target2;
				text = target2Amount;
				image2 = target2Tick;
				tARGET_TYPE = StageLoader.instance.target2Type;
				num = StageLoader.instance.target2Amount;
				num2 = StageLoader.instance.target2Color;
				break;
			case 3:
				image = target3;
				text = target3Amount;
				image2 = target3Tick;
				tARGET_TYPE = StageLoader.instance.target3Type;
				num = StageLoader.instance.target3Amount;
				num2 = StageLoader.instance.target3Color;
				break;
			case 4:
				image = target4;
				text = target4Amount;
				image2 = target4Tick;
				tARGET_TYPE = StageLoader.instance.target4Type;
				num = StageLoader.instance.target4Amount;
				num2 = StageLoader.instance.target4Color;
				break;
			}
			switch (tARGET_TYPE)
			{
			case TARGET_TYPE.ITEM:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				switch (num2)
				{
				case 1:
					gameObject = Resources.Load(Configuration.Item1()) as GameObject;
					break;
				case 2:
					gameObject = Resources.Load(Configuration.Item2()) as GameObject;
					break;
				case 3:
					gameObject = Resources.Load(Configuration.Item3()) as GameObject;
					break;
				case 4:
					gameObject = Resources.Load(Configuration.Item4()) as GameObject;
					break;
				case 5:
					gameObject = Resources.Load(Configuration.Item5()) as GameObject;
					break;
				case 6:
					gameObject = Resources.Load(Configuration.Item6()) as GameObject;
					break;
				}
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				image.rectTransform.localScale = new Vector3(1f, 1f, 0f);
				break;
			case TARGET_TYPE.BREAKABLE:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.Breakable()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				image.rectTransform.localScale = new Vector3(1f, 1f, 0f);
				break;
			case TARGET_TYPE.WAFFLE:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.Waffle1()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				image.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0f);
				break;
			case TARGET_TYPE.COLLECTIBLE:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				switch (num2)
				{
				case 1:
					gameObject = Resources.Load(Configuration.Collectible1()) as GameObject;
					break;
				case 2:
					gameObject = Resources.Load(Configuration.Collectible2()) as GameObject;
					break;
				case 3:
					gameObject = Resources.Load(Configuration.Collectible3()) as GameObject;
					break;
				case 4:
					gameObject = Resources.Load(Configuration.Collectible4()) as GameObject;
					break;
				case 5:
					gameObject = Resources.Load(Configuration.Collectible5()) as GameObject;
					break;
				case 6:
					gameObject = Resources.Load(Configuration.Collectible6()) as GameObject;
					break;
				case 7:
					gameObject = Resources.Load(Configuration.Collectible7()) as GameObject;
					break;
				case 8:
					gameObject = Resources.Load(Configuration.Collectible8()) as GameObject;
					break;
				case 9:
					gameObject = Resources.Load(Configuration.Collectible9()) as GameObject;
					break;
				case 10:
					gameObject = Resources.Load(Configuration.Collectible10()) as GameObject;
					break;
				case 11:
					gameObject = Resources.Load(Configuration.Collectible11()) as GameObject;
					break;
				case 12:
					gameObject = Resources.Load(Configuration.Collectible12()) as GameObject;
					break;
				case 13:
					gameObject = Resources.Load(Configuration.Collectible13()) as GameObject;
					break;
				case 14:
					gameObject = Resources.Load(Configuration.Collectible14()) as GameObject;
					break;
				case 15:
					gameObject = Resources.Load(Configuration.Collectible15()) as GameObject;
					break;
				case 16:
					gameObject = Resources.Load(Configuration.Collectible16()) as GameObject;
					break;
				case 17:
					gameObject = Resources.Load(Configuration.Collectible17()) as GameObject;
					break;
				case 18:
					gameObject = Resources.Load(Configuration.Collectible18()) as GameObject;
					break;
				case 19:
					gameObject = Resources.Load(Configuration.Collectible19()) as GameObject;
					break;
				case 20:
					gameObject = Resources.Load(Configuration.Collectible20()) as GameObject;
					break;
				}
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				image.rectTransform.localScale = new Vector3(1f, 1f, 0f);
				break;
			case TARGET_TYPE.COLUMN_ROW_BREAKER:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.ColumnRowBreaker()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				break;
			case TARGET_TYPE.BOMB_BREAKER:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.GenericBombBreaker()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				break;
			case TARGET_TYPE.X_BREAKER:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.GenericXBreaker()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				break;
			case TARGET_TYPE.LOCK:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.Lock1()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				image.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0f);
				break;
			case TARGET_TYPE.COLORCONE:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.ItemColorCone()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				break;
			case TARGET_TYPE.ROCKET:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.RocketGeneric()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				break;
			case TARGET_TYPE.TOYMINE:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.ToyMine1()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				break;
			case TARGET_TYPE.ROCK_CANDY:
				image.gameObject.SetActive(true);
				text.gameObject.SetActive(true);
				image2.gameObject.SetActive(false);
				gameObject = Resources.Load(Configuration.LegoBoxGeneric()) as GameObject;
				if (gameObject != null)
				{
					image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				}
				text.text = num.ToString();
				break;
			default:
				image.gameObject.transform.parent.gameObject.SetActive(false);
				break;
			}
		}
	}

	public void UpdateTargetAmount(int target)
	{
		switch (target)
		{
		case 1:
			target1Amount.text = (int.Parse(target1Amount.text) - 1).ToString();
			if (int.Parse(target1Amount.text) <= 0)
			{
				target1Amount.gameObject.SetActive(false);
				target1Tick.gameObject.SetActive(true);
			}
			break;
		case 2:
			target2Amount.text = (int.Parse(target2Amount.text) - 1).ToString();
			if (int.Parse(target2Amount.text) <= 0)
			{
				target2Amount.gameObject.SetActive(false);
				target2Tick.gameObject.SetActive(true);
			}
			break;
		case 3:
			target3Amount.text = (int.Parse(target3Amount.text) - 1).ToString();
			if (int.Parse(target3Amount.text) <= 0)
			{
				target3Amount.gameObject.SetActive(false);
				target3Tick.gameObject.SetActive(true);
			}
			break;
		case 4:
			target4Amount.text = (int.Parse(target4Amount.text) - 1).ToString();
			if (int.Parse(target4Amount.text) <= 0)
			{
				target4Amount.gameObject.SetActive(false);
				target4Tick.gameObject.SetActive(true);
			}
			break;
		}
	}
}
