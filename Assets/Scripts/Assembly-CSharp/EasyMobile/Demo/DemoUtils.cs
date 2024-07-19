using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
	public class DemoUtils : MonoBehaviour
	{
		public Sprite checkedSprite;

		public Sprite uncheckedSprite;

		public void GoHome()
		{
			SceneManager.LoadScene("DemoHome");
		}

		public void DisplayBool(GameObject infoObj, bool state, string msg)
		{
			Image componentInChildren = infoObj.GetComponentInChildren<Image>();
			Text componentInChildren2 = infoObj.GetComponentInChildren<Text>();
			if (componentInChildren == null || componentInChildren2 == null)
			{
				Debug.LogError("Could not found Image or Text component beneath object: " + infoObj.name);
			}
			if (state)
			{
				componentInChildren.sprite = checkedSprite;
				componentInChildren.color = Color.green;
			}
			else
			{
				componentInChildren.sprite = uncheckedSprite;
				componentInChildren.color = Color.red;
			}
			componentInChildren2.text = msg;
		}

		public void PlayButtonSound()
		{
			SoundManager.Instance.PlaySound(SoundManager.Instance.button);
		}
	}
}
