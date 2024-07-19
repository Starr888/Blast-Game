using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EasyMobile.Demo
{
	public class DemoHomeController : MonoBehaviour
	{
		private void OnEnable()
		{
			NotificationManager.NotificationOpened += NotificationManager_NotificationOpened;
		}

		private void OnDisable()
		{
			NotificationManager.NotificationOpened -= NotificationManager_NotificationOpened;
		}

		public void AdvertisingDemo()
		{
			SceneManager.LoadScene("AdvertisingDemo");
		}

		public void GameServiceDemo()
		{
			SceneManager.LoadScene("GameServiceDemo");
		}

		public void GifDemo()
		{
			SceneManager.LoadScene("GifDemo");
		}

		public void InAppPurchaseDemo()
		{
			SceneManager.LoadScene("InAppPurchasingDemo");
		}

		public void MobileNativeShareDemo()
		{
			SceneManager.LoadScene("MobileNativeShareDemo");
		}

		public void MobileNativeUIDemo()
		{
			SceneManager.LoadScene("MobileNativeUIDemo");
		}

		public void UtilityDemo()
		{
			SceneManager.LoadScene("UtilitiesDemo");
		}

		public void Restart()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		private void Update()
		{
			if (!Input.GetKeyUp(KeyCode.Escape))
			{
				return;
			}
			MobileNativeAlert mobileNativeAlert = MobileNativeUI.ShowTwoButtonAlert("Exit App", "Do you want to exit?", "Yes", "No");
			if (!(mobileNativeAlert != null))
			{
				return;
			}
			mobileNativeAlert.OnComplete += delegate(int button)
			{
				if (button == 0)
				{
					Application.Quit();
				}
			};
		}

		private void NotificationManager_NotificationOpened(string message, string actionID, Dictionary<string, object> additionalData, bool isAppInFocus)
		{
			Debug.Log("Push notification received!");
			Debug.Log("Message: " + message);
			Debug.Log("isAppInFocus: " + isAppInFocus);
			if (additionalData == null)
			{
				return;
			}
			Debug.Log("AdditionalData:");
			foreach (KeyValuePair<string, object> additionalDatum in additionalData)
			{
				Debug.Log("Key: " + additionalDatum.Key + " - Value: " + additionalDatum.Value.ToString());
			}
			if (additionalData.ContainsKey("newUpdate") && !isAppInFocus)
			{
				Debug.Log("New update available! Should open the update page now.");
			}
		}
	}
}
