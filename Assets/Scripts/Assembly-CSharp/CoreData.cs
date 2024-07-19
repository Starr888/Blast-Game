using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MiniJSON;
using UnityEngine;

public class CoreData : MonoBehaviour
{
	public static CoreData instance;

	public static string STORE_PURCHASED_REMOVE_ADS = "Purchased Remove Ads";

	[Header("Player & Levels Data")]
	public int playerCoin;

	public int openedLevel;

	public int playerStars;

	public int playerPuan;

	public int giftAmount;

	public int toplamScore;

	[Header("Breaker Values")]
	public int singleBreaker;

	public int rowBreaker;

	public int columnBreaker;

	public int rainbowBreaker;

	public int ovenBreaker;

	[Header("Power Ups Value")]
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
		if (LoadGameData() == null)
		{
			SaveGameData(PrepareGameData());
		}
	}

	private string LoadGameData()
	{
		if (File.Exists(Application.persistentDataPath + "/" + Configuration.game_data))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream fileStream = File.Open(Application.persistentDataPath + "/" + Configuration.game_data, FileMode.Open);
			string text = (string)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
			Dictionary<string, object> dictionary = Json.Deserialize(text) as Dictionary<string, object>;
			playerCoin = int.Parse(dictionary[Configuration.player_coin].ToString());
			openedLevel = int.Parse(dictionary[Configuration.opened_level].ToString());
			openedLevel = ((openedLevel <= 0) ? 1 : openedLevel);
			singleBreaker = int.Parse(dictionary[Configuration.single_breaker].ToString());
			rowBreaker = int.Parse(dictionary[Configuration.row_breaker].ToString());
			columnBreaker = int.Parse(dictionary[Configuration.column_breaker].ToString());
			rainbowBreaker = int.Parse(dictionary[Configuration.rainbow_breaker].ToString());
			ovenBreaker = int.Parse(dictionary[Configuration.oven_breaker].ToString());
			beginFiveMoves = int.Parse(dictionary[Configuration.begin_five_moves].ToString());
			beginRainbow = int.Parse(dictionary[Configuration.begin_rainbow].ToString());
			beginBombBreaker = int.Parse(dictionary[Configuration.begin_bomb_breaker].ToString());
			List<object> list = (List<object>)dictionary[Configuration.level_statistics];
			foreach (object item2 in list)
			{
				Dictionary<string, object> item = (Dictionary<string, object>)item2;
				levelStatistics.Add(item);
			}
			playerStars = int.Parse(dictionary[Configuration.player_stars].ToString());
			playerPuan = int.Parse(dictionary[Configuration.player_puan].ToString());
			giftAmount = int.Parse(dictionary[Configuration.gift_amount].ToString());
			toplamScore = int.Parse(dictionary[Configuration.toplam_score].ToString());
			return text;
		}
		return null;
	}

	private void SaveGameData(string jsonString)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Create(Application.persistentDataPath + "/" + Configuration.game_data);
		binaryFormatter.Serialize(fileStream, jsonString);
		fileStream.Close();
	}

	private string PrepareGameData()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		if (openedLevel == 0)
		{
			openedLevel = 1;
		}
		dictionary.Add(Configuration.player_coin, playerCoin);
		dictionary.Add(Configuration.opened_level, openedLevel);
		dictionary.Add(Configuration.single_breaker, singleBreaker);
		dictionary.Add(Configuration.row_breaker, rowBreaker);
		dictionary.Add(Configuration.column_breaker, columnBreaker);
		dictionary.Add(Configuration.rainbow_breaker, rainbowBreaker);
		dictionary.Add(Configuration.oven_breaker, ovenBreaker);
		dictionary.Add(Configuration.begin_five_moves, beginFiveMoves);
		dictionary.Add(Configuration.begin_rainbow, beginRainbow);
		dictionary.Add(Configuration.begin_bomb_breaker, beginBombBreaker);
		dictionary.Add(Configuration.player_stars, playerStars);
		dictionary.Add(Configuration.player_puan, playerPuan);
		dictionary.Add(Configuration.level_statistics, levelStatistics);
		dictionary.Add(Configuration.gift_amount, giftAmount);
		dictionary.Add(Configuration.toplam_score, toplamScore);
		return Json.Serialize(dictionary);
	}

	public int GetOpendedLevel()
	{
		return openedLevel;
	}

	public void SaveOpendedLevel(int level)
	{
		openedLevel = level;
		SaveGameData(PrepareGameData());
	}

	public int GetLevelScore(int level)
	{
		foreach (Dictionary<string, object> levelStatistic in levelStatistics)
		{
			if (int.Parse(levelStatistic[Configuration.level_number].ToString()) == level)
			{
				return int.Parse(levelStatistic[Configuration.level_score].ToString());
			}
		}
		return 0;
	}

	public int GetLevelStar(int level)
	{
		foreach (Dictionary<string, object> levelStatistic in levelStatistics)
		{
			if (int.Parse(levelStatistic[Configuration.level_number].ToString()) == level)
			{
				return int.Parse(levelStatistic[Configuration.level_star].ToString());
			}
		}
		return 0;
	}

	public void SaveLevelStatistics(int level, int score, int star)
	{
		foreach (Dictionary<string, object> levelStatistic in levelStatistics)
		{
			if (int.Parse(levelStatistic[Configuration.level_number].ToString()) == level)
			{
				if (int.Parse(levelStatistic[Configuration.level_score].ToString()) < score)
				{
					levelStatistic[Configuration.level_score] = score;
				}
				if (int.Parse(levelStatistic[Configuration.level_star].ToString()) < star)
				{
					levelStatistic[Configuration.level_star] = star;
				}
				SaveGameData(PrepareGameData());
				return;
			}
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add(Configuration.level_number, level);
		dictionary.Add(Configuration.level_score, score);
		dictionary.Add(Configuration.level_star, star);
		levelStatistics.Add(dictionary);
		SaveGameData(PrepareGameData());
	}

	public int GetPlayerCoin()
	{
		return playerCoin;
	}

	public int GetPlayerStars()
	{
		return playerStars;
	}

	public int GetPlayerPuan()
	{
		return playerPuan;
	}

	public int GetGiftAmount()
	{
		return giftAmount;
	}

	public int GetToplamScore()
	{
		return toplamScore;
	}

	public void SaveGiftAmount(int gamount)
	{
		giftAmount = gamount;
		SaveGameData(PrepareGameData());
	}

	public void SavePlayerCoin(int coin)
	{
		playerCoin = coin;
		SaveGameData(PrepareGameData());
	}

	public void SavePlayerStars(int stars)
	{
		playerStars = stars;
		SaveGameData(PrepareGameData());
	}

	public void SavePlayerPuan(int puan)
	{
		playerPuan = puan;
		SaveGameData(PrepareGameData());
	}

	public void SaveToplamScore(int topscore)
	{
		toplamScore = topscore;
		SaveGameData(PrepareGameData());
	}

	public int GetBeginFiveMoves()
	{
		return beginFiveMoves;
	}

	public void SaveBeginFiveMoves(int number)
	{
		beginFiveMoves = number;
		SaveGameData(PrepareGameData());
	}

	public int GetBeginRainbow()
	{
		return beginRainbow;
	}

	public void SaveBeginRainbow(int number)
	{
		beginRainbow = number;
		SaveGameData(PrepareGameData());
	}

	public int GetBeginBombBreaker()
	{
		return beginBombBreaker;
	}

	public void SaveBeginBombBreaker(int number)
	{
		beginBombBreaker = number;
		SaveGameData(PrepareGameData());
	}

	public int GetSingleBreaker()
	{
		return singleBreaker;
	}

	public void SaveSingleBreaker(int number)
	{
		singleBreaker = number;
		SaveGameData(PrepareGameData());
	}

	public int GetRowBreaker()
	{
		return rowBreaker;
	}

	public void SaveRowBreaker(int number)
	{
		rowBreaker = number;
		SaveGameData(PrepareGameData());
	}

	public int GetColumnBreaker()
	{
		return columnBreaker;
	}

	public void SaveColumnBreaker(int number)
	{
		columnBreaker = number;
		SaveGameData(PrepareGameData());
	}

	public int GetRainbowBreaker()
	{
		return rainbowBreaker;
	}

	public void SaveRainbowBreaker(int number)
	{
		rainbowBreaker = number;
		SaveGameData(PrepareGameData());
	}

	public int GetOvenBreaker()
	{
		return ovenBreaker;
	}

	public void SaveOvenBreaker(int number)
	{
		ovenBreaker = number;
		SaveGameData(PrepareGameData());
	}
}
