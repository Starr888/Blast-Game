using GoogleMobileAds.Api;

namespace EasyMobile
{
	internal static class BannerAdPositionMethods
	{
		public static AdPosition ToAdMobAdPosition(this BannerAdPosition pos)
		{
			switch (pos)
			{
			case BannerAdPosition.Top:
				return AdPosition.Top;
			case BannerAdPosition.Bottom:
				return AdPosition.Bottom;
			case BannerAdPosition.TopLeft:
				return AdPosition.TopLeft;
			case BannerAdPosition.TopRight:
				return AdPosition.TopRight;
			case BannerAdPosition.BottomLeft:
				return AdPosition.BottomLeft;
			case BannerAdPosition.BottomRight:
				return AdPosition.BottomRight;
			default:
				return AdPosition.Top;
			}
		}
	}
}
