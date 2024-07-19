using System.Collections.Generic;
using UnityEngine;

public class GameDataWWW : MonoBehaviour
{
	public static GameDataWWW instance;

	[Header("Data")]
	public int playerCoin;

	public int openedLevel;

	[Header("")]
	public int singleBreaker;

	public int rowBreaker;

	public int columnBreaker;

	public int rainbowBreaker;

	public int ovenBreaker;

	[Header("")]
	public int beginFiveMoves;

	public int beginRainbow;

	public int beginBombBreaker;

	public List<Dictionary<string, object>> levelStatistics = new List<Dictionary<string, object>>();

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Object.Destroy(base.gameObject);
		}
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		if (LoadGameDataWWW() == null)
		{
			SaveGameDataWWW(PrepareGameDataWWW());
		}
	}

	private string LoadGameDataWWW()
	{
		return string.Empty;
	}

	private void SaveGameDataWWW(string jsonString)
	{
	}

	private string PrepareGameDataWWW()
	{
		return string.Empty;
	}
}
