using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
	public Color backgroundColor = new Color(2f / 51f, 2f / 51f, 2f / 51f, 0.6f);

	private GameObject m_background;

	public void Open()
	{
	}

	public void Close()
	{
		Animator component = GetComponent<Animator>();
		if (component.GetCurrentAnimatorStateInfo(0).IsName("Open"))
		{
			component.Play("Close");
		}
		RemoveBackground();
		StartCoroutine(RunPopupDestroy());
	}

	public void Closeilk()
	{
		Animator component = GetComponent<Animator>();
		if (component.GetCurrentAnimatorStateInfo(0).IsName("Open"))
		{
			component.Play("Close");
		}
		RemoveBackgroundilk();
		StartCoroutine(RunPopupDestroy());
	}

	private IEnumerator RunPopupDestroy()
	{
		yield return new WaitForSeconds(0.5f);
		Object.Destroy(m_background);
		Object.Destroy(base.gameObject);
	}

	private void AddBackground()
	{
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixel(0, 0, backgroundColor);
		texture2D.Apply();
		m_background = new GameObject("PopupBackground");
		Image image = m_background.AddComponent<Image>();
		Rect rect = new Rect(0f, 0f, texture2D.width, texture2D.height);
		Sprite sprite = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f), 1f);
		image.material.mainTexture = texture2D;
		image.sprite = sprite;
		Color color = image.color;
		image.color = color;
		image.canvasRenderer.SetAlpha(0f);
		image.CrossFadeAlpha(1f, 0.4f, false);
		GameObject gameObject = GameObject.Find("Page 4");
		m_background.transform.localScale = new Vector3(1f, 1f, 1f);
		m_background.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
		m_background.transform.SetParent(gameObject.transform, false);
		m_background.transform.SetSiblingIndex(base.transform.GetSiblingIndex());
	}

	private void RemoveBackground()
	{
	}

	private void RemoveBackgroundilk()
	{
	}
}
