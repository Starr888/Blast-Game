using UnityEngine;
using UnityEngine.UI;

public class LevelMap : MonoBehaviour
{
	public GameObject[] Unlocked;

	public GameObject[] Locked;

	public int episode;

	public Text EpisodeLabelText;

	private void Start()
	{
		Invoke("updateLevelMap", 5f);
	}

	public void updateLevelMap()
	{
		int openedLevel = CoreData.instance.openedLevel;
		if (openedLevel <= 5)
		{
			Unlocked[1].SetActive(true);
			Locked[1].SetActive(false);
			EpisodeLabelText.text = "Episode:1 Cottage";
		}
		if (openedLevel >= 5 && openedLevel <= 15)
		{
			episode = 2;
			for (int i = 1; i <= episode; i++)
			{
				Unlocked[i].SetActive(true);
				Locked[i].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Red Magic";
		}
		if (openedLevel >= 15 && openedLevel <= 22)
		{
			episode = 3;
			for (int j = 1; j <= episode; j++)
			{
				Unlocked[j].SetActive(true);
				Locked[j].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Mysterious Forest";
		}
		if (openedLevel >= 22 && openedLevel <= 31)
		{
			episode = 4;
			for (int k = 1; k <= episode; k++)
			{
				Unlocked[k].SetActive(true);
				Locked[k].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Pink River";
		}
		if (openedLevel >= 31 && openedLevel <= 41)
		{
			episode = 5;
			for (int l = 1; l <= episode; l++)
			{
				Unlocked[l].SetActive(true);
				Locked[l].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Colorful Pumpkins";
		}
		if (openedLevel >= 41 && openedLevel <= 48)
		{
			episode = 6;
			for (int m = 1; m <= episode; m++)
			{
				Unlocked[m].SetActive(true);
				Locked[m].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Lake Racing";
		}
		if (openedLevel >= 48 && openedLevel <= 59)
		{
			episode = 7;
			for (int n = 1; n <= episode; n++)
			{
				Unlocked[n].SetActive(true);
				Locked[n].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Frosted Candies";
		}
		if (openedLevel >= 59 && openedLevel <= 72)
		{
			episode = 8;
			for (int num = 1; num <= episode; num++)
			{
				Unlocked[num].SetActive(true);
				Locked[num].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Peaceful Forest";
		}
		if (openedLevel >= 72 && openedLevel <= 82)
		{
			episode = 9;
			for (int num2 = 1; num2 <= episode; num2++)
			{
				Unlocked[num2].SetActive(true);
				Locked[num2].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Cactus Way";
		}
		if (openedLevel >= 82 && openedLevel <= 92)
		{
			episode = 10;
			for (int num3 = 1; num3 <= episode; num3++)
			{
				Unlocked[num3].SetActive(true);
				Locked[num3].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Dream Steps";
		}
		if (openedLevel >= 92 && openedLevel <= 100)
		{
			episode = 11;
			for (int num4 = 1; num4 <= episode; num4++)
			{
				Unlocked[num4].SetActive(true);
				Locked[num4].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Sky Rivers";
		}
		if (openedLevel >= 100 && openedLevel <= 108)
		{
			episode = 12;
			for (int num5 = 1; num5 <= episode; num5++)
			{
				Unlocked[num5].SetActive(true);
				Locked[num5].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Ayscookies Candies";
		}
		if (openedLevel >= 108 && openedLevel <= 117)
		{
			episode = 13;
			for (int num6 = 1; num6 <= episode; num6++)
			{
				Unlocked[num6].SetActive(true);
				Locked[num6].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Cake Castle";
		}
		if (openedLevel >= 117 && openedLevel <= 129)
		{
			episode = 14;
			for (int num7 = 1; num7 <= episode; num7++)
			{
				Unlocked[num7].SetActive(true);
				Locked[num7].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Ant Houses";
		}
		if (openedLevel >= 129 && openedLevel <= 142)
		{
			episode = 15;
			for (int num8 = 1; num8 <= episode; num8++)
			{
				Unlocked[num8].SetActive(true);
				Locked[num8].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Sleep Time";
		}
		if (openedLevel >= 142 && openedLevel <= 152)
		{
			episode = 16;
			for (int num9 = 1; num9 <= episode; num9++)
			{
				Unlocked[num9].SetActive(true);
				Locked[num9].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Milled Plain";
		}
		if (openedLevel >= 152 && openedLevel <= 165)
		{
			episode = 17;
			for (int num10 = 1; num10 <= episode; num10++)
			{
				Unlocked[num10].SetActive(true);
				Locked[num10].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Kaan's House";
		}
		if (openedLevel >= 165 && openedLevel <= 179)
		{
			episode = 18;
			for (int num11 = 1; num11 <= episode; num11++)
			{
				Unlocked[num11].SetActive(true);
				Locked[num11].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Maysa's House";
		}
		if (openedLevel >= 179 && openedLevel <= 192)
		{
			episode = 19;
			for (int num12 = 1; num12 <= episode; num12++)
			{
				Unlocked[num12].SetActive(true);
				Locked[num12].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Colorful Lake";
		}
		if (openedLevel >= 192 && openedLevel <= 202)
		{
			episode = 20;
			for (int num13 = 1; num13 <= episode; num13++)
			{
				Unlocked[num13].SetActive(true);
				Locked[num13].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Fly Trap";
		}
		if (openedLevel >= 202 && openedLevel <= 213)
		{
			episode = 21;
			for (int num14 = 1; num14 <= episode; num14++)
			{
				Unlocked[num14].SetActive(true);
				Locked[num14].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Toy Castle";
		}
		if (openedLevel >= 213 && openedLevel <= 225)
		{
			episode = 22;
			for (int num15 = 1; num15 <= episode; num15++)
			{
				Unlocked[num15].SetActive(true);
				Locked[num15].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Princess House";
		}
		if (openedLevel >= 225 && openedLevel <= 238)
		{
			episode = 23;
			for (int num16 = 1; num16 <= episode; num16++)
			{
				Unlocked[num16].SetActive(true);
				Locked[num16].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Play Time";
		}
		if (openedLevel >= 238 && openedLevel <= 252)
		{
			episode = 24;
			for (int num17 = 1; num17 <= episode; num17++)
			{
				Unlocked[num17].SetActive(true);
				Locked[num17].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Stony Road";
		}
		if (openedLevel >= 252 && openedLevel <= 263)
		{
			episode = 25;
			for (int num18 = 1; num18 <= episode; num18++)
			{
				Unlocked[num18].SetActive(true);
				Locked[num18].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Animal Farm";
		}
		if (openedLevel >= 263 && openedLevel <= 273)
		{
			episode = 26;
			for (int num19 = 1; num19 <= episode; num19++)
			{
				Unlocked[num19].SetActive(true);
				Locked[num19].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Lost Ship";
		}
		if (openedLevel >= 273 && openedLevel <= 284)
		{
			episode = 27;
			for (int num20 = 1; num20 <= episode; num20++)
			{
				Unlocked[num20].SetActive(true);
				Locked[num20].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Crab Holiday";
		}
		if (openedLevel >= 284 && openedLevel <= 295)
		{
			episode = 28;
			for (int num21 = 1; num21 <= episode; num21++)
			{
				Unlocked[num21].SetActive(true);
				Locked[num21].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Pink Mill";
		}
		if (openedLevel >= 295 && openedLevel <= 305)
		{
			episode = 29;
			for (int num22 = 1; num22 <= episode; num22++)
			{
				Unlocked[num22].SetActive(true);
				Locked[num22].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Rest Homes";
		}
		if (openedLevel >= 305 && openedLevel <= 315)
		{
			episode = 30;
			for (int num23 = 1; num23 <= episode; num23++)
			{
				Unlocked[num23].SetActive(true);
				Locked[num23].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Robin's Home";
		}
		if (openedLevel >= 315 && openedLevel <= 326)
		{
			episode = 31;
			for (int num24 = 1; num24 <= episode; num24++)
			{
				Unlocked[num24].SetActive(true);
				Locked[num24].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Robin's Neighborhood";
		}
		if (openedLevel >= 326 && openedLevel <= 340)
		{
			episode = 32;
			for (int num25 = 1; num25 <= episode; num25++)
			{
				Unlocked[num25].SetActive(true);
				Locked[num25].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Sweet Road";
		}
		if (openedLevel >= 340 && openedLevel <= 350)
		{
			episode = 33;
			for (int num26 = 1; num26 <= episode; num26++)
			{
				Unlocked[num26].SetActive(true);
				Locked[num26].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Sweet Road 2";
		}
		if (openedLevel >= 350 && openedLevel <= 365)
		{
			episode = 34;
			for (int num27 = 1; num27 <= episode; num27++)
			{
				Unlocked[num27].SetActive(true);
				Locked[num27].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Robin's Beach";
		}
		if (openedLevel >= 365 && openedLevel <= 379)
		{
			episode = 35;
			for (int num28 = 1; num28 <= episode; num28++)
			{
				Unlocked[num28].SetActive(true);
				Locked[num28].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Robin's Beach 2";
		}
		if (openedLevel >= 379 && openedLevel <= 392)
		{
			episode = 36;
			for (int num29 = 1; num29 <= episode; num29++)
			{
				Unlocked[num29].SetActive(true);
				Locked[num29].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Cake Grass";
		}
		if (openedLevel >= 392 && openedLevel <= 405)
		{
			episode = 37;
			for (int num30 = 1; num30 <= episode; num30++)
			{
				Unlocked[num30].SetActive(true);
				Locked[num30].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Ice Cream Volcanoes";
		}
		if (openedLevel >= 405 && openedLevel <= 421)
		{
			episode = 38;
			for (int num31 = 1; num31 <= episode; num31++)
			{
				Unlocked[num31].SetActive(true);
				Locked[num31].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Popcorn Time";
		}
		if (openedLevel >= 421 && openedLevel <= 445)
		{
			episode = 39;
			for (int num32 = 1; num32 <= episode; num32++)
			{
				Unlocked[num32].SetActive(true);
				Locked[num32].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Lost Sea";
		}
		if (openedLevel >= 442 && openedLevel <= 458)
		{
			episode = 40;
			for (int num33 = 1; num33 <= episode; num33++)
			{
				Unlocked[num33].SetActive(true);
				Locked[num33].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Bee Village";
		}
		if (openedLevel >= 458 && openedLevel <= 479)
		{
			episode = 41;
			for (int num34 = 1; num34 <= episode; num34++)
			{
				Unlocked[num34].SetActive(true);
				Locked[num34].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Apaches Village";
		}
		if (openedLevel >= 479 && openedLevel <= 503)
		{
			episode = 42;
			for (int num35 = 1; num35 <= episode; num35++)
			{
				Unlocked[num35].SetActive(true);
				Locked[num35].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Deserted Cottage";
		}
		if (openedLevel >= 503 && openedLevel <= 526)
		{
			episode = 43;
			for (int num36 = 1; num36 <= episode; num36++)
			{
				Unlocked[num36].SetActive(true);
				Locked[num36].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Halloween Time";
		}
		if (openedLevel >= 526 && openedLevel <= 549)
		{
			episode = 44;
			for (int num37 = 1; num37 <= episode; num37++)
			{
				Unlocked[num37].SetActive(true);
				Locked[num37].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Lonely Hut";
		}
		if (openedLevel >= 549 && openedLevel <= 574)
		{
			episode = 45;
			for (int num38 = 1; num38 <= episode; num38++)
			{
				Unlocked[num38].SetActive(true);
				Locked[num38].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Lost Toy";
		}
		if (openedLevel >= 574 && openedLevel <= 600)
		{
			episode = 46;
			for (int num39 = 1; num39 <= episode; num39++)
			{
				Unlocked[num39].SetActive(true);
				Locked[num39].SetActive(false);
			}
			EpisodeLabelText.text = "Episode:" + episode + " Purple Candy Way";
		}
	}

	public void close()
	{
		base.gameObject.SetActive(false);
		AudioManager.instance.ButtonClickAudio();
	}
}
