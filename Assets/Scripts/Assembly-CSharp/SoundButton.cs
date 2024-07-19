using UnityEngine;

public class SoundButton : ToggleButton
{
	private void Start()
	{
		btnToggleGraphicsPosition = btnToggleGraphics.rectTransform.anchoredPosition;
	}

	private void OnEnable()
	{
		Invoke("initSoundStatus", 0.1f);
	}

	private void initSoundStatus()
	{
		bool isSoundEnabled = AudioManager.instance.isSoundEnabled;
		Vector2 anchoredPosition = btnToggleGraphics.rectTransform.anchoredPosition;
		btnToggleGraphics.rectTransform.anchoredPosition = new Vector2((!isSoundEnabled) ? (0f - Mathf.Abs(anchoredPosition.x)) : Mathf.Abs(anchoredPosition.x), anchoredPosition.y);
	}

	public override void OnToggleStatusChanged(bool status)
	{
		AudioManager.instance.PlayButtonClickSound();
		AudioManager.instance.ToggleSoundStatus(status);
	}
}
