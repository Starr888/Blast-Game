using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelBox : MonoBehaviour
{
	public static LevelBox instance;

	public int levelsayisi;

	public GameObject levelinfopen;

	public GameObject box;

	public GameObject disablekutu;

	public GameObject kapak;

	public Text levelinfotext;

	public int openedLevel;

	public Text levellabel;

	private void Start()
	{
		openedLevel = CoreData.instance.openedLevel;
		if (PlayerPrefs.GetInt("levelgift alindi" + openedLevel, 1) == 1)
		{
			if (openedLevel <= levelsayisi)
			{
				disablekutu.gameObject.SetActive(false);
				box.gameObject.SetActive(true);
				kapak.gameObject.SetActive(true);
			}
			else if (openedLevel > levelsayisi)
			{
				disablekutu.gameObject.SetActive(true);
				box.gameObject.SetActive(false);
				kapak.gameObject.SetActive(false);
			}
		}
		if (openedLevel <= 10)
		{
			levelsayisi = 10;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 10 && openedLevel <= 30)
		{
			levelsayisi = 30;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 30 && openedLevel <= 55)
		{
			levelsayisi = 55;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 55 && openedLevel <= 70)
		{
			levelsayisi = 70;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 70 && openedLevel <= 85)
		{
			levelsayisi = 85;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 85 && openedLevel <= 100)
		{
			levelsayisi = 100;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 100 && openedLevel <= 130)
		{
			levelsayisi = 130;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 130 && openedLevel <= 155)
		{
			levelsayisi = 155;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 155 && openedLevel <= 185)
		{
			levelsayisi = 185;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 185 && openedLevel <= 200)
		{
			levelsayisi = 200;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 200 && openedLevel <= 222)
		{
			levelsayisi = 222;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 222 && openedLevel <= 250)
		{
			levelsayisi = 250;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 250 && openedLevel <= 270)
		{
			levelsayisi = 270;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 270 && openedLevel <= 294)
		{
			levelsayisi = 294;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 294 && openedLevel <= 308)
		{
			levelsayisi = 308;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 308 && openedLevel <= 323)
		{
			levelsayisi = 323;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 323 && openedLevel <= 343)
		{
			levelsayisi = 343;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 343 && openedLevel <= 353)
		{
			levelsayisi = 353;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 353 && openedLevel <= 373)
		{
			levelsayisi = 373;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 373 && openedLevel <= 384)
		{
			levelsayisi = 384;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 384 && openedLevel <= 399)
		{
			levelsayisi = 400;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 399 && openedLevel <= 414)
		{
			levelsayisi = 415;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 414 && openedLevel <= 434)
		{
			levelsayisi = 435;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 434 && openedLevel <= 448)
		{
			levelsayisi = 449;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 448 && openedLevel <= 459)
		{
			levelsayisi = 460;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 459 && openedLevel <= 469)
		{
			levelsayisi = 470;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 469 && openedLevel <= 479)
		{
			levelsayisi = 480;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 479 && openedLevel <= 489)
		{
			levelsayisi = 490;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 489 && openedLevel <= 499)
		{
			levelsayisi = 500;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 499 && openedLevel <= 509)
		{
			levelsayisi = 510;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 509 && openedLevel <= 519)
		{
			levelsayisi = 520;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 519 && openedLevel <= 529)
		{
			levelsayisi = 530;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 529 && openedLevel <= 539)
		{
			levelsayisi = 540;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 539 && openedLevel <= 549)
		{
			levelsayisi = 550;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 549 && openedLevel <= 559)
		{
			levelsayisi = 560;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 559 && openedLevel <= 569)
		{
			levelsayisi = 570;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 569 && openedLevel <= 579)
		{
			levelsayisi = 580;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 579 && openedLevel <= 589)
		{
			levelsayisi = 590;
			levellabel.text = "Reach level " + levelsayisi;
		}
		else if (openedLevel > 590)
		{
			levelsayisi = 600;
			levellabel.text = string.Empty;
		}
	}

	private void Update()
	{
	}

	public void showlevelinfopopup()
	{
		levelinfotext.text = "Reach level " + levelsayisi + " to get the Level Toy Box!";
		StartCoroutine(levelinfopopup());
		AudioManager.instance.ButtonClickAudio();
	}

	private IEnumerator levelinfopopup()
	{
		levelinfopen.gameObject.SetActive(true);
		yield return new WaitForSeconds(3f);
		levelinfopen.gameObject.SetActive(false);
	}
}
