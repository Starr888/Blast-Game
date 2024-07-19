using UnityEngine;

public class achievementtable : MonoBehaviour
{
	public GameObject[] achievement;

	private void Start()
	{
		Invoke("updateachiment", 1.5f);
	}

	public void updateachiment()
	{
		int openedLevel = CoreData.instance.openedLevel;
		int toplamScore = CoreData.instance.GetToplamScore();
		int num = 0;
		for (int i = 1; i <= CoreData.instance.GetOpendedLevel(); i++)
		{
			num += CoreData.instance.GetLevelStar(i);
		}
		if (openedLevel >= 10)
		{
			achievement[1].SetActive(true);
		}
		if (openedLevel >= 30)
		{
			achievement[2].SetActive(true);
		}
		if (openedLevel >= 55)
		{
			achievement[3].SetActive(true);
		}
		if (openedLevel >= 70)
		{
			achievement[4].SetActive(true);
		}
		if (openedLevel >= 85)
		{
			achievement[5].SetActive(true);
		}
		if (openedLevel >= 100)
		{
			achievement[6].SetActive(true);
		}
		if (openedLevel >= 130)
		{
			achievement[7].SetActive(true);
		}
		if (openedLevel >= 155)
		{
			achievement[8].SetActive(true);
		}
		if (openedLevel >= 185)
		{
			achievement[9].SetActive(true);
		}
		if (openedLevel >= 200)
		{
			achievement[10].SetActive(true);
		}
		if (openedLevel >= 222)
		{
			achievement[11].SetActive(true);
		}
		if (openedLevel >= 250)
		{
			achievement[12].SetActive(true);
		}
		if (openedLevel >= 270)
		{
			achievement[13].SetActive(true);
		}
		if (num > 74)
		{
			achievement[14].SetActive(true);
		}
		if (num > 299)
		{
			achievement[15].SetActive(true);
		}
		if (num > 599)
		{
			achievement[16].SetActive(true);
		}
		if (num > 899)
		{
			achievement[17].SetActive(true);
		}
		if (toplamScore > 199999)
		{
			achievement[18].SetActive(true);
		}
		if (toplamScore > 399999)
		{
			achievement[19].SetActive(true);
		}
		if (toplamScore > 599999)
		{
			achievement[20].SetActive(true);
		}
	}
}
