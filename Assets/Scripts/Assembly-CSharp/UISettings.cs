using UnityEngine;

public class UISettings : MonoBehaviour
{
	public PopupOpener settingsPopup;

	private itemGrid itmInstance;

	public static bool isclick;

	private void Start()
	{
		isclick = true;
		itmInstance = base.gameObject.GetComponent<itemGrid>();
	}

	public void SettingsClick()
	{
		isclick = false;
		Debug.Log("setting button clicked");
		AudioManager.instance.ButtonClickAudio();
		settingsPopup.OpenPopup();
	}
}
