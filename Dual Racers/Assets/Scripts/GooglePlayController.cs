using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GooglePlayController : MonoBehaviour {

	public GameObject achievementsButton;
	public GameObject leadersBoardButton;

	public static void ActivateGooglePlayServices () {
		PlayGamesPlatform.Activate ();
	}

	public static void GooglePlayLogin () {
		Social.localUser.Authenticate ((bool success) => {
			if(success) {
				Debug.Log ("Login Success");
			} else {
				Debug.Log ("Login Failed");
			}
		});
	}

	public void AchievementsButtonClick () {
		if (!Social.localUser.authenticated) {
			GooglePlayLogin();
		}
		Social.ShowAchievementsUI ();
	}

	public void LeadersBoardButtonClick () {
		if (!Social.localUser.authenticated) {
			GooglePlayLogin();
		}
		Social.ShowLeaderboardUI ();
	}

	public static void GooglePlayLogout() {
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.SignOut ();
			Debug.Log ("Logout");
		} else {
			Debug.Log ("Already Logout");
		}
	}

	public void UpdateLeadersboard (int highestScore) {
		Social.ReportScore(highestScore, GooglePlayConstants.leaderboard_highest_score, (bool success) => {
			// handle success or failure
		});
	}

	public void UnlockAchievements () {
		if (HUDController.playCount >= 1) {
			AchievementUnlocker (GooglePlayConstants.achievement_get_set_go);
		}

		if (HUDController.playCount >= 50) {
			AchievementUnlocker (GooglePlayConstants.achievement_50_rounds);
		}

		if (HUDController.playCount >= 100) {
			AchievementUnlocker (GooglePlayConstants.achievement_100_rounds);
		}

		if (HUDController.playCount >= 500) {
			AchievementUnlocker (GooglePlayConstants.achievement_500_rounds);
		}

		if (HUDController.playCount >= 1000) {
			AchievementUnlocker (GooglePlayConstants.achievement_1000_rounds);
		}

		if (HUDController.highestScore >= 20) {
			AchievementUnlocker (GooglePlayConstants.achievement_20_score);
		}

		if (HUDController.highestScore >= 50) {
			AchievementUnlocker (GooglePlayConstants.achievement_50_score);
		}

		if (HUDController.highestScore >= 100) {
			AchievementUnlocker (GooglePlayConstants.achievement_100_score);
		}

		if (HUDController.highestScore >= 250) {
			AchievementUnlocker (GooglePlayConstants.achievement_250_score);
		}

		if (HUDController.highestScore >= 500) {
			AchievementUnlocker (GooglePlayConstants.achievement_500_score);
		}

		if (HUDController.highestScore >= 1000) {
			AchievementUnlocker (GooglePlayConstants.achievement_1000_score);
		}

		if (HUDController.totalShieldsCollected >= 50) {
			AchievementUnlocker (GooglePlayConstants.achievement_50_shields);
		}

		if (HUDController.totalShieldsCollected >= 100) {
			AchievementUnlocker (GooglePlayConstants.achievement_100_shields);
		}

		if (HUDController.totalShieldsCollected >= 500) {
			AchievementUnlocker (GooglePlayConstants.achievement_500_shields);
		}

		if (HUDController.maxScoreWithoutHit >= 25) {
			AchievementUnlocker (GooglePlayConstants.achievement_25_score_without_hit);
		}

		if (HUDController.maxScoreWithoutHit >= 50) {
			AchievementUnlocker (GooglePlayConstants.achievement_50_score_without_hit);
		}

		if (HUDController.maxScoreWithoutHit >= 100) {
			AchievementUnlocker (GooglePlayConstants.achievement_100_score_without_hit);
		}

		if (HUDController.maxScoreWithoutHit >= 250) {
			AchievementUnlocker (GooglePlayConstants.achievement_250_score_without_hit);
		}

		if (HUDController.maxScoreWithoutHit >= 500) {
			AchievementUnlocker (GooglePlayConstants.achievement_500_score_without_hit);
		}

		if (HUDController.maxScoreWithoutHit >= 1000) {
			AchievementUnlocker (GooglePlayConstants.achievement_1000_score_without_hit);
		}
	}

	private void AchievementUnlocker (string achievementId) {
		Social.ReportProgress (achievementId, 100.0f, (bool success) => {
			if (success) {
				// Do nothing
			} else {
				// Do nothing
			}
		});
	}
}
