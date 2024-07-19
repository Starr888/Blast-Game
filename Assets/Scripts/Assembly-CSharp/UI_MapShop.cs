using UnityEngine;

public class UI_MapShop : MonoBehaviour
{
	private void Start()
	{
		float num = (float)Screen.height / (float)Screen.width * 720f;
	}

	public void ButtonClickAudio()
	{
		AudioManager.instance.ButtonClickAudio();
	}
}
