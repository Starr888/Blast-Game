using UnityEngine;

namespace EasyMobile.Demo
{
	public class MobileNativeUIDemo : MonoBehaviour
	{
		public GameObject isFirstButtonBool;

		public GameObject isSecondButtonBool;

		public GameObject isThirdButtonBool;

		public DemoUtils demoUtils;

		public void ShowThreeButtonsAlert()
		{
			MobileNativeAlert mobileNativeAlert = MobileNativeUI.ShowThreeButtonAlert("Sample Alert", "This is a 3-button alert.", "Button 1", "Button 2", "Button 3");
			if (mobileNativeAlert != null)
			{
				mobileNativeAlert.OnComplete += OnAlertComplete;
			}
		}

		public void ShowTwoButtonsAlert()
		{
			MobileNativeAlert mobileNativeAlert = MobileNativeUI.ShowTwoButtonAlert("Sample Alert", "This is a 2-button alert.", "Button 1", "Button 2");
			if (mobileNativeAlert != null)
			{
				mobileNativeAlert.OnComplete += OnAlertComplete;
			}
		}

		public void ShowOneButtonAlert()
		{
			MobileNativeAlert mobileNativeAlert = MobileNativeUI.Alert("Sample Alert", "This is a simple (1-button) alert.");
			if (mobileNativeAlert != null)
			{
				mobileNativeAlert.OnComplete += OnAlertComplete;
			}
		}

		public void ShowToast()
		{
			MobileNativeUI.ShowToast("This is a sample Android toast");
		}

		private void OnAlertComplete(int buttonIndex)
		{
			bool flag = buttonIndex == 0;
			bool flag2 = buttonIndex == 1;
			bool flag3 = buttonIndex == 2;
			if (flag)
			{
				demoUtils.DisplayBool(isFirstButtonBool, true, "isFirstButtonClicked: TRUE");
			}
			else
			{
				demoUtils.DisplayBool(isFirstButtonBool, false, "isFirstButtonClicked: FALSE");
			}
			if (flag2)
			{
				demoUtils.DisplayBool(isSecondButtonBool, true, "isSecondButtonClicked: TRUE");
			}
			else
			{
				demoUtils.DisplayBool(isSecondButtonBool, false, "isSecondButtonClicked: FALSE");
			}
			if (flag3)
			{
				demoUtils.DisplayBool(isThirdButtonBool, true, "isThirdButtonClicked: TRUE");
			}
			else
			{
				demoUtils.DisplayBool(isThirdButtonBool, false, "isThirdButtonClicked: FALSE");
			}
		}
	}
}
