using System.Collections;
using System.ComponentModel;
using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class UIWinPopup : MonoBehaviour
{
	public static MapScene instance;

	public Text levelText;

	public Text scoreText;

	public Text bonusText;

	public Image doll;

	public Image star1;

	public Image star2;

	public Image star3;

	public Text buttonText;

	public GameObject levelbox;

	public GameObject Rateus;

	public GameObject ToyBox;

	private void Awake()
	{
		levelText.text = "Level " + StageLoader.instance.Stage;
		scoreText.text = "Score: " + PlayerPrefs.GetInt("lastMatchScore").ToString();
	}
    private void Start()
	{
		GoogleAnalyticsV4.instance.LogScreen("win popup " + StageLoader.instance.Stage);
		itemGrid component = GameObject.Find("Board").GetComponent<itemGrid>();
		int star = component.star;
       // Debug.Log("Score :" + component.score + "Level:" + StageLoader.instance.Stage);

        //levelText.text = "Level " + StageLoader.instance.Stage;
		star1.gameObject.SetActive(false);
		star2.gameObject.SetActive(false);
		star3.gameObject.SetActive(false);
		StartCoroutine(ShowStars());
		buttonText.gameObject.transform.parent.gameObject.SetActive(false);
		switch (star)
		{
		case 1:
			bonusText.text = Configuration.instance.bonus1Star.ToString();
			break;
		case 2:
			bonusText.text = Configuration.instance.bonus2Star.ToString();
			break;
		case 3:
			bonusText.text = Configuration.instance.bonus3Star.ToString();
			break;
		default:
			bonusText.text = "0";
			break;
		}
		//scoreText.text = "Score: " + component.score;
		string text = "doll_" + StageLoader.instance.doll + "_4";
		if (StageLoader.instance.Stage == Configuration.instance.maxLevel)
		{
			buttonText.text = "Close";
		}
		Configuration.instance.life++;
		if (Configuration.instance.life > Configuration.instance.maxLife)
		{
			Configuration.instance.life = Configuration.instance.maxLife;
		}
		int opendedLevel = CoreData.instance.GetOpendedLevel();
		if (PlayerPrefs.GetInt("levelgift alindi" + opendedLevel, 0) == 0)
		{
			if (opendedLevel == 11 || opendedLevel == 31 || opendedLevel == 56 || opendedLevel == 71 || opendedLevel == 86 || opendedLevel == 101 || opendedLevel == 131 || opendedLevel == 156 || opendedLevel == 186 || opendedLevel == 201 || opendedLevel == 223 || opendedLevel == 251 || opendedLevel == 271 || opendedLevel == 295 || opendedLevel == 309 || opendedLevel == 324 || opendedLevel == 344 || opendedLevel == 354 || opendedLevel == 374 || opendedLevel == 385 || opendedLevel == 400 || opendedLevel == 415 || opendedLevel == 435 || opendedLevel == 449 || opendedLevel == 460 || opendedLevel == 470 || opendedLevel == 480 || opendedLevel == 490 || opendedLevel == 500 || opendedLevel == 510 || opendedLevel == 520 || opendedLevel == 530 || opendedLevel == 540 || opendedLevel == 550 || opendedLevel == 560 || opendedLevel == 570 || opendedLevel == 580 || opendedLevel == 590)
			{
				levelbox.SetActive(true);
			}
			else
			{
				levelbox.SetActive(false);
			}
		}
		if (PlayerPrefs.GetInt("Rated", 0) == 0 && (opendedLevel == 12 || opendedLevel == 17 || opendedLevel == 22 || opendedLevel == 26 || opendedLevel == 32 || opendedLevel == 36 || opendedLevel == 41 || opendedLevel == 45 || opendedLevel == 50 || opendedLevel == 57 || opendedLevel == 61 || opendedLevel == 66 || opendedLevel == 72 || opendedLevel == 75 || opendedLevel == 79 || opendedLevel == 83 || opendedLevel == 88 || opendedLevel == 92 || opendedLevel == 96 || opendedLevel == 102 || opendedLevel == 106 || opendedLevel == 111 || opendedLevel == 117 || opendedLevel == 122 || opendedLevel == 127 || opendedLevel == 132 || opendedLevel == 141 || opendedLevel == 148 || opendedLevel == 151 || opendedLevel == 157 || opendedLevel == 161 || opendedLevel == 165 || opendedLevel == 171 || opendedLevel == 175 || opendedLevel == 181 || opendedLevel == 187 || opendedLevel == 191 || opendedLevel == 195 || opendedLevel == 202 || opendedLevel == 206 || opendedLevel == 211 || opendedLevel == 216 || opendedLevel == 224 || opendedLevel == 228 || opendedLevel == 231 || opendedLevel == 237 || opendedLevel == 244 || opendedLevel == 250 || opendedLevel == 252 || opendedLevel == 255 || opendedLevel == 260 || opendedLevel == 272 || opendedLevel == 277 || opendedLevel == 285 || opendedLevel == 290 || opendedLevel == 296 || opendedLevel == 300 || opendedLevel == 305 || opendedLevel == 310 || opendedLevel == 315 || opendedLevel == 320 || opendedLevel == 325 || opendedLevel == 330 || opendedLevel == 335 || opendedLevel == 345 || opendedLevel == 350 || opendedLevel == 355 || opendedLevel == 375))
		{
			ShowRate();
		}
		int toplamScore = CoreData.instance.GetToplamScore();
		int num = 0;
		for (int i = 1; i <= CoreData.instance.GetOpendedLevel(); i++)
		{
			num += CoreData.instance.GetLevelStar(i);
		}
		GameServiceManager.ReportScore(toplamScore, "score leaderboard");
		GameServiceManager.ReportScore(num, "star leaderboard");
		if (num > 74)
		{
			GameServiceManager.UnlockAchievement("collect star 75");
		}
		if (num > 299)
		{
			GameServiceManager.UnlockAchievement("collect star 300");
		}
		if (num > 599)
		{
			GameServiceManager.UnlockAchievement("collect star 600");
		}
		if (num > 899)
		{
			GameServiceManager.UnlockAchievement("collect star 900");
		}
		if (toplamScore > 199999)
		{
			GameServiceManager.UnlockAchievement("collect score 200000");
		}
		if (toplamScore > 399999)
		{
			GameServiceManager.UnlockAchievement("collect score 400000");
		}
		if (toplamScore > 599999)
		{
			GameServiceManager.UnlockAchievement("collect score 600000");
		}
	}

	public void Showtoybox()
	{
		GetComponent<Animator>().Play("toyboxwinyandangel");
		toy.instance.closeNewToy();
		AudioManager.instance.GingerbreadExplodeAudio();
	}

	public void CloseToyBox()
	{
		ToyBox.SetActive(false);
	}

	public void likethislevel()
	{
		GoogleAnalyticsV4.instance.LogScreen("Like " + StageLoader.instance.Stage);
		Debug.Log("Liked");
		AudioManager.instance.ButtonClickAudio();
	}

	public void dislikethislevel()
	{
		GoogleAnalyticsV4.instance.LogScreen("Dislike " + StageLoader.instance.Stage);
		Debug.Log("DisLiked");
		AudioManager.instance.ButtonClickAudio();
	}

	public void ShowRate()
	{
		Rateus.gameObject.SetActive(true);
	}

	public void closerateus()
	{
		Rateus.gameObject.SetActive(false);
		GoogleAnalyticsV4.instance.LogScreen("rate us kapatanlar");
	}

	public void zipla()
	{
		AudioManager.instance.DropAudio();
	}

	public void MapAutoPopup()
	{
		Configuration.instance.autoPopup = StageLoader.instance.Stage + 1;
	}

	private IEnumerator ShowStars()
	{
		yield return new WaitForSeconds(1f);
		itemGrid board = GameObject.Find("Board").GetComponent<itemGrid>();
		switch (board.star)
		{
		case 1:
		{
			star1.gameObject.SetActive(true);
			AudioManager.instance.Star1Audio();
			GameObject explosion3 = Object.Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, base.transform.position, Quaternion.identity);
			explosion3.transform.SetParent(star1.gameObject.transform, false);
			star2.gameObject.SetActive(false);
			star3.gameObject.SetActive(false);
			break;
		}
		case 2:
		{
			star1.gameObject.SetActive(true);
			AudioManager.instance.Star1Audio();
			GameObject explosion3 = Object.Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, base.transform.position, Quaternion.identity);
			explosion3.transform.SetParent(star1.gameObject.transform, false);
			yield return new WaitForSeconds(0.5f);
			star2.gameObject.SetActive(true);
			AudioManager.instance.Star2Audio();
			GameObject explosion5 = Object.Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, base.transform.position, Quaternion.identity);
			explosion5.transform.SetParent(star2.gameObject.transform, false);
			star3.gameObject.SetActive(false);
			break;
		}
		case 3:
		{
			star1.gameObject.SetActive(true);
			AudioManager.instance.Star1Audio();
			GameObject explosion3 = Object.Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, base.transform.position, Quaternion.identity);
			explosion3.transform.SetParent(star1.gameObject.transform, false);
			yield return new WaitForSeconds(0.5f);
			star2.gameObject.SetActive(true);
			AudioManager.instance.Star2Audio();
			GameObject explosion5 = Object.Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, base.transform.position, Quaternion.identity);
			explosion5.transform.SetParent(star2.gameObject.transform, false);
			yield return new WaitForSeconds(0.5f);
			star3.gameObject.SetActive(true);
			AudioManager.instance.Star3Audio();
			GameObject explosion6 = Object.Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, base.transform.position, Quaternion.identity);
			explosion6.transform.SetParent(star3.gameObject.transform, false);
			break;
		}
		default:
			star1.gameObject.SetActive(false);
			star2.gameObject.SetActive(false);
			star3.gameObject.SetActive(false);
			break;
		}
		yield return new WaitForSeconds(0.5f);
		buttonText.gameObject.transform.parent.gameObject.SetActive(true);
	}
}
