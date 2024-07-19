using System;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using UnityEngine;

public class itemGrid : MonoBehaviour
{
	[Header("Nodes")]
	public List<Node> nodes = new List<Node>();

	[Header("Board variables")]
	public GAME_STATE state;

	public bool lockSwap;

	public int moveLeft;

	public int dropTime;

	public int score;

	public int star;

	public int stars;

	public int target1Left;

	public int target2Left;

	public int target3Left;

	public int target4Left;

	[Header("Booster")]
	public BOOSTER_TYPE booster;

	public List<Item> boosterItems = new List<Item>();

	public Item ovenTouchItem;

	[Header("Check")]
	public int destroyingItems;

	public int droppingItems;

	public int flyingItems;

	public int matching;

	[Header("collectable items")]
	public bool movingGingerbread;

	public bool generatingGingerbread;

	public bool skipGenerateGingerbread;

	public bool showingInspiringPopup;

	public int skipGingerbreadCount;

	[Header("Item Lists")]
	public List<Item> changingList;

	public List<Item> sameColorList;

	[Header("Swap")]
	public Item touchedItem;

	public Item swappedItem;

	[Header("UI")]
	public GameObject target1;

	public GameObject target2;

	public GameObject target3;

	public GameObject target4;

	public UITarget UITarget;

	public UITop UITop;

	[Header("Popup")]
	public PopupOpener targetPopup;

	public PopupOpener completedPopup;

	public PopupOpener winPopup;

	public PopupOpener losePopup;

	public PopupOpener noMatchesPopup;

	public PopupOpener plus5MovesPopup;

	public PopupOpener excellentPopup;

	public PopupOpener greatPopup;

	public PopupOpener morePopup;

	[Header("Hint")]
	public int checkHintCall;

	public int showHintCall;

	public List<Item> hintItems = new List<Item>();

	private Vector3 firstNodePosition;

	public GameObject level1;

	public GameObject level2;

	public GameObject level3;

	public GameObject level4;

	public GameObject level5;

	public GameObject level6;

	public GameObject level7;

	public GameObject level8;

	public GameObject level9;

	public GameObject level10;

	public GameObject level11;

	public GameObject level12;

	public GameObject level13;

	public GameObject level14;

	public GameObject level15;

	public GameObject level16;

	public GameObject level17;

	public GameObject level18;

	public GameObject BackMusic;

	private void Start()
	{
		if (CoreData.instance.GetOpendedLevel() >= 12)
		{
			AdManager.ShowBannerAd(BannerAdPosition.Bottom);
			AdManager.LoadInterstitialAd();
		}
		state = GAME_STATE.PREPARING_LEVEL;
		moveLeft = StageLoader.instance.moves;
		target1Left = StageLoader.instance.target1Amount;
		target2Left = StageLoader.instance.target2Amount;
		target3Left = StageLoader.instance.target3Amount;
		target4Left = StageLoader.instance.target4Amount;
		GenerateBoard();
		BeginBooster();
		TargetPopup();
		Configuration.instance.passLevelCounter++;
		if (CoreData.instance.GetOpendedLevel() == 1)
		{
			level1.SetActive(true);
			level13.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 2)
		{
			level2.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 3)
		{
			level3.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 4)
		{
			level4.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 5)
		{
			level5.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 6)
		{
			level6.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 9)
		{
			level7.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 15)
		{
			level8.SetActive(true);
			level16.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 18)
		{
			level9.SetActive(true);
			level17.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 27)
		{
			level10.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 29)
		{
			level11.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 40)
		{
			level12.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 7)
		{
			level14.SetActive(true);
		}
		if (CoreData.instance.GetOpendedLevel() == 12)
		{
			level15.SetActive(true);
		}
	}

	private void Update()
	{
		if (!UISettings.isclick || state != GAME_STATE.WAITING_USER_SWAP || lockSwap || moveLeft <= 0 || Configuration.instance.touchIsSwallowed)
		{
			return;
		}
		if (booster == BOOSTER_TYPE.NONE)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Collider2D collider2D = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
				if (collider2D != null)
				{
					Item component = collider2D.gameObject.GetComponent<Item>();
					if (component != null)
					{
						component.drag = true;
						component.mousePostion = component.GetMousePosition();
						component.deltaPosition = Vector3.zero;
						movingGingerbread = false;
						generatingGingerbread = false;
						skipGenerateGingerbread = false;
					}
				}
			}
			else
			{
				if (!Input.GetMouseButtonUp(0))
				{
					return;
				}
				Collider2D collider2D2 = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
				if (collider2D2 != null)
				{
					Item component2 = collider2D2.gameObject.GetComponent<Item>();
					if (component2 != null)
					{
						component2.drag = false;
					}
				}
			}
		}
		else
		{
			if (!Input.GetMouseButtonDown(0))
			{
				return;
			}
			Collider2D collider2D3 = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			if (collider2D3 != null)
			{
				Item component3 = collider2D3.gameObject.GetComponent<Item>();
				if (component3 != null)
				{
					DestroyBoosterItems(component3);
				}
			}
		}

	}

	private void GenerateBoard()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				int num = NodeOrder(i, j);
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.NodePrefab())) as GameObject;
				gameObject.transform.SetParent(base.gameObject.transform, false);
				gameObject.name = "Node " + num;
				gameObject.GetComponent<Node>().grid = this;
				gameObject.GetComponent<Node>().i = i;
				gameObject.GetComponent<Node>().j = j;
				nodes.Add(gameObject.GetComponent<Node>());
			}
		}
		GenerateTileLayer();
		GenerateTileBorder();
		GeneratebreakableLayer();
		GenerateItemLayer();
		GenerateCageLayer();
		GenerateCollectibleBoxByColumn();
		GenerateCollectibleBoxByNode();
	}

	private void GenerateTileLayer()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				int index = NodeOrder(i, j);
				List<TILE_TYPE> tileLayerData = StageLoader.instance.tileLayerData;
				GameObject gameObject = null;
				switch (tileLayerData[index])
				{
				case TILE_TYPE.NONE:
					gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.NoneTilePrefab())) as GameObject;
					break;
				case TILE_TYPE.PASS_THROUGH:
					gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.NoneTilePrefab())) as GameObject;
					break;
				case TILE_TYPE.LIGHT_TILE:
					gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.LightTilePrefab())) as GameObject;
					break;
				case TILE_TYPE.DARD_TILE:
					gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.DarkTilePrefab())) as GameObject;
					break;
				}
				if ((bool)gameObject)
				{
					gameObject.transform.SetParent(nodes[index].gameObject.transform);
					gameObject.name = "Tile";
					gameObject.transform.localPosition = NodeLocalPosition(i, j);
					gameObject.GetComponent<Tile>().type = tileLayerData[index];
					gameObject.GetComponent<Tile>().node = nodes[index];
					nodes[index].tile = gameObject.GetComponent<Tile>();
				}
			}
		}
	}

	private void GenerateTileBorder()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				int index = NodeOrder(i, j);
				nodes[index].tile.SetBorder();
			}
		}
	}

	private void GeneratebreakableLayer()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				int index = NodeOrder(i, j);
				List<WAFFLE_TYPE> breakableLayerData = StageLoader.instance.breakableLayerData;
				GameObject gameObject = null;
				switch (breakableLayerData[index])
				{
				case WAFFLE_TYPE.WAFFLE_1:
					gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.Waffle1())) as GameObject;
					break;
				case WAFFLE_TYPE.WAFFLE_2:
					gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.Waffle2())) as GameObject;
					break;
				case WAFFLE_TYPE.WAFFLE_3:
					gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.Waffle3())) as GameObject;
					break;
				}
				if ((bool)gameObject)
				{
					gameObject.transform.SetParent(nodes[index].gameObject.transform);
					gameObject.name = "Waffle";
					gameObject.transform.localPosition = NodeLocalPosition(i, j);
					gameObject.GetComponent<Waffle>().type = breakableLayerData[index];
					gameObject.GetComponent<Waffle>().node = nodes[index];
					nodes[index].waffle = gameObject.GetComponent<Waffle>();
				}
			}
		}
	}

	private void GenerateItemLayer()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				int index = NodeOrder(i, j);
				List<ITEM_TYPE> itemLayerData = StageLoader.instance.itemLayerData;
				if (nodes[index].CanStoreItem())
				{
					nodes[index].GenerateItem(itemLayerData[index]);
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.Mask())) as GameObject;
					gameObject.transform.SetParent(nodes[index].transform);
					gameObject.transform.localPosition = NodeLocalPosition(i, j);
					gameObject.name = "Mask";
				}
			}
		}
	}

	private void GenerateCageLayer()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				int index = NodeOrder(i, j);
				List<LOCK_TYPE> lockLayerData = StageLoader.instance.lockLayerData;
				GameObject gameObject = null;
				LOCK_TYPE lOCK_TYPE = lockLayerData[index];
				if (lOCK_TYPE == LOCK_TYPE.LOCK_1)
				{
					gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.Lock1())) as GameObject;
				}
				if ((bool)gameObject)
				{
					gameObject.transform.SetParent(nodes[index].gameObject.transform);
					gameObject.name = "Lock";
					gameObject.transform.localPosition = NodeLocalPosition(i, j);
					gameObject.GetComponent<Cage>().type = lockLayerData[index];
					gameObject.GetComponent<Cage>().node = nodes[index];
					nodes[index].cage = gameObject.GetComponent<Cage>();
				}
			}
		}
	}

	private void GenerateCollectibleBoxByColumn()
	{
		if (StageLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
		{
			return;
		}
		int row = StageLoader.instance.row;
		foreach (int collectibleCollectColumnMarker in StageLoader.instance.collectibleCollectColumnMarkers)
		{
			Node node = GetNode(row - 1, collectibleCollectColumnMarker);
			if (node != null && node.CanStoreItem())
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.CollectibleBox())) as GameObject;
				if ((bool)gameObject)
				{
					gameObject.transform.SetParent(node.gameObject.transform);
					gameObject.name = "Box";
					gameObject.transform.localPosition = NodeLocalPosition(node.i, node.j) + new Vector3(0f, -1f * NodeSize() + 0.2f, 0f);
				}
			}
		}
	}

	private void GenerateCollectibleBoxByNode()
	{
		if (StageLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
		{
			return;
		}
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				int item = NodeOrder(i, j);
				if (!StageLoader.instance.collectibleCollectNodeMarkers.Contains(item))
				{
					continue;
				}
				Node node = GetNode(i, j);
				if (node != null)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(Configuration.CollectibleBox())) as GameObject;
					if ((bool)gameObject)
					{
						gameObject.transform.SetParent(node.gameObject.transform);
						gameObject.name = "Box";
						gameObject.transform.localPosition = NodeLocalPosition(node.i, node.j) + new Vector3(0f, -1f * NodeSize() + 0.2f, 0f);
					}
				}
			}
		}
	}

	private void BeginBooster()
	{
		if (Configuration.instance.beginFiveMoves)
		{
			CoreData.instance.SaveBeginFiveMoves(CoreData.instance.GetBeginFiveMoves() - 1);
			moveLeft += Configuration.instance.plusMoves;
		}
		if (Configuration.instance.beginRainbow)
		{
			Configuration.instance.beginRainbow = false;
			CoreData.instance.SaveBeginRainbow(CoreData.instance.GetBeginRainbow() - 1);
			List<Item> listItems = GetListItems();
			List<Item> list = new List<Item>();
			foreach (Item item3 in listItems)
			{
				if (item3 != null && item3.IsCookie() && item3.Movable())
				{
					list.Add(item3);
				}
			}
			Item item = list[UnityEngine.Random.Range(0, list.Count)];
			item.ChangeToRainbow();
		}
		if (!Configuration.instance.beginBombBreaker)
		{
			return;
		}
		Configuration.instance.beginBombBreaker = false;
		CoreData.instance.SaveBeginBombBreaker(CoreData.instance.GetBeginBombBreaker() - 1);
		List<Item> listItems2 = GetListItems();
		List<Item> list2 = new List<Item>();
		foreach (Item item4 in listItems2)
		{
			if (item4 != null && item4.IsCookie() && item4.Movable())
			{
				list2.Add(item4);
			}
		}
		Item item2 = list2[UnityEngine.Random.Range(0, list2.Count)];
		item2.ChangeToBombBreaker();
	}

	private Vector3 CalculateFirstNodePosition()
	{
		float num = NodeSize();
		float num2 = NodeSize();
		int column = StageLoader.instance.column;
		int row = StageLoader.instance.row;
		Vector3 vector = new Vector3(0f, -1f, 0f);
		return new Vector3(0f - (float)(column - 1) * num / 2f, (float)(row - 1) * num2 / 2f, 0f) + vector;
	}

	public float NodeSize()
	{
		//return 0.96f;
        return 0.98f;
    }

	public Vector3 NodeLocalPosition(int i, int j)
	{
		float num = NodeSize();
		float num2 = NodeSize();
		if (firstNodePosition == Vector3.zero)
		{
			firstNodePosition = CalculateFirstNodePosition();
		}
		float x = firstNodePosition.x + (float)j * num;
		float y = firstNodePosition.y - (float)i * num2;
		return new Vector3(x, y, 0f);
	}

	public int NodeOrder(int i, int j)
	{
		return i * StageLoader.instance.column + j;
	}

	public Node GetNode(int row, int column)
	{
		if (row < 0 || row >= StageLoader.instance.row || column < 0 || column >= StageLoader.instance.column)
		{
			return null;
		}
		return nodes[row * StageLoader.instance.column + column];
	}

	private Vector3 ColumnFirstItemPosition(int i, int j)
	{
		Node node = GetNode(i, j);
		if (node != null)
		{
			Item item = node.item;
			if (item != null)
			{
				return item.gameObject.transform.position;
			}
			return ColumnFirstItemPosition(i + 1, j);
		}
		return Vector3.zero;
	}

	public List<Item> GetListItems()
	{
		List<Item> list = new List<Item>();
		foreach (Node node in nodes)
		{
			if (node != null)
			{
				list.Add(node.item);
			}
		}
		return list;
	}

	public void helpclose()
	{
		level1.SetActive(false);
		level2.SetActive(false);
		level3.SetActive(false);
		level4.SetActive(false);
		level5.SetActive(false);
		level6.SetActive(false);
		level7.SetActive(false);
		level8.SetActive(false);
		level9.SetActive(false);
		level10.SetActive(false);
		level11.SetActive(false);
		level12.SetActive(false);
		level13.SetActive(false);
		level14.SetActive(false);
		level15.SetActive(false);
		level16.SetActive(false);
		level17.SetActive(false);
		level18.SetActive(false);
	}

	private void GenerateNoMatches()
	{
		List<List<Item>> matches = GetMatches();
		foreach (List<Item> item in matches)
		{
			int num = 0;
			foreach (Item item2 in item)
			{
				if (item2 != null && item2.OriginCookieType() == ITEM_TYPE.ITEM_RAMDOM)
				{
					item2.GenerateColor(item2.color + num);
					num++;
				}
			}
		}
		matches = GetMatches();
	}

	public List<List<Item>> GetMatches(FIND_DIRECTION direction = FIND_DIRECTION.NONE, int matches = 2)
	{
		List<List<Item>> list = new List<List<Item>>();
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				if (GetNode(i, j) != null)
				{
					List<Item> list2 = GetNode(i, j).FindMatches(direction, matches);
					if (list2 != null && list2.Count >= matches)
					{
						list.Add(list2);
					}
				}
			}
		}
		return list;
	}

	public void FindMatches()
	{
		StartCoroutine(DestroyMatches());
	}

	private IEnumerator DestroyMatches()
	{
		matching++;
		List<List<Item>> combines = GetMatches();
		helpclose();
		bool movecheck = true;
		foreach (List<Item> item in combines)
		{
			if (item.Count == 3 && item.Count > 3)
			{
				SetBombBreakerOrXBreakerCombine(GetMatches(FIND_DIRECTION.ROW));
				SetBombBreakerOrXBreakerCombine(GetMatches(FIND_DIRECTION.COLUMN));
			}
			else if (item.Count == 6)
			{
				SetColRowBreakerCombine(item);
			}
			else if (item.Count >= 10)
			{
				SetRainbowCombine(item);
			}
			foreach (Item item2 in item)
			{
				if (!item.Contains(item2.swapItem))
				{
					continue;
				}
				if (movecheck)
				{
					moveLeft--;
					UITop.DecreaseMoves(true);
					movecheck = false;
				}
				foreach (Item item3 in item)
				{
					item3.Destroy();
				}
			}
		}
		while (destroyingItems > 0)
		{
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForEndOfFrame();
		Drop();
		while (droppingItems > 0)
		{
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForEndOfFrame();
		if (!CollectCollectible())
		{
		}
		dropTime++;
		while (flyingItems > 0)
		{
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForEndOfFrame();
		if (matching > 1)
		{
			matching--;
			yield break;
		}
		if (state == GAME_STATE.WAITING_USER_SWAP)
		{
			if (moveLeft > 0)
			{
				if (IsLevelCompleted())
				{
					StartCoroutine(PreWinAutoPlay());
					AudioManager.instance.GingerbreadExplodeAudio();
				}
				else
				{
					yield return new WaitForSeconds(Configuration.instance.swapTime);
					yield return new WaitForSeconds(0.2f);
					FindMatches();
					if (GenerateGingerbread())
					{
						yield return new WaitForSeconds(0.2f);
						FindMatches();
					}
					StartCoroutine(CheckHint());
				}
			}
			else if (moveLeft == 0)
			{
				if (IsLevelCompleted())
				{
					SaveLevelInfo();
					state = GAME_STATE.OPENING_POPUP;
					winPopup.OpenPopup();
					AudioManager.instance.PopupWinAudio();
					AudioManager.instance.GingerbreadExplodeAudio();
					BackMusic.SetActive(false);
				}
				else
				{
					state = GAME_STATE.OPENING_POPUP;
					losePopup.OpenPopup();
					if (AdManager.IsInterstitialAdReady() && CoreData.instance.GetOpendedLevel() >= 12)
					{
						AdManager.ShowInterstitialAd();
						Debug.Log("REKLAM");
					}
				}
			}
		}
		matching--;
		if (dropTime >= Configuration.instance.encouragingPopup && state == GAME_STATE.WAITING_USER_SWAP && !showingInspiringPopup && combines.Count >= 4)
		{
			ShowInspiringPopup();
		}
		yield return new WaitForSeconds(0.2f);
		lockSwap = false;
	}

	private void Drop()
	{
		SetDropTargets();
		GenerateNewItems(true, Vector3.zero);
		Move();
		DropItems();
		dropTime = 0;
	}

	private void SetDropTargets()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < column; i++)
		{
			for (int num = row - 1; num >= 0; num--)
			{
				Node node = GetNode(num, i);
				if (node != null)
				{
					Item item = node.item;
					if (item != null && item.Movable())
					{
						Node node2 = node.BottomNeighbor();
						if (node2 != null && node2.CanGoThrough())
						{
							if (node2.item == null)
							{
								for (int j = num + 2; j < row; j++)
								{
									if (GetNode(j, i) != null)
									{
										if (GetNode(j, i).item == null && GetNode(j, i).CanStoreItem())
										{
											node2 = GetNode(j, i);
										}
										if (!GetNode(j, i).CanGoThrough() || (GetNode(j, i).item != null && !GetNode(j, i).item.Movable()))
										{
											break;
										}
									}
								}
							}
							if (node2.item == null && node2.CanStoreItem())
							{
								node2.item = item;
								node2.item.gameObject.transform.SetParent(node2.gameObject.transform);
								node2.item.node = node2;
								node.item = null;
							}
						}
					}
				}
			}
		}
	}

	private void GenerateNewItems(bool IsDrop, Vector3 pos)
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		bool flag = false;
		for (int i = 0; i < column; i++)
		{
			int num = -1;
			Vector3 zero = Vector3.zero;
			for (int num2 = row - 1; num2 >= 0; num2--)
			{
				if (GetNode(num2, i) != null && GetNode(num2, i).item == null && GetNode(num2, i).CanGenerateNewItem())
				{
					bool flag2 = false;
					if (num2 == 0 && CheckGenerateCollectible() != null && CheckGenerateCollectible().Count > 0 && (StageLoader.instance.collectibleGenerateMarkers.Contains(i) || StageLoader.instance.collectibleGenerateMarkers.Count == 0))
					{
						flag2 = true;
					}
					bool flag3 = false;
					if (CheckGenerateMarshmallow())
					{
						flag3 = true;
					}
					if (pos != Vector3.zero)
					{
						zero = pos + Vector3.up * NodeSize();
					}
					else
					{
						if (num2 > num)
						{
							num = num2;
						}
						int num3 = 0;
						for (int j = 0; j < row; j++)
						{
							Node node = GetNode(j, i);
							if (node != null && node.tile != null && node.tile.type == TILE_TYPE.PASS_THROUGH)
							{
								num3++;
								continue;
							}
							break;
						}
						zero = NodeLocalPosition(num2, i) + Vector3.up * (num - num3 + 1) * NodeSize();
					}
					if (flag2 && UnityEngine.Random.Range(0, 2) == 1)
					{
						GetNode(num2, i).GenerateItem(CheckGenerateCollectible()[UnityEngine.Random.Range(0, CheckGenerateCollectible().Count)]);
					}
					else if (flag3 && UnityEngine.Random.Range(0, 2) == 1 && !flag)
					{
						flag = true;
						GetNode(num2, i).GenerateItem(ITEM_TYPE.BREAKABLE);
					}
					else
					{
						GetNode(num2, i).GenerateItem(ITEM_TYPE.ITEM_RAMDOM);
					}
					Item item = GetNode(num2, i).item;
					if (item != null)
					{
						if (IsDrop)
						{
							item.gameObject.transform.position = zero;
						}
						else
						{
							item.gameObject.transform.position = NodeLocalPosition(num2, i);
						}
					}
				}
			}
		}
	}

	private void Move()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int num = row - 1; num >= 0; num--)
		{
			for (int i = 0; i < column; i++)
			{
				Node node = GetNode(num, i);
				if (!(node != null) || !(node.item == null) || !node.CanStoreItem())
				{
					continue;
				}
				Node sourceNode = node.GetSourceNode();
				if (sourceNode != null)
				{
					Vector3 pos = ColumnFirstItemPosition(0, sourceNode.j);
					List<Vector3> movePath = node.GetMovePath();
					if (sourceNode.transform.position != NodeLocalPosition(sourceNode.i, sourceNode.j))
					{
						movePath.Add(NodeLocalPosition(sourceNode.i, sourceNode.j));
					}
					node.item = sourceNode.item;
					node.item.gameObject.transform.SetParent(node.gameObject.transform);
					node.item.node = node;
					sourceNode.item = null;
					if (movePath.Count > 1)
					{
						movePath.Reverse();
						node.item.dropPath = movePath;
					}
					SetDropTargets();
					GenerateNewItems(true, pos);
				}
			}
		}
	}

	private void DropItems()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < column; i++)
		{
			for (int num = row - 1; num >= 0; num--)
			{
				if (GetNode(num, i) != null && GetNode(num, i).item != null)
				{
					GetNode(num, i).item.Drop();
				}
			}
		}
	}

	public void SetBombBreakerOrXBreakerCombine(List<List<Item>> lists)
	{
		foreach (List<Item> list2 in lists)
		{
			foreach (Item item in list2)
			{
				if (!(item != null) || !(item.node != null) || item.node.FindMatches(FIND_DIRECTION.COLUMN).Count <= 2 || item.next != 0)
				{
					continue;
				}
				List<Item> list = item.node.FindMatches(FIND_DIRECTION.COLUMN);
				Node node = item.node.TopNeighbor();
				Node node2 = item.node.BottomNeighbor();
				if (node != null && node2 != null)
				{
					if (node.item != null && node2.item != null && list.Contains(node.item) && list.Contains(node2.item))
					{
						item.next = item.GetXBreaker(item.type);
						return;
					}
					Node node3 = node.TopNeighbor();
					Node node4 = node2.BottomNeighbor();
					if (node3 != null && node.item != null && node3.item != null && list.Contains(node.item) && list.Contains(node3.item))
					{
						Node node5 = item.node.LeftNeighbor();
						Node node6 = item.node.RightNeighbor();
						if (node5 != null && node6 != null && node5.item != null && node6.item != null && list2.Contains(node5.item) && list2.Contains(node6.item))
						{
							item.next = item.GetXBreaker(item.type);
							return;
						}
					}
					if (node4 != null && node2.item != null && node4.item != null && list.Contains(node2.item) && list.Contains(node4.item))
					{
						Node node7 = item.node.LeftNeighbor();
						Node node8 = item.node.RightNeighbor();
						if (node7 != null && node8 != null && node7.item != null && node8.item != null && list2.Contains(node7.item) && list2.Contains(node8.item))
						{
							item.next = item.GetXBreaker(item.type);
							return;
						}
					}
				}
				item.next = item.GetBombBreaker(item.type);
			}
		}
	}

	public void SetColRowBreakerCombine(List<Item> combine)
	{
		bool flag = false;
		foreach (Item item2 in combine)
		{
			if (item2.next != 0)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			return;
		}
		Item item = null;
		foreach (Item item3 in combine)
		{
			if (item == null)
			{
				item = item3;
			}
			else if (item3.node.OrderOnBoard() < item.node.OrderOnBoard())
			{
				item = item3;
			}
		}
		foreach (Item item4 in combine)
		{
			if ((bool)item.node.RightNeighbor() && item4.node.OrderOnBoard() == item.node.RightNeighbor().OrderOnBoard())
			{
				item.next = item.GetColumnBreaker(item.type);
				break;
			}
			if ((bool)item.node.BottomNeighbor() && item4.node.OrderOnBoard() == item.node.BottomNeighbor().OrderOnBoard())
			{
				item.next = item.GetRowBreaker(item.type);
				break;
			}
		}
	}

	public void SetRainbowCombine(List<Item> combine)
	{
		bool flag = false;
		foreach (Item item2 in combine)
		{
			if (item2.next != 0)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			return;
		}
		Item item = null;
		foreach (Item item3 in combine)
		{
			if (item == null)
			{
				item = item3;
			}
			else if (item3.node.OrderOnBoard() < item.node.OrderOnBoard())
			{
				item = item3;
			}
		}
		foreach (Item item4 in combine)
		{
			if ((bool)item.node.RightNeighbor() && item4.node.OrderOnBoard() == item.node.RightNeighbor().OrderOnBoard())
			{
				combine[2].next = ITEM_TYPE.ITEM_COLORCONE;
				break;
			}
			if ((bool)item.node.BottomNeighbor() && item4.node.OrderOnBoard() == item.node.BottomNeighbor().OrderOnBoard())
			{
				item.next = ITEM_TYPE.ITEM_COLORCONE;
				break;
			}
		}
	}

	public List<Item> ItemAround(Node node)
	{
		List<Item> list = new List<Item>();
		for (int i = node.i - 1; i <= node.i + 1; i++)
		{
			for (int j = node.j - 1; j <= node.j + 1; j++)
			{
				if (GetNode(i, j) != null)
				{
					list.Add(GetNode(i, j).item);
				}
			}
		}
		return list;
	}

	public List<Item> XCrossItems(Node node)
	{
		List<Item> list = new List<Item>();
		int row = StageLoader.instance.row;
		for (int i = 0; i < row; i++)
		{
			if (i < node.i)
			{
				Node node2 = GetNode(i, node.j - (node.i - i));
				Node node3 = GetNode(i, node.j + (node.i - i));
				if (node2 != null && node2.item != null)
				{
					list.Add(node2.item);
				}
				if (node3 != null && node3.item != null)
				{
					list.Add(node3.item);
				}
			}
			else if (i == node.i)
			{
				if (node.item != null)
				{
					list.Add(node.item);
				}
			}
			else if (i > node.i)
			{
				Node node4 = GetNode(i, node.j - (i - node.i));
				Node node5 = GetNode(i, node.j + (i - node.i));
				if (node4 != null && node4.item != null)
				{
					list.Add(node4.item);
				}
				if (node5 != null && node5.item != null)
				{
					list.Add(node5.item);
				}
			}
		}
		return list;
	}

	public List<Item> ColumnItems(int column)
	{
		List<Item> list = new List<Item>();
		int row = StageLoader.instance.row;
		for (int i = 0; i < row; i++)
		{
			if (GetNode(i, column) != null)
			{
				list.Add(GetNode(i, column).item);
			}
		}
		return list;
	}

	public List<Item> RowItems(int row)
	{
		List<Item> list = new List<Item>();
		int column = StageLoader.instance.column;
		for (int i = 0; i < column; i++)
		{
			if (GetNode(row, i) != null)
			{
				list.Add(GetNode(row, i).item);
			}
		}
		return list;
	}

	public void DoubleRainbowDestroy()
	{
		StartCoroutine(DestroyWholeBoard());
	}

	private IEnumerator DestroyWholeBoard()
	{
		int column = StageLoader.instance.column;
		for (int i = 0; i < column; i++)
		{
			List<Item> items = ColumnItems(i);
			foreach (Item item in items)
			{
				if (item != null && item.Destroyable())
				{
					GameObject nextObject = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);
					if (nextObject != null)
					{
						nextObject.transform.position = item.transform.position;
					}
					item.Destroy();
				}
			}
			yield return new WaitForSeconds(0.2f);
		}
		FindMatches();
	}

	public void DestroyChangingList()
	{
		StartCoroutine(StartDestroyChangingList());
	}

	private IEnumerator StartDestroyChangingList()
	{
		GAME_STATE originalState = state;
		state = GAME_STATE.DESTROYING_ITEMS;
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < changingList.Count; i++)
		{
			Item item = changingList[i];
			if (item != null)
			{
				item.Destroy();
			}
			while (destroyingItems > 0)
			{
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForEndOfFrame();
			while (droppingItems > 0)
			{
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForEndOfFrame();
		}
		changingList.Clear();
		state = originalState;
		FindMatches();
	}

	public void DestroySameColorList()
	{
		StartCoroutine(StartDestroySameColorList());
	}

	private IEnumerator StartDestroySameColorList()
	{
		GAME_STATE originalState = state;
		state = GAME_STATE.DESTROYING_ITEMS;
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < sameColorList.Count; i++)
		{
			Item item = sameColorList[i];
			if (item != null && !item.destroying)
			{
				GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);
				if (explosion != null)
				{
					explosion.transform.position = item.transform.position;
				}
				item.Destroy();
				yield return new WaitForSeconds(0.1f);
			}
		}
		sameColorList.Clear();
		state = originalState;
		FindMatches();
	}

	public void DestroyNeighborItems(Item item)
	{
		DestroyMarshmallow(item);
		DestroyChocolate(item);
		DestroyRockCandy(item);
	}

	public void DestroyMarshmallow(Item item)
	{
		if (item.IsMarshmallow() || item.IsCollectible() || item.IsGingerbread() || item.IsChocolate() || item.IsRockCandy() || state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
		{
			return;
		}
		List<Item> list = new List<Item>();
		if (item.node.TopNeighbor() != null && item.node.TopNeighbor().item != null && item.node.TopNeighbor().item.IsMarshmallow())
		{
			list.Add(item.node.TopNeighbor().item);
		}
		if (item.node.RightNeighbor() != null && item.node.RightNeighbor().item != null && item.node.RightNeighbor().item.IsMarshmallow())
		{
			list.Add(item.node.RightNeighbor().item);
		}
		if (item.node.BottomNeighbor() != null && item.node.BottomNeighbor().item != null && item.node.BottomNeighbor().item.IsMarshmallow())
		{
			list.Add(item.node.BottomNeighbor().item);
		}
		if (item.node.LeftNeighbor() != null && item.node.LeftNeighbor().item != null && item.node.LeftNeighbor().item.IsMarshmallow())
		{
			list.Add(item.node.LeftNeighbor().item);
		}
		foreach (Item item2 in list)
		{
			item2.Destroy();
		}
	}

	public void DestroyChocolate(Item item)
	{
		if (item.IsMarshmallow() || item.IsCollectible() || item.IsGingerbread() || item.IsChocolate() || item.IsRockCandy() || state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
		{
			return;
		}
		List<Item> list = new List<Item>();
		if (item.node.TopNeighbor() != null && item.node.TopNeighbor().item != null && item.node.TopNeighbor().item.IsChocolate())
		{
			list.Add(item.node.TopNeighbor().item);
		}
		if (item.node.RightNeighbor() != null && item.node.RightNeighbor().item != null && item.node.RightNeighbor().item.IsChocolate())
		{
			list.Add(item.node.RightNeighbor().item);
		}
		if (item.node.BottomNeighbor() != null && item.node.BottomNeighbor().item != null && item.node.BottomNeighbor().item.IsChocolate())
		{
			list.Add(item.node.BottomNeighbor().item);
		}
		if (item.node.LeftNeighbor() != null && item.node.LeftNeighbor().item != null && item.node.LeftNeighbor().item.IsChocolate())
		{
			list.Add(item.node.LeftNeighbor().item);
		}
		foreach (Item item2 in list)
		{
			item2.Destroy();
		}
	}

	public void DestroyRockCandy(Item item)
	{
		if (item.IsMarshmallow() || item.IsCollectible() || item.IsGingerbread() || item.IsChocolate() || item.IsRockCandy() || state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
		{
			return;
		}
		List<Item> list = new List<Item>();
		if (item.node.TopNeighbor() != null && item.node.TopNeighbor().item != null && item.node.TopNeighbor().item.IsRockCandy() && item.node.TopNeighbor().item.color == item.color)
		{
			list.Add(item.node.TopNeighbor().item);
		}
		if (item.node.RightNeighbor() != null && item.node.RightNeighbor().item != null && item.node.RightNeighbor().item.IsRockCandy() && item.node.RightNeighbor().item.color == item.color)
		{
			list.Add(item.node.RightNeighbor().item);
		}
		if (item.node.BottomNeighbor() != null && item.node.BottomNeighbor().item != null && item.node.BottomNeighbor().item.IsRockCandy() && item.node.BottomNeighbor().item.color == item.color)
		{
			list.Add(item.node.BottomNeighbor().item);
		}
		if (item.node.LeftNeighbor() != null && item.node.LeftNeighbor().item != null && item.node.LeftNeighbor().item.IsRockCandy() && item.node.LeftNeighbor().item.color == item.color)
		{
			list.Add(item.node.LeftNeighbor().item);
		}
		foreach (Item item2 in list)
		{
			item2.Destroy();
		}
	}

	public void CollectItem(Item item)
	{
		GameObject gameObject = null;
		int order = 0;
		if (item.IsCookie())
		{
			if (StageLoader.instance.target1Type == TARGET_TYPE.ITEM && StageLoader.instance.target1Color == item.color && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.ITEM && StageLoader.instance.target2Color == item.color && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.ITEM && StageLoader.instance.target3Color == item.color && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.ITEM && StageLoader.instance.target4Color == item.color && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying Cookie";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject2 = null;
				switch (item.color)
				{
				case 1:
					gameObject2 = Resources.Load(Configuration.Item1()) as GameObject;
					break;
				case 2:
					gameObject2 = Resources.Load(Configuration.Item2()) as GameObject;
					break;
				case 3:
					gameObject2 = Resources.Load(Configuration.Item3()) as GameObject;
					break;
				case 4:
					gameObject2 = Resources.Load(Configuration.Item4()) as GameObject;
					break;
				case 5:
					gameObject2 = Resources.Load(Configuration.Item5()) as GameObject;
					break;
				case 6:
					gameObject2 = Resources.Load(Configuration.Item6()) as GameObject;
					break;
				}
				if (gameObject2 != null)
				{
					spriteRenderer.sprite = gameObject2.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
			}
		}
		else if (item.IsGingerbread())
		{
			if (StageLoader.instance.target1Type == TARGET_TYPE.ROCKET && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.ROCKET && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.ROCKET && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.ROCKET && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying Gingerbread";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer2 = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject3 = Resources.Load(Configuration.RocketGeneric()) as GameObject;
				if (gameObject3 != null)
				{
					spriteRenderer2.sprite = gameObject3.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
			}
		}
		else if (item.IsMarshmallow())
		{
			if (StageLoader.instance.target1Type == TARGET_TYPE.BREAKABLE && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.BREAKABLE && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.BREAKABLE && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.BREAKABLE && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying Marshmallow";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer3 = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject4 = Resources.Load(Configuration.Breakable()) as GameObject;
				if (gameObject4 != null)
				{
					spriteRenderer3.sprite = gameObject4.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
			}
		}
		else if (item.IsChocolate())
		{
			if (StageLoader.instance.target1Type == TARGET_TYPE.TOYMINE && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.TOYMINE && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.TOYMINE && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.TOYMINE && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying Chocolate";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer4 = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject5 = Resources.Load(Configuration.ToyMine1()) as GameObject;
				if (gameObject5 != null)
				{
					spriteRenderer4.sprite = gameObject5.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
			}
		}
		else if (item.IsColumnBreaker(item.type) || item.IsRowBreaker(item.type))
		{
			if (StageLoader.instance.target1Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying Column Row Breaker";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer5 = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject6 = Resources.Load(Configuration.ColumnRowBreaker()) as GameObject;
				if (gameObject6 != null)
				{
					spriteRenderer5.sprite = gameObject6.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
			}
		}
		else if (item.IsBombBreaker(item.type))
		{
			if (StageLoader.instance.target1Type == TARGET_TYPE.BOMB_BREAKER && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.BOMB_BREAKER && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.BOMB_BREAKER && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.BOMB_BREAKER && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying Bomb Breaker";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer6 = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject7 = Resources.Load(Configuration.GenericBombBreaker()) as GameObject;
				if (gameObject7 != null)
				{
					spriteRenderer6.sprite = gameObject7.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
			}
		}
		else if (item.IsXBreaker(item.type))
		{
			if (StageLoader.instance.target1Type == TARGET_TYPE.X_BREAKER && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.X_BREAKER && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.X_BREAKER && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.X_BREAKER && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying X Breaker";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer7 = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject8 = Resources.Load(Configuration.GenericXBreaker()) as GameObject;
				if (gameObject8 != null)
				{
					spriteRenderer7.sprite = gameObject8.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
			}
		}
		else if (item.type == ITEM_TYPE.ITEM_COLORCONE)
		{
			if (StageLoader.instance.target1Type == TARGET_TYPE.COLORCONE && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.COLORCONE && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.COLORCONE && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.COLORCONE && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying Rainbow";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer8 = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject9 = Resources.Load(Configuration.ItemColorCone()) as GameObject;
				if (gameObject9 != null)
				{
					spriteRenderer8.sprite = gameObject9.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
			}
		}
		else
		{
			if (!item.IsRockCandy())
			{
				return;
			}
			if (StageLoader.instance.target1Type == TARGET_TYPE.ROCK_CANDY && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.ROCK_CANDY && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.ROCK_CANDY && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.ROCK_CANDY && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying Rock Candy";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer9 = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject10 = Resources.Load(Configuration.LegoBoxGeneric()) as GameObject;
				if (gameObject10 != null)
				{
					spriteRenderer9.sprite = gameObject10.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
			}
		}
	}

	public void CollectWaffle(Waffle waffle)
	{
		GameObject gameObject = null;
		int order = 0;
		if (StageLoader.instance.target1Type == TARGET_TYPE.WAFFLE && target1Left > 0)
		{
			target1Left--;
			gameObject = new GameObject();
			order = 1;
		}
		else if (StageLoader.instance.target2Type == TARGET_TYPE.WAFFLE && target2Left > 0)
		{
			target2Left--;
			gameObject = new GameObject();
			order = 2;
		}
		else if (StageLoader.instance.target3Type == TARGET_TYPE.WAFFLE && target3Left > 0)
		{
			target3Left--;
			gameObject = new GameObject();
			order = 3;
		}
		else if (StageLoader.instance.target4Type == TARGET_TYPE.WAFFLE && target4Left > 0)
		{
			target4Left--;
			gameObject = new GameObject();
			order = 4;
		}
		if (gameObject != null)
		{
			gameObject.transform.position = waffle.transform.position;
			gameObject.name = "Flying Waffle";
			gameObject.layer = LayerMask.NameToLayer("On Top UI");
			gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0f);
			SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
			GameObject gameObject2 = Resources.Load(Configuration.Waffle1()) as GameObject;
			spriteRenderer.sprite = gameObject2.GetComponent<SpriteRenderer>().sprite;
			StartCoroutine(CollectItemAnim(gameObject, order));
		}
	}

	public void CollectCage(Cage cage)
	{
		GameObject gameObject = null;
		int order = 0;
		if (StageLoader.instance.target1Type == TARGET_TYPE.LOCK && target1Left > 0)
		{
			target1Left--;
			gameObject = new GameObject();
			order = 1;
		}
		else if (StageLoader.instance.target2Type == TARGET_TYPE.LOCK && target2Left > 0)
		{
			target2Left--;
			gameObject = new GameObject();
			order = 2;
		}
		else if (StageLoader.instance.target3Type == TARGET_TYPE.LOCK && target3Left > 0)
		{
			target3Left--;
			gameObject = new GameObject();
			order = 3;
		}
		else if (StageLoader.instance.target4Type == TARGET_TYPE.LOCK && target4Left > 0)
		{
			target4Left--;
			gameObject = new GameObject();
			order = 4;
		}
		if (gameObject != null)
		{
			gameObject.transform.position = cage.transform.position;
			gameObject.name = "Flying Cage";
			gameObject.layer = LayerMask.NameToLayer("On Top UI");
			gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0f);
			SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
			GameObject gameObject2 = Resources.Load(Configuration.Lock1()) as GameObject;
			spriteRenderer.sprite = gameObject2.GetComponent<SpriteRenderer>().sprite;
			StartCoroutine(CollectItemAnim(gameObject, order));
		}
	}

	private bool CollectCollectible()
	{
		if (StageLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
		{
			return false;
		}
		List<Item> listItems = GetListItems();
		foreach (Item item in listItems)
		{
			bool flag = false;
			if (item != null && item.node.i == StageLoader.instance.row - 1 && StageLoader.instance.collectibleCollectColumnMarkers.Contains(item.node.j) && item.IsCollectible())
			{
				flag = true;
			}
			if (item != null && StageLoader.instance.collectibleCollectNodeMarkers.Contains(NodeOrder(item.node.i, item.node.j)) && item.IsCollectible())
			{
				flag = true;
			}
			if (!flag)
			{
				continue;
			}
			GameObject gameObject = null;
			int order = 0;
			if (StageLoader.instance.target1Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target1Color == item.color && target1Left > 0)
			{
				target1Left--;
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target2Color == item.color && target2Left > 0)
			{
				target2Left--;
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target3Color == item.color && target3Left > 0)
			{
				target3Left--;
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target4Color == item.color && target4Left > 0)
			{
				target4Left--;
				gameObject = new GameObject();
				order = 4;
			}
			else if (StageLoader.instance.target1Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target1Color == item.color && target1Left == 0)
			{
				gameObject = new GameObject();
				order = 1;
			}
			else if (StageLoader.instance.target2Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target2Color == item.color && target2Left == 0)
			{
				gameObject = new GameObject();
				order = 2;
			}
			else if (StageLoader.instance.target3Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target3Color == item.color && target3Left == 0)
			{
				gameObject = new GameObject();
				order = 3;
			}
			else if (StageLoader.instance.target4Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target4Color == item.color && target4Left == 0)
			{
				gameObject = new GameObject();
				order = 4;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = item.transform.position;
				gameObject.name = "Flying Collectible";
				gameObject.layer = LayerMask.NameToLayer("On Top UI");
				SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
				GameObject gameObject2 = null;
				switch (item.color)
				{
				case 1:
					gameObject2 = Resources.Load(Configuration.Collectible1()) as GameObject;
					break;
				case 2:
					gameObject2 = Resources.Load(Configuration.Collectible2()) as GameObject;
					break;
				case 3:
					gameObject2 = Resources.Load(Configuration.Collectible3()) as GameObject;
					break;
				case 4:
					gameObject2 = Resources.Load(Configuration.Collectible4()) as GameObject;
					break;
				case 5:
					gameObject2 = Resources.Load(Configuration.Collectible5()) as GameObject;
					break;
				case 6:
					gameObject2 = Resources.Load(Configuration.Collectible6()) as GameObject;
					break;
				case 7:
					gameObject2 = Resources.Load(Configuration.Collectible7()) as GameObject;
					break;
				case 8:
					gameObject2 = Resources.Load(Configuration.Collectible7()) as GameObject;
					break;
				case 9:
					gameObject2 = Resources.Load(Configuration.Collectible9()) as GameObject;
					break;
				case 10:
					gameObject2 = Resources.Load(Configuration.Collectible10()) as GameObject;
					break;
				case 11:
					gameObject2 = Resources.Load(Configuration.Collectible11()) as GameObject;
					break;
				case 12:
					gameObject2 = Resources.Load(Configuration.Collectible12()) as GameObject;
					break;
				case 13:
					gameObject2 = Resources.Load(Configuration.Collectible13()) as GameObject;
					break;
				case 14:
					gameObject2 = Resources.Load(Configuration.Collectible14()) as GameObject;
					break;
				case 15:
					gameObject2 = Resources.Load(Configuration.Collectible15()) as GameObject;
					break;
				case 16:
					gameObject2 = Resources.Load(Configuration.Collectible16()) as GameObject;
					break;
				case 17:
					gameObject2 = Resources.Load(Configuration.Collectible17()) as GameObject;
					break;
				case 18:
					gameObject2 = Resources.Load(Configuration.Collectible18()) as GameObject;
					break;
				case 19:
					gameObject2 = Resources.Load(Configuration.Collectible19()) as GameObject;
					break;
				case 20:
					gameObject2 = Resources.Load(Configuration.Collectible20()) as GameObject;
					break;
				}
				if (gameObject2 != null)
				{
					spriteRenderer.sprite = gameObject2.GetComponent<SpriteRenderer>().sprite;
				}
				StartCoroutine(CollectItemAnim(gameObject, order));
				item.Destroy(true);
				return true;
			}
		}
		return false;
	}

	private IEnumerator CollectItemAnim(GameObject item, int order)
	{
		yield return new WaitForFixedUpdate();
		GameObject target = null;
		switch (order)
		{
		case 1:
			target = target1;
			break;
		case 2:
			target = target2;
			break;
		case 3:
			target = target3;
			break;
		case 4:
			target = target4;
			break;
		}
		flyingItems++;
		AnimationCurve curveX = new AnimationCurve(new Keyframe(0f, item.transform.localPosition.x), new Keyframe(0.4f, target.transform.position.x));
		AnimationCurve curveY = new AnimationCurve(new Keyframe(0f, item.transform.localPosition.y), new Keyframe(0.4f, target.transform.position.y));
		curveX.AddKey(0.12f, item.transform.localPosition.x + UnityEngine.Random.Range(-1f, 2f));
		curveY.AddKey(0.12f, item.transform.localPosition.y + UnityEngine.Random.Range(-1f, 0f));
		float startTime = Time.time;
		float speed = 1.2f + (float)flyingItems * 0.15f;
		float distCovered = 0f;
		while (distCovered < 0.4f)
		{
			distCovered = (Time.time - startTime) / speed;
			item.transform.localPosition = new Vector3(curveX.Evaluate(distCovered), curveY.Evaluate(distCovered), 0f);
			yield return new WaitForFixedUpdate();
		}
		AudioManager.instance.CollectTargetAudio();
		switch (order)
		{
		case 1:
			UITarget.UpdateTargetAmount(1);
			break;
		case 2:
			UITarget.UpdateTargetAmount(2);
			break;
		case 3:
			UITarget.UpdateTargetAmount(3);
			break;
		case 4:
			UITarget.UpdateTargetAmount(4);
			break;
		}
		UnityEngine.Object.Destroy(item);
		flyingItems--;
	}

	private void TargetPopup()
	{
		StartCoroutine(StartTargetPopup());
	}

	private IEnumerator StartTargetPopup()
	{
		state = GAME_STATE.OPENING_POPUP;
		yield return new WaitForSeconds(0.5f);
		AudioManager.instance.PopupTargetAudio();
		targetPopup.OpenPopup();
		yield return new WaitForSeconds(2.5f);
		GameObject popup = GameObject.Find("Target(Clone)");
		if ((bool)popup)
		{
			popup.GetComponent<Popup>().Close();
		}
		yield return new WaitForSeconds(0.5f);
		state = GAME_STATE.WAITING_USER_SWAP;
		StartCoroutine(CheckHint());
		if (Configuration.instance.beginFiveMoves)
		{
			StartCoroutine(Plus5MovesPopup());
		}
	}

	private IEnumerator Plus5MovesPopup()
	{
		Configuration.instance.beginFiveMoves = false;
		plus5MovesPopup.OpenPopup();
		yield return new WaitForSeconds(1f);
		GameObject popup = GameObject.Find("Plus5MovesPopup(Clone)");
		if ((bool)popup)
		{
			popup.GetComponent<Popup>().Close();
		}
	}

	private void ShowInspiringPopup()
	{
		int num = UnityEngine.Random.Range(0, 3);
		switch (num)
		{
		case 0:
			AudioManager.instance.amazingAudio();
			break;
		case 1:
			StartCoroutine(InspiringPopup(excellentPopup, num));
			AudioManager.instance.exellentAudio();
			break;
		case 2:
			StartCoroutine(InspiringPopup(greatPopup, num));
			AudioManager.instance.greatAudio();
			break;
		}
	}

	private IEnumerator InspiringPopup(PopupOpener popupOpener, int encouraging)
	{
		if (!showingInspiringPopup)
		{
			showingInspiringPopup = true;
		}
		else
		{
			yield return null;
		}
		popupOpener.OpenPopup();
		yield return new WaitForSeconds(1f);
		GameObject popup = null;
		switch (encouraging)
		{
		case 1:
			popup = GameObject.Find("ExcellentPopup(Clone)");
			break;
		case 2:
			popup = GameObject.Find("GreatPopup(Clone)");
			break;
		}
		if ((bool)popup)
		{
			popup.GetComponent<Popup>().Close();
		}
		yield return new WaitForSeconds(1f);
		showingInspiringPopup = false;
	}

	private bool IsLevelCompleted()
	{
		if (target1Left == 0 && target2Left == 0 && target3Left == 0 && target4Left == 0)
		{
			return true;
		}
		return false;
	}

	private IEnumerator PreWinAutoPlay()
	{
		HideHint();
		dropTime = 1;
		state = GAME_STATE.OPENING_POPUP;
		yield return new WaitForSeconds(0.5f);
		completedPopup.OpenPopup();
		BackMusic.SetActive(false);
		AudioManager.instance.PopupCompletedAudio();
		AudioManager.instance.PopupCompletedMusicAudio();
		yield return new WaitForSeconds(1f);
		if ((bool)GameObject.Find("CompletedPopup(Clone)"))
		{
			GameObject.Find("CompletedPopup(Clone)").GetComponent<Popup>().Close();
			yield return new WaitForSeconds(0.5f);
			state = GAME_STATE.PRE_WIN_AUTO_PLAYING;
			List<Item> items = GetRandomItems(moveLeft);
			foreach (Item item2 in items)
			{
				item2.SetRandomNextType();
				item2.nextSound = false;
				moveLeft--;
				UITop.DecreaseMoves(true);
				GameObject prefab = UnityEngine.Object.Instantiate(Resources.Load(Configuration.StarGold())) as GameObject;
				prefab.transform.position = UITop.GetComponent<UITop>().movesText.gameObject.transform.position;
				Vector3 startPosition = prefab.transform.position;
				Vector3 endPosition = item2.gameObject.transform.position;
				Vector3 bending = new Vector3(1f, 1f, 0f);
				float timeToTravel = 0.05f;
				float timeStamp = Time.time;
				while (Time.time < timeStamp + timeToTravel)
				{
					Vector3 currentPos = Vector3.Lerp(startPosition, endPosition, (Time.time - timeStamp) / timeToTravel);
					currentPos.x += bending.x * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * (float)Math.PI);
					currentPos.y += bending.y * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * (float)Math.PI);
					currentPos.z += bending.z * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * (float)Math.PI);
					prefab.transform.position = currentPos;
					yield return null;
				}
				UnityEngine.Object.Destroy(prefab);
				item2.Destroy();
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForSeconds(0.1f);
			while (GetAllSpecialItems().Count > 0)
			{
				while (GetAllSpecialItems().Count > 0)
				{
					List<Item> specials = GetAllSpecialItems();
					Item item = specials[UnityEngine.Random.Range(0, specials.Count)];
					if (item.type == ITEM_TYPE.ITEM_COLORCONE)
					{
						item.DestroyItemsSameColor(StageLoader.instance.RandomColor());
					}
					item.Destroy();
					while (destroyingItems > 0)
					{
						yield return new WaitForSeconds(0.1f);
					}
					yield return new WaitForEndOfFrame();
					yield return new WaitForSeconds(0.05f);
					while (droppingItems > 0)
					{
						yield return new WaitForSeconds(0.1f);
					}
					yield return new WaitForEndOfFrame();
				}
				yield return StartCoroutine(DestroyMatches());
			}
			while (destroyingItems > 0)
			{
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForEndOfFrame();
			while (droppingItems > 0)
			{
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForEndOfFrame();
			AudioManager.instance.GetComponent<AudioSource>().Stop();
			if (AdManager.IsInterstitialAdReady() && CoreData.instance.GetOpendedLevel() >= 12)
			{
				AdManager.ShowInterstitialAd();
				Debug.Log("REKLAM");
			}
		}
		yield return new WaitForSeconds(0.5f);
		state = GAME_STATE.OPENING_POPUP;
		SaveLevelInfo();
		AudioManager.instance.PopupWinAudio();
		Debug.Log("Calling Win Popup....");
		PlayerPrefs.SetInt("lastMatchScore", score);
        winPopup.OpenPopup();
	}

	private List<Item> GetRandomItems(int number)
	{
		List<Item> list = new List<Item>();
		List<Item> list2 = new List<Item>();
		foreach (Item listItem in GetListItems())
		{
			if (listItem != null && listItem.node != null && listItem.IsCookie())
			{
				list.Add(listItem);
			}
		}
		while (list2.Count < number && list.Count > 0)
		{
			Item item = list[UnityEngine.Random.Range(0, list.Count)];
			list2.Add(item);
			list.Remove(item);
		}
		return list2;
	}

	private List<Item> GetAllSpecialItems()
	{
		List<Item> list = new List<Item>();
		foreach (Item listItem in GetListItems())
		{
			if (listItem != null && (listItem.type == ITEM_TYPE.ITEM_COLORCONE || listItem.IsColumnBreaker(listItem.type) || listItem.IsRowBreaker(listItem.type) || listItem.IsBombBreaker(listItem.type) || listItem.IsXBreaker(listItem.type)))
			{
				list.Add(listItem);
			}
		}
		return list;
	}

	public void SaveLevelInfo()
	{
		if (score < StageLoader.instance.score_Star_1)
		{
			star = 0;
		}
		else if (StageLoader.instance.score_Star_1 <= score && score < StageLoader.instance.score_Star_2)
		{
			star = 1;
		}
		else if (StageLoader.instance.score_Star_2 <= score && score < StageLoader.instance.score_Star_3)
		{
			star = 2;
		}
		else if (score >= StageLoader.instance.score_Star_2)
		{
			star = 3;
		}
		CoreData.instance.SaveLevelStatistics(StageLoader.instance.Stage, score, star);
		int opendedLevel = CoreData.instance.GetOpendedLevel();
		GameServiceManager.ReportScore(opendedLevel, "level leaderboard");
		if (StageLoader.instance.Stage == opendedLevel && opendedLevel < Configuration.instance.maxLevel)
		{
			CoreData.instance.SaveOpendedLevel(opendedLevel + 1);
		}
		int playerStars = CoreData.instance.GetPlayerStars();
		if (star == 1)
		{
			CoreData.instance.SavePlayerStars(playerStars + 1);
		}
		else if (star == 2)
		{
			CoreData.instance.SavePlayerStars(playerStars + 2);
		}
		else if (star == 3)
		{
			CoreData.instance.SavePlayerStars(playerStars + 3);
		}
		int playerPuan = CoreData.instance.GetPlayerPuan();
		CoreData.instance.SavePlayerPuan(playerPuan + score);
		int toplamScore = CoreData.instance.GetToplamScore();
		CoreData.instance.SaveToplamScore(toplamScore + score);
		int playerCoin = CoreData.instance.GetPlayerCoin();
		if (star == 1)
		{
			CoreData.instance.SavePlayerCoin(playerCoin + Configuration.instance.bonus1Star);
		}
		else if (star == 2)
		{
			CoreData.instance.SavePlayerCoin(playerCoin + Configuration.instance.bonus2Star);
		}
		else if (star == 3)
		{
			CoreData.instance.SavePlayerCoin(playerCoin + Configuration.instance.bonus3Star);
		}
		if (opendedLevel == 10)
		{
			GameServiceManager.UnlockAchievement("Level 10");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 30)
		{
			GameServiceManager.UnlockAchievement("Level 30");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 55)
		{
			GameServiceManager.UnlockAchievement("Level 55");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 70)
		{
			GameServiceManager.UnlockAchievement("Level 70");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 85)
		{
			GameServiceManager.UnlockAchievement("Level 85");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 100)
		{
			GameServiceManager.UnlockAchievement("Level 100");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 130)
		{
			GameServiceManager.UnlockAchievement("Level 130");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 155)
		{
			GameServiceManager.UnlockAchievement("Level 155");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 185)
		{
			GameServiceManager.UnlockAchievement("Level 185");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 200)
		{
			GameServiceManager.UnlockAchievement("Level 200");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 222)
		{
			GameServiceManager.UnlockAchievement("Level 222");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 250)
		{
			GameServiceManager.UnlockAchievement("Level 250");
			AudioManager.instance.giftbuton();
		}
		if (opendedLevel == 270)
		{
			GameServiceManager.UnlockAchievement("Level 270");
			AudioManager.instance.giftbuton();
		}
	}

	public void Hint()
	{
		StartCoroutine(CheckHint());
	}

	public IEnumerator CheckHint()
	{
		checkHintCall++;
		if (checkHintCall > 1)
		{
			checkHintCall--;
		}
		else
		{
			if (!Configuration.instance.showHint || moveLeft <= 0)
			{
				yield break;
			}
			HideHint();
			while (state != GAME_STATE.WAITING_USER_SWAP)
			{
				yield return new WaitForEndOfFrame();
			}
			while (lockSwap)
			{
				yield return new WaitForEndOfFrame();
			}
			if (GetHintByRainbowItem() || GetHintByBreaker() || GetHintByColor())
			{
				StartCoroutine(ShowHint());
				checkHintCall--;
				yield break;
			}
			if (!GameObject.Find("NoMatchesdPopup(Clone)"))
			{
				yield return new WaitForSeconds(3f);
				state = GAME_STATE.NO_MATCHES_REGENERATING;
				lockSwap = true;
				AudioManager.instance.PopupNoMatchesAudio();
				noMatchesPopup.OpenPopup();
				yield return new WaitForEndOfFrame();
				if ((bool)GameObject.Find("NoMatchesdPopup(Clone)"))
				{
					GameObject.Find("NoMatchesdPopup(Clone)").GetComponent<Popup>().Close();
				}
				yield return new WaitForEndOfFrame();
				float position = Camera.main.aspect * Camera.main.orthographicSize * 2f;
				yield return new WaitForEndOfFrame();
				NoMoveRegenerate();
				while (!GetHintByColor())
				{
					NoMoveRegenerate();
					yield return new WaitForEndOfFrame();
				}
				yield return new WaitForEndOfFrame();
				state = GAME_STATE.WAITING_USER_SWAP;
				FindMatches();
			}
			checkHintCall--;
		}
	}

	public IEnumerator ShowHint()
	{
		showHintCall++;
		if (showHintCall > 1)
		{
			showHintCall--;
		}
		else
		{
			if (!Configuration.instance.showHint)
			{
				yield break;
			}
			yield return new WaitForSeconds(Configuration.instance.hintDelay);
			while (state != GAME_STATE.WAITING_USER_SWAP)
			{
				yield return new WaitForSeconds(0.1f);
			}
			while (lockSwap)
			{
				yield return new WaitForSeconds(0.1f);
			}
			foreach (Item listItem in GetListItems())
			{
				if (listItem != null && !hintItems.Contains(listItem))
				{
					iTween.StopByName(listItem.gameObject, "HintAnimation");
					listItem.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				}
			}
			foreach (Item hintItem in hintItems)
			{
				if (hintItem != null)
				{
					iTween.ShakeRotation(hintItem.gameObject, iTween.Hash("name", "HintAnimation", "amount", new Vector3(0f, 0f, 50f), "easetype", iTween.EaseType.easeOutBack, "oncomplete", "OnCompleteShowHint", "oncompletetarget", base.gameObject, "oncompleteparams", new Hashtable { { "item", hintItem } }, "time", 1f));
				}
			}
			if (hintItems.Count > 0)
			{
				yield return new WaitForSeconds(1.5f);
			}
			showHintCall--;
			StartCoroutine(CheckHint());
		}
	}

	public void OnCompleteShowHint(Hashtable args)
	{
		Item item = (Item)args["item"];
		iTween.RotateTo(item.gameObject, iTween.Hash("rotation", Vector3.zero, "time", 0.2f));
	}

	public void HideHint()
	{
		foreach (Item hintItem in hintItems)
		{
			if (hintItem != null)
			{
				iTween.StopByName(hintItem.gameObject, "HintAnimation");
				iTween.RotateTo(hintItem.gameObject, iTween.Hash("rotation", Vector3.zero, "time", 0.2f));
			}
		}
		hintItems.Clear();
	}

	private List<int> Shuffle(List<int> list)
	{
		System.Random random = new System.Random();
		int num = list.Count;
		while (num > 1)
		{
			num--;
			int index = random.Next(num + 1);
			int value = list[index];
			list[index] = list[num];
			list[num] = value;
		}
		return list;
	}

	private void CheckHintNode(Node node, int color, bool needMove = false)
	{
		if (node != null && node.item != null && node.item.color == color)
		{
			if (node.item.Movable() && node.item.Matchable())
			{
				hintItems.Add(node.item);
			}
			else if (node.item.Matchable())
			{
				hintItems.Add(node.item);
			}
		}
	}

	private void NoMoveRegenerate()
	{
		foreach (Item listItem in GetListItems())
		{
			if (listItem != null && listItem.Movable() && listItem.IsCookie())
			{
				listItem.color = StageLoader.instance.RandomColor();
				listItem.ChangeSpriteAndType(listItem.color);
			}
		}
	}

	private bool GetHintByColor()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		foreach (int item in Shuffle(StageLoader.instance.usingColors))
		{
			for (int i = 0; i < column; i++)
			{
				for (int j = 0; j < row; j++)
				{
					Node node = GetNode(j, i);
					if (node != null && !(node.item == null) && node.item.Movable())
					{
						CheckHintNode(GetNode(j, i), item, true);
						CheckHintNode(GetNode(j + 1, i), item);
						if (hintItems.Count == 2)
						{
							return true;
						}
						hintItems.Clear();
						CheckHintNode(GetNode(j - 1, i), item, true);
						CheckHintNode(GetNode(j, i), item);
						if (hintItems.Count == 2)
						{
							return true;
						}
						hintItems.Clear();
						CheckHintNode(GetNode(j, i), item, true);
						CheckHintNode(GetNode(j, i + 1), item);
						if (hintItems.Count == 2)
						{
							return true;
						}
						hintItems.Clear();
						CheckHintNode(GetNode(j, i), item, true);
						CheckHintNode(GetNode(j, i - 1), item);
						if (hintItems.Count == 2)
						{
							return true;
						}
						hintItems.Clear();
					}
				}
			}
		}
		return false;
	}

	private bool GetHintByRainbowItem()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				Node node = GetNode(i, j);
				if (node != null && !(node.item == null) && node.item.Movable() && node.item.type == ITEM_TYPE.ITEM_COLORCONE)
				{
					Node node2 = null;
					node2 = node.LeftNeighbor();
					if (node2 != null && node2.item != null && node2.item.Movable())
					{
						hintItems.Add(node.item);
						return true;
					}
					node2 = node.RightNeighbor();
					if (node2 != null && node2.item != null && node2.item.Movable())
					{
						hintItems.Add(node.item);
						return true;
					}
					node2 = node.TopNeighbor();
					if (node2 != null && node2.item != null && node2.item.Movable())
					{
						hintItems.Add(node.item);
						return true;
					}
					node2 = node.BottomNeighbor();
					if (node2 != null && node2.item != null && node2.item.Movable())
					{
						hintItems.Add(node.item);
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool GetHintByBreaker()
	{
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				Node node = GetNode(i, j);
				if (node != null && !(node.item == null) && node.item.Movable() && node.item.IsBreaker(node.item.type))
				{
					Node node2 = null;
					node2 = node.LeftNeighbor();
					if (node2 != null && node2.item != null && node2.item.Movable() && node2.item.IsBreaker(node2.item.type))
					{
						hintItems.Add(node2.item);
						hintItems.Add(node.item);
						return true;
					}
					node2 = node.RightNeighbor();
					if (node2 != null && node2.item != null && node2.item.Movable() && node2.item.IsBreaker(node2.item.type))
					{
						hintItems.Add(node2.item);
						hintItems.Add(node.item);
						return true;
					}
					node2 = node.TopNeighbor();
					if (node2 != null && node2.item != null && node2.item.Movable() && node2.item.IsBreaker(node2.item.type))
					{
						hintItems.Add(node2.item);
						hintItems.Add(node.item);
						return true;
					}
					node2 = node.BottomNeighbor();
					if (node2 != null && node2.item != null && node2.item.Movable() && node2.item.IsBreaker(node2.item.type))
					{
						hintItems.Add(node2.item);
						hintItems.Add(node.item);
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool GenerateGingerbread()
	{
		if (!IsGingerbreadTarget())
		{
			return false;
		}
		if (skipGenerateGingerbread)
		{
			return false;
		}
		int num = 0;
		for (int i = 1; i <= 4; i++)
		{
			switch (i)
			{
			case 1:
				if (StageLoader.instance.target1Type == TARGET_TYPE.ROCKET)
				{
					num += target1Left;
				}
				break;
			case 2:
				if (StageLoader.instance.target2Type == TARGET_TYPE.ROCKET)
				{
					num += target2Left;
				}
				break;
			case 3:
				if (StageLoader.instance.target3Type == TARGET_TYPE.ROCKET)
				{
					num += target3Left;
				}
				break;
			case 4:
				if (StageLoader.instance.target4Type == TARGET_TYPE.ROCKET)
				{
					num += target4Left;
				}
				break;
			}
		}
		if (num <= 0)
		{
			return false;
		}
		int count = GingerbreadOnBoard().Count;
		if (count >= StageLoader.instance.maxRockettoys)
		{
			return false;
		}
		if (generatingGingerbread)
		{
			return false;
		}
		if (UnityEngine.Random.Range(0, 2) == 0 && skipGingerbreadCount < 2)
		{
			skipGingerbreadCount++;
			return false;
		}
		skipGingerbreadCount = 0;
		generatingGingerbread = true;
		int row = StageLoader.instance.row - 1;
		int column = StageLoader.instance.rocketToysMarkers[UnityEngine.Random.Range(0, StageLoader.instance.rocketToysMarkers.Count)];
		Node node = GetNode(row, column);
		if (node != null && node.item != null)
		{
			node.item.ChangeToGingerbread(StageLoader.instance.RandomRockets());
			return true;
		}
		return false;
	}

	private bool IsGingerbreadTarget()
	{
		if (StageLoader.instance.target1Type == TARGET_TYPE.ROCKET || StageLoader.instance.target2Type == TARGET_TYPE.ROCKET || StageLoader.instance.target3Type == TARGET_TYPE.ROCKET || StageLoader.instance.target4Type == TARGET_TYPE.ROCKET)
		{
			return true;
		}
		return false;
	}

	private List<Item> GingerbreadOnBoard()
	{
		List<Item> list = new List<Item>();
		List<Item> listItems = GetListItems();
		foreach (Item item in listItems)
		{
			if (item != null && item.IsGingerbread())
			{
				list.Add(item);
			}
		}
		return list;
	}

	private bool MoveGingerbread()
	{
		if (!IsGingerbreadTarget())
		{
			return false;
		}
		if (movingGingerbread)
		{
			return false;
		}
		movingGingerbread = true;
		bool result = false;
		foreach (Item item in GingerbreadOnBoard())
		{
			if (item != null)
			{
				Item upperItem = GetUpperItem(item.node);
				if (upperItem != null && upperItem.node != null && !upperItem.IsGingerbread() && item.node.cage == null)
				{
					Vector3 vector = NodeLocalPosition(upperItem.node.i, upperItem.node.j);
					Vector3 vector2 = NodeLocalPosition(item.node.i, item.node.j);
					item.neighborNode = upperItem.node;
					item.swapItem = upperItem;
					touchedItem = item;
					swappedItem = upperItem;
					item.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
					iTween.MoveTo(item.gameObject, iTween.Hash("position", vector, "easetype", iTween.EaseType.linear, "time", Configuration.instance.swapTime));
					iTween.MoveTo(upperItem.gameObject, iTween.Hash("position", vector2, "easetype", iTween.EaseType.linear, "time", Configuration.instance.swapTime));
				}
				else if (upperItem == null || upperItem.node == null)
				{
					AudioManager.instance.GingerbreadExplodeAudio();
					item.color = StageLoader.instance.RandomColor();
					item.ChangeSpriteAndType(item.color);
					skipGenerateGingerbread = true;
				}
				result = true;
			}
		}
		return result;
	}

	public Item GetUpperItem(Node node)
	{
		Node node2 = node.TopNeighbor();
		if (node2 == null)
		{
			return null;
		}
		if (node2.tile.type == TILE_TYPE.NONE || node2.tile.type == TILE_TYPE.PASS_THROUGH)
		{
			return GetUpperItem(node2);
		}
		if (node2.item != null && node2.item.Movable())
		{
			return node2.item;
		}
		return node.item;
	}

	private void DestroyBoosterItems(Item boosterItem)
	{
		if (boosterItem == null)
		{
			return;
		}
		if (boosterItem.Destroyable() && booster != BOOSTER_TYPE.OVEN_BREAKER)
		{
			if (booster == BOOSTER_TYPE.RAINBOW_BREAKER && !boosterItem.IsCookie())
			{
				return;
			}
			lockSwap = true;
			switch (booster)
			{
			case BOOSTER_TYPE.SINGLE_BREAKER:
				DestroySingleBooster(boosterItem);
				break;
			case BOOSTER_TYPE.ROW_BREAKER:
				StartCoroutine(DestroyRowBooster(boosterItem));
				break;
			case BOOSTER_TYPE.COLUMN_BREAKER:
				StartCoroutine(DestroyColumnBooster(boosterItem));
				break;
			case BOOSTER_TYPE.RAINBOW_BREAKER:
				StartCoroutine(DestroyRainbowBooster(boosterItem));
				break;
			}
			Booster.instance.BoosterComplete();
		}
		if (boosterItem.Movable() && booster == BOOSTER_TYPE.OVEN_BREAKER)
		{
			StartCoroutine(DestroyOvenBooster(boosterItem));
		}
	}

	private void DestroySingleBooster(Item boosterItem)
	{
		HideHint();
		AudioManager.instance.SingleBoosterAudio();
		if (boosterItem.type == ITEM_TYPE.ITEM_COLORCONE)
		{
			boosterItem.DestroyItemsSameColor(StageLoader.instance.RandomColor());
		}
		boosterItem.Destroy();
		FindMatches();
	}

	private IEnumerator DestroyRowBooster(Item boosterItem)
	{
		HideHint();
		AudioManager.instance.RowBoosterAudio();
		List<Item> items2 = new List<Item>();
		items2 = RowItems(boosterItem.node.i);
		foreach (Item item in items2)
		{
			if (item != null)
			{
				if (item.type == ITEM_TYPE.ITEM_COLORCONE)
				{
					item.DestroyItemsSameColor(StageLoader.instance.RandomColor());
				}
				item.Destroy();
			}
			yield return new WaitForSeconds(0.1f);
		}
		FindMatches();
	}

	private IEnumerator DestroyColumnBooster(Item boosterItem)
	{
		HideHint();
		AudioManager.instance.ColumnBoosterAudio();
		List<Item> items2 = new List<Item>();
		items2 = ColumnItems(boosterItem.node.j);
		foreach (Item item in items2)
		{
			if (item != null)
			{
				if (item.type == ITEM_TYPE.ITEM_COLORCONE)
				{
					item.DestroyItemsSameColor(StageLoader.instance.RandomColor());
				}
				item.Destroy();
			}
			yield return new WaitForSeconds(0.1f);
		}
		FindMatches();
	}

	private IEnumerator DestroyRainbowBooster(Item boosterItem)
	{
		HideHint();
		AudioManager.instance.RainbowBoosterAudio();
		boosterItem.DestroyItemsSameColor(boosterItem.color);
		yield return new WaitForFixedUpdate();
	}

	private IEnumerator DestroyOvenBooster(Item boosterItem)
	{
		HideHint();
		if (ovenTouchItem == null)
		{
			ovenTouchItem = boosterItem;
			ovenTouchItem.node.AddOvenBoosterActive();
			AudioManager.instance.ButtonClickAudio();
		}
		else if (ovenTouchItem.node.OrderOnBoard() == boosterItem.node.OrderOnBoard())
		{
			ovenTouchItem.node.RemoveOvenBoosterActive();
			ovenTouchItem = null;
			AudioManager.instance.ButtonClickAudio();
		}
		else
		{
			lockSwap = true;
			boosterItem.node.AddOvenBoosterActive();
			AudioManager.instance.OvenBoosterAudio();
			AudioManager.instance.ButtonClickAudio();
			iTween.MoveTo(ovenTouchItem.gameObject, iTween.Hash("position", boosterItem.gameObject.transform.position, "easetype", iTween.EaseType.linear, "time", Configuration.instance.swapTime));
			iTween.MoveTo(boosterItem.gameObject, iTween.Hash("position", ovenTouchItem.gameObject.transform.position, "easetype", iTween.EaseType.linear, "time", Configuration.instance.swapTime));
			yield return new WaitForSeconds(Configuration.instance.swapTime);
			ovenTouchItem.node.RemoveOvenBoosterActive();
			boosterItem.node.RemoveOvenBoosterActive();
			Node ovenTouchNode = ovenTouchItem.node;
			Node boosterItemNode = boosterItem.node;
			ovenTouchNode.item = boosterItem;
			boosterItemNode.item = ovenTouchItem;
			ovenTouchItem.node = boosterItemNode;
			boosterItem.node = ovenTouchNode;
			ovenTouchItem.gameObject.transform.SetParent(boosterItemNode.gameObject.transform);
			boosterItem.gameObject.transform.SetParent(ovenTouchNode.gameObject.transform);
			yield return new WaitForEndOfFrame();
			ovenTouchItem = null;
			Booster.instance.BoosterComplete();
			yield return new WaitForSeconds(0.1f);
			FindMatches();
		}
		yield return new WaitForFixedUpdate();
	}

	private List<ITEM_TYPE> CheckGenerateCollectible()
	{
		if (StageLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
		{
			return null;
		}
		List<ITEM_TYPE> list = new List<ITEM_TYPE>();
		if (CollectibleOnBoard() >= StageLoader.instance.collectibleMaxOnBoard)
		{
			return null;
		}
		for (int i = 0; i <= 4; i++)
		{
			TARGET_TYPE tARGET_TYPE = TARGET_TYPE.NONE;
			int color = 0;
			int num = 0;
			int num2 = 0;
			switch (i)
			{
			case 1:
				tARGET_TYPE = StageLoader.instance.target1Type;
				color = StageLoader.instance.target1Color;
				num = CollectibleOnBoard(StageLoader.instance.target1Color);
				num2 = target1Left;
				break;
			case 2:
				tARGET_TYPE = StageLoader.instance.target2Type;
				color = StageLoader.instance.target2Color;
				num = CollectibleOnBoard(StageLoader.instance.target2Color);
				num2 = target2Left;
				break;
			case 3:
				tARGET_TYPE = StageLoader.instance.target3Type;
				color = StageLoader.instance.target3Color;
				num = CollectibleOnBoard(StageLoader.instance.target3Color);
				num2 = target3Left;
				break;
			case 4:
				tARGET_TYPE = StageLoader.instance.target4Type;
				color = StageLoader.instance.target4Color;
				num = CollectibleOnBoard(StageLoader.instance.target4Color);
				num2 = target4Left;
				break;
			}
			if (tARGET_TYPE == TARGET_TYPE.COLLECTIBLE && num < num2)
			{
				for (int j = 0; j < num2 - num; j++)
				{
					list.Add(ColorToCollectible(color));
				}
			}
		}
		return list;
	}

	private ITEM_TYPE ColorToCollectible(int color)
	{
		switch (color)
		{
		case 1:
			return ITEM_TYPE.COLLECTIBLE_1;
		case 2:
			return ITEM_TYPE.COLLECTIBLE_2;
		case 3:
			return ITEM_TYPE.COLLECTIBLE_3;
		case 4:
			return ITEM_TYPE.COLLECTIBLE_4;
		case 5:
			return ITEM_TYPE.COLLECTIBLE_5;
		case 6:
			return ITEM_TYPE.COLLECTIBLE_6;
		case 7:
			return ITEM_TYPE.COLLECTIBLE_7;
		case 8:
			return ITEM_TYPE.COLLECTIBLE_8;
		case 9:
			return ITEM_TYPE.COLLECTIBLE_9;
		case 10:
			return ITEM_TYPE.COLLECTIBLE_10;
		case 11:
			return ITEM_TYPE.COLLECTIBLE_11;
		case 12:
			return ITEM_TYPE.COLLECTIBLE_12;
		case 13:
			return ITEM_TYPE.COLLECTIBLE_13;
		case 14:
			return ITEM_TYPE.COLLECTIBLE_14;
		case 15:
			return ITEM_TYPE.COLLECTIBLE_15;
		case 16:
			return ITEM_TYPE.COLLECTIBLE_16;
		case 17:
			return ITEM_TYPE.COLLECTIBLE_17;
		case 18:
			return ITEM_TYPE.COLLECTIBLE_18;
		case 19:
			return ITEM_TYPE.COLLECTIBLE_19;
		case 20:
			return ITEM_TYPE.COLLECTIBLE_20;
		default:
			return ITEM_TYPE.NONE;
		}
	}

	private int CollectibleOnBoard(int color = 0)
	{
		int num = 0;
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				Node node = GetNode(i, j);
				if (node != null && node.item != null && node.item.IsCollectible())
				{
					if (color == 0)
					{
						num++;
					}
					else if (node.item.color == color)
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	private bool CheckGenerateMarshmallow()
	{
		if (StageLoader.instance.target1Type != TARGET_TYPE.BREAKABLE && StageLoader.instance.target2Type != TARGET_TYPE.BREAKABLE && StageLoader.instance.target3Type != TARGET_TYPE.BREAKABLE && StageLoader.instance.target4Type != TARGET_TYPE.BREAKABLE)
		{
			return false;
		}
		int num = 0;
		for (int i = 1; i <= 4; i++)
		{
			switch (i)
			{
			case 1:
				if (StageLoader.instance.target1Type == TARGET_TYPE.BREAKABLE)
				{
					num += target1Left;
				}
				break;
			case 2:
				if (StageLoader.instance.target2Type == TARGET_TYPE.BREAKABLE)
				{
					num += target2Left;
				}
				break;
			case 3:
				if (StageLoader.instance.target3Type == TARGET_TYPE.BREAKABLE)
				{
					num += target3Left;
				}
				break;
			case 4:
				if (StageLoader.instance.target4Type == TARGET_TYPE.BREAKABLE)
				{
					num += target4Left;
				}
				break;
			}
		}
		if (num + StageLoader.instance.marshmallowMoreThanTarget <= MarshmallowOnBoard())
		{
			return false;
		}
		return true;
	}

	private int MarshmallowOnBoard()
	{
		int num = 0;
		int row = StageLoader.instance.row;
		int column = StageLoader.instance.column;
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				Node node = GetNode(i, j);
				if (node != null && node.item != null && node.item.IsMarshmallow())
				{
					num++;
				}
			}
		}
		return num;
	}
}
