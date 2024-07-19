using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[Header("Parent")]
	public itemGrid board;

	public Node node;

	[Header("Variables")]
	public int color;

	public ITEM_TYPE type;

	public ITEM_TYPE next;

	public BREAKER_EFFECT effect;

	[Header("Check")]
	public bool drag;

	public bool nextSound = true;

	public bool destroying;

	public bool dropping;

	public bool changing;

	public Vector3 mousePostion = Vector3.zero;

	public Vector3 deltaPosition = Vector3.zero;

	public Vector3 swapDirection = Vector3.zero;

	[Header("Swap")]
	public Node neighborNodeLeft;

	public Node neighborNodeRight;

	public Node neighborNodeTop;

	public Node neighborNodeBottom;

	public Node neighborNodeTopLeft;

	public Node neighborNodeTopRight;

	public Node neighborNodeBottomLeft;

	public Node neighborNodeBottomRight;

	public Node neighborNode;

	public Item swapItem;

	[Header("Drop")]
	public List<Vector3> dropPath;

	private int totalRow = StageLoader.instance.row;

    private void Update()
	{
		checkNeighbor();
		if (drag)
		{
			getNeighbor();
		}
		//Call the sorter order of sprite layer
        node.sortingOrder();
    }

    public bool Movable()
	{
		if (type == ITEM_TYPE.MINE_1_LAYER || type == ITEM_TYPE.MINE_2_LAYER || type == ITEM_TYPE.MINE_3_LAYER || type == ITEM_TYPE.MINE_4_LAYER || type == ITEM_TYPE.MINE_5_LAYER || type == ITEM_TYPE.MINE_6_LAYER || type == ITEM_TYPE.ROCK_CANDY_1 || type == ITEM_TYPE.ROCK_CANDY_2 || type == ITEM_TYPE.ROCK_CANDY_3 || type == ITEM_TYPE.ROCK_CANDY_4 || type == ITEM_TYPE.ROCK_CANDY_5 || type == ITEM_TYPE.ROCK_CANDY_6)
		{
			return false;
		}
		if (node.cage != null && node.cage.type == LOCK_TYPE.LOCK_1)
		{
			return false;
		}
		return true;
	}

	public bool Matchable()
	{
		if (type == ITEM_TYPE.MINE_1_LAYER || type == ITEM_TYPE.MINE_2_LAYER || type == ITEM_TYPE.MINE_3_LAYER || type == ITEM_TYPE.MINE_4_LAYER || type == ITEM_TYPE.MINE_5_LAYER || type == ITEM_TYPE.MINE_6_LAYER || type == ITEM_TYPE.ROCK_CANDY_1 || type == ITEM_TYPE.ROCK_CANDY_2 || type == ITEM_TYPE.ROCK_CANDY_3 || type == ITEM_TYPE.ROCK_CANDY_4 || type == ITEM_TYPE.ROCK_CANDY_5 || type == ITEM_TYPE.ROCK_CANDY_6 || type == ITEM_TYPE.BREAKABLE || type == ITEM_TYPE.ITEM_COLORCONE || type == ITEM_TYPE.COLLECTIBLE_1 || type == ITEM_TYPE.COLLECTIBLE_2 || type == ITEM_TYPE.COLLECTIBLE_3 || type == ITEM_TYPE.COLLECTIBLE_4 || type == ITEM_TYPE.COLLECTIBLE_5 || type == ITEM_TYPE.COLLECTIBLE_6 || type == ITEM_TYPE.COLLECTIBLE_7 || type == ITEM_TYPE.COLLECTIBLE_8 || type == ITEM_TYPE.COLLECTIBLE_9)
		{
			return false;
		}
		return true;
	}

	public bool Destroyable()
	{
		if (type == ITEM_TYPE.COLLECTIBLE_1 || type == ITEM_TYPE.COLLECTIBLE_2 || type == ITEM_TYPE.COLLECTIBLE_3 || type == ITEM_TYPE.COLLECTIBLE_4 || type == ITEM_TYPE.COLLECTIBLE_5 || type == ITEM_TYPE.COLLECTIBLE_6 || type == ITEM_TYPE.COLLECTIBLE_7 || type == ITEM_TYPE.COLLECTIBLE_8 || type == ITEM_TYPE.COLLECTIBLE_9 || type == ITEM_TYPE.COLLECTIBLE_10 || type == ITEM_TYPE.COLLECTIBLE_11 || type == ITEM_TYPE.COLLECTIBLE_12 || type == ITEM_TYPE.COLLECTIBLE_13 || type == ITEM_TYPE.COLLECTIBLE_14 || type == ITEM_TYPE.COLLECTIBLE_15 || type == ITEM_TYPE.COLLECTIBLE_16 || type == ITEM_TYPE.COLLECTIBLE_17 || type == ITEM_TYPE.COLLECTIBLE_18 || type == ITEM_TYPE.COLLECTIBLE_19 || type == ITEM_TYPE.COLLECTIBLE_20)
		{
			return false;
		}
		return true;
	}

	public bool IsCookie()
	{
		if (type == ITEM_TYPE.BlueBox || type == ITEM_TYPE.GreenBox || type == ITEM_TYPE.ORANGEBOX || type == ITEM_TYPE.PURPLEBOX || type == ITEM_TYPE.REDBOX || type == ITEM_TYPE.YELLOWBOX)
		{
			return true;
		}
		return false;
	}

	public bool IsCollectible()
	{
		if (type == ITEM_TYPE.COLLECTIBLE_1 || type == ITEM_TYPE.COLLECTIBLE_2 || type == ITEM_TYPE.COLLECTIBLE_3 || type == ITEM_TYPE.COLLECTIBLE_4 || type == ITEM_TYPE.COLLECTIBLE_5 || type == ITEM_TYPE.COLLECTIBLE_6 || type == ITEM_TYPE.COLLECTIBLE_7 || type == ITEM_TYPE.COLLECTIBLE_8 || type == ITEM_TYPE.COLLECTIBLE_9 || type == ITEM_TYPE.COLLECTIBLE_10 || type == ITEM_TYPE.COLLECTIBLE_11 || type == ITEM_TYPE.COLLECTIBLE_12 || type == ITEM_TYPE.COLLECTIBLE_13 || type == ITEM_TYPE.COLLECTIBLE_14 || type == ITEM_TYPE.COLLECTIBLE_15 || type == ITEM_TYPE.COLLECTIBLE_16 || type == ITEM_TYPE.COLLECTIBLE_17 || type == ITEM_TYPE.COLLECTIBLE_18 || type == ITEM_TYPE.COLLECTIBLE_19 || type == ITEM_TYPE.COLLECTIBLE_20)
		{
			return true;
		}
		return false;
	}

	public bool IsGingerbread()
	{
		if (type == ITEM_TYPE.ROCKET_1 || type == ITEM_TYPE.ROCKET_2 || type == ITEM_TYPE.ROCKET_3 || type == ITEM_TYPE.ROCKET_4 || type == ITEM_TYPE.ROCKET_5 || type == ITEM_TYPE.ROCKET_6)
		{
			return true;
		}
		return false;
	}

	public bool IsMarshmallow()
	{
		if (type == ITEM_TYPE.BREAKABLE)
		{
			return true;
		}
		return false;
	}

	public bool IsChocolate()
	{
		if (type == ITEM_TYPE.MINE_1_LAYER || type == ITEM_TYPE.MINE_2_LAYER || type == ITEM_TYPE.MINE_3_LAYER || type == ITEM_TYPE.MINE_4_LAYER || type == ITEM_TYPE.MINE_5_LAYER || type == ITEM_TYPE.MINE_6_LAYER)
		{
			return true;
		}
		return false;
	}

	public bool IsRockCandy()
	{
		if (type == ITEM_TYPE.ROCK_CANDY_1 || type == ITEM_TYPE.ROCK_CANDY_2 || type == ITEM_TYPE.ROCK_CANDY_3 || type == ITEM_TYPE.ROCK_CANDY_4 || type == ITEM_TYPE.ROCK_CANDY_5 || type == ITEM_TYPE.ROCK_CANDY_6)
		{
			return true;
		}
		return false;
	}

	public ITEM_TYPE OriginCookieType()
	{
		int index = board.NodeOrder(node.i, node.j);
		return StageLoader.instance.itemLayerData[index];
	}

	private ITEM_TYPE GetColRowBreaker(ITEM_TYPE check, Vector3 direction)
	{
		if (Random.Range(0, 2) == 0)
		{
			switch (check)
			{
			case ITEM_TYPE.BlueBox:
			case ITEM_TYPE.BlueBox_COLUMN:
			case ITEM_TYPE.BlueBox_ROW:
			case ITEM_TYPE.BlueBox_BOMB:
			case ITEM_TYPE.BlueBox_Cross:
				return ITEM_TYPE.BlueBox_ROW;
			case ITEM_TYPE.GreenBox:
			case ITEM_TYPE.GreenBox_COLUMN:
			case ITEM_TYPE.GreenBox_ROW:
			case ITEM_TYPE.GreenBox_BOMB:
			case ITEM_TYPE.GreenBox_Cross:
				return ITEM_TYPE.GreenBox_ROW;
			case ITEM_TYPE.ORANGEBOX:
			case ITEM_TYPE.ORANGEBOX_COLUMN:
			case ITEM_TYPE.ORANGEBOX_ROW:
			case ITEM_TYPE.ORANGEBOX_BOMB:
			case ITEM_TYPE.ORANGEBOX_Cross:
				return ITEM_TYPE.ORANGEBOX_ROW;
			case ITEM_TYPE.PURPLEBOX:
			case ITEM_TYPE.PURPLEBOX_COLUMN:
			case ITEM_TYPE.PURPLEBOX_ROW:
			case ITEM_TYPE.PURPLEBOX_BOMB:
			case ITEM_TYPE.PURPLEBOX_Cross:
				return ITEM_TYPE.PURPLEBOX_ROW;
			case ITEM_TYPE.REDBOX:
			case ITEM_TYPE.REDBOX_COLUMN:
			case ITEM_TYPE.REDBOX_ROW:
			case ITEM_TYPE.REDBOX_BOMB:
			case ITEM_TYPE.REDBOX_Cross:
				return ITEM_TYPE.REDBOX_ROW;
			case ITEM_TYPE.YELLOWBOX:
			case ITEM_TYPE.YELLOWBOX_COLUMN:
			case ITEM_TYPE.YELLOWBOX_ROW:
			case ITEM_TYPE.YELLOWBOX_BOMB:
			case ITEM_TYPE.YELLOWBOX_Cross:
				return ITEM_TYPE.YELLOWBOX_ROW;
			default:
				return ITEM_TYPE.NONE;
			}
		}
		switch (check)
		{
		case ITEM_TYPE.BlueBox:
		case ITEM_TYPE.BlueBox_COLUMN:
		case ITEM_TYPE.BlueBox_ROW:
		case ITEM_TYPE.BlueBox_BOMB:
		case ITEM_TYPE.BlueBox_Cross:
			return ITEM_TYPE.BlueBox_COLUMN;
		case ITEM_TYPE.GreenBox:
		case ITEM_TYPE.GreenBox_COLUMN:
		case ITEM_TYPE.GreenBox_ROW:
		case ITEM_TYPE.GreenBox_BOMB:
		case ITEM_TYPE.GreenBox_Cross:
			return ITEM_TYPE.GreenBox_COLUMN;
		case ITEM_TYPE.ORANGEBOX:
		case ITEM_TYPE.ORANGEBOX_COLUMN:
		case ITEM_TYPE.ORANGEBOX_ROW:
		case ITEM_TYPE.ORANGEBOX_BOMB:
		case ITEM_TYPE.ORANGEBOX_Cross:
			return ITEM_TYPE.ORANGEBOX_COLUMN;
		case ITEM_TYPE.PURPLEBOX:
		case ITEM_TYPE.PURPLEBOX_COLUMN:
		case ITEM_TYPE.PURPLEBOX_ROW:
		case ITEM_TYPE.PURPLEBOX_BOMB:
		case ITEM_TYPE.PURPLEBOX_Cross:
			return ITEM_TYPE.PURPLEBOX_COLUMN;
		case ITEM_TYPE.REDBOX:
		case ITEM_TYPE.REDBOX_COLUMN:
		case ITEM_TYPE.REDBOX_ROW:
		case ITEM_TYPE.REDBOX_BOMB:
		case ITEM_TYPE.REDBOX_Cross:
			return ITEM_TYPE.REDBOX_COLUMN;
		case ITEM_TYPE.YELLOWBOX:
		case ITEM_TYPE.YELLOWBOX_COLUMN:
		case ITEM_TYPE.YELLOWBOX_ROW:
		case ITEM_TYPE.YELLOWBOX_BOMB:
		case ITEM_TYPE.YELLOWBOX_Cross:
			return ITEM_TYPE.YELLOWBOX_COLUMN;
		default:
			return ITEM_TYPE.NONE;
		}
	}

	public bool IsBombBreaker(ITEM_TYPE check)
	{
		if (check == ITEM_TYPE.BlueBox_BOMB || check == ITEM_TYPE.GreenBox_BOMB || check == ITEM_TYPE.ORANGEBOX_BOMB || check == ITEM_TYPE.PURPLEBOX_BOMB || check == ITEM_TYPE.REDBOX_BOMB || check == ITEM_TYPE.YELLOWBOX_BOMB)
		{
			return true;
		}
		return false;
	}

	public bool IsXBreaker(ITEM_TYPE check)
	{
		if (check == ITEM_TYPE.BlueBox_Cross || check == ITEM_TYPE.GreenBox_Cross || check == ITEM_TYPE.ORANGEBOX_Cross || check == ITEM_TYPE.PURPLEBOX_Cross || check == ITEM_TYPE.REDBOX_Cross || check == ITEM_TYPE.YELLOWBOX_Cross)
		{
			return true;
		}
		return false;
	}

	public bool IsColumnBreaker(ITEM_TYPE check)
	{
		if (check == ITEM_TYPE.BlueBox_COLUMN || check == ITEM_TYPE.GreenBox_COLUMN || check == ITEM_TYPE.ORANGEBOX_COLUMN || check == ITEM_TYPE.PURPLEBOX_COLUMN || check == ITEM_TYPE.REDBOX_COLUMN || check == ITEM_TYPE.YELLOWBOX_COLUMN)
		{
			return true;
		}
		return false;
	}

	public bool IsRowBreaker(ITEM_TYPE check)
	{
		if (check == ITEM_TYPE.BlueBox_ROW || check == ITEM_TYPE.GreenBox_ROW || check == ITEM_TYPE.ORANGEBOX_ROW || check == ITEM_TYPE.PURPLEBOX_ROW || check == ITEM_TYPE.REDBOX_ROW || check == ITEM_TYPE.YELLOWBOX_ROW)
		{
			return true;
		}
		return false;
	}

	public bool IsBreaker(ITEM_TYPE check)
	{
		if (IsBombBreaker(check) || IsXBreaker(check) || IsColumnBreaker(check) || IsRowBreaker(check))
		{
			return true;
		}
		return false;
	}

	public ITEM_TYPE GetBombBreaker(ITEM_TYPE check)
	{
		switch (check)
		{
		case ITEM_TYPE.BlueBox:
		case ITEM_TYPE.BlueBox_COLUMN:
		case ITEM_TYPE.BlueBox_ROW:
		case ITEM_TYPE.BlueBox_BOMB:
		case ITEM_TYPE.BlueBox_Cross:
			return ITEM_TYPE.BlueBox_BOMB;
		case ITEM_TYPE.GreenBox:
		case ITEM_TYPE.GreenBox_COLUMN:
		case ITEM_TYPE.GreenBox_ROW:
		case ITEM_TYPE.GreenBox_BOMB:
		case ITEM_TYPE.GreenBox_Cross:
			return ITEM_TYPE.GreenBox_BOMB;
		case ITEM_TYPE.ORANGEBOX:
		case ITEM_TYPE.ORANGEBOX_COLUMN:
		case ITEM_TYPE.ORANGEBOX_ROW:
		case ITEM_TYPE.ORANGEBOX_BOMB:
		case ITEM_TYPE.ORANGEBOX_Cross:
			return ITEM_TYPE.ORANGEBOX_BOMB;
		case ITEM_TYPE.PURPLEBOX:
		case ITEM_TYPE.PURPLEBOX_COLUMN:
		case ITEM_TYPE.PURPLEBOX_ROW:
		case ITEM_TYPE.PURPLEBOX_BOMB:
		case ITEM_TYPE.PURPLEBOX_Cross:
			return ITEM_TYPE.PURPLEBOX_BOMB;
		case ITEM_TYPE.REDBOX:
		case ITEM_TYPE.REDBOX_COLUMN:
		case ITEM_TYPE.REDBOX_ROW:
		case ITEM_TYPE.REDBOX_BOMB:
		case ITEM_TYPE.REDBOX_Cross:
			return ITEM_TYPE.REDBOX_BOMB;
		case ITEM_TYPE.YELLOWBOX:
		case ITEM_TYPE.YELLOWBOX_COLUMN:
		case ITEM_TYPE.YELLOWBOX_ROW:
		case ITEM_TYPE.YELLOWBOX_BOMB:
		case ITEM_TYPE.YELLOWBOX_Cross:
			return ITEM_TYPE.YELLOWBOX_BOMB;
		default:
			return ITEM_TYPE.NONE;
		}
	}

	public ITEM_TYPE GetXBreaker(ITEM_TYPE check)
	{
		switch (check)
		{
		case ITEM_TYPE.BlueBox:
		case ITEM_TYPE.BlueBox_COLUMN:
		case ITEM_TYPE.BlueBox_ROW:
		case ITEM_TYPE.BlueBox_BOMB:
		case ITEM_TYPE.BlueBox_Cross:
			return ITEM_TYPE.BlueBox_Cross;
		case ITEM_TYPE.GreenBox:
		case ITEM_TYPE.GreenBox_COLUMN:
		case ITEM_TYPE.GreenBox_ROW:
		case ITEM_TYPE.GreenBox_BOMB:
		case ITEM_TYPE.GreenBox_Cross:
			return ITEM_TYPE.GreenBox_Cross;
		case ITEM_TYPE.ORANGEBOX:
		case ITEM_TYPE.ORANGEBOX_COLUMN:
		case ITEM_TYPE.ORANGEBOX_ROW:
		case ITEM_TYPE.ORANGEBOX_BOMB:
		case ITEM_TYPE.ORANGEBOX_Cross:
			return ITEM_TYPE.ORANGEBOX_Cross;
		case ITEM_TYPE.PURPLEBOX:
		case ITEM_TYPE.PURPLEBOX_COLUMN:
		case ITEM_TYPE.PURPLEBOX_ROW:
		case ITEM_TYPE.PURPLEBOX_BOMB:
		case ITEM_TYPE.PURPLEBOX_Cross:
			return ITEM_TYPE.PURPLEBOX_Cross;
		case ITEM_TYPE.REDBOX:
		case ITEM_TYPE.REDBOX_COLUMN:
		case ITEM_TYPE.REDBOX_ROW:
		case ITEM_TYPE.REDBOX_BOMB:
		case ITEM_TYPE.REDBOX_Cross:
			return ITEM_TYPE.REDBOX_Cross;
		case ITEM_TYPE.YELLOWBOX:
		case ITEM_TYPE.YELLOWBOX_COLUMN:
		case ITEM_TYPE.YELLOWBOX_ROW:
		case ITEM_TYPE.YELLOWBOX_BOMB:
		case ITEM_TYPE.YELLOWBOX_Cross:
			return ITEM_TYPE.YELLOWBOX_Cross;
		default:
			return ITEM_TYPE.NONE;
		}
	}

	public ITEM_TYPE GetColumnBreaker(ITEM_TYPE check)
	{
		switch (check)
		{
		case ITEM_TYPE.BlueBox:
		case ITEM_TYPE.BlueBox_COLUMN:
		case ITEM_TYPE.BlueBox_ROW:
		case ITEM_TYPE.BlueBox_BOMB:
		case ITEM_TYPE.BlueBox_Cross:
			return ITEM_TYPE.BlueBox_COLUMN;
		case ITEM_TYPE.GreenBox:
		case ITEM_TYPE.GreenBox_COLUMN:
		case ITEM_TYPE.GreenBox_ROW:
		case ITEM_TYPE.GreenBox_BOMB:
		case ITEM_TYPE.GreenBox_Cross:
			return ITEM_TYPE.GreenBox_COLUMN;
		case ITEM_TYPE.ORANGEBOX:
		case ITEM_TYPE.ORANGEBOX_COLUMN:
		case ITEM_TYPE.ORANGEBOX_ROW:
		case ITEM_TYPE.ORANGEBOX_BOMB:
		case ITEM_TYPE.ORANGEBOX_Cross:
			return ITEM_TYPE.ORANGEBOX_COLUMN;
		case ITEM_TYPE.PURPLEBOX:
		case ITEM_TYPE.PURPLEBOX_COLUMN:
		case ITEM_TYPE.PURPLEBOX_ROW:
		case ITEM_TYPE.PURPLEBOX_BOMB:
		case ITEM_TYPE.PURPLEBOX_Cross:
			return ITEM_TYPE.PURPLEBOX_COLUMN;
		case ITEM_TYPE.REDBOX:
		case ITEM_TYPE.REDBOX_COLUMN:
		case ITEM_TYPE.REDBOX_ROW:
		case ITEM_TYPE.REDBOX_BOMB:
		case ITEM_TYPE.REDBOX_Cross:
			return ITEM_TYPE.REDBOX_COLUMN;
		case ITEM_TYPE.YELLOWBOX:
		case ITEM_TYPE.YELLOWBOX_COLUMN:
		case ITEM_TYPE.YELLOWBOX_ROW:
		case ITEM_TYPE.YELLOWBOX_BOMB:
		case ITEM_TYPE.YELLOWBOX_Cross:
			return ITEM_TYPE.YELLOWBOX_COLUMN;
		default:
			return ITEM_TYPE.NONE;
		}
	}

	public ITEM_TYPE GetRowBreaker(ITEM_TYPE check)
	{
		switch (check)
		{
		case ITEM_TYPE.BlueBox:
		case ITEM_TYPE.BlueBox_COLUMN:
		case ITEM_TYPE.BlueBox_ROW:
		case ITEM_TYPE.BlueBox_BOMB:
		case ITEM_TYPE.BlueBox_Cross:
			return ITEM_TYPE.BlueBox_ROW;
		case ITEM_TYPE.GreenBox:
		case ITEM_TYPE.GreenBox_COLUMN:
		case ITEM_TYPE.GreenBox_ROW:
		case ITEM_TYPE.GreenBox_BOMB:
		case ITEM_TYPE.GreenBox_Cross:
			return ITEM_TYPE.GreenBox_ROW;
		case ITEM_TYPE.ORANGEBOX:
		case ITEM_TYPE.ORANGEBOX_COLUMN:
		case ITEM_TYPE.ORANGEBOX_ROW:
		case ITEM_TYPE.ORANGEBOX_BOMB:
		case ITEM_TYPE.ORANGEBOX_Cross:
			return ITEM_TYPE.ORANGEBOX_ROW;
		case ITEM_TYPE.PURPLEBOX:
		case ITEM_TYPE.PURPLEBOX_COLUMN:
		case ITEM_TYPE.PURPLEBOX_ROW:
		case ITEM_TYPE.PURPLEBOX_BOMB:
		case ITEM_TYPE.PURPLEBOX_Cross:
			return ITEM_TYPE.PURPLEBOX_ROW;
		case ITEM_TYPE.REDBOX:
		case ITEM_TYPE.REDBOX_COLUMN:
		case ITEM_TYPE.REDBOX_ROW:
		case ITEM_TYPE.REDBOX_BOMB:
		case ITEM_TYPE.REDBOX_Cross:
			return ITEM_TYPE.REDBOX_ROW;
		case ITEM_TYPE.YELLOWBOX:
		case ITEM_TYPE.YELLOWBOX_COLUMN:
		case ITEM_TYPE.YELLOWBOX_ROW:
		case ITEM_TYPE.YELLOWBOX_BOMB:
		case ITEM_TYPE.YELLOWBOX_Cross:
			return ITEM_TYPE.YELLOWBOX_ROW;
		default:
			return ITEM_TYPE.NONE;
		}
	}

	public ITEM_TYPE GetCookie(ITEM_TYPE check)
	{
		switch (check)
		{
		case ITEM_TYPE.BlueBox:
		case ITEM_TYPE.BlueBox_COLUMN:
		case ITEM_TYPE.BlueBox_ROW:
		case ITEM_TYPE.BlueBox_BOMB:
		case ITEM_TYPE.BlueBox_Cross:
			return ITEM_TYPE.BlueBox;
		case ITEM_TYPE.GreenBox:
		case ITEM_TYPE.GreenBox_COLUMN:
		case ITEM_TYPE.GreenBox_ROW:
		case ITEM_TYPE.GreenBox_BOMB:
		case ITEM_TYPE.GreenBox_Cross:
			return ITEM_TYPE.GreenBox;
		case ITEM_TYPE.ORANGEBOX:
		case ITEM_TYPE.ORANGEBOX_COLUMN:
		case ITEM_TYPE.ORANGEBOX_ROW:
		case ITEM_TYPE.ORANGEBOX_BOMB:
		case ITEM_TYPE.ORANGEBOX_Cross:
			return ITEM_TYPE.ORANGEBOX;
		case ITEM_TYPE.PURPLEBOX:
		case ITEM_TYPE.PURPLEBOX_COLUMN:
		case ITEM_TYPE.PURPLEBOX_ROW:
		case ITEM_TYPE.PURPLEBOX_BOMB:
		case ITEM_TYPE.PURPLEBOX_Cross:
			return ITEM_TYPE.PURPLEBOX;
		case ITEM_TYPE.REDBOX:
		case ITEM_TYPE.REDBOX_COLUMN:
		case ITEM_TYPE.REDBOX_ROW:
		case ITEM_TYPE.REDBOX_BOMB:
		case ITEM_TYPE.REDBOX_Cross:
			return ITEM_TYPE.REDBOX;
		case ITEM_TYPE.YELLOWBOX:
		case ITEM_TYPE.YELLOWBOX_COLUMN:
		case ITEM_TYPE.YELLOWBOX_ROW:
		case ITEM_TYPE.YELLOWBOX_BOMB:
		case ITEM_TYPE.YELLOWBOX_Cross:
			return ITEM_TYPE.YELLOWBOX;
		default:
			return ITEM_TYPE.NONE;
		}
	}

	public Vector3 GetMousePosition()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void checkNeighbor()
	{
		if (node != null)
		{
			if (node.LeftNeighbor() != null && node.LeftNeighbor().item != null)
			{
				neighborNodeLeft = node.LeftNeighbor();
			}
			if (node.RightNeighbor() != null && node.RightNeighbor().item != null)
			{
				neighborNodeRight = node.RightNeighbor();
			}
			if (node.TopNeighbor() != null && node.TopNeighbor().item != null)
			{
				neighborNodeTop = node.TopNeighbor();
			}
			if (node.BottomNeighbor() != null && node.BottomNeighbor().item != null)
			{
				neighborNodeBottom = node.BottomNeighbor();
			}
			if (node.TopLeftNeighbor() != null && node.TopLeftNeighbor().item != null)
			{
				neighborNodeTopLeft = node.TopLeftNeighbor();
			}
			if (node.TopRightNeighbor() != null && node.TopRightNeighbor().item != null)
			{
				neighborNodeTopRight = node.TopRightNeighbor();
			}
			if (node.BottomLeftNeighbor() != null && node.BottomLeftNeighbor().item != null)
			{
				neighborNodeBottomLeft = node.BottomLeftNeighbor();
			}
			if (node.BottomRightNeighbor() != null && node.BottomRightNeighbor().item != null)
			{
				neighborNodeBottomRight = node.BottomRightNeighbor();
			}
		}
	}

	private void getNeighbor()
	{
		if (neighborNode == null)
		{
			swapItem = this;
			board.touchedItem = swapItem;
			board.swappedItem = swapItem;
		}
		OnStartSwap();
		OnCompleteSwap();
		Reset();
	}

	public void OnStartSwap()
	{
		base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
		AudioManager.instance.SwapAudio();
		board.lockSwap = true;
		board.dropTime = 1;
	}

	public void OnCompleteSwap()
	{
		base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
		int num = ((node.FindMatches() != null) ? node.FindMatches().Count : 0);
		int num2 = ((swapItem.node.FindMatches() != null) ? swapItem.node.FindMatches().Count : 0);
		bool flag = false;
		if (type == ITEM_TYPE.ITEM_COLORCONE && (swapItem.IsCookie() || IsBreaker(swapItem.type) || swapItem.type == ITEM_TYPE.ITEM_COLORCONE))
		{
			flag = true;
		}
		else if (swapItem.type == ITEM_TYPE.ITEM_COLORCONE && (IsCookie() || IsBreaker(type) || type == ITEM_TYPE.ITEM_COLORCONE))
		{
			flag = true;
		}
		if (IsBreaker(type) && IsBreaker(swapItem.type))
		{
			flag = true;
		}
		if (flag)
		{
			RainbowDestroy(this, swapItem);
			TwoColRowBreakerDestroy(this, swapItem);
			TwoBombBreakerDestroy(this, swapItem);
			TwoXBreakerDestory(this, swapItem);
			ColRowBreakerAndBombBreakerDestroy(this, swapItem);
			ColRowBreakerAndXBreakerDestroy(this, swapItem);
			BombBreakerAndXBreakerDestroy(this, swapItem);
			simpleColumnBreaderDestroy(this, swapItem);
			simpleRowBreakerDestroy(this, swapItem);
		}
		else
		{
			if (num == 5)
			{
				next = GetColRowBreaker(type, base.transform.position - swapItem.transform.position);
			}
			else if (num2 == 5)
			{
				swapItem.next = GetColRowBreaker(swapItem.type, base.transform.position - swapItem.transform.position);
			}
			else if (num >= 7)
			{
				next = GetXBreaker(type);
			}
			else if (num2 >= 7)
			{
				swapItem.next = GetXBreaker(swapItem.type);
			}
			else if (num == 6)
			{
				next = GetBombBreaker(type);
			}
			else if (num2 == 6)
			{
				swapItem.next = GetBombBreaker(swapItem.type);
			}
			board.FindMatches();
		}
		Reset();
	}

	public void OnStartSwapBack()
	{
		base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
		AudioManager.instance.SwapBackAudio();
	}

	public void OnCompleteSwapBack()
	{
		base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
		base.transform.position = board.NodeLocalPosition(node.i, node.j);
		Reset();
		StartCoroutine(board.ShowHint());
	}

	public void Reset()
	{
		drag = false;
		neighborNodeBottom = null;
		neighborNodeLeft = null;
		neighborNodeRight = null;
		neighborNodeTop = null;
		neighborNode = null;
		swapItem = null;
	}

	public void GenerateColor(int except)
	{
		List<int> list = new List<int>();
		List<int> usingColors = StageLoader.instance.usingColors;
		for (int i = 0; i < usingColors.Count; i++)
		{
			int num = usingColors[i];
			bool flag = true;
			Node node = null;
			node = this.node.TopNeighbor();
			if (node != null && node.item != null && node.item.color == num)
			{
				flag = false;
			}
			node = this.node.LeftNeighbor();
			if (node != null && node.item != null && node.item.color == num)
			{
				flag = false;
			}
			node = this.node.RightNeighbor();
			if (node != null && node.item != null && node.item.color == num)
			{
				flag = false;
			}
			if (flag && num != except)
			{
				list.Add(num);
			}
		}
		int num2 = usingColors[Random.Range(0, usingColors.Count)];
		if (list.Count > 0)
		{
			num2 = list[Random.Range(0, list.Count)];
		}
		if (num2 == except)
		{
			num2 = num2++ % usingColors.Count;
		}
		color = num2;
		ChangeSpriteAndType(num2);
	}

	public void ChangeSpriteAndType(int itemColor)
	{
		GameObject gameObject = null;
		switch (itemColor)
		{
		case 1:
			gameObject = Resources.Load(Configuration.Item1()) as GameObject;
			type = ITEM_TYPE.BlueBox;
			break;
		case 2:
			gameObject = Resources.Load(Configuration.Item2()) as GameObject;
			type = ITEM_TYPE.GreenBox;
			break;
		case 3:
			gameObject = Resources.Load(Configuration.Item3()) as GameObject;
			type = ITEM_TYPE.ORANGEBOX;
			break;
		case 4:
			gameObject = Resources.Load(Configuration.Item4()) as GameObject;
			type = ITEM_TYPE.PURPLEBOX;
			break;
		case 5:
			gameObject = Resources.Load(Configuration.Item5()) as GameObject;
			type = ITEM_TYPE.REDBOX;
			break;
		case 6:
			gameObject = Resources.Load(Configuration.Item6()) as GameObject;
			type = ITEM_TYPE.YELLOWBOX;
			break;
		}
		if (gameObject != null)
		{
			GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
		}
	}

	public void ChangeToRainbow()
	{
		GameObject gameObject = Resources.Load(Configuration.ItemColorCone()) as GameObject;
		type = ITEM_TYPE.ITEM_COLORCONE;
		color = 0;
		GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
	}

	public void ChangeToGingerbread(ITEM_TYPE check)
	{
		if (node.item.IsGingerbread())
		{
			return;
		}
		Item upperItem = board.GetUpperItem(node);
		if (!(upperItem != null) || !upperItem.IsGingerbread())
		{
			AudioManager.instance.GingerbreadAudio();
			GameObject gameObject = null;
			switch (node.item.type)
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
			if (gameObject != null)
			{
				gameObject.transform.position = node.item.transform.position;
			}
			GameObject gameObject2 = null;
			switch (check)
			{
			case ITEM_TYPE.ROCKET_1:
				gameObject2 = Resources.Load(Configuration.Rocket1()) as GameObject;
				check = ITEM_TYPE.ROCKET_1;
				color = 1;
				break;
			case ITEM_TYPE.ROCKET_2:
				gameObject2 = Resources.Load(Configuration.Rocket2()) as GameObject;
				check = ITEM_TYPE.ROCKET_2;
				color = 2;
				break;
			case ITEM_TYPE.ROCKET_3:
				gameObject2 = Resources.Load(Configuration.Rocket3()) as GameObject;
				check = ITEM_TYPE.ROCKET_3;
				color = 3;
				break;
			case ITEM_TYPE.ROCKET_4:
				gameObject2 = Resources.Load(Configuration.Rocket4()) as GameObject;
				check = ITEM_TYPE.ROCKET_4;
				color = 4;
				break;
			case ITEM_TYPE.ROCKET_5:
				gameObject2 = Resources.Load(Configuration.Rocket5()) as GameObject;
				check = ITEM_TYPE.ROCKET_5;
				color = 5;
				break;
			case ITEM_TYPE.ROCKET_6:
				gameObject2 = Resources.Load(Configuration.Rocket6()) as GameObject;
				check = ITEM_TYPE.ROCKET_6;
				color = 6;
				break;
			}
			if (gameObject2 != null)
			{
				type = check;
				effect = BREAKER_EFFECT.NORMAL;
				GetComponent<SpriteRenderer>().sprite = gameObject2.GetComponent<SpriteRenderer>().sprite;
			}
		}
	}

	public void ChangeToBombBreaker()
	{
		GameObject gameObject = null;
		switch (type)
		{
		case ITEM_TYPE.BlueBox:
			gameObject = Resources.Load(Configuration.Item1Bomb()) as GameObject;
			type = ITEM_TYPE.BlueBox_BOMB;
			break;
		case ITEM_TYPE.GreenBox:
			gameObject = Resources.Load(Configuration.Item2Bomb()) as GameObject;
			type = ITEM_TYPE.GreenBox_BOMB;
			break;
		case ITEM_TYPE.ORANGEBOX:
			gameObject = Resources.Load(Configuration.Item3Bomb()) as GameObject;
			type = ITEM_TYPE.ORANGEBOX_BOMB;
			break;
		case ITEM_TYPE.PURPLEBOX:
			gameObject = Resources.Load(Configuration.Item4Bomb()) as GameObject;
			type = ITEM_TYPE.PURPLEBOX_BOMB;
			break;
		case ITEM_TYPE.REDBOX:
			gameObject = Resources.Load(Configuration.Item5Bomb()) as GameObject;
			type = ITEM_TYPE.REDBOX_BOMB;
			break;
		case ITEM_TYPE.YELLOWBOX:
			gameObject = Resources.Load(Configuration.Item6Bomb()) as GameObject;
			type = ITEM_TYPE.YELLOWBOX_BOMB;
			break;
		}
		if (gameObject != null)
		{
			GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
		}
	}

	public void ChangeToXBreaker()
	{
		GameObject gameObject = null;
		switch (type)
		{
		case ITEM_TYPE.BlueBox:
			gameObject = Resources.Load(Configuration.Item1Cross()) as GameObject;
			type = ITEM_TYPE.BlueBox_Cross;
			break;
		case ITEM_TYPE.GreenBox:
			gameObject = Resources.Load(Configuration.Item2Cross()) as GameObject;
			type = ITEM_TYPE.GreenBox_Cross;
			break;
		case ITEM_TYPE.ORANGEBOX:
			gameObject = Resources.Load(Configuration.Item3Cross()) as GameObject;
			type = ITEM_TYPE.ORANGEBOX_Cross;
			break;
		case ITEM_TYPE.PURPLEBOX:
			gameObject = Resources.Load(Configuration.Item4Cross()) as GameObject;
			type = ITEM_TYPE.PURPLEBOX_Cross;
			break;
		case ITEM_TYPE.REDBOX:
			gameObject = Resources.Load(Configuration.Item5Cross()) as GameObject;
			type = ITEM_TYPE.REDBOX_Cross;
			break;
		case ITEM_TYPE.YELLOWBOX:
			gameObject = Resources.Load(Configuration.Item6Cross()) as GameObject;
			type = ITEM_TYPE.YELLOWBOX_Cross;
			break;
		}
		if (gameObject != null)
		{
			GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
		}
	}

	public void ChangeToColRowBreaker()
	{
		GameObject gameObject = null;
		if (Random.Range(0, 2) == 0)
		{
			switch (type)
			{
			case ITEM_TYPE.BlueBox:
				gameObject = Resources.Load(Configuration.Item1Column()) as GameObject;
				type = ITEM_TYPE.BlueBox_COLUMN;
				break;
			case ITEM_TYPE.GreenBox:
				gameObject = Resources.Load(Configuration.Item2Column()) as GameObject;
				type = ITEM_TYPE.GreenBox_COLUMN;
				break;
			case ITEM_TYPE.ORANGEBOX:
				gameObject = Resources.Load(Configuration.Item3Column()) as GameObject;
				type = ITEM_TYPE.ORANGEBOX_COLUMN;
				break;
			case ITEM_TYPE.PURPLEBOX:
				gameObject = Resources.Load(Configuration.Item4Column()) as GameObject;
				type = ITEM_TYPE.PURPLEBOX_COLUMN;
				break;
			case ITEM_TYPE.REDBOX:
				gameObject = Resources.Load(Configuration.Item5Column()) as GameObject;
				type = ITEM_TYPE.REDBOX_COLUMN;
				break;
			case ITEM_TYPE.YELLOWBOX:
				gameObject = Resources.Load(Configuration.Item6Column()) as GameObject;
				type = ITEM_TYPE.YELLOWBOX_COLUMN;
				break;
			}
		}
		else
		{
			switch (type)
			{
			case ITEM_TYPE.BlueBox:
				gameObject = Resources.Load(Configuration.Item1Row()) as GameObject;
				type = ITEM_TYPE.BlueBox_ROW;
				break;
			case ITEM_TYPE.GreenBox:
				gameObject = Resources.Load(Configuration.Item2Row()) as GameObject;
				type = ITEM_TYPE.GreenBox_ROW;
				break;
			case ITEM_TYPE.ORANGEBOX:
				gameObject = Resources.Load(Configuration.Item3Row()) as GameObject;
				type = ITEM_TYPE.ORANGEBOX_ROW;
				break;
			case ITEM_TYPE.PURPLEBOX:
				gameObject = Resources.Load(Configuration.Item4Row()) as GameObject;
				type = ITEM_TYPE.PURPLEBOX_ROW;
				break;
			case ITEM_TYPE.REDBOX:
				gameObject = Resources.Load(Configuration.Item5Row()) as GameObject;
				type = ITEM_TYPE.REDBOX_ROW;
				break;
			case ITEM_TYPE.YELLOWBOX:
				gameObject = Resources.Load(Configuration.Item6Row()) as GameObject;
				type = ITEM_TYPE.YELLOWBOX_ROW;
				break;
			}
		}
		if (gameObject != null)
		{
			GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
		}
	}

	public void SetRandomNextType()
	{
		switch (Random.Range(0, 2))
		{
		case 0:
			switch (type)
			{
			case ITEM_TYPE.BlueBox:
				next = ITEM_TYPE.BlueBox_COLUMN;
				break;
			case ITEM_TYPE.GreenBox:
				next = ITEM_TYPE.GreenBox_COLUMN;
				break;
			case ITEM_TYPE.ORANGEBOX:
				next = ITEM_TYPE.ORANGEBOX_COLUMN;
				break;
			case ITEM_TYPE.PURPLEBOX:
				next = ITEM_TYPE.PURPLEBOX_COLUMN;
				break;
			case ITEM_TYPE.REDBOX:
				next = ITEM_TYPE.REDBOX_COLUMN;
				break;
			case ITEM_TYPE.YELLOWBOX:
				next = ITEM_TYPE.YELLOWBOX_COLUMN;
				break;
			}
			break;
		case 1:
			switch (type)
			{
			case ITEM_TYPE.BlueBox:
				next = ITEM_TYPE.BlueBox_ROW;
				break;
			case ITEM_TYPE.GreenBox:
				next = ITEM_TYPE.GreenBox_ROW;
				break;
			case ITEM_TYPE.ORANGEBOX:
				next = ITEM_TYPE.ORANGEBOX_ROW;
				break;
			case ITEM_TYPE.PURPLEBOX:
				next = ITEM_TYPE.PURPLEBOX_ROW;
				break;
			case ITEM_TYPE.REDBOX:
				next = ITEM_TYPE.REDBOX_ROW;
				break;
			case ITEM_TYPE.YELLOWBOX:
				next = ITEM_TYPE.YELLOWBOX_ROW;
				break;
			}
			break;
		case 2:
			switch (type)
			{
			case ITEM_TYPE.BlueBox:
				next = ITEM_TYPE.BlueBox_BOMB;
				break;
			case ITEM_TYPE.GreenBox:
				next = ITEM_TYPE.GreenBox_BOMB;
				break;
			case ITEM_TYPE.ORANGEBOX:
				next = ITEM_TYPE.ORANGEBOX_BOMB;
				break;
			case ITEM_TYPE.PURPLEBOX:
				next = ITEM_TYPE.PURPLEBOX_BOMB;
				break;
			case ITEM_TYPE.REDBOX:
				next = ITEM_TYPE.REDBOX_BOMB;
				break;
			case ITEM_TYPE.YELLOWBOX:
				next = ITEM_TYPE.YELLOWBOX_BOMB;
				break;
			}
			break;
		}
	}

	public void Destroy(bool forced = false)
	{
		if ((Destroyable() || forced) && !destroying)
		{
			destroying = true;
			if (node != null && node.cage != null)
			{
				node.CageExplode();
				return;
			}
			board.destroyingItems++;

			iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.zero, "onstart", "OnStartDestroy", "oncomplete", "OnCompleteDestroy", "easetype", iTween.EaseType.easeInBack, "time", Configuration.instance.destroyTime));
		}
	}

	public void OnStartDestroy()
	{
		if (node != null)
		{
			node.WaffleExplode();
		}
		board.CollectItem(this);
		board.DestroyNeighborItems(this);
		if (effect == BREAKER_EFFECT.BIG_COLUMN_BREAKER)
		{
			BigColumnBreakerExplosion();
		}
		else if (effect == BREAKER_EFFECT.BIG_ROW_BREAKER)
		{
			BigRowBreakerExplosion();
		}
		else if (effect == BREAKER_EFFECT.CROSS)
		{
			CrossBreakerExplosion();
		}
		else if (effect == BREAKER_EFFECT.BOMB_X_BREAKER)
		{
			BombXBreakerExplosion();
		}
		else if (effect == BREAKER_EFFECT.CROSS_X_BREAKER)
		{
			CrossXBreakerExplosion();
		}
		else if (effect == BREAKER_EFFECT.COLUMN_EFFECT)
		{
			col_BreakerExplosion();
		}
		else if (effect == BREAKER_EFFECT.ROW_EFFECT)
		{
			row_BreakerExplosion();
		}
		else if (effect == BREAKER_EFFECT.NORMAL)
		{
			if (IsCookie())
			{
				CookieExplosion();
			}
			else if (IsGingerbread())
			{
				GingerbreadExplosion();
			}
			else if (IsMarshmallow())
			{
				MarshmallowExplosion();
			}
			else if (IsChocolate())
			{
				ChocolateExplosion();
			}
			else if (IsRockCandy())
			{
				RockCandyExplosion();
			}
			else if (IsCollectible())
			{
				CollectibleExplosion();
			}
			else if (IsBombBreaker(type))
			{
				BombBreakerExplosion();
			}
			else if (IsXBreaker(type))
			{
				XBreakerExplosion();
			}
			else if (type == ITEM_TYPE.ITEM_COLORCONE)
			{
				RainbowExplosion();
			}
			else if (IsColumnBreaker(type))
			{
				ColumnBreakerExplosion();
			}
			else if (IsRowBreaker(type))
			{
				RowBreakerExplosion();
			}
		}
	}

	public void OnCompleteDestroy()
	{
		if (board.state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
		{
			board.score += Configuration.instance.finishedScoreItem * board.dropTime;
		}
		else
		{
			board.score += Configuration.instance.scoreItem * board.dropTime;
		}
		board.UITop.UpdateProgressBar(board.score);
		board.UITop.UpdateScoreAmount(board.score);
		if (next != 0)
		{
			if (IsBombBreaker(next) || IsXBreaker(next))
			{
				if (nextSound)
				{
					AudioManager.instance.BombBreakerAudio();
				}
			}
			else if (IsRowBreaker(next) || IsColumnBreaker(next))
			{
				if (nextSound)
				{
					AudioManager.instance.ColRowBreakerAudio();
				}
			}
			else if (next == ITEM_TYPE.ITEM_COLORCONE && nextSound)
			{
				AudioManager.instance.RainbowAudio();
			}
			node.GenerateItem(next);
		}
		else if (type == ITEM_TYPE.MINE_2_LAYER)
		{
			node.GenerateItem(ITEM_TYPE.MINE_1_LAYER);
			board.GetNode(node.i, node.j).item.gameObject.transform.position = board.NodeLocalPosition(node.i, node.j);
		}
		else if (type == ITEM_TYPE.MINE_3_LAYER)
		{
			node.GenerateItem(ITEM_TYPE.MINE_2_LAYER);
			board.GetNode(node.i, node.j).item.gameObject.transform.position = board.NodeLocalPosition(node.i, node.j);
		}
		else if (type == ITEM_TYPE.MINE_4_LAYER)
		{
			node.GenerateItem(ITEM_TYPE.MINE_3_LAYER);
			board.GetNode(node.i, node.j).item.gameObject.transform.position = board.NodeLocalPosition(node.i, node.j);
		}
		else
		{
			node.item = null;
		}
		if (destroying)
		{
			board.destroyingItems--;
			if (dropping)
			{
				board.droppingItems--;
			}
			Object.Destroy(base.gameObject);
		}
	}

	public IEnumerator ResetDestroying()
	{
		yield return new WaitForSeconds(Configuration.instance.destroyTime);
		destroying = false;
	}

	private void CookieExplosion()
	{
		AudioManager.instance.CookieCrushAudio();
		GameObject gameObject = null;
		switch (type)
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
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
	}

	private void GingerbreadExplosion()
	{
		AudioManager.instance.GingerbreadExplodeAudio();
		GameObject gameObject = null;
		switch (type)
		{
		case ITEM_TYPE.ROCKET_1:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BlueBoxExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCKET_2:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.GreenBoxExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCKET_3:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.OrangeCookieExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCKET_4:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.PurpleCookieExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCKET_5:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RedCookieExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCKET_6:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.YellowCookieExplosion()) as GameObject);
			break;
		}
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
	}

	private void MarshmallowExplosion()
	{
		AudioManager.instance.MarshmallowExplodeAudio();
		GameObject gameObject = null;
		gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakableExplosion()) as GameObject);
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
	}

	public void ChocolateExplosion()
	{
		AudioManager.instance.ChocolateExplodeAudio();
		GameObject gameObject = null;
		gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.MineExplosion()) as GameObject);
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
	}

	public void RockCandyExplosion()
	{
		AudioManager.instance.RockCandyExplodeAudio();
		GameObject gameObject = null;
		switch (type)
		{
		case ITEM_TYPE.ROCK_CANDY_1:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BlueBoxExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCK_CANDY_2:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.GreenBoxExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCK_CANDY_3:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.OrangeCookieExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCK_CANDY_4:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.PurpleCookieExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCK_CANDY_5:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RedCookieExplosion()) as GameObject);
			break;
		case ITEM_TYPE.ROCK_CANDY_6:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.YellowCookieExplosion()) as GameObject);
			break;
		}
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
	}

	private void CollectibleExplosion()
	{
		AudioManager.instance.CollectibleExplodeAudio();
	}

	private void BombBreakerExplosion()
	{
		AudioManager.instance.BombExplodeAudio();
		BombBreakerDestroy();
		GameObject gameObject = null;
		switch (type)
		{
		case ITEM_TYPE.BlueBox_BOMB:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakerExplosion1()) as GameObject);
			break;
		case ITEM_TYPE.GreenBox_BOMB:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakerExplosion2()) as GameObject);
			break;
		case ITEM_TYPE.ORANGEBOX_BOMB:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakerExplosion3()) as GameObject);
			break;
		case ITEM_TYPE.PURPLEBOX_BOMB:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakerExplosion4()) as GameObject);
			break;
		case ITEM_TYPE.REDBOX_BOMB:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakerExplosion5()) as GameObject);
			break;
		case ITEM_TYPE.YELLOWBOX_BOMB:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakerExplosion6()) as GameObject);
			break;
		}
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -12f);
		}
	}

	private void RainbowExplosion()
	{
		AudioManager.instance.RainbowExplodeAudio();
		GameObject nextObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);
		if (nextObject != null)
		{
			nextObject.transform.position = base.transform.position;
		}
	}

	private void XBreakerExplosion()
	{
		AudioManager.instance.ColRowBreakerExplodeAudio();
		XBreakerDestroy();
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		GameObject gameObject3 = null;
		switch (GetCookie(type))
		{
		case ITEM_TYPE.BlueBox:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.GreenBox:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker2()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation2()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.ORANGEBOX:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker3()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation3()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.PURPLEBOX:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker4()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation4()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.REDBOX:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker5()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation5()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.YELLOWBOX:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker6()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation6()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		}
		if (gameObject2 != null)
		{
			gameObject3 = Object.Instantiate(gameObject2);
			gameObject2.transform.Rotate(Vector3.back, 45f);
			gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, -12f);
		}
		if (gameObject3 != null)
		{
			gameObject3.transform.Rotate(Vector3.back, -45f);
			gameObject3.transform.position = new Vector3(gameObject3.transform.position.x, gameObject3.transform.position.y, -12f);
		}
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
		Object.Destroy(gameObject2, 1f);
	}

	private void ColumnBreakerExplosion()
	{
		AudioManager.instance.ColRowBreakerExplodeAudio();
		ColumnDestroy();
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		switch (type)
		{
		case ITEM_TYPE.BlueBox_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.GreenBox_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker2()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation2()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.ORANGEBOX_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker3()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation3()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.PURPLEBOX_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker4()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation4()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.REDBOX_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker5()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation5()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.YELLOWBOX_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker6()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation6()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		}
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
		if (gameObject2 != null)
		{
			gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, -12f);
		}
		Object.Destroy(gameObject2, 1f);
	}

	private void BigColumnBreakerExplosion()
	{
		AudioManager.instance.ColRowBreakerExplodeAudio();
		ColumnDestroy(node.j - 1);
		ColumnDestroy(node.j);
		ColumnDestroy(node.j + 1);
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		switch (type)
		{
		case ITEM_TYPE.BlueBox_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation1()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.GreenBox_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker2()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation2()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.ORANGEBOX_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker3()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation3()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.PURPLEBOX_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker4()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation4()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.REDBOX_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker5()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation5()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.YELLOWBOX_COLUMN:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker6()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation6()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		}
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
		if (gameObject2 != null)
		{
			gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, -12f);
		}
		Object.Destroy(gameObject2, 1f);
	}

	private void CrossBreakerExplosion()
	{
		AudioManager.instance.ColRowBreakerExplodeAudio();
		ColumnDestroy();
		RowDestroy();
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		GameObject gameObject3 = null;
		switch (GetCookie(type))
		{
		case ITEM_TYPE.BlueBox:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.GreenBox:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker2()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation2()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.ORANGEBOX:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker3()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation3()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.PURPLEBOX:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker4()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation4()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.REDBOX:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker5()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation5()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.YELLOWBOX:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker6()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation6()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		}
		if (gameObject2 != null)
		{
			gameObject3 = Object.Instantiate(gameObject2, base.transform.position, Quaternion.identity);
			gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, -12f);
		}
		if (gameObject3 != null)
		{
			gameObject3.transform.Rotate(Vector3.back, 90f);
			gameObject3.transform.position = new Vector3(gameObject3.transform.position.x, gameObject3.transform.position.y, -12f);
		}
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
		Object.Destroy(gameObject3, 1f);
		Object.Destroy(gameObject2, 1f);
	}

	private void RowBreakerExplosion()
	{
		AudioManager.instance.ColRowBreakerExplodeAudio();
		RowDestroy();
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		switch (type)
		{
		case ITEM_TYPE.BlueBox_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.GreenBox_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker2()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation2()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.ORANGEBOX_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker3()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation3()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.PURPLEBOX_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker4()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation4()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.REDBOX_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker5()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation5()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.YELLOWBOX_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker6()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation6()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		}
		if (gameObject2 != null)
		{
			gameObject2.transform.Rotate(Vector3.back, 90f);
			gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, -12f);
		}
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
		Object.Destroy(gameObject2, 1f);
	}

	private void BigRowBreakerExplosion()
	{
		AudioManager.instance.ColRowBreakerExplodeAudio();
		RowDestroy(node.i - 1);
		RowDestroy(node.i);
		RowDestroy(node.i + 1);
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		switch (type)
		{
		case ITEM_TYPE.BlueBox_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation1()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.GreenBox_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker2()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation2()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.ORANGEBOX_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker3()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation3()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.PURPLEBOX_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker4()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation4()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.REDBOX_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker5()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation5()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		case ITEM_TYPE.YELLOWBOX_ROW:
			gameObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker6()) as GameObject);
			gameObject2 = Object.Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation6()) as GameObject, base.transform.position, Quaternion.identity);
			break;
		}
		if (gameObject2 != null)
		{
			gameObject2.transform.Rotate(Vector3.back, 90f);
			gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, -12f);
		}
		if (gameObject != null)
		{
			gameObject.transform.position = base.transform.position;
		}
		Object.Destroy(gameObject2, 1f);
	}

	private void BombXBreakerExplosion()
	{
		BombBreakerExplosion();
		XBreakerExplosion();
	}

	private void CrossXBreakerExplosion()
	{
		CrossBreakerExplosion();
		XBreakerExplosion();
	}

	private void col_BreakerExplosion()
	{
		ColumnBreakerExplosion();
	}

	private void row_BreakerExplosion()
	{
		RowBreakerExplosion();
	}

	private void BombBreakerDestroy()
	{
		List<Item> list = board.ItemAround(node);
		foreach (Item item in list)
		{
			if (item != null)
			{
				if (item.type == ITEM_TYPE.ITEM_COLORCONE)
				{
					DestroyItemsSameColor(StageLoader.instance.RandomColor());
				}
				item.Destroy();
			}
		}
	}

	private void XBreakerDestroy()
	{
		List<Item> list = board.XCrossItems(node);
		foreach (Item item in list)
		{
			if (item != null)
			{
				if (item.type == ITEM_TYPE.ITEM_COLORCONE)
				{
					DestroyItemsSameColor(StageLoader.instance.RandomColor());
				}
				item.Destroy();
			}
		}
	}

	public void DestroyItemsSameColor(int color)
	{
		List<Item> listItems = board.GetListItems();
		foreach (Item item in listItems)
		{
			if (item != null && item.color == color)
			{
				board.sameColorList.Add(item);
			}
		}
		board.DestroySameColorList();
	}

	public void RainbowDestroy(Item thisItem, Item otherItem)
	{
		if (!thisItem.Destroyable() || !otherItem.Destroyable())
		{
			return;
		}
		if (thisItem.type == ITEM_TYPE.ITEM_COLORCONE)
		{
			if (otherItem.IsCookie())
			{
				DestroyItemsSameColor(otherItem.color);
				thisItem.Destroy();
			}
			else if (otherItem.IsBombBreaker(otherItem.type) || otherItem.IsRowBreaker(otherItem.type) || otherItem.IsColumnBreaker(otherItem.type) || otherItem.IsXBreaker(otherItem.type))
			{
				ChangeItemsType(otherItem.color, otherItem.type);
				thisItem.Destroy();
			}
			else if (otherItem.type == ITEM_TYPE.ITEM_COLORCONE)
			{
				board.DoubleRainbowDestroy();
				thisItem.Destroy();
				otherItem.Destroy();
			}
		}
		else if (otherItem.type == ITEM_TYPE.ITEM_COLORCONE)
		{
			if (thisItem.IsCookie())
			{
				DestroyItemsSameColor(thisItem.color);
				otherItem.Destroy();
			}
			else if (thisItem.IsBombBreaker(thisItem.type) || thisItem.IsRowBreaker(thisItem.type) || thisItem.IsColumnBreaker(thisItem.type) || thisItem.IsXBreaker(thisItem.type))
			{
				ChangeItemsType(thisItem.color, thisItem.type);
				otherItem.Destroy();
			}
			else if (thisItem.type == ITEM_TYPE.ITEM_COLORCONE)
			{
				board.DoubleRainbowDestroy();
				thisItem.Destroy();
				otherItem.Destroy();
			}
		}
	}

	private void ColumnDestroy(int col = -1)
	{
		List<Item> list = new List<Item>();
		list = ((col != -1) ? board.ColumnItems(col) : board.ColumnItems(node.j));
		foreach (Item item in list)
		{
			if (item != null)
			{
				if (item.type == ITEM_TYPE.ITEM_COLORCONE)
				{
					DestroyItemsSameColor(StageLoader.instance.RandomColor());
				}
				item.Destroy();
			}
		}
	}

	public void RowDestroy(int row = -1)
	{
		List<Item> list = new List<Item>();
		list = ((row != -1) ? board.RowItems(row) : board.RowItems(node.i));
		foreach (Item item in list)
		{
			if (item != null)
			{
				if (item.type == ITEM_TYPE.ITEM_COLORCONE)
				{
					DestroyItemsSameColor(StageLoader.instance.RandomColor());
				}
				item.Destroy();
			}
		}
	}

	private void TwoColRowBreakerDestroy(Item thisItem, Item otherItem)
	{
		if (!(thisItem == null) && !(otherItem == null) && (IsRowBreaker(thisItem.type) || IsColumnBreaker(thisItem.type)) && (IsRowBreaker(otherItem.type) || IsColumnBreaker(otherItem.type)))
		{
			thisItem.effect = BREAKER_EFFECT.CROSS;
			otherItem.effect = BREAKER_EFFECT.NONE;
			thisItem.Destroy();
			otherItem.Destroy();
			board.FindMatches();
		}
	}

	private void simpleColumnBreaderDestroy(Item thisItem, Item otherItem)
	{
		if (!(thisItem == null) && !(otherItem == null) && IsColumnBreaker(thisItem.type))
		{
			thisItem.effect = BREAKER_EFFECT.COLUMN_EFFECT;
			board.FindMatches();
		}
	}

	private void simpleRowBreakerDestroy(Item thisItem, Item otherItem)
	{
		if (!(thisItem == null) && !(otherItem == null) && IsRowBreaker(thisItem.type))
		{
			thisItem.effect = BREAKER_EFFECT.ROW_EFFECT;
			board.FindMatches();
		}
	}

	private void TwoBombBreakerDestroy(Item thisItem, Item otherItem)
	{
		if (!(thisItem == null) && !(otherItem == null) && IsBombBreaker(thisItem.type) && IsBombBreaker(otherItem.type))
		{
			thisItem.Destroy();
			otherItem.Destroy();
			board.FindMatches();
		}
	}

	private void TwoXBreakerDestory(Item thisItem, Item otherItem)
	{
		if (!(thisItem == null) && !(otherItem == null) && IsXBreaker(thisItem.type) && IsXBreaker(otherItem.type))
		{
			thisItem.Destroy();
			otherItem.Destroy();
			board.FindMatches();
		}
	}

	private void ColRowBreakerAndBombBreakerDestroy(Item thisItem, Item otherItem)
	{
		if (thisItem == null || otherItem == null)
		{
			return;
		}
		if ((IsRowBreaker(thisItem.type) || IsColumnBreaker(thisItem.type)) && IsBombBreaker(otherItem.type))
		{
			if (IsRowBreaker(thisItem.type))
			{
				thisItem.effect = BREAKER_EFFECT.BIG_ROW_BREAKER;
			}
			else if (IsColumnBreaker(thisItem.type))
			{
				thisItem.effect = BREAKER_EFFECT.BIG_COLUMN_BREAKER;
			}
			otherItem.type = otherItem.GetCookie(otherItem.type);
			thisItem.ChangeToBig();
		}
		else if ((IsRowBreaker(otherItem.type) || IsColumnBreaker(otherItem.type)) && IsBombBreaker(thisItem.type))
		{
			if (IsRowBreaker(otherItem.type))
			{
				otherItem.effect = BREAKER_EFFECT.BIG_ROW_BREAKER;
			}
			else if (IsColumnBreaker(otherItem.type))
			{
				otherItem.effect = BREAKER_EFFECT.BIG_COLUMN_BREAKER;
			}
			thisItem.type = otherItem.GetCookie(otherItem.type);
			otherItem.ChangeToBig();
		}
	}

	private void ColRowBreakerAndXBreakerDestroy(Item thisItem, Item otherItem)
	{
		if (!(thisItem == null) && !(otherItem == null) && ((IsXBreaker(thisItem.type) && (IsColumnBreaker(otherItem.type) || IsRowBreaker(otherItem.type))) || (IsXBreaker(otherItem.type) && (IsColumnBreaker(thisItem.type) || IsRowBreaker(thisItem.type)))))
		{
			thisItem.effect = BREAKER_EFFECT.CROSS_X_BREAKER;
			otherItem.type = otherItem.GetCookie(otherItem.type);
			thisItem.Destroy();
			otherItem.Destroy();
			board.FindMatches();
		}
	}

	private void BombBreakerAndXBreakerDestroy(Item thisItem, Item otherItem)
	{
		if (!(thisItem == null) && !(otherItem == null) && ((IsBombBreaker(thisItem.type) && IsXBreaker(otherItem.type)) || (IsBombBreaker(otherItem.type) && IsXBreaker(thisItem.type))))
		{
			thisItem.effect = BREAKER_EFFECT.BOMB_X_BREAKER;
			otherItem.type = otherItem.GetCookie(otherItem.type);
			thisItem.Destroy();
			otherItem.Destroy();
			board.FindMatches();
		}
	}

	private void ChangeItemsType(int color, ITEM_TYPE changeToType)
	{
		List<Item> listItems = board.GetListItems();
		foreach (Item item in listItems)
		{
			if (!(item != null) || item.color != color || !item.IsCookie())
			{
				continue;
			}
			GameObject nextObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);
			if (nextObject != null)
			{
				nextObject.transform.position = item.transform.position;
			}
			if (item.IsColumnBreaker(changeToType) || item.IsRowBreaker(changeToType))
			{
				if (item.IsCookie())
				{
					item.ChangeToColRowBreaker();
				}
			}
			else if (item.IsBombBreaker(changeToType))
			{
				if (item.IsCookie())
				{
					item.ChangeToBombBreaker();
				}
			}
			else if (item.IsXBreaker(changeToType) && item.IsCookie())
			{
				item.ChangeToXBreaker();
			}
			board.changingList.Add(item);
		}
		board.DestroyChangingList();
	}

	private void ChangeToBig()
	{
		if (!changing)
		{
			changing = true;
			GetComponent<SpriteRenderer>().sortingLayerName = "Effect";
			iTween.ScaleTo(base.gameObject, iTween.Hash("scale", new Vector3(2.5f, 2.5f, 0f), "oncomplete", "CompleteChangeToBig", "easetype", iTween.EaseType.easeInOutBack, "time", Configuration.instance.changingTime));
		}
	}

	private void CompleteChangeToBig()
	{
		Destroy();
		board.FindMatches();
	}

	public void Drop()
	{
		if (dropping)
		{
			return;
		}
		dropping = true;
		if (dropPath.Count > 1)
		{
			board.droppingItems++;
			float num = base.transform.position.y - dropPath[0].y;
			float num2 = (base.transform.position.y - dropPath[dropPath.Count - 1].y) / board.NodeSize();
			while (num > 0.1f)
			{
				num -= board.NodeSize();
				dropPath.Insert(0, dropPath[0] + new Vector3(0f, board.NodeSize(), 0f));
			}
			iTween.MoveTo(base.gameObject, iTween.Hash("path", dropPath.ToArray(), "movetopath", false, "onstart", "OnStartDrop", "oncomplete", "OnCompleteDrop", "easetype", iTween.EaseType.easeOutBack, "time", Configuration.instance.dropTime * num2));
		}
		else
		{
			Vector3 vector = board.NodeLocalPosition(node.i, node.j);
			if (Mathf.Abs(base.transform.position.x - vector.x) > 0.1f || Mathf.Abs(base.transform.position.y - vector.y) > 0.1f)
			{
				board.droppingItems++;
				float num3 = (base.transform.position.y - vector.y) / board.NodeSize();
				iTween.MoveTo(base.gameObject, iTween.Hash("position", vector, "onstart", "OnStartDrop", "oncomplete", "OnCompleteDrop", "easetype", iTween.EaseType.easeOutBounce, "time", Configuration.instance.dropTime * num3));
			}
			else
			{
				dropping = false;
			}
		}
	}

	public void OnStartDrop()
	{
	}

	public void OnCompleteDrop()
	{
		if (dropping)
		{
			AudioManager.instance.DropAudio();
			dropPath.Clear();
			board.droppingItems--;
			dropping = false;
		}
	}
}
