using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SgLib.UI
{
	public class ScrollableList : MonoBehaviour
	{
		[Header("Visual Settings")]
		public Vector3 position = Vector3.zero;

		public bool horizontalScroll = true;

		public bool verticalScroll = true;

		public float width = 500f;

		public float height = 700f;

		public float itemHeight = 90f;

		public float spacing = 10f;

		public int paddingLeft = 10;

		public int paddingRight = 10;

		public int paddingTop = 20;

		public int paddingBottom = 20;

		public Color bodyColor = Color.white;

		public Color itemColor = Color.gray;

		[Header("Internal References")]
		public Text title;

		public ScrollRect scrollRect;

		public Transform content;

		public GameObject itemPrefab;

		public Dictionary<string, string> items;

		public event Action<ScrollableList, string, string> ItemSelected = delegate
		{
		};

		public static ScrollableList Create(GameObject listPrefab, string title, Dictionary<string, string> items)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(listPrefab, listPrefab.transform.position, listPrefab.transform.rotation);
			ScrollableList component = gameObject.GetComponent<ScrollableList>();
			component.title.text = title;
			component.items = items;
			component.Init();
			return component;
		}

		public void Init()
		{
			base.transform.position = position;
			scrollRect.GetComponent<RectTransform>().sizeDelta.Set(width, height);
			scrollRect.GetComponent<Image>().color = bodyColor;
			HorizontalOrVerticalLayoutGroup component = content.GetComponent<HorizontalOrVerticalLayoutGroup>();
			component.spacing = spacing;
			component.padding.left = paddingLeft;
			component.padding.right = paddingRight;
			component.padding.top = paddingTop;
			component.padding.bottom = paddingBottom;
			float y = (float)paddingTop + itemHeight * (float)items.Count + spacing * (float)(items.Count - 1) + (float)paddingBottom;
			RectTransform component2 = content.GetComponent<RectTransform>();
			component2.sizeDelta = new Vector2(component2.sizeDelta.x, y);
			Vector3 localPosition = component2.localPosition;
			localPosition.y = 0f;
			component2.localPosition = localPosition;
			foreach (KeyValuePair<string, string> item in items)
			{
				AddItem(item.Key, item.Value);
			}
			scrollRect.horizontal = false;
			scrollRect.vertical = false;
			Invoke("EnableScroll", 0.1f);
		}

		public GameObject AddItem(string title, string subtitle)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(itemPrefab, content.position, Quaternion.identity);
			gameObject.GetComponent<LayoutElement>().preferredHeight = itemHeight;
			gameObject.GetComponent<Image>().color = itemColor;
			ScrollableListItem component = gameObject.GetComponent<ScrollableListItem>();
			component.title.text = title;
			component.subtitle.text = subtitle;
			component.button.onClick.AddListener(delegate
			{
				this.ItemSelected(this, title, subtitle);
				UnityEngine.Object.Destroy(base.gameObject, 0.1f);
			});
			gameObject.transform.SetParent(content, false);
			return gameObject;
		}

		public void Close()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private void EnableScroll()
		{
			scrollRect.horizontal = horizontalScroll;
			scrollRect.vertical = verticalScroll;
			scrollRect.verticalNormalizedPosition = 1f;
		}
	}
}
