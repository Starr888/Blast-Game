using System;
using System.Collections.Generic;
using SgLib.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
	public class GameServiceDemo : MonoBehaviour
	{
		public GameObject curtain;

		public GameObject isAutoInitInfo;

		public GameObject isInitializedInfo;

		public Text selectedAchievementInfo;

		public Text selectedLeaderboardInfo;

		public InputField scoreInput;

		public DemoUtils demoUtils;

		public GameObject scrollableListPrefab;

		private Achievement selectedAchievement;

		private Leaderboard selectedLeaderboard;

		private bool lastLoginState;

		private void Start()
		{
			curtain.SetActive(!EM_Settings.IsGameServiceModuleEnable);
		}

		private void Update()
		{
			if (EM_Settings.GameService.IsAutoInit)
			{
				demoUtils.DisplayBool(isAutoInitInfo, true, "Auto Initialization: ON");
			}
			else
			{
				demoUtils.DisplayBool(isAutoInitInfo, false, "Auto Initialization: OFF");
			}
			if (GameServiceManager.IsInitialized())
			{
				demoUtils.DisplayBool(isInitializedInfo, true, "User Logged In: TRUE");
			}
			else
			{
				demoUtils.DisplayBool(isInitializedInfo, false, "User Logged In: FALSE");
				if (lastLoginState)
				{
					MobileNativeUI.Alert("User Logged Out", "User has logged out.");
				}
			}
			lastLoginState = GameServiceManager.IsInitialized();
		}

		public void Init()
		{
			if (GameServiceManager.IsInitialized())
			{
				MobileNativeUI.Alert("Alert", "The module is already initialized.");
			}
			else
			{
				GameServiceManager.Init();
			}
		}

		public void ShowLeaderboardUI()
		{
			if (GameServiceManager.IsInitialized())
			{
				GameServiceManager.ShowLeaderboardUI();
			}
			else
			{
				GameServiceManager.Init();
			}
		}

		public void ShowAchievementUI()
		{
			if (GameServiceManager.IsInitialized())
			{
				GameServiceManager.ShowAchievementsUI();
			}
			else
			{
				GameServiceManager.Init();
			}
		}

		public void SelectAchievement()
		{
			Achievement[] achievements = EM_Settings.GameService.Achievements;
			if (achievements == null || achievements.Length == 0)
			{
				MobileNativeUI.Alert("Alert", "You haven't added any achievement. Please go to Window > Easy Mobile > Settings and add some.");
				selectedAchievement = null;
				return;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Achievement[] array = achievements;
			foreach (Achievement achievement in array)
			{
				dictionary.Add(achievement.Name, achievement.Id);
			}
			ScrollableList scrollableList = ScrollableList.Create(scrollableListPrefab, "ACHIEVEMENTS", dictionary);
			scrollableList.ItemSelected += OnAchievementSelected;
		}

		public void UnlockAchievement()
		{
			if (!GameServiceManager.IsInitialized())
			{
				MobileNativeUI.Alert("Alert", "You need to initialize the module first.");
			}
			else if (selectedAchievement != null)
			{
				GameServiceManager.UnlockAchievement(selectedAchievement.Name);
			}
			else
			{
				MobileNativeUI.Alert("Alert", "Please select an achievement to unlock.");
			}
		}

		public void SelectLeaderboard()
		{
			Leaderboard[] leaderboards = EM_Settings.GameService.Leaderboards;
			if (leaderboards == null || leaderboards.Length == 0)
			{
				MobileNativeUI.Alert("Alert", "You haven't added any leaderboard. Please go to Window > Easy Mobile > Settings and add some.");
				selectedAchievement = null;
				return;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Leaderboard[] array = leaderboards;
			foreach (Leaderboard leaderboard in array)
			{
				dictionary.Add(leaderboard.Name, leaderboard.Id);
			}
			ScrollableList scrollableList = ScrollableList.Create(scrollableListPrefab, "LEADERBOARDS", dictionary);
			scrollableList.ItemSelected += OnLeaderboardSelected;
		}

		public void ReportScore()
		{
			if (!GameServiceManager.IsInitialized())
			{
				MobileNativeUI.Alert("Alert", "You need to initialize the module first.");
				return;
			}
			if (selectedLeaderboard == null)
			{
				MobileNativeUI.Alert("Alert", "Please select a leaderboard to report score to.");
				return;
			}
			if (string.IsNullOrEmpty(scoreInput.text))
			{
				MobileNativeUI.Alert("Alert", "Please enter a score to report.");
				return;
			}
			int num = Convert.ToInt32(scoreInput.text);
			GameServiceManager.ReportScore(num, selectedLeaderboard.Name);
			MobileNativeUI.Alert("Alert", "Reported score " + num + " to leaderboard \"" + selectedLeaderboard.Name + "\".");
		}

		public void LoadLocalUserScore()
		{
			if (!GameServiceManager.IsInitialized())
			{
				MobileNativeUI.Alert("Alert", "You need to initialize the module first.");
			}
			else if (selectedLeaderboard == null)
			{
				MobileNativeUI.Alert("Alert", "Please select a leaderboard to load score from.");
			}
			else
			{
				GameServiceManager.LoadLocalUserScore(selectedLeaderboard.Name, OnLocalUserScoreLoaded);
			}
		}

		public void LoadFriends()
		{
			if (!GameServiceManager.IsInitialized())
			{
				MobileNativeUI.Alert("Alert", "You need to initialize the module first.");
			}
			else
			{
				GameServiceManager.LoadFriends(OnFriendsLoaded);
			}
		}

		public void SignOut()
		{
			GameServiceManager.SignOut();
		}

		private void OnAchievementSelected(ScrollableList list, string title, string subtitle)
		{
			list.ItemSelected -= OnAchievementSelected;
			selectedAchievement = GameServiceManager.GetAchievementByName(title);
			selectedAchievementInfo.text = "Selected achievement: " + title;
		}

		private void OnLeaderboardSelected(ScrollableList list, string title, string subtitle)
		{
			list.ItemSelected -= OnLeaderboardSelected;
			selectedLeaderboard = GameServiceManager.GetLeaderboardByName(title);
			selectedLeaderboardInfo.text = "Selected leaderboard: " + title;
		}

		private void OnLocalUserScoreLoaded(string leaderboardName, IScore score)
		{
			if (score != null)
			{
				MobileNativeUI.Alert("Local User Score Loaded", "Your score on leaderboard \"" + leaderboardName + "\" is " + score.value);
			}
			else
			{
				MobileNativeUI.Alert("Local User Score Load Failed", "You don't have any score reported to leaderboard \"" + leaderboardName + "\".");
			}
		}

		private void OnFriendsLoaded(IUserProfile[] friends)
		{
			if (friends.Length > 0)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (IUserProfile userProfile in friends)
				{
					dictionary.Add(userProfile.userName, userProfile.id);
				}
				ScrollableList.Create(scrollableListPrefab, "FRIEND LIST", dictionary);
			}
			else
			{
				MobileNativeUI.Alert("Load Friends Result", "Couldn't find any friend.");
			}
		}
	}
}
