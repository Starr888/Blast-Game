using UnityEngine;

public class bgController : MonoBehaviour
{
	public GoogleAnalyticsV4 googleAnalytics;

	private SpriteRenderer current_Bgsprite;

	public Sprite[] levels_Bgsprite;

	private void Start()
	{
		googleAnalytics.LogScreen("Play " + StageLoader.instance.Stage);
		current_Bgsprite = GetComponent<SpriteRenderer>();
		changeBGSprite();
	}

	private void changeBGSprite()
	{
		if (StageLoader.instance.Stage <= 5)
		{
			current_Bgsprite.sprite = levels_Bgsprite[0];
		}
		if (StageLoader.instance.Stage >= 5 && StageLoader.instance.Stage <= 15)
		{
			current_Bgsprite.sprite = levels_Bgsprite[1];
		}
		if (StageLoader.instance.Stage >= 15 && StageLoader.instance.Stage <= 22)
		{
			current_Bgsprite.sprite = levels_Bgsprite[2];
		}
		if (StageLoader.instance.Stage >= 22 && StageLoader.instance.Stage <= 31)
		{
			current_Bgsprite.sprite = levels_Bgsprite[3];
		}
		if (StageLoader.instance.Stage >= 31 && StageLoader.instance.Stage <= 41)
		{
			current_Bgsprite.sprite = levels_Bgsprite[4];
		}
		if (StageLoader.instance.Stage >= 41 && StageLoader.instance.Stage <= 48)
		{
			current_Bgsprite.sprite = levels_Bgsprite[5];
		}
		if (StageLoader.instance.Stage >= 48 && StageLoader.instance.Stage <= 59)
		{
			current_Bgsprite.sprite = levels_Bgsprite[6];
		}
		if (StageLoader.instance.Stage >= 59 && StageLoader.instance.Stage <= 72)
		{
			current_Bgsprite.sprite = levels_Bgsprite[7];
		}
		if (StageLoader.instance.Stage >= 72 && StageLoader.instance.Stage <= 82)
		{
			current_Bgsprite.sprite = levels_Bgsprite[8];
		}
		if (StageLoader.instance.Stage >= 82 && StageLoader.instance.Stage <= 92)
		{
			current_Bgsprite.sprite = levels_Bgsprite[9];
		}
		if (StageLoader.instance.Stage >= 92 && StageLoader.instance.Stage <= 100)
		{
			current_Bgsprite.sprite = levels_Bgsprite[10];
		}
		if (StageLoader.instance.Stage >= 100 && StageLoader.instance.Stage <= 108)
		{
			current_Bgsprite.sprite = levels_Bgsprite[11];
		}
		if (StageLoader.instance.Stage >= 108 && StageLoader.instance.Stage <= 117)
		{
			current_Bgsprite.sprite = levels_Bgsprite[12];
		}
		if (StageLoader.instance.Stage >= 117 && StageLoader.instance.Stage <= 129)
		{
			current_Bgsprite.sprite = levels_Bgsprite[13];
		}
		if (StageLoader.instance.Stage >= 129 && StageLoader.instance.Stage <= 142)
		{
			current_Bgsprite.sprite = levels_Bgsprite[14];
		}
		if (StageLoader.instance.Stage >= 142 && StageLoader.instance.Stage <= 152)
		{
			current_Bgsprite.sprite = levels_Bgsprite[15];
		}
		if (StageLoader.instance.Stage >= 152 && StageLoader.instance.Stage <= 165)
		{
			current_Bgsprite.sprite = levels_Bgsprite[16];
		}
		if (StageLoader.instance.Stage >= 165 && StageLoader.instance.Stage <= 179)
		{
			current_Bgsprite.sprite = levels_Bgsprite[17];
		}
		if (StageLoader.instance.Stage >= 179 && StageLoader.instance.Stage <= 192)
		{
			current_Bgsprite.sprite = levels_Bgsprite[18];
		}
		if (StageLoader.instance.Stage >= 192 && StageLoader.instance.Stage <= 202)
		{
			current_Bgsprite.sprite = levels_Bgsprite[19];
		}
		if (StageLoader.instance.Stage >= 202 && StageLoader.instance.Stage <= 213)
		{
			current_Bgsprite.sprite = levels_Bgsprite[20];
		}
		if (StageLoader.instance.Stage >= 213 && StageLoader.instance.Stage <= 225)
		{
			current_Bgsprite.sprite = levels_Bgsprite[21];
		}
		if (StageLoader.instance.Stage >= 225 && StageLoader.instance.Stage <= 238)
		{
			current_Bgsprite.sprite = levels_Bgsprite[22];
		}
		if (StageLoader.instance.Stage >= 238 && StageLoader.instance.Stage <= 252)
		{
			current_Bgsprite.sprite = levels_Bgsprite[23];
		}
		if (StageLoader.instance.Stage >= 252 && StageLoader.instance.Stage <= 263)
		{
			current_Bgsprite.sprite = levels_Bgsprite[24];
		}
		if (StageLoader.instance.Stage >= 263 && StageLoader.instance.Stage <= 273)
		{
			current_Bgsprite.sprite = levels_Bgsprite[25];
		}
		if (StageLoader.instance.Stage >= 273 && StageLoader.instance.Stage <= 284)
		{
			current_Bgsprite.sprite = levels_Bgsprite[26];
		}
		if (StageLoader.instance.Stage >= 284 && StageLoader.instance.Stage <= 295)
		{
			current_Bgsprite.sprite = levels_Bgsprite[27];
		}
		if (StageLoader.instance.Stage >= 295 && StageLoader.instance.Stage <= 305)
		{
			current_Bgsprite.sprite = levels_Bgsprite[28];
		}
		if (StageLoader.instance.Stage >= 305 && StageLoader.instance.Stage <= 315)
		{
			current_Bgsprite.sprite = levels_Bgsprite[29];
		}
		if (StageLoader.instance.Stage >= 315 && StageLoader.instance.Stage <= 326)
		{
			current_Bgsprite.sprite = levels_Bgsprite[30];
		}
		if (StageLoader.instance.Stage >= 326 && StageLoader.instance.Stage <= 340)
		{
			current_Bgsprite.sprite = levels_Bgsprite[31];
		}
		if (StageLoader.instance.Stage >= 340 && StageLoader.instance.Stage <= 350)
		{
			current_Bgsprite.sprite = levels_Bgsprite[32];
		}
		if (StageLoader.instance.Stage >= 350 && StageLoader.instance.Stage <= 365)
		{
			current_Bgsprite.sprite = levels_Bgsprite[33];
		}
		if (StageLoader.instance.Stage >= 365 && StageLoader.instance.Stage <= 379)
		{
			current_Bgsprite.sprite = levels_Bgsprite[34];
		}
		if (StageLoader.instance.Stage >= 379 && StageLoader.instance.Stage <= 392)
		{
			current_Bgsprite.sprite = levels_Bgsprite[35];
		}
		if (StageLoader.instance.Stage >= 392 && StageLoader.instance.Stage <= 405)
		{
			current_Bgsprite.sprite = levels_Bgsprite[36];
		}
		if (StageLoader.instance.Stage >= 405 && StageLoader.instance.Stage <= 421)
		{
			current_Bgsprite.sprite = levels_Bgsprite[37];
		}
		if (StageLoader.instance.Stage >= 421 && StageLoader.instance.Stage <= 442)
		{
			current_Bgsprite.sprite = levels_Bgsprite[38];
		}
		if (StageLoader.instance.Stage >= 442 && StageLoader.instance.Stage <= 458)
		{
			current_Bgsprite.sprite = levels_Bgsprite[39];
		}
		if (StageLoader.instance.Stage >= 458 && StageLoader.instance.Stage <= 475)
		{
			current_Bgsprite.sprite = levels_Bgsprite[40];
		}
		if (StageLoader.instance.Stage >= 479 && StageLoader.instance.Stage <= 503)
		{
			current_Bgsprite.sprite = levels_Bgsprite[41];
		}
		if (StageLoader.instance.Stage >= 503 && StageLoader.instance.Stage <= 526)
		{
			current_Bgsprite.sprite = levels_Bgsprite[42];
		}
		if (StageLoader.instance.Stage >= 526 && StageLoader.instance.Stage <= 549)
		{
			current_Bgsprite.sprite = levels_Bgsprite[43];
		}
		if (StageLoader.instance.Stage >= 549 && StageLoader.instance.Stage <= 574)
		{
			current_Bgsprite.sprite = levels_Bgsprite[44];
		}
		if (StageLoader.instance.Stage >= 574 && StageLoader.instance.Stage <= 600)
		{
			current_Bgsprite.sprite = levels_Bgsprite[45];
		}
	}
}
