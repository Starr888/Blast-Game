using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
	public class MobileNativeShareDemo : MonoBehaviour
	{
		public Image clockRect;

		public Text clockText;

		private string TwoStepScreenshotName = "EM_Screenshot";

		private string OneStepScreenshotName = "EM_OneStepScreenshot";

		private string TwoStepScreenshotPath;

		private string sampleMessage = "This is a sample sharing message #sampleshare";

		private string sampleText = "Hello from Easy Mobile!";

		private string sampleURL = "http://u3d.as/Dd2";

		private void OnEnable()
		{
			ColorChooser.colorSelected += ColorChooser_colorSelected;
		}

		private void OnDisable()
		{
			ColorChooser.colorSelected -= ColorChooser_colorSelected;
		}

		private void ColorChooser_colorSelected(Color obj)
		{
			clockRect.color = obj;
		}

		private void Update()
		{
			clockText.text = DateTime.Now.ToString("hh:mm:ss");
		}

		public void ShareText()
		{
			MobileNativeShare.ShareText(sampleText, string.Empty);
		}

		public void ShareURL()
		{
			MobileNativeShare.ShareURL(sampleURL, string.Empty);
		}

		public void SaveScreenshot()
		{
			StartCoroutine(CRSaveScreenshot());
		}

		public void ShareScreenshot()
		{
			if (!string.IsNullOrEmpty(TwoStepScreenshotPath))
			{
				MobileNativeShare.ShareImage(TwoStepScreenshotPath, sampleMessage, string.Empty);
			}
			else
			{
				MobileNativeUI.Alert("Alert", "Please save a screenshot first.");
			}
		}

		public void OneStepSharing()
		{
			StartCoroutine(CROneStepSharing());
		}

		private IEnumerator CRSaveScreenshot()
		{
			yield return new WaitForEndOfFrame();
			TwoStepScreenshotPath = MobileNativeShare.SaveScreenshot(TwoStepScreenshotName);
			MobileNativeUI.Alert("Alert", "A new screenshot was saved at " + TwoStepScreenshotPath);
		}

		private IEnumerator CROneStepSharing()
		{
			yield return new WaitForEndOfFrame();
			MobileNativeShare.ShareScreenshot(OneStepScreenshotName, sampleMessage, string.Empty);
		}
	}
}
