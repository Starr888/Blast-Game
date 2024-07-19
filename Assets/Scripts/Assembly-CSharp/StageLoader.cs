using System;
using System.Collections.Generic;
using System.Linq;
using MiniJSON;
using UnityEngine;

public class StageLoader : MonoBehaviour
{
	public static StageLoader instance;

	[Header("Basic")]
	public int Stage;

	public int column;

	public int row;

	public int moves;

	[Header("Layers")]
	public List<TILE_TYPE> tileLayerData;

	public List<ITEM_TYPE> itemLayerData;

	public List<WAFFLE_TYPE> breakableLayerData;

	public List<LOCK_TYPE> lockLayerData;

	private Dictionary<string, string> names = new Dictionary<string, string>();

	[Header("Items")]
	public List<ITEM_TYPE> usingItems;

	public List<int> usingColors;

	public List<int> itemWeight;

	[Header("Rocket Toys")]
	public List<ITEM_TYPE> rocketToys;

	public List<int> rockettoysWeight;

	public List<int> rocketToysMarkers;

	public int maxRockettoys;

	[Header("Target")]
	public TARGET_TYPE target1Type;

	public TARGET_TYPE target2Type;

	public TARGET_TYPE target3Type;

	public TARGET_TYPE target4Type;

	[Header("Target Values")]
	public int target1Amount;

	public int target2Amount;

	public int target3Amount;

	public int target4Amount;

	[Header("Target Items")]
	public int target1Color;

	public int target2Color;

	public int target3Color;

	public int target4Color;

	[Header("Stars")]
	public int score_Star_1;

	public int score_Star_2;

	public int score_Star_3;

	[Header("Target Text")]
	public string targetlbl;

	[Header("Collectible")]
	public List<int> collectibleCollectColumnMarkers;

	public List<int> collectibleCollectNodeMarkers;

	public List<int> collectibleGenerateMarkers;

	public int collectibleMaxOnBoard;

	public int marshmallowMoreThanTarget;

	[Header("Doll")]
	public int doll;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void LoadLevel(bool debug = false)
	{
		if (debug)
		{
			Stage = 999;
		}
		TextAsset textAsset = Resources.Load("Levels/" + Stage, typeof(TextAsset)) as TextAsset;
		if (textAsset == null)
		{
			MonoBehaviour.print("Can not load level data");
			return;
		}
		Clear();
		Dictionary<string, object> dictionary = Json.Deserialize(textAsset.text) as Dictionary<string, object>;
		column = int.Parse(dictionary["width"].ToString());
		row = int.Parse(dictionary["height"].ToString());
		List<object> list = (List<object>)dictionary["tilesets"];
		Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[0];
		Dictionary<string, object> dictionary3 = (Dictionary<string, object>)dictionary2["tiles"];
		foreach (KeyValuePair<string, object> item11 in dictionary3)
		{
			Dictionary<string, object> dictionary4 = (Dictionary<string, object>)item11.Value;
			string text = (string)dictionary4["image"];
			string[] array = text.Split(new string[1] { "/" }, StringSplitOptions.None);
			string text2 = array[array.Length - 1];
			string[] array2 = text2.Split(new string[1] { "." }, StringSplitOptions.None);
			string value = array2[0];
			if (!names.ContainsKey(item11.Key))
			{
				names.Add(item11.Key, value);
			}
		}
		List<object> list2 = (List<object>)dictionary["layers"];
		foreach (object item12 in list2)
		{
			Dictionary<string, object> dictionary5 = (Dictionary<string, object>)item12;
			List<object> list3 = (List<object>)dictionary5["data"];
			switch ((string)dictionary5["name"])
			{
			case "Tiles":
				foreach (object item13 in list3)
				{
					TILE_TYPE item4 = DataToTileType(int.Parse(item13.ToString()));
					tileLayerData.Add(item4);
				}
				break;
			case "items":
				foreach (object item14 in list3)
				{
					ITEM_TYPE item3 = DataToItemType(int.Parse(item14.ToString()));
					itemLayerData.Add(item3);
				}
				break;
			case "breakable":
				foreach (object item15 in list3)
				{
					WAFFLE_TYPE item2 = DataToWaffleType(int.Parse(item15.ToString()));
					breakableLayerData.Add(item2);
				}
				break;
			case "Lock":
				foreach (object item16 in list3)
				{
					LOCK_TYPE item = DataToLockType(int.Parse(item16.ToString()));
					lockLayerData.Add(item);
				}
				break;
			}
		}
		Dictionary<string, object> dictionary6 = (Dictionary<string, object>)dictionary["properties"];
		List<string> list4 = ((string)dictionary6["items"]).Split(',').ToList();
		List<string> list5 = ((string)dictionary6["items_weight"]).Split(',').ToList();
		int num = 0;
		foreach (string item17 in list4)
		{
			int num2 = int.Parse(item17.ToString());
			int item5 = int.Parse(list5[num].ToString());
			num++;
			if (num2 == 1)
			{
				switch (num)
				{
				case 1:
					usingItems.Add(ITEM_TYPE.BlueBox);
					usingColors.Add(1);
					break;
				case 2:
					usingItems.Add(ITEM_TYPE.GreenBox);
					usingColors.Add(2);
					break;
				case 3:
					usingItems.Add(ITEM_TYPE.ORANGEBOX);
					usingColors.Add(3);
					break;
				case 4:
					usingItems.Add(ITEM_TYPE.PURPLEBOX);
					usingColors.Add(4);
					break;
				case 5:
					usingItems.Add(ITEM_TYPE.REDBOX);
					usingColors.Add(5);
					break;
				case 6:
					usingItems.Add(ITEM_TYPE.YELLOWBOX);
					usingColors.Add(6);
					break;
				}
				itemWeight.Add(item5);
			}
		}
		if (dictionary6.ContainsKey("rocket") && (string)dictionary6["rocket"] != string.Empty && (string)dictionary6["rocket_weight"] != string.Empty)
		{
			List<string> list6 = ((string)dictionary6["rocket"]).Split(',').ToList();
			List<string> list7 = ((string)dictionary6["rocket_weight"]).Split(',').ToList();
			num = 0;
			foreach (string item18 in list6)
			{
				int num3 = int.Parse(item18.ToString());
				int item6 = int.Parse(list7[num].ToString());
				num++;
				if (num3 == 1)
				{
					switch (num)
					{
					case 1:
						rocketToys.Add(ITEM_TYPE.ROCKET_1);
						break;
					case 2:
						rocketToys.Add(ITEM_TYPE.ROCKET_2);
						break;
					case 3:
						rocketToys.Add(ITEM_TYPE.ROCKET_3);
						break;
					case 4:
						rocketToys.Add(ITEM_TYPE.ROCKET_4);
						break;
					case 5:
						rocketToys.Add(ITEM_TYPE.ROCKET_5);
						break;
					case 6:
						rocketToys.Add(ITEM_TYPE.ROCKET_6);
						break;
					}
					rockettoysWeight.Add(item6);
				}
			}
		}
		if (dictionary6.ContainsKey("rocket_max") && dictionary6["rocket_max"].ToString() != string.Empty)
		{
			maxRockettoys = int.Parse(dictionary6["rocket_max"].ToString());
		}
		if (dictionary6.ContainsKey("rocket_maker"))
		{
			List<string> list8 = ((string)dictionary6["rocket_maker"]).Split(',').ToList();
			foreach (string item19 in list8)
			{
				if (item19.ToString() != string.Empty)
				{
					int item7 = int.Parse(item19.ToString());
					rocketToysMarkers.Add(item7);
				}
			}
		}
		moves = int.Parse(dictionary6["moves"].ToString());
		List<string> list9 = ((string)dictionary6["target_1"]).Split(',').ToList();
		if (list9.Count >= 2)
		{
			target1Type = DataToTargetType(int.Parse(list9[0].ToString()));
			target1Amount = int.Parse(list9[1].ToString());
			if (list9.Count == 3)
			{
				target1Color = int.Parse(list9[2].ToString());
			}
		}
		List<string> list10 = ((string)dictionary6["target_2"]).Split(',').ToList();
		if (list10.Count >= 2)
		{
			target2Type = DataToTargetType(int.Parse(list10[0].ToString()));
			target2Amount = int.Parse(list10[1].ToString());
			if (list10.Count == 3)
			{
				target2Color = int.Parse(list10[2].ToString());
			}
		}
		List<string> list11 = ((string)dictionary6["target_3"]).Split(',').ToList();
		if (list11.Count >= 2)
		{
			target3Type = DataToTargetType(int.Parse(list11[0].ToString()));
			target3Amount = int.Parse(list11[1].ToString());
			if (list11.Count == 3)
			{
				target3Color = int.Parse(list11[2].ToString());
			}
		}
		List<string> list12 = ((string)dictionary6["target_4"]).Split(',').ToList();
		if (list12.Count >= 2)
		{
			target4Type = DataToTargetType(int.Parse(list12[0].ToString()));
			target4Amount = int.Parse(list12[1].ToString());
			if (list12.Count == 3)
			{
				target4Color = int.Parse(list12[2].ToString());
			}
		}
		score_Star_1 = int.Parse(dictionary6["score_star_1"].ToString());
		score_Star_2 = int.Parse(dictionary6["score_star_2"].ToString());
		score_Star_3 = int.Parse(dictionary6["score_star_3"].ToString());
		doll = int.Parse(dictionary6["doll"].ToString());
		targetlbl = dictionary6["target_lbl"].ToString();
		if (dictionary6.ContainsKey("collectible_signs"))
		{
			List<string> list13 = ((string)dictionary6["collectible_signs"]).Split(',').ToList();
			foreach (string item20 in list13)
			{
				if (item20.ToString() != string.Empty)
				{
					int item8 = int.Parse(item20.ToString());
					collectibleCollectColumnMarkers.Add(item8);
				}
			}
		}
		if (dictionary6.ContainsKey("collectible_node"))
		{
			List<string> list14 = ((string)dictionary6["collectible_node"]).Split(',').ToList();
			foreach (string item21 in list14)
			{
				if (item21.ToString() != string.Empty)
				{
					int item9 = int.Parse(item21.ToString());
					collectibleCollectNodeMarkers.Add(item9);
				}
			}
		}
		if (dictionary6.ContainsKey("collectible_maker"))
		{
			List<string> list15 = ((string)dictionary6["collectible_maker"]).Split(',').ToList();
			foreach (string item22 in list15)
			{
				if (item22.ToString() != string.Empty)
				{
					int item10 = int.Parse(item22.ToString());
					collectibleGenerateMarkers.Add(item10);
				}
			}
		}
		if (dictionary6.ContainsKey("collectible_max") && dictionary6["collectible_max"].ToString() != string.Empty)
		{
			collectibleMaxOnBoard = int.Parse(dictionary6["collectible_max"].ToString());
		}
		if (dictionary6.ContainsKey("color_toy") && dictionary6["color_toy"].ToString() != string.Empty)
		{
			marshmallowMoreThanTarget = int.Parse(dictionary6["color_toy"].ToString());
		}
	}

	private void Clear()
	{
		tileLayerData.Clear();
		breakableLayerData.Clear();
		itemLayerData.Clear();
		lockLayerData.Clear();
		usingItems.Clear();
		usingColors.Clear();
		itemWeight.Clear();
		rocketToys.Clear();
		rockettoysWeight.Clear();
		rocketToysMarkers.Clear();
		maxRockettoys = 0;
		target1Type = TARGET_TYPE.NONE;
		target2Type = TARGET_TYPE.NONE;
		target3Type = TARGET_TYPE.NONE;
		target4Type = TARGET_TYPE.NONE;
		target1Amount = 0;
		target2Amount = 0;
		target3Amount = 0;
		target4Amount = 0;
		target1Color = 0;
		target2Color = 0;
		target3Color = 0;
		target4Color = 0;
		collectibleCollectColumnMarkers.Clear();
		collectibleCollectNodeMarkers.Clear();
		collectibleGenerateMarkers.Clear();
		collectibleMaxOnBoard = 0;
		marshmallowMoreThanTarget = 0;
	}

	private TILE_TYPE DataToTileType(int key)
	{
		if (key == 0)
		{
			return TILE_TYPE.NONE;
		}
		switch (names[(key - 1).ToString()])
		{
		case "none_tile":
			return TILE_TYPE.NONE;
		case "pass_through_tile":
			return TILE_TYPE.PASS_THROUGH;
		case "light_tile":
			return TILE_TYPE.LIGHT_TILE;
		case "dark_tile":
			return TILE_TYPE.DARD_TILE;
		default:
			return TILE_TYPE.NONE;
		}
	}

	private ITEM_TYPE DataToItemType(int key)
	{
		if (key == 0)
		{
			return ITEM_TYPE.NONE;
		}
		switch (names[(key - 1).ToString()])
		{
		case "random_item":
			return ITEM_TYPE.ITEM_RAMDOM;
		case "colorcone":
			return ITEM_TYPE.ITEM_COLORCONE;
		case "bluebox":
			return ITEM_TYPE.BlueBox;
		case "blue_bomb":
			return ITEM_TYPE.BlueBox_BOMB;
		case "blue_column":
			return ITEM_TYPE.BlueBox_COLUMN;
		case "blue_row":
			return ITEM_TYPE.BlueBox_ROW;
		case "blue_cross":
			return ITEM_TYPE.BlueBox_Cross;
		case "greenbox":
			return ITEM_TYPE.GreenBox;
		case "green_bomb":
			return ITEM_TYPE.GreenBox_BOMB;
		case "green_column":
			return ITEM_TYPE.GreenBox_COLUMN;
		case "green_row":
			return ITEM_TYPE.GreenBox_ROW;
		case "green_cross":
			return ITEM_TYPE.GreenBox_Cross;
		case "orangebox":
			return ITEM_TYPE.ORANGEBOX;
		case "orange_bomb":
			return ITEM_TYPE.ORANGEBOX_BOMB;
		case "orange_column":
			return ITEM_TYPE.ORANGEBOX_COLUMN;
		case "orange_row":
			return ITEM_TYPE.ORANGEBOX_ROW;
		case "orange_cross":
			return ITEM_TYPE.ORANGEBOX_Cross;
		case "purplebox":
			return ITEM_TYPE.PURPLEBOX;
		case "purple_bomb":
			return ITEM_TYPE.PURPLEBOX_BOMB;
		case "purple_column":
			return ITEM_TYPE.PURPLEBOX_COLUMN;
		case "purple_row":
			return ITEM_TYPE.PURPLEBOX_ROW;
		case "purple_cross":
			return ITEM_TYPE.PURPLEBOX_Cross;
		case "redbox":
			return ITEM_TYPE.REDBOX;
		case "red_bomb":
			return ITEM_TYPE.REDBOX_BOMB;
		case "red_column":
			return ITEM_TYPE.REDBOX_COLUMN;
		case "red_row":
			return ITEM_TYPE.REDBOX_ROW;
		case "red_cross":
			return ITEM_TYPE.REDBOX_Cross;
		case "yellowbox":
			return ITEM_TYPE.YELLOWBOX;
		case "yellow_bomb":
			return ITEM_TYPE.YELLOWBOX_BOMB;
		case "yellow_column":
			return ITEM_TYPE.YELLOWBOX_COLUMN;
		case "yellow_row":
			return ITEM_TYPE.YELLOWBOX_ROW;
		case "yellow_cross":
			return ITEM_TYPE.YELLOWBOX_Cross;
		case "breakable":
			return ITEM_TYPE.BREAKABLE;
		case "generic_CollectableToy":
			return ITEM_TYPE.ROCKET_RANDOM;
		case "blue_CollectableToy":
			return ITEM_TYPE.ROCKET_1;
		case "green_CollectableToy":
			return ITEM_TYPE.ROCKET_2;
		case "orange_CollectableToy":
			return ITEM_TYPE.ROCKET_3;
		case "purple_CollectableToy":
			return ITEM_TYPE.ROCKET_4;
		case "red_CollectableToy":
			return ITEM_TYPE.ROCKET_5;
		case "yellow_CollectableToy":
			return ITEM_TYPE.ROCKET_6;
		case "toymine_1":
			return ITEM_TYPE.MINE_1_LAYER;
		case "toymine_2":
			return ITEM_TYPE.MINE_2_LAYER;
		case "toymine_3":
			return ITEM_TYPE.MINE_3_LAYER;
		case "toymine_4":
			return ITEM_TYPE.MINE_4_LAYER;
		case "toymine_5":
			return ITEM_TYPE.MINE_5_LAYER;
		case "toymine_6":
			return ITEM_TYPE.MINE_6_LAYER;
		case "generic_lego_box":
			return ITEM_TYPE.ROCK_CANDY_RANDOM;
		case "blue_lego_box":
			return ITEM_TYPE.ROCK_CANDY_1;
		case "green_lego_box":
			return ITEM_TYPE.ROCK_CANDY_2;
		case "orange_lego_box":
			return ITEM_TYPE.ROCK_CANDY_3;
		case "purple_lego_box":
			return ITEM_TYPE.ROCK_CANDY_4;
		case "red_lego_box":
			return ITEM_TYPE.ROCK_CANDY_5;
		case "yellow_lego_box":
			return ITEM_TYPE.ROCK_CANDY_6;
		case "collectible_1":
			return ITEM_TYPE.COLLECTIBLE_1;
		case "collectible_2":
			return ITEM_TYPE.COLLECTIBLE_2;
		case "collectible_3":
			return ITEM_TYPE.COLLECTIBLE_3;
		case "collectible_4":
			return ITEM_TYPE.COLLECTIBLE_4;
		case "collectible_5":
			return ITEM_TYPE.COLLECTIBLE_5;
		case "collectible_6":
			return ITEM_TYPE.COLLECTIBLE_6;
		case "collectible_7":
			return ITEM_TYPE.COLLECTIBLE_7;
		case "collectible_8":
			return ITEM_TYPE.COLLECTIBLE_8;
		case "collectible_9":
			return ITEM_TYPE.COLLECTIBLE_9;
		case "collectible_10":
			return ITEM_TYPE.COLLECTIBLE_10;
		case "collectible_11":
			return ITEM_TYPE.COLLECTIBLE_11;
		case "collectible_12":
			return ITEM_TYPE.COLLECTIBLE_12;
		case "collectible_13":
			return ITEM_TYPE.COLLECTIBLE_13;
		case "collectible_14":
			return ITEM_TYPE.COLLECTIBLE_14;
		case "collectible_15":
			return ITEM_TYPE.COLLECTIBLE_15;
		case "collectible_16":
			return ITEM_TYPE.COLLECTIBLE_16;
		case "collectible_17":
			return ITEM_TYPE.COLLECTIBLE_17;
		case "collectible_18":
			return ITEM_TYPE.COLLECTIBLE_18;
		case "collectible_19":
			return ITEM_TYPE.COLLECTIBLE_19;
		case "collectible_20":
			return ITEM_TYPE.COLLECTIBLE_20;
		default:
			return ITEM_TYPE.NONE;
		}
	}

	private WAFFLE_TYPE DataToWaffleType(int key)
	{
		if (key == 0)
		{
			return WAFFLE_TYPE.NONE;
		}
		switch (names[(key - 1).ToString()])
		{
		case "waffle_1":
			return WAFFLE_TYPE.WAFFLE_1;
		case "waffle_2":
			return WAFFLE_TYPE.WAFFLE_2;
		case "waffle_3":
			return WAFFLE_TYPE.WAFFLE_3;
		default:
			return WAFFLE_TYPE.NONE;
		}
	}

	private LOCK_TYPE DataToLockType(int key)
	{
		if (key == 0)
		{
			return LOCK_TYPE.NONE;
		}
		string text = names[(key - 1).ToString()];
		if (text != null && text == "Lock")
		{
			return LOCK_TYPE.LOCK_1;
		}
		return LOCK_TYPE.NONE;
	}

	private TARGET_TYPE DataToTargetType(int data)
	{
		switch (data)
		{
		case 1:
			return TARGET_TYPE.SCORE;
		case 2:
			return TARGET_TYPE.ITEM;
		case 3:
			return TARGET_TYPE.BREAKABLE;
		case 4:
			return TARGET_TYPE.WAFFLE;
		case 5:
			return TARGET_TYPE.COLLECTIBLE;
		case 6:
			return TARGET_TYPE.COLUMN_ROW_BREAKER;
		case 7:
			return TARGET_TYPE.BOMB_BREAKER;
		case 8:
			return TARGET_TYPE.X_BREAKER;
		case 9:
			return TARGET_TYPE.LOCK;
		case 10:
			return TARGET_TYPE.COLORCONE;
		case 11:
			return TARGET_TYPE.ROCKET;
		case 12:
			return TARGET_TYPE.TOYMINE;
		case 13:
			return TARGET_TYPE.ROCK_CANDY;
		default:
			return TARGET_TYPE.NONE;
		}
	}

	public ITEM_TYPE RandomItems()
	{
		List<ITEM_TYPE> list = new List<ITEM_TYPE>();
		for (int i = 0; i < usingItems.Count; i++)
		{
			ITEM_TYPE iTEM_TYPE = usingItems[i];
			for (int j = 0; j < itemWeight[i]; j++)
			{
				switch (iTEM_TYPE)
				{
				case ITEM_TYPE.BlueBox:
					list.Add(ITEM_TYPE.BlueBox);
					break;
				case ITEM_TYPE.GreenBox:
					list.Add(ITEM_TYPE.GreenBox);
					break;
				case ITEM_TYPE.ORANGEBOX:
					list.Add(ITEM_TYPE.ORANGEBOX);
					break;
				case ITEM_TYPE.PURPLEBOX:
					list.Add(ITEM_TYPE.PURPLEBOX);
					break;
				case ITEM_TYPE.REDBOX:
					list.Add(ITEM_TYPE.REDBOX);
					break;
				case ITEM_TYPE.YELLOWBOX:
					list.Add(ITEM_TYPE.YELLOWBOX);
					break;
				}
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		return list[index];
	}

	public int RandomColor()
	{
		switch (RandomItems())
		{
		case ITEM_TYPE.BlueBox:
			return 1;
		case ITEM_TYPE.GreenBox:
			return 2;
		case ITEM_TYPE.ORANGEBOX:
			return 3;
		case ITEM_TYPE.PURPLEBOX:
			return 4;
		case ITEM_TYPE.REDBOX:
			return 5;
		case ITEM_TYPE.YELLOWBOX:
			return 6;
		default:
			return 0;
		}
	}

	public ITEM_TYPE RandomRockets()
	{
		List<ITEM_TYPE> list = new List<ITEM_TYPE>();
		for (int i = 0; i < rocketToys.Count; i++)
		{
			ITEM_TYPE iTEM_TYPE = rocketToys[i];
			for (int j = 0; j < rockettoysWeight[i]; j++)
			{
				switch (iTEM_TYPE)
				{
				case ITEM_TYPE.ROCKET_1:
					list.Add(ITEM_TYPE.ROCKET_1);
					break;
				case ITEM_TYPE.ROCKET_2:
					list.Add(ITEM_TYPE.ROCKET_2);
					break;
				case ITEM_TYPE.ROCKET_3:
					list.Add(ITEM_TYPE.ROCKET_3);
					break;
				case ITEM_TYPE.ROCKET_4:
					list.Add(ITEM_TYPE.ROCKET_4);
					break;
				case ITEM_TYPE.ROCKET_5:
					list.Add(ITEM_TYPE.ROCKET_5);
					break;
				case ITEM_TYPE.ROCKET_6:
					list.Add(ITEM_TYPE.ROCKET_6);
					break;
				}
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		return list[index];
	}
}
