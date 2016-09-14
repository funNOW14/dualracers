using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverBoardController : MonoBehaviour {

	public GameObject gameOverBoard;
	public GameObject gameOverBoardText;
	public GameObject gameOverBoardSubText;

	public void ToggleGameOverBoard () {
		gameOverBoardText.GetComponent<Text> ().text = "GAME OVER";
		gameOverBoardSubText.GetComponent<Text> ().text = HUDController.gameOverText;
		StartCoroutine (ToggleGameOverBoardFlyAnimation (gameOverBoard));
	}

	private IEnumerator ToggleGameOverBoardFlyAnimation (GameObject gameObject) {
		Animator gameObjectAnimator = gameObject.GetComponent<Animator> ();
		if (gameObject.activeSelf == false) {
			RectTransform rt = gameObject.GetComponent<RectTransform> ();
			rt.anchorMin = new Vector2 (0, 1.0f);
			rt.anchorMax = new Vector2 (1.0f, 1.59f);
			gameObject.SetActive (true);
			gameObjectAnimator.SetTrigger ("GameOverBoardFlyIn");
		} else {
			gameObjectAnimator.SetTrigger ("GameOverBoardFlyOut");
			yield return new WaitForSeconds (0.5f);
			gameObject.SetActive (false);
		}
		yield return null;
	}
}
