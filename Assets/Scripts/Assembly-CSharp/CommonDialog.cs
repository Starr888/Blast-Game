using UnityEngine;
using UnityEngine.UI;

public class CommonDialog : MonoBehaviour
{
	public Text MessageText;

	public void OnCloseButtonPressed()
	{
		if (InputManager.instance.canInput())
		{
			AudioManager.instance.PlayButtonClickSound();
			InputManager.instance.AddButtonTouchEffect();
		}
	}

	public void OnOkButtonPressed()
	{
		if (InputManager.instance.canInput())
		{
			AudioManager.instance.PlayButtonClickSound();
			InputManager.instance.AddButtonTouchEffect();
		}
	}
}
