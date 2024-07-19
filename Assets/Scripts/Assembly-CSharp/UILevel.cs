using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
	public int level;

	public MAP_LEVEL_STATUS status;

	public Image background;

	public Text number;

	public Image star1;

	public Image star2;

	public Image star3;

	public Sprite starGoldSprite;

	public Sprite starGreySprite;

	public GameObject button;

	public PopupOpener levelPopup;

	private void Start()
	{
		level = base.transform.GetSiblingIndex();
		number.text = level.ToString();
		int opendedLevel = CoreData.instance.GetOpendedLevel();
		if (opendedLevel == level)
		{
			status = MAP_LEVEL_STATUS.CURRENT;
			base.gameObject.SetActive(true);
			if (level == Configuration.instance.maxLevel)
			{
				switch (CoreData.instance.GetLevelStar(level))
				{
				case 1:
					star1.sprite = starGoldSprite;
					star2.sprite = starGreySprite;
					star3.sprite = starGreySprite;
					break;
				case 2:
					star1.sprite = starGoldSprite;
					star2.sprite = starGoldSprite;
					star3.sprite = starGreySprite;
					break;
				case 3:
					star1.sprite = starGoldSprite;
					star2.sprite = starGoldSprite;
					star3.sprite = starGoldSprite;
					break;
				}
			}
			if ((bool)GameObject.Find("TargetPointer"))
			{
				GameObject gameObject = GameObject.Find("TargetPointer");
				gameObject.transform.position = base.gameObject.transform.position + new Vector3(0f, 1f, 0f);
				gameObject.transform.SetParent(base.gameObject.transform.parent.transform);
				iTween.MoveBy(gameObject, iTween.Hash("y", 0.2f, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear, "time", 1f));
			}
		}
		else if (opendedLevel > level)
		{
			status = MAP_LEVEL_STATUS.OPENED;
			switch (CoreData.instance.GetLevelStar(level))
			{
			case 1:
				star1.sprite = starGoldSprite;
				star2.sprite = starGreySprite;
				star3.sprite = starGreySprite;
				break;
			case 2:
				star1.sprite = starGoldSprite;
				star2.sprite = starGoldSprite;
				star3.sprite = starGreySprite;
				break;
			case 3:
				star1.sprite = starGoldSprite;
				star2.sprite = starGoldSprite;
				star3.sprite = starGoldSprite;
				break;
			}
		}
		else if (opendedLevel < level)
		{
			base.gameObject.SetActive(false);
			background.gameObject.SetActive(false);
			number.gameObject.SetActive(false);
			star1.gameObject.SetActive(false);
			star2.gameObject.SetActive(false);
			star3.gameObject.SetActive(false);
		}
	}

	public void LevelClick()
	{
		AudioManager.instance.ButtonClickAudio();
		if (status != 0)
		{
			StageLoader.instance.Stage = level;
			StageLoader.instance.LoadLevel();
			levelPopup.OpenPopup();
		}
	}

	public void ShowHelp(int level)
	{
		StartCoroutine(StartShowHelp(level));
	}

	private IEnumerator StartShowHelp(int level)
	{
		yield return new WaitForSeconds(0.5f);
	}
}
