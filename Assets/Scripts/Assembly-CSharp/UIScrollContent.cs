using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollContent : MonoBehaviour
{
	public List<RawImage> images = new List<RawImage>();

	private void Start()
	{
/*		float num = 0f;
		foreach (RawImage image in images)
		{
			RectTransform rectTransform = image.rectTransform;
			num += rectTransform.rect.height;
		}
		base.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(720f, num + 10f, 0f);*/
	}
}
