using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UITop : MonoBehaviour
{
	public Text levelText;

	public Text scoreText;

	public Text movesText;

	public Image progess;

	public Image doll;

	public Image progressStar1;

	public Image progressStar2;

	public Image progressStar3;

	private float progress;

	private float progress1 = 0.33f;

	private float progress2 = 0.66f;

	private float progress3 = 1f;

	private int star1;

	private int star2;

	private int star3;

	private bool greeting1;

	private bool greeting2;

	private bool greeting3;

	private float duration = 0.5f;

	private int start;

	private int moves;

	private void Start()
	{
		levelText.text = "Level " + StageLoader.instance.Stage;
		scoreText.text = "0";
		moves = StageLoader.instance.moves;
		if (Configuration.instance.beginFiveMoves)
		{
			moves += Configuration.instance.plusMoves;
		}
		movesText.text = moves.ToString();
		star1 = StageLoader.instance.score_Star_1;
		star2 = StageLoader.instance.score_Star_2;
		star3 = StageLoader.instance.score_Star_3;
		progess.fillAmount = 0f;
	}

	public void UpdateScoreAmount(int score)
	{
		StartCoroutine("StartUpdateScore", score);
	}

	private IEnumerator StartUpdateScore(int target)
	{
		for (float timer = 0f; timer < duration; timer += Time.deltaTime)
		{
			scoreText.text = ((int)Mathf.Lerp(start, target, timer / duration)).ToString();
			yield return null;
		}
		start = target;
		scoreText.text = target.ToString();
	}

	public void DecreaseMoves(bool effect = false)
	{
		if (effect)
		{
			GameObject nextObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RingExplosion()) as GameObject);
			nextObject.transform.position = movesText.gameObject.transform.position;
		}
		if (moves > 0)
		{
			moves--;
			movesText.text = moves.ToString();
		}
	}

	public void Set5Moves()
	{
		GameObject nextObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RingExplosion()) as GameObject);
		nextObject.transform.position = movesText.gameObject.transform.position;
		moves = 10;
		movesText.text = moves.ToString();
	}

	public void Set2Moves()
	{
		GameObject nextObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RingExplosion()) as GameObject);
		nextObject.transform.position = movesText.gameObject.transform.position;
		moves = 3;
		movesText.text = moves.ToString();
	}

	public void UpdateProgressBar(int score)
	{
		if (score < star1)
		{
			progress = (float)score / (float)star1 * progress1;
		}
		else if (star1 <= score && score < star2)
		{
			progress = progress1 + ((float)score - (float)star1) / ((float)star2 - (float)star1) * (progress2 - progress1);
			if (!greeting1)
			{
				greeting1 = true;
				StartCoroutine(Star2Gold(progressStar1));
			}
		}
		else if (star2 <= score && score < star3)
		{
			progress = progress2 + ((float)score - (float)star2) / ((float)star3 - (float)star2) * (progress3 - progress2);
			if (!greeting2)
			{
				greeting2 = true;
				StartCoroutine(Star2Gold(progressStar2));
			}
		}
		else if (score >= star3)
		{
			progress = progress3;
			if (!greeting3)
			{
				greeting3 = true;
				StartCoroutine(Star2Gold(progressStar3));
			}
		}
		StartCoroutine("StartUpdateProgress", progress);
	}

	private IEnumerator StartUpdateProgress(float progress)
	{
		float start = progess.fillAmount;
		for (float timer = 0f; timer < duration; timer += Time.deltaTime)
		{
			progess.fillAmount = Mathf.Lerp(start, progress, timer / duration);
			yield return null;
		}
	}

	private IEnumerator Star2Gold(Image progressStar)
	{
		yield return new WaitForSeconds(duration);
		progressStar.sprite = Resources.Load<GameObject>(Configuration.ProgressGoldStar()).GetComponent<SpriteRenderer>().sprite;
	}
}
