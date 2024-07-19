using UnityEngine;

public class ScaleToScreenSize : MonoBehaviour
{
	private void Start()
	{
		SpriteRenderer component = GetComponent<SpriteRenderer>();
		if (!(component == null))
		{
			base.transform.localScale = new Vector3(1f, 1f, 1f);
			float x = component.sprite.bounds.size.x;
			float y = component.sprite.bounds.size.y;
			float num = Camera.main.orthographicSize * 2.5f;
			float num2 = num / (float)Screen.height * (float)Screen.width;
			Vector3 localScale = base.transform.localScale;
			localScale.x = num2 / x;
			base.transform.localScale = localScale;
			Vector3 localScale2 = base.transform.localScale;
			localScale2.y = num / y;
			base.transform.localScale = localScale2;
		}
	}
}
