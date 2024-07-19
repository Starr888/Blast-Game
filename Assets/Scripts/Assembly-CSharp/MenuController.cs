using UnityEngine;

public class MenuController : MonoBehaviour
{
	public GameObject menucanvas;

	public GameObject mapCanvas;

	public GameObject AdditionalCanvas;

	private void Start()
	{
		PlayerPrefs.GetInt("canvas");
		Debug.Log(PlayerPrefs.GetInt("canvas"));
		if (PlayerPrefs.GetInt("canvas") == 1)
		{
			menucanvas.SetActive(false);
			mapCanvas.SetActive(true);
			AdditionalCanvas.SetActive(true);
		}
	}

	public void playButton()
	{
		menucanvas.SetActive(false);
		mapCanvas.SetActive(true);
		AdditionalCanvas.SetActive(true);
	}
}
