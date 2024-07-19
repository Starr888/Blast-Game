namespace EasyMobile
{
	internal static class AndroidNativeUtility
	{
		private static readonly string ANDROID_JAVA_UTILITY_CLASS = "com.sglib.easymobile.androidnative.EMUtility";

		internal static void RequestRating(RatingRequestSettings settings)
		{
			RatingRequestSettings.AndroidRatingDialog androidRatingDialogContent = settings.AndroidRatingDialogContent;
			AndroidUtil.CallJavaStaticMethod(ANDROID_JAVA_UTILITY_CLASS, "RequestReview", androidRatingDialogContent.title, androidRatingDialogContent.startMessage, androidRatingDialogContent.lowRatingMessage, androidRatingDialogContent.highRatingMessage, androidRatingDialogContent.postponeButtonText, androidRatingDialogContent.refuseButtonText, androidRatingDialogContent.cancelButtonText, androidRatingDialogContent.feedbackButtonText, androidRatingDialogContent.rateButtonText, settings.MinimumAcceptedStars.ToString());
		}
	}
}
