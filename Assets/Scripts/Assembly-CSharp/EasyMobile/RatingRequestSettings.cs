using System;
using UnityEngine;

namespace EasyMobile
{
	[Serializable]
	public class RatingRequestSettings
	{
		[Serializable]
		public class AndroidRatingDialog
		{
			public string title = "Rate $PRODUCT_NAME";

			public string startMessage = "How would you rate $PRODUCT_NAME?";

			public string lowRatingMessage = "That's bad. Would you like to give us some feedback instead?";

			public string highRatingMessage = "Awesome! Let's do it!";

			public string postponeButtonText = "Not Now";

			public string refuseButtonText = "Don't Ask Again";

			public string cancelButtonText = "Cancel";

			public string feedbackButtonText = "Send Feedback";

			public string rateButtonText = "Rate Now!";
		}

		[Serializable]
		public class iOSRatingDialog
		{
			public string title = "Rate $PRODUCT_NAME";

			public string message = "Would you mind spending a moment to rate $PRODUCT_NAME on the App Store?";

			public string postponeButtonText = "Remind Me Later";

			public string refuseButtonText = "Don't Ask Again";

			public string rateButtonText = "Rate Now!";
		}

		public const string PRODUCT_NAME_PLACEHOLDER = "$PRODUCT_NAME";

		[SerializeField]
		private iOSRatingDialog _iosDialogContent = new iOSRatingDialog();

		[SerializeField]
		private AndroidRatingDialog _androidDialogContent = new AndroidRatingDialog();

		[SerializeField]
		[Range(0f, 5f)]
		private uint _minimumAcceptedStars = 4u;

		[SerializeField]
		private string _supportEmail;

		[SerializeField]
		private string _iosAppId;

		[SerializeField]
		[Range(3f, 100f)]
		private uint _annualCap = 12u;

		public iOSRatingDialog IosRatingDialogContent
		{
			get
			{
				return _iosDialogContent;
			}
		}

		public AndroidRatingDialog AndroidRatingDialogContent
		{
			get
			{
				return _androidDialogContent;
			}
		}

		public uint MinimumAcceptedStars
		{
			get
			{
				return _minimumAcceptedStars;
			}
		}

		public string SupportEmail
		{
			get
			{
				return _supportEmail;
			}
		}

		public string IosAppId
		{
			get
			{
				return _iosAppId;
			}
		}

		public uint AnnualCap
		{
			get
			{
				return _annualCap;
			}
		}
	}
}
