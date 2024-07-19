using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Pagination
{
	public static class PaginationUtilities
	{
		public static GameObject InstantiatePrefab(string name, Type requiredParentType = null)
		{
			GameObject gameObject = Resources.Load<GameObject>("Prefabs/" + name);
			if (gameObject == null)
			{
				throw new UnityException(string.Format("Could not find prefab '{0}'!", name));
			}
			Transform transform = null;
			if (transform == null || !(transform is RectTransform))
			{
				transform = GetCanvasTransform();
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject);
			gameObject2.name = name;
			gameObject2.transform.SetParent(transform);
			RectTransform instanceTransform = (RectTransform)gameObject2.transform;
			RectTransform baseTransform = (RectTransform)gameObject.transform;
			FixInstanceTransform(baseTransform, instanceTransform);
			return gameObject2;
		}

		public static Transform GetCanvasTransform()
		{
			Canvas canvas = null;
			if (canvas == null)
			{
				canvas = UnityEngine.Object.FindObjectsOfType<Canvas>().FirstOrDefault((Canvas c) => c.isRootCanvas);
				if (canvas != null)
				{
					return canvas.transform;
				}
			}
			GameObject gameObject = new GameObject("Canvas");
			gameObject.layer = LayerMask.NameToLayer("UI");
			canvas = gameObject.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			gameObject.AddComponent<CanvasScaler>();
			gameObject.AddComponent<GraphicRaycaster>();
			EventSystem eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
			if (eventSystem == null)
			{
				GameObject gameObject2 = new GameObject("EventSystem");
				eventSystem = gameObject2.AddComponent<EventSystem>();
				gameObject2.AddComponent<StandaloneInputModule>();
				eventSystem.pixelDragThreshold = 20;
			}
			return canvas.transform;
		}

		public static void FixInstanceTransform(RectTransform baseTransform, RectTransform instanceTransform)
		{
			instanceTransform.position = baseTransform.position;
			instanceTransform.position = new Vector3(instanceTransform.position.x, instanceTransform.position.y, 0f);
			instanceTransform.localPosition = baseTransform.localPosition;
			instanceTransform.localPosition = new Vector3(instanceTransform.localPosition.x, instanceTransform.localPosition.y, 0f);
			instanceTransform.rotation = baseTransform.rotation;
			instanceTransform.localScale = baseTransform.localScale;
			instanceTransform.anchoredPosition = baseTransform.anchoredPosition;
			instanceTransform.sizeDelta = baseTransform.sizeDelta;
		}
	}
}
