using UnityEngine;

public class PopupOpener : MonoBehaviour
{
	public GameObject popupPrefab;

	protected Canvas m_canvas;

	protected void Start()
	{
		m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
	}

	public virtual void OpenPopup()
	{
		GameObject gameObject = Object.Instantiate(popupPrefab);
		gameObject.SetActive(true);
		gameObject.transform.localScale = Vector3.zero;
		if (!m_canvas)
		{
			m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		}
		gameObject.transform.SetParent(m_canvas.transform, false);
		gameObject.GetComponent<Popup>().Open();
	}
}
