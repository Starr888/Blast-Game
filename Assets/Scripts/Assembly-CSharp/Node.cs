using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	[Header("Variables")]
	public itemGrid grid;

	public Tile tile;

	public Item item;

	public Waffle waffle;

	public Cage cage;

	public GameObject ovenActive;

	[Header("Row & Columns")]
	public int i;

	public int j;

	public Node LeftNeighbor()
	{
		return grid.GetNode(i, j - 1);
	}

	public Node RightNeighbor()
	{
		return grid.GetNode(i, j + 1);
	}

	public Node TopNeighbor()
	{
		return grid.GetNode(i - 1, j);
	}

	public Node BottomNeighbor()
	{
		return grid.GetNode(i + 1, j);
	}

	public Node TopLeftNeighbor()
	{
		return grid.GetNode(i - 1, j - 1);
	}

	public Node TopRightNeighbor()
	{
		return grid.GetNode(i - 1, j + 1);
	}

	public Node BottomLeftNeighbor()
	{
		return grid.GetNode(i + 1, j - 1);
	}

	public Node BottomRightNeighbor()
	{
		return grid.GetNode(i + 1, j + 1);
	}

	private void Start()
	{

		sortingOrder();

    }

    public void sortingOrder()
    {
        //Debug.Log("Row in this level: " + StageLoader.instance.row);

        //Debug.Log("Need node position i: " + i);

        int totalRows = StageLoader.instance.row;

        for (int k = 0; k < totalRows; k++)
        {

            int sortingOrder = totalRows - k - 1;
            if (i == k)
            {
                // Find the child with the name "Item"
                Transform child = transform.Find("Item");

                if (child != null)
                {
                    SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();

                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sortingOrder = sortingOrder;
                    }
                    else
                    {
                        //Debug.LogError("SpriteRenderer not found on child object.");
                    }
                }
                else
                {
                    //Debug.LogError("Child object with name 'Item' not found.");
                }

                // Since 'i' matches 'k', no need to check further
                break;
            }
        }
    }

    public Item GenerateItem(ITEM_TYPE type)
	{
		Item result = null;
		switch (type)
		{
		case ITEM_TYPE.ITEM_RAMDOM:
			GenerateRandomCookie();
			break;
		case ITEM_TYPE.ITEM_COLORCONE:
		case ITEM_TYPE.BlueBox:
		case ITEM_TYPE.BlueBox_COLUMN:
		case ITEM_TYPE.BlueBox_ROW:
		case ITEM_TYPE.BlueBox_BOMB:
		case ITEM_TYPE.GreenBox:
		case ITEM_TYPE.GreenBox_COLUMN:
		case ITEM_TYPE.GreenBox_ROW:
		case ITEM_TYPE.GreenBox_BOMB:
		case ITEM_TYPE.ORANGEBOX:
		case ITEM_TYPE.ORANGEBOX_COLUMN:
		case ITEM_TYPE.ORANGEBOX_ROW:
		case ITEM_TYPE.ORANGEBOX_BOMB:
		case ITEM_TYPE.PURPLEBOX:
		case ITEM_TYPE.PURPLEBOX_COLUMN:
		case ITEM_TYPE.PURPLEBOX_ROW:
		case ITEM_TYPE.PURPLEBOX_BOMB:
		case ITEM_TYPE.REDBOX:
		case ITEM_TYPE.REDBOX_COLUMN:
		case ITEM_TYPE.REDBOX_ROW:
		case ITEM_TYPE.REDBOX_BOMB:
		case ITEM_TYPE.YELLOWBOX:
		case ITEM_TYPE.YELLOWBOX_COLUMN:
		case ITEM_TYPE.YELLOWBOX_ROW:
		case ITEM_TYPE.YELLOWBOX_BOMB:
		case ITEM_TYPE.BREAKABLE:
			InstantiateItem(type);
			break;
		case ITEM_TYPE.ROCKET_RANDOM:
			GenerateRandomGingerbread();
			break;
		case ITEM_TYPE.ROCKET_1:
		case ITEM_TYPE.ROCKET_2:
		case ITEM_TYPE.ROCKET_3:
		case ITEM_TYPE.ROCKET_4:
		case ITEM_TYPE.ROCKET_5:
		case ITEM_TYPE.ROCKET_6:
		case ITEM_TYPE.MINE_1_LAYER:
		case ITEM_TYPE.MINE_2_LAYER:
		case ITEM_TYPE.MINE_3_LAYER:
		case ITEM_TYPE.MINE_4_LAYER:
		case ITEM_TYPE.MINE_5_LAYER:
		case ITEM_TYPE.MINE_6_LAYER:
			InstantiateItem(type);
			break;
		case ITEM_TYPE.ROCK_CANDY_RANDOM:
			GenerateRandomRockCandy();
			break;
		case ITEM_TYPE.ROCK_CANDY_1:
		case ITEM_TYPE.ROCK_CANDY_2:
		case ITEM_TYPE.ROCK_CANDY_3:
		case ITEM_TYPE.ROCK_CANDY_4:
		case ITEM_TYPE.ROCK_CANDY_5:
		case ITEM_TYPE.ROCK_CANDY_6:
		case ITEM_TYPE.BlueBox_Cross:
		case ITEM_TYPE.GreenBox_Cross:
		case ITEM_TYPE.ORANGEBOX_Cross:
		case ITEM_TYPE.PURPLEBOX_Cross:
		case ITEM_TYPE.REDBOX_Cross:
		case ITEM_TYPE.YELLOWBOX_Cross:
		case ITEM_TYPE.COLLECTIBLE_1:
		case ITEM_TYPE.COLLECTIBLE_2:
		case ITEM_TYPE.COLLECTIBLE_3:
		case ITEM_TYPE.COLLECTIBLE_4:
		case ITEM_TYPE.COLLECTIBLE_5:
		case ITEM_TYPE.COLLECTIBLE_6:
		case ITEM_TYPE.COLLECTIBLE_7:
		case ITEM_TYPE.COLLECTIBLE_8:
		case ITEM_TYPE.COLLECTIBLE_9:
		case ITEM_TYPE.COLLECTIBLE_10:
		case ITEM_TYPE.COLLECTIBLE_11:
		case ITEM_TYPE.COLLECTIBLE_12:
		case ITEM_TYPE.COLLECTIBLE_13:
		case ITEM_TYPE.COLLECTIBLE_14:
		case ITEM_TYPE.COLLECTIBLE_15:
		case ITEM_TYPE.COLLECTIBLE_16:
		case ITEM_TYPE.COLLECTIBLE_17:
		case ITEM_TYPE.COLLECTIBLE_18:
		case ITEM_TYPE.COLLECTIBLE_19:
		case ITEM_TYPE.COLLECTIBLE_20:
			InstantiateItem(type);
                break;

		}
		return result;
	}

	private Item GenerateRandomCookie()
	{
		ITEM_TYPE type = StageLoader.instance.RandomItems();
		return InstantiateItem(type);
	}

	private Item GenerateRandomGingerbread()
	{
		switch (StageLoader.instance.RandomItems())
		{
		case ITEM_TYPE.BlueBox:
			InstantiateItem(ITEM_TYPE.ROCKET_1);
			break;
		case ITEM_TYPE.GreenBox:
			InstantiateItem(ITEM_TYPE.ROCKET_2);
			break;
		case ITEM_TYPE.ORANGEBOX:
			InstantiateItem(ITEM_TYPE.ROCKET_3);
			break;
		case ITEM_TYPE.PURPLEBOX:
			InstantiateItem(ITEM_TYPE.ROCKET_4);
			break;
		case ITEM_TYPE.REDBOX:
			InstantiateItem(ITEM_TYPE.ROCKET_5);
			break;
		case ITEM_TYPE.YELLOWBOX:
			InstantiateItem(ITEM_TYPE.ROCKET_6);
			break;
		}
		return null;
	}

	private Item GenerateRandomRockCandy()
	{
		switch (StageLoader.instance.RandomItems())
		{
		case ITEM_TYPE.BlueBox:
			InstantiateItem(ITEM_TYPE.ROCK_CANDY_1);
			break;
		case ITEM_TYPE.GreenBox:
			InstantiateItem(ITEM_TYPE.ROCK_CANDY_2);
			break;
		case ITEM_TYPE.ORANGEBOX:
			InstantiateItem(ITEM_TYPE.ROCK_CANDY_3);
			break;
		case ITEM_TYPE.PURPLEBOX:
			InstantiateItem(ITEM_TYPE.ROCK_CANDY_4);
			break;
		case ITEM_TYPE.REDBOX:
			InstantiateItem(ITEM_TYPE.ROCK_CANDY_5);
			break;
		case ITEM_TYPE.YELLOWBOX:
			InstantiateItem(ITEM_TYPE.ROCK_CANDY_6);
			break;
		}
		return null;
	}

	private Item InstantiateItem(ITEM_TYPE type)
	{
        GameObject gameObject = null;
		int color = 0;
		switch (type)
		{
		case ITEM_TYPE.ITEM_COLORCONE:
			gameObject = Object.Instantiate(Resources.Load(Configuration.ItemColorCone())) as GameObject;
			break;
		case ITEM_TYPE.BlueBox:
			color = 1;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item1())) as GameObject;
			break;
		case ITEM_TYPE.BlueBox_COLUMN:
			color = 1;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item1Column())) as GameObject;
			break;
		case ITEM_TYPE.BlueBox_ROW:
			color = 1;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item1Row())) as GameObject;
			break;
		case ITEM_TYPE.BlueBox_BOMB:
			color = 1;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item1Bomb())) as GameObject;
			break;
		case ITEM_TYPE.GreenBox:
			color = 2;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item2())) as GameObject;
			break;
		case ITEM_TYPE.GreenBox_COLUMN:
			color = 2;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item2Column())) as GameObject;
			break;
		case ITEM_TYPE.GreenBox_ROW:
			color = 2;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item2Row())) as GameObject;
			break;
		case ITEM_TYPE.GreenBox_BOMB:
			color = 2;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item2Bomb())) as GameObject;
			break;
		case ITEM_TYPE.ORANGEBOX:
			color = 3;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item3())) as GameObject;
			break;
		case ITEM_TYPE.ORANGEBOX_COLUMN:
			color = 3;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item3Column())) as GameObject;
			break;
		case ITEM_TYPE.ORANGEBOX_ROW:
			color = 3;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item3Row())) as GameObject;
			break;
		case ITEM_TYPE.ORANGEBOX_BOMB:
			color = 3;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item3Bomb())) as GameObject;
			break;
		case ITEM_TYPE.PURPLEBOX:
			color = 4;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item4())) as GameObject;
			break;
		case ITEM_TYPE.PURPLEBOX_COLUMN:
			color = 4;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item4Column())) as GameObject;
			break;
		case ITEM_TYPE.PURPLEBOX_ROW:
			color = 4;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item4Row())) as GameObject;
			break;
		case ITEM_TYPE.PURPLEBOX_BOMB:
			color = 4;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item4Bomb())) as GameObject;
			break;
		case ITEM_TYPE.REDBOX:
			color = 5;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item5())) as GameObject;
			break;
		case ITEM_TYPE.REDBOX_COLUMN:
			color = 5;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item5Column())) as GameObject;
			break;
		case ITEM_TYPE.REDBOX_ROW:
			color = 5;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item5Row())) as GameObject;
			break;
		case ITEM_TYPE.REDBOX_BOMB:
			color = 5;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item5Bomb())) as GameObject;
			break;
		case ITEM_TYPE.YELLOWBOX:
			color = 6;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item6())) as GameObject;
			break;
		case ITEM_TYPE.YELLOWBOX_COLUMN:
			color = 6;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item6Column())) as GameObject;
			break;
		case ITEM_TYPE.YELLOWBOX_ROW:
			color = 6;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item6Row())) as GameObject;
			break;
		case ITEM_TYPE.YELLOWBOX_BOMB:
			color = 6;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item6Bomb())) as GameObject;
			break;
		case ITEM_TYPE.BREAKABLE:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Breakable())) as GameObject;
			break;
		case ITEM_TYPE.ROCKET_1:
			color = 1;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Rocket1())) as GameObject;
			break;
		case ITEM_TYPE.ROCKET_2:
			color = 2;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Rocket2())) as GameObject;
			break;
		case ITEM_TYPE.ROCKET_3:
			color = 3;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Rocket3())) as GameObject;
			break;
		case ITEM_TYPE.ROCKET_4:
			color = 4;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Rocket4())) as GameObject;
			break;
		case ITEM_TYPE.ROCKET_5:
			color = 5;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Rocket5())) as GameObject;
			break;
		case ITEM_TYPE.ROCKET_6:
			color = 6;
			gameObject = Object.Instantiate(Resources.Load(Configuration.Rocket6())) as GameObject;
			break;
		case ITEM_TYPE.MINE_1_LAYER:
			gameObject = Object.Instantiate(Resources.Load(Configuration.ToyMine1())) as GameObject;
			break;
		case ITEM_TYPE.MINE_2_LAYER:
			gameObject = Object.Instantiate(Resources.Load(Configuration.ToyMine2())) as GameObject;
			break;
		case ITEM_TYPE.MINE_3_LAYER:
			gameObject = Object.Instantiate(Resources.Load(Configuration.ToyMine3())) as GameObject;
			break;
		case ITEM_TYPE.MINE_4_LAYER:
			gameObject = Object.Instantiate(Resources.Load(Configuration.ToyMine4())) as GameObject;
			break;
		case ITEM_TYPE.MINE_5_LAYER:
			gameObject = Object.Instantiate(Resources.Load(Configuration.ToyMine5())) as GameObject;
			break;
		case ITEM_TYPE.MINE_6_LAYER:
			gameObject = Object.Instantiate(Resources.Load(Configuration.ToyMine6())) as GameObject;
			break;
		case ITEM_TYPE.ROCK_CANDY_1:
			color = 1;
			gameObject = Object.Instantiate(Resources.Load(Configuration.LegoBox1())) as GameObject;
			break;
		case ITEM_TYPE.ROCK_CANDY_2:
			color = 2;
			gameObject = Object.Instantiate(Resources.Load(Configuration.LegoBox2())) as GameObject;
			break;
		case ITEM_TYPE.ROCK_CANDY_3:
			color = 3;
			gameObject = Object.Instantiate(Resources.Load(Configuration.LegoBox3())) as GameObject;
			break;
		case ITEM_TYPE.ROCK_CANDY_4:
			color = 4;
			gameObject = Object.Instantiate(Resources.Load(Configuration.LegoBox4())) as GameObject;
			break;
		case ITEM_TYPE.ROCK_CANDY_5:
			color = 5;
			gameObject = Object.Instantiate(Resources.Load(Configuration.LegoBox5())) as GameObject;
			break;
		case ITEM_TYPE.ROCK_CANDY_6:
			color = 6;
			gameObject = Object.Instantiate(Resources.Load(Configuration.LegoBox6())) as GameObject;
			break;
		case ITEM_TYPE.BlueBox_Cross:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item1Cross())) as GameObject;
			color = 1;
			break;
		case ITEM_TYPE.GreenBox_Cross:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item2Cross())) as GameObject;
			color = 2;
			break;
		case ITEM_TYPE.ORANGEBOX_Cross:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item3Cross())) as GameObject;
			color = 3;
			break;
		case ITEM_TYPE.PURPLEBOX_Cross:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item4Cross())) as GameObject;
			color = 4;
			break;
		case ITEM_TYPE.REDBOX_Cross:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item5Cross())) as GameObject;
			color = 5;
			break;
		case ITEM_TYPE.YELLOWBOX_Cross:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Item6Cross())) as GameObject;
			color = 6;
			break;
		case ITEM_TYPE.COLLECTIBLE_1:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible1())) as GameObject;
			color = 1;
			break;
		case ITEM_TYPE.COLLECTIBLE_2:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible2())) as GameObject;
			color = 2;
			break;
		case ITEM_TYPE.COLLECTIBLE_3:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible3())) as GameObject;
			color = 3;
			break;
		case ITEM_TYPE.COLLECTIBLE_4:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible4())) as GameObject;
			color = 4;
			break;
		case ITEM_TYPE.COLLECTIBLE_5:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible5())) as GameObject;
			color = 5;
			break;
		case ITEM_TYPE.COLLECTIBLE_6:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible6())) as GameObject;
			color = 6;
			break;
		case ITEM_TYPE.COLLECTIBLE_7:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible7())) as GameObject;
			color = 7;
			break;
		case ITEM_TYPE.COLLECTIBLE_8:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible8())) as GameObject;
			color = 8;
			break;
		case ITEM_TYPE.COLLECTIBLE_9:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible9())) as GameObject;
			color = 9;
			break;
		case ITEM_TYPE.COLLECTIBLE_10:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible10())) as GameObject;
			color = 10;
			break;
		case ITEM_TYPE.COLLECTIBLE_11:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible11())) as GameObject;
			color = 11;
			break;
		case ITEM_TYPE.COLLECTIBLE_12:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible12())) as GameObject;
			color = 12;
			break;
		case ITEM_TYPE.COLLECTIBLE_13:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible13())) as GameObject;
			color = 13;
			break;
		case ITEM_TYPE.COLLECTIBLE_14:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible14())) as GameObject;
			color = 14;
			break;
		case ITEM_TYPE.COLLECTIBLE_15:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible15())) as GameObject;
			color = 15;
			break;
		case ITEM_TYPE.COLLECTIBLE_16:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible16())) as GameObject;
			color = 16;
			break;
		case ITEM_TYPE.COLLECTIBLE_17:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible17())) as GameObject;
			color = 17;
			break;
		case ITEM_TYPE.COLLECTIBLE_18:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible18())) as GameObject;
			color = 18;
			break;
		case ITEM_TYPE.COLLECTIBLE_19:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible19())) as GameObject;
			color = 19;
			break;
		case ITEM_TYPE.COLLECTIBLE_20:
			gameObject = Object.Instantiate(Resources.Load(Configuration.Collectible20())) as GameObject;
			color = 20;
			break;
		}
		if (gameObject != null)
		{
            gameObject.transform.SetParent(base.gameObject.transform);
			gameObject.name = "Item";
			gameObject.transform.localPosition = grid.NodeLocalPosition(i, j);
			gameObject.GetComponent<Item>().node = this;
			gameObject.GetComponent<Item>().board = grid;
			gameObject.GetComponent<Item>().type = type;
			gameObject.GetComponent<Item>().color = color;
			item = gameObject.GetComponent<Item>();
			//reSorting();
            return gameObject.GetComponent<Item>();
        }
        return null;
    }

	public List<Item> FindMatches(FIND_DIRECTION direction = FIND_DIRECTION.NONE, int matches = 2)
	{
		List<Item> list = new List<Item>();
		Dictionary<int, Item> dictionary = new Dictionary<int, Item>();
		if (item == null || !item.Matchable())
		{
			return null;
		}
		if (direction != FIND_DIRECTION.COLUMN)
		{
			dictionary = FindMoreMatches(item.color, dictionary, FIND_DIRECTION.ROW);
		}
		if (dictionary.Count < matches)
		{
			dictionary.Clear();
		}
		if (direction != FIND_DIRECTION.ROW)
		{
			dictionary = FindMoreMatches(item.color, dictionary, FIND_DIRECTION.COLUMN);
		}
		if (dictionary.Count < matches)
		{
			dictionary.Clear();
		}
		foreach (KeyValuePair<int, Item> item in dictionary)
		{
			list.Add(item.Value);
		}
		return list;
	}

	private Dictionary<int, Item> FindMoreMatches(int color, Dictionary<int, Item> countedNodes, FIND_DIRECTION direction)
	{
		if (item == null || item.destroying)
		{
			return countedNodes;
		}
		if (item.color == color && !countedNodes.ContainsValue(item) && item.Matchable() && item.node != null)
		{
			countedNodes.Add(item.node.OrderOnBoard(), item);
			switch (direction)
			{
			case FIND_DIRECTION.ROW:
				if (LeftNeighbor() != null)
				{
					countedNodes = LeftNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.ROW);
				}
				if (RightNeighbor() != null)
				{
					countedNodes = RightNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.ROW);
				}
				if (TopNeighbor() != null)
				{
					countedNodes = TopNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.COLUMN);
				}
				if (BottomNeighbor() != null)
				{
					countedNodes = BottomNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.COLUMN);
				}
				break;
			case FIND_DIRECTION.COLUMN:
				if (TopNeighbor() != null)
				{
					countedNodes = TopNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.COLUMN);
				}
				if (BottomNeighbor() != null)
				{
					countedNodes = BottomNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.COLUMN);
				}
				if (LeftNeighbor() != null)
				{
					countedNodes = LeftNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.ROW);
				}
				if (RightNeighbor() != null)
				{
					countedNodes = RightNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.ROW);
				}
				break;
			}
		}
		return countedNodes;
	}

	public int OrderOnBoard()
	{
        return i * StageLoader.instance.column + j;
	}

	public bool CanStoreItem()
	{
		if (tile != null && (tile.type == TILE_TYPE.DARD_TILE || tile.type == TILE_TYPE.LIGHT_TILE))
		{
			return true;
		}
		return false;
	}

	public bool CanGoThrough()
	{
		if (tile == null || tile.type == TILE_TYPE.NONE)
		{
			return false;
		}
		return true;
	}

	public bool CanGenerateNewItem()
	{
		if (CanStoreItem())
		{
			for (int num = i - 1; num >= 0; num--)
			{
				Node node = grid.GetNode(num, j);
				if (node != null)
				{
					if (!node.CanGoThrough())
					{
						return false;
					}
					if (node.item != null && !node.item.Movable())
					{
						return false;
					}
				}
			}
			return true;
		}
		return false;
	}

	public Node GetSourceNode()
	{
		Node node = null;
		Node node2 = grid.GetNode(i - 1, j);
		if (node2 != null && node2.item == null && node2.CanGoThrough() && node2.GetSourceNode() != null)
		{
			node = node2.GetSourceNode();
		}
		if (node != null)
		{
			return node;
		}
		Node node3 = grid.GetNode(i - 1, j - 1);
		if (node3 != null)
		{
			if (node3.item == null && node3.CanGoThrough())
			{
				if (node3.GetSourceNode() != null)
				{
					node = node3.GetSourceNode();
				}
			}
			else if (node3.item != null && node3.item.Movable())
			{
				node = node3;
			}
		}
		if (node != null)
		{
			return node;
		}
		Node node4 = grid.GetNode(i - 1, j + 1);
		if (node4 != null)
		{
			if (node4.item == null && node4.CanGoThrough())
			{
				if (node4.GetSourceNode() != null)
				{
					node = node4.GetSourceNode();
				}
			}
			else if (node4.item != null && node4.item.Movable())
			{
				node = node4;
			}
		}
		return node;
	}

	public List<Vector3> GetMovePath()
	{
		List<Vector3> list = new List<Vector3>();
		list.Add(grid.NodeLocalPosition(i, j));
		Node node = grid.GetNode(i - 1, j);
		if (node != null && node.item == null && node.CanGoThrough() && node.GetSourceNode() != null)
		{
			list.AddRange(node.GetMovePath());
			return list;
		}
		Node node2 = grid.GetNode(i - 1, j - 1);
		if (node2 != null)
		{
			if (node2.item == null && node2.CanGoThrough())
			{
				if (node2.GetSourceNode() != null)
				{
					list.AddRange(node2.GetMovePath());
					return list;
				}
			}
			else if (node2.item != null && node2.item.Movable())
			{
				return list;
			}
		}
		Node node3 = grid.GetNode(i - 1, j + 1);
		if (node3 != null)
		{
			if (node3.item == null && node3.CanGoThrough())
			{
				if (node3.GetSourceNode() != null)
				{
					list.AddRange(node3.GetMovePath());
					return list;
				}
			}
			else if (node3.item != null && node3.item.Movable())
			{
				return list;
			}
		}
		return list;
	}

	public void WaffleExplode()
	{
		if (waffle != null && ((item != null) & (item.IsCookie() || item.IsBreaker(item.type) || item.type == ITEM_TYPE.ITEM_COLORCONE)))
		{
			AudioManager.instance.WaffleExplodeAudio();
			grid.CollectWaffle(waffle);
			GameObject gameObject = null;
			if (waffle.type == WAFFLE_TYPE.WAFFLE_3)
			{
				gameObject = Resources.Load(Configuration.Waffle2()) as GameObject;
				waffle.gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				waffle.type = WAFFLE_TYPE.WAFFLE_2;
			}
			else if (waffle.type == WAFFLE_TYPE.WAFFLE_2)
			{
				gameObject = Resources.Load(Configuration.Waffle1()) as GameObject;
				waffle.gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				waffle.type = WAFFLE_TYPE.WAFFLE_1;
			}
			else if (waffle.type == WAFFLE_TYPE.WAFFLE_1)
			{
				Object.Destroy(waffle.gameObject);
				waffle = null;
			}
		}
	}

	public void CageExplode()
	{
		if (cage == null)
		{
			return;
		}
		GameObject gameObject = null;
		if (item != null)
		{
			switch (item.GetCookie(item.type))
			{
			case ITEM_TYPE.BlueBox:
				gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BlueBoxExplosion()) as GameObject);
				break;
			case ITEM_TYPE.GreenBox:
				gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.GreenBoxExplosion()) as GameObject);
				break;
			case ITEM_TYPE.ORANGEBOX:
				gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.OrangeCookieExplosion()) as GameObject);
				break;
			case ITEM_TYPE.PURPLEBOX:
				gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.PurpleCookieExplosion()) as GameObject);
				break;
			case ITEM_TYPE.REDBOX:
				gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RedCookieExplosion()) as GameObject);
				break;
			case ITEM_TYPE.YELLOWBOX:
				gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.YellowCookieExplosion()) as GameObject);
				break;
			}
		}
		grid.CollectCage(cage);
		if (gameObject != null)
		{
			gameObject.transform.position = item.transform.position;
		}
		AudioManager.instance.CageExplodeAudio();
		Object.Destroy(cage.gameObject);
		cage = null;
		StartCoroutine(item.ResetDestroying());
	}

	public void AddOvenBoosterActive()
	{
		ovenActive = Object.Instantiate(Resources.Load(Configuration.BoosterActive())) as GameObject;
		ovenActive.transform.localPosition = grid.NodeLocalPosition(i, j);
	}

	public void RemoveOvenBoosterActive()
	{
		Object.Destroy(ovenActive);
		ovenActive = null;
	}
}
