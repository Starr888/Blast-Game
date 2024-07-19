using UnityEngine;

public class Tile : MonoBehaviour
{
	public TILE_TYPE type;

	public Node node;

    //private float borderPosition = 0.55f;
    private float borderPosition = 0.60f;

    public void SetBorder()
	{
		SetBorderTop();
		SetBorderBottom();
		SetBorderLeft();
		SetBorderRight();
	}

	private void SetBorderTop()
	{
		if (NoTile())
		{
			return;
		}
		string text = string.Empty;
		Node check = node.TopNeighbor();
		if (!TileNode(check))
		{
			text += "top_";
			Node check2 = node.TopLeftNeighbor();
			if (TileNode(check2))
			{
				text += "left_bevel_";
			}
			else
			{
				Node check3 = node.LeftNeighbor();
				text = ((!TileNode(check3)) ? (text + "left_corner_") : (text + "top_"));
			}
			Node check4 = node.TopRightNeighbor();
			if (TileNode(check4))
			{
				text += "right_bevel";
			}
			else
			{
				Node check5 = node.RightNeighbor();
				text = ((!TileNode(check5)) ? (text + "right_corner") : (text + "top"));
			}
		}
		if (text != string.Empty)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load(Configuration.TileBorderTop() + text)) as GameObject;
			gameObject.name = text;
			gameObject.transform.position = base.gameObject.transform.position + new Vector3(0f, borderPosition, 0f);
			gameObject.transform.SetParent(base.gameObject.transform);
		}
	}

	private void SetBorderBottom()
	{
		if (NoTile())
		{
			return;
		}
		string text = string.Empty;
		Node check = node.BottomNeighbor();
		if (!TileNode(check))
		{
			text += "bottom_";
			Node check2 = node.BottomLeftNeighbor();
			if (TileNode(check2))
			{
				text += "left_bevel_";
			}
			else
			{
				Node check3 = node.LeftNeighbor();
				text = ((!TileNode(check3)) ? (text + "left_corner_") : (text + "bottom_"));
			}
			Node check4 = node.BottomRightNeighbor();
			if (TileNode(check4))
			{
				text += "right_bevel";
			}
			else
			{
				Node check5 = node.RightNeighbor();
				text = ((!TileNode(check5)) ? (text + "right_corner") : (text + "bottom"));
			}
		}
		if (text != string.Empty)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load(Configuration.TileBorderBottom() + text)) as GameObject;
			gameObject.name = text;
			gameObject.transform.position = base.gameObject.transform.position + new Vector3(0f, 0f - borderPosition, 0f);
			gameObject.transform.SetParent(base.gameObject.transform);
		}
	}

	private void SetBorderLeft()
	{
		if (NoTile())
		{
			return;
		}
		string text = string.Empty;
		Node check = node.LeftNeighbor();
		if (!TileNode(check))
		{
			text += "left_";
			Node check2 = node.TopLeftNeighbor();
			if (TileNode(check2))
			{
				text += "top_bevel_";
			}
			else
			{
				Node check3 = node.TopNeighbor();
				text = ((!TileNode(check3)) ? (text + "top_corner_") : (text + "left_"));
			}
			Node check4 = node.BottomLeftNeighbor();
			if (TileNode(check4))
			{
				text += "bottom_bevel";
			}
			else
			{
				Node check5 = node.BottomNeighbor();
				text = ((!TileNode(check5)) ? (text + "bottom_corner") : (text + "left"));
			}
		}
		if (text != string.Empty)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load(Configuration.TileBorderLeft() + text)) as GameObject;
			gameObject.name = text;
			gameObject.transform.position = base.gameObject.transform.position + new Vector3(0f - borderPosition, 0f, 0f);
			gameObject.transform.SetParent(base.gameObject.transform);
		}
	}

	private void SetBorderRight()
	{
		if (NoTile())
		{
			return;
		}
		string text = string.Empty;
		Node check = node.RightNeighbor();
		if (!TileNode(check))
		{
			text += "right_";
			Node check2 = node.TopRightNeighbor();
			if (TileNode(check2))
			{
				text += "top_bevel_";
			}
			else
			{
				Node check3 = node.TopNeighbor();
				text = ((!TileNode(check3)) ? (text + "top_corner_") : (text + "right_"));
			}
			Node check4 = node.BottomRightNeighbor();
			if (TileNode(check4))
			{
				text += "bottom_bevel";
			}
			else
			{
				Node check5 = node.BottomNeighbor();
				text = ((!TileNode(check5)) ? (text + "bottom_corner") : (text + "right"));
			}
		}
		if (text != string.Empty)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load(Configuration.TileBorderRight() + text)) as GameObject;
			gameObject.name = text;
			gameObject.transform.position = base.gameObject.transform.position + new Vector3(borderPosition, 0f, 0f);
			gameObject.transform.SetParent(base.gameObject.transform);
		}
	}

	public bool NoTile()
	{
		if (type == TILE_TYPE.NONE || type == TILE_TYPE.PASS_THROUGH)
		{
			return true;
		}
		return false;
	}

	public bool TileNode(Node check)
	{
		if (check != null)
		{
			if (check.tile.type == TILE_TYPE.LIGHT_TILE || check.tile.type == TILE_TYPE.DARD_TILE)
			{
				return true;
			}
			return false;
		}
		return false;
	}
}
