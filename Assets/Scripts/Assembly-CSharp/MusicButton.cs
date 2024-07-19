using UnityEngine;

public class MusicButton : ToggleButton
{
	private void Start()
	{
		btnToggleGraphicsPosition = btnToggleGraphics.rectTransform.anchoredPosition;
	}

	private void OnEnable()
	{
		Invoke("initMusicStatus", 0.1f);
	}

	private void initMusicStatus()
	{
		bool isMusicEnabled = AudioManager.instance.isMusicEnabled;
		Vector2 anchoredPosition = btnToggleGraphics.rectTransform.anchoredPosition;
		btnToggleGraphics.rectTransform.anchoredPosition = new Vector2((!isMusicEnabled) ? (0f - Mathf.Abs(anchoredPosition.x)) : Mathf.Abs(anchoredPosition.x), anchoredPosition.y);
	}

	public override void OnToggleStatusChanged(bool status)
	{
		AudioManager.instance.PlayButtonClickSound();
		AudioManager.instance.ToggleMusicStatus(status);
	}
}
