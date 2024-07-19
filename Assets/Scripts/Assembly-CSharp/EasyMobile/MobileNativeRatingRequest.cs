using System;
using UnityEngine;

namespace EasyMobile
{
	[AddComponentMenu("")]
	public class MobileNativeRatingRequest : MonoBehaviour
	{
		public enum UserAction
		{
			Refuse = 0,
			Postpone = 1,
			Feedback = 2,
			Rate = 3
		}

		private static readonly string RATING_DIALOG_GAMEOBJECT = "MobileNativeRatingDialog";

		private const string RATING_REQUEST_DISABLE_PPKEY = "EM_RATING_REQUEST_DISABLE";

		private const int RATING_REQUEST_ENABLED = 1;

		private const int RATING_REQUEST_DISABLED = -1;

		private const string ANNUAL_REQUESTS_MADE_PPKEY_PREFIX = "EM_RATING_REQUESTS_MADE_YEAR_";

		private static Action<UserAction> customBehaviour;

		public static MobileNativeRatingRequest Instance { get; private set; }

		public static void RequestRating()
		{
			customBehaviour = null;
			DoRequestRating();
		}

		public static void RequestRating(Action<UserAction> callback)
		{
			customBehaviour = callback;
			DoRequestRating();
		}

		public static bool CanRequestRating()
		{
			return GetThisYearRemainingRequests() > 0 && !IsRatingRequestDisabled();
		}

		public static bool IsRatingRequestDisabled()
		{
			return PlayerPrefs.GetInt("EM_RATING_REQUEST_DISABLE", 1) == -1;
		}

		public static int GetThisYearRemainingRequests()
		{
			int annualUsedRequests = GetAnnualUsedRequests(DateTime.Now.Year);
			return (int)EM_Settings.RatingRequest.AnnualCap - annualUsedRequests;
		}

		public static int GetAnnualRequestsLimit()
		{
			return (int)EM_Settings.RatingRequest.AnnualCap;
		}

		public static void DisableRatingRequest()
		{
			PlayerPrefs.SetInt("EM_RATING_REQUEST_DISABLE", -1);
			PlayerPrefs.Save();
		}

		private static int GetAnnualUsedRequests(int year)
		{
			string key = "EM_RATING_REQUESTS_MADE_YEAR_" + year;
			return PlayerPrefs.GetInt(key, 0);
		}

		private static void SetAnnualUsedRequests(int year, int requestNumber)
		{
			string key = "EM_RATING_REQUESTS_MADE_YEAR_" + year;
			PlayerPrefs.SetInt(key, requestNumber);
		}

		private static void DoRequestRating()
		{
			if (!CanRequestRating())
			{
				Debug.Log("Rating request was either disabled or the annual cap was reached.");
			}
			else if (!(Instance != null))
			{
				Instance = new GameObject(RATING_DIALOG_GAMEOBJECT).AddComponent<MobileNativeRatingRequest>();
				RatingRequestSettings ratingRequest = EM_Settings.RatingRequest;
				RatingRequestSettings.AndroidRatingDialog androidRatingDialogContent = ratingRequest.AndroidRatingDialogContent;
				androidRatingDialogContent.title = androidRatingDialogContent.title.Replace("$PRODUCT_NAME", Application.productName);
				androidRatingDialogContent.startMessage = androidRatingDialogContent.startMessage.Replace("$PRODUCT_NAME", Application.productName);
				androidRatingDialogContent.lowRatingMessage = androidRatingDialogContent.lowRatingMessage.Replace("$PRODUCT_NAME", Application.productName);
				androidRatingDialogContent.highRatingMessage = androidRatingDialogContent.highRatingMessage.Replace("$PRODUCT_NAME", Application.productName);
				AndroidNativeUtility.RequestRating(ratingRequest);
				SetAnnualUsedRequests(DateTime.Now.Year, GetAnnualUsedRequests(DateTime.Now.Year) + 1);
			}
		}

		private static void DefaultCallback(UserAction action)
		{
			if (customBehaviour != null)
			{
				customBehaviour(action);
			}
			else
			{
				PerformDefaultBehaviour(action);
			}
		}

		private static void PerformDefaultBehaviour(UserAction action)
		{
			switch (action)
			{
			case UserAction.Refuse:
				DisableRatingRequest();
				break;
			case UserAction.Postpone:
				break;
			case UserAction.Feedback:
				Application.OpenURL("mailto:" + EM_Settings.RatingRequest.SupportEmail);
				break;
			case UserAction.Rate:
				if (Application.platform == RuntimePlatform.IPhonePlayer)
				{
					Application.OpenURL("itms-apps://itunes.apple.com/app/id" + EM_Settings.RatingRequest.IosAppId + "?action=write-review");
				}
				else if (Application.platform == RuntimePlatform.Android)
				{
					Application.OpenURL("market://details?id=" + Application.identifier);
				}
				DisableRatingRequest();
				break;
			}
		}

		private static UserAction ConvertToUserAction(int index)
		{
			switch (index)
			{
			case 0:
				return UserAction.Refuse;
			case 1:
				return UserAction.Postpone;
			case 2:
				return UserAction.Feedback;
			case 3:
				return UserAction.Rate;
			default:
				return UserAction.Postpone;
			}
		}

		private void OnAndroidRatingDialogCallback(string userAction)
		{
			int index = Convert.ToInt16(userAction);
			DefaultCallback(ConvertToUserAction(index));
			Instance = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
