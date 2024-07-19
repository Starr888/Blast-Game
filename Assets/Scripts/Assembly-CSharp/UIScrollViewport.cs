using UnityEngine;

public class UIScrollViewport : MonoBehaviour
{
	private void Start()
	{
		base.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(720f, 1280f, 0f);
	}
}
