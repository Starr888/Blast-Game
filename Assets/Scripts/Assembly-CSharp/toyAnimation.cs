using UnityEngine;

public class toyAnimation : MonoBehaviour
{
	private RectTransform tr;

	public float range = 10f;

	public float Speed = 0.5f;

	private Quaternion target;

	public void log(string s)
	{
		Debug.Log(s);
	}

	private void Start()
	{
		tr = GetComponent<RectTransform>();
		target = Quaternion.Euler(0f, 0f, range);
	}
}
