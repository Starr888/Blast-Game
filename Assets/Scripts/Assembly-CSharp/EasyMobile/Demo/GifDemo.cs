using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
	[AddComponentMenu("")]
	public class GifDemo : MonoBehaviour
	{
		[Header("GIF Settings")]
		public Recorder recorder;

		public string gifFilename = "easy_mobile_demo";

		public int loop;

		[Range(1f, 100f)]
		public int quality = 90;

		public System.Threading.ThreadPriority exportThreadPriority;

		[Header("UI Stuff")]
		public GameObject recordingMark;

		public GameObject startRecordingButton;

		public GameObject stopRecordingButton;

		public GameObject playbackPanel;

		public ClipPlayerUI clipPlayer;

		public GameObject giphyLogo;

		public GameObject activityProgress;

		public Text activityText;

		public Text progressText;

		private AnimatedClip recordedClip;

		private bool isExportingGif;

		private bool isUploadingGif;

		private string exportedGifPath;

		private string uploadedGifUrl;

		private void OnDestroy()
		{
			if (recordedClip != null)
			{
				recordedClip.Dispose();
			}
		}

		private void Start()
		{
			startRecordingButton.SetActive(true);
			stopRecordingButton.SetActive(false);
			recordingMark.SetActive(false);
			playbackPanel.SetActive(false);
			activityProgress.SetActive(false);
		}

		private void Update()
		{
			giphyLogo.SetActive(Giphy.IsUsingAPI);
			activityProgress.SetActive(isExportingGif || isUploadingGif);
		}

		public void StartRecording()
		{
			if (recordedClip != null)
			{
				recordedClip.Dispose();
			}
			Gif.StartRecording(recorder);
			startRecordingButton.SetActive(false);
			stopRecordingButton.SetActive(true);
			recordingMark.SetActive(true);
		}

		public void StopRecording()
		{
			recordedClip = Gif.StopRecording(recorder);
			startRecordingButton.SetActive(true);
			stopRecordingButton.SetActive(false);
			recordingMark.SetActive(false);
		}

		public void OpenPlaybackPanel()
		{
			if (recordedClip != null)
			{
				playbackPanel.SetActive(true);
				Gif.PlayClip(clipPlayer, recordedClip, 1f);
			}
			else
			{
				MobileNativeUI.Alert("Nothing Recorded", "Please finish recording first.");
			}
		}

		public void ClosePlaybackPanel()
		{
			clipPlayer.Stop();
			playbackPanel.SetActive(false);
		}

		public void ExportGIF()
		{
			if (isExportingGif)
			{
				MobileNativeUI.Alert("Exporting In Progress", "Please wait until the current GIF exporting is completed.");
				return;
			}
			if (isUploadingGif)
			{
				MobileNativeUI.Alert("Uploading In Progress", "Please wait until the GIF uploading is completed.");
				return;
			}
			isExportingGif = true;
			Gif.ExportGif(recordedClip, gifFilename, loop, quality, exportThreadPriority, OnGifExportProgress, OnGifExportCompleted);
		}

		public void UploadGIFToGiphy()
		{
			if (isExportingGif)
			{
				MobileNativeUI.Alert("Exporting In Progress", "Please wait until the GIF exporting is completed.");
				return;
			}
			if (string.IsNullOrEmpty(exportedGifPath))
			{
				MobileNativeUI.Alert("No Exported GIF", "Please export a GIF file first.");
				return;
			}
			isUploadingGif = true;
			GiphyUploadParams content = default(GiphyUploadParams);
			content.localImagePath = exportedGifPath;
			content.tags = "demo, easy mobile, sglib games, unity3d";
			Giphy.Upload(content, OnGiphyUploadProgress, OnGiphyUploadCompleted, OnGiphyUploadFailed);
		}

		public void ShareGiphyURL()
		{
			if (string.IsNullOrEmpty(uploadedGifUrl))
			{
				MobileNativeAlert.Alert("Invalid URL", "No valid Giphy URL found. Did the upload succeed?");
			}
			else
			{
				MobileNativeShare.ShareURL(uploadedGifUrl, string.Empty);
			}
		}

		private void OnGifExportProgress(AnimatedClip clip, float progress)
		{
			activityText.text = "GENERATING GIF...";
			progressText.text = string.Format("{0:P0}", progress);
		}

		private void OnGifExportCompleted(AnimatedClip clip, string path)
		{
			progressText.text = "DONE";
			isExportingGif = false;
			exportedGifPath = path;
			MobileNativeAlert mobileNativeAlert = MobileNativeUI.ShowTwoButtonAlert("Export Completed", "A GIF file has been created. Do you want to upload it to Giphy?", "Yes", "No");
			if (!(mobileNativeAlert != null))
			{
				return;
			}
			mobileNativeAlert.OnComplete += delegate(int buttonId)
			{
				if (buttonId == 0)
				{
					UploadGIFToGiphy();
				}
			};
		}

		private void OnGiphyUploadProgress(float progress)
		{
			activityText.text = "UPLOADING TO GIPHY...";
			progressText.text = string.Format("{0:P0}", progress);
		}

		private void OnGiphyUploadCompleted(string url)
		{
			progressText.text = "DONE";
			isUploadingGif = false;
			uploadedGifUrl = url;
			MobileNativeAlert mobileNativeAlert = MobileNativeUI.ShowTwoButtonAlert("Upload Completed", "The GIF image has been uploaded to Giphy at " + url + ". Open it in the browser?", "Yes", "No");
			if (!(mobileNativeAlert != null))
			{
				return;
			}
			mobileNativeAlert.OnComplete += delegate(int buttonId)
			{
				if (buttonId == 0)
				{
					Application.OpenURL(uploadedGifUrl);
				}
			};
		}

		private void OnGiphyUploadFailed(string error)
		{
			isUploadingGif = false;
			MobileNativeUI.Alert("Upload Failed", "Uploading to Giphy has failed with error " + error);
		}
	}
}
