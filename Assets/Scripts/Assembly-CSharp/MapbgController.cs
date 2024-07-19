using UnityEngine;
using UnityEngine.UI;

public class MapbgController : MonoBehaviour
{
	private Image current_Bgsprite;

	public Sprite[] levels_Bgsprite;

	private void Start()
	{
		current_Bgsprite = GetComponent<Image>();
		changeBGSprite();
	}

	private void changeBGSprite()
	{
		int openedLevel = CoreData.instance.openedLevel;
		if (openedLevel <= 20)
		{
			current_Bgsprite.sprite = levels_Bgsprite[0];
		}
		if (openedLevel >= 21 && openedLevel <= 40)
		{
			current_Bgsprite.sprite = levels_Bgsprite[1];
		}
		if (openedLevel >= 40 && openedLevel <= 60)
		{
			current_Bgsprite.sprite = levels_Bgsprite[2];
		}
		if (openedLevel >= 60 && openedLevel <= 80)
		{
			current_Bgsprite.sprite = levels_Bgsprite[3];
		}
		if (openedLevel >= 80 && openedLevel <= 100)
		{
			current_Bgsprite.sprite = levels_Bgsprite[4];
		}
		if (openedLevel >= 100 && openedLevel <= 120)
		{
			current_Bgsprite.sprite = levels_Bgsprite[5];
		}
		if (openedLevel >= 120 && openedLevel <= 140)
		{
			current_Bgsprite.sprite = levels_Bgsprite[6];
		}
		if (openedLevel >= 140 && openedLevel <= 160)
		{
			current_Bgsprite.sprite = levels_Bgsprite[7];
		}
		if (openedLevel >= 160 && openedLevel <= 180)
		{
			current_Bgsprite.sprite = levels_Bgsprite[8];
		}
		if (openedLevel >= 180 && openedLevel <= 200)
		{
			current_Bgsprite.sprite = levels_Bgsprite[9];
		}
		if (openedLevel >= 200 && openedLevel <= 220)
		{
			current_Bgsprite.sprite = levels_Bgsprite[10];
		}
		if (openedLevel >= 220 && openedLevel <= 240)
		{
			current_Bgsprite.sprite = levels_Bgsprite[11];
		}
		if (openedLevel >= 240 && openedLevel <= 260)
		{
			current_Bgsprite.sprite = levels_Bgsprite[12];
		}
		if (openedLevel >= 260 && openedLevel <= 280)
		{
			current_Bgsprite.sprite = levels_Bgsprite[13];
		}
		if (openedLevel >= 280 && openedLevel <= 300)
		{
			current_Bgsprite.sprite = levels_Bgsprite[14];
		}
		if (openedLevel >= 300 && openedLevel <= 320)
		{
			current_Bgsprite.sprite = levels_Bgsprite[15];
		}
	}

	private void Update()
	{
	}
}
