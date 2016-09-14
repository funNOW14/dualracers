using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public static int score;
	public static int shieldScore;
	public static int currentScoreWithoutHit;
	public static int highestScore;				// highest score ever scored
	public static int playCount;				// number of games played
	public static int totalShieldsCollected;	// total number of shields collected
	public static int maxScoreWithoutHit;		// max score without taking shield
	public static string gameOverText;

	public Text scoreText;
	public Text shieldText;
	public GooglePlayController googlePlayController;

	void Awake () {
		LoadGameData ();
	}

	void Start () {
		score = 0;
		shieldScore = 0;
		currentScoreWithoutHit = 0;
		gameOverText = "Score : " + score + "\nHighest Score : " + highestScore;

		if (playCount == 0) {
			UIController.enableTutorial = true;
		} else {
			UIController.enableTutorial = false;
		}
	}

	void Update () {
		scoreText.text = score + "";
		shieldText.text = shieldScore + "";
	}

	private void LoadGameData () {
		highestScore = PlayerPrefs.GetInt (DRConstants.highest_score, 0);
		playCount = PlayerPrefs.GetInt (DRConstants.play_count, 0);
		totalShieldsCollected = PlayerPrefs.GetInt (DRConstants.total_shields_collected, 0);
		maxScoreWithoutHit = PlayerPrefs.GetInt (DRConstants.max_score_without_hit, 0);

//		Debug.Log ("highestScore " + highestScore);
//		Debug.Log ("playCount " + playCount);
//		Debug.Log ("totalShieldsCollected " + totalShieldsCollected);
//		Debug.Log ("maxScoreWithoutHit " + maxScoreWithoutHit);
	}

	public void GameOverScoreUpdate () {
		// update play count
		playCount += 1;

		// check if new score > highest score
		if (score > highestScore) {
			highestScore = score;
			gameOverText = "New Highest Score!\nScore : " + score;
			googlePlayController.UpdateLeadersboard (highestScore);
		} else {
			gameOverText = "Score : " + score + "\nHighest Score : " + highestScore;
		}

		// initialize Shield score
		shieldScore = 0;

		CommitData ();
	}

	public void ResetScore () {
		score = 0;
		shieldScore = 0;
		currentScoreWithoutHit = 0;
	}

	private void CommitData () {
		PlayerPrefs.SetInt (DRConstants.highest_score, highestScore);
		PlayerPrefs.SetInt (DRConstants.play_count, playCount);
		PlayerPrefs.SetInt (DRConstants.total_shields_collected, totalShieldsCollected);
		PlayerPrefs.SetInt (DRConstants.max_score_without_hit, maxScoreWithoutHit);

		Debug.Log (DRConstants.highest_score + highestScore);
		Debug.Log (DRConstants.play_count + playCount);
		Debug.Log (DRConstants.total_shields_collected + totalShieldsCollected);
		Debug.Log (DRConstants.max_score_without_hit + maxScoreWithoutHit);

		googlePlayController.UnlockAchievements ();
	}
}
