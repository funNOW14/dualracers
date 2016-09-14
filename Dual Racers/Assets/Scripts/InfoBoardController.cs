using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoBoardController : MonoBehaviour {

	public enum InfoBoardAction {PauseAction, ResumeAction, HomeActionFromGame, RestartActionFromGame };
	public static InfoBoardAction infoBoardAction;
	public GameObject infoBoard;
	public GameObject infoBoardText;

	public void ToggleInfoBoard () {
		switch (infoBoardAction) {
		case InfoBoardAction.PauseAction:
			infoBoardText.GetComponent<Text> ().text = DRConstants.pause_action_info_board_text;
			break;
		case InfoBoardAction.ResumeAction:
			infoBoardText.GetComponent<Text> ().text = "";
			break;
		case InfoBoardAction.HomeActionFromGame:
			infoBoardText.GetComponent<Text> ().text = "";
			break;
		case InfoBoardAction.RestartActionFromGame:
			infoBoardText.GetComponent<Text> ().text = "";
			break;
		}

		StartCoroutine (ToggleInfoBoardFlyAnimation (infoBoard));
	}

	private IEnumerator ToggleInfoBoardFlyAnimation (GameObject gameObject) {
		Animator gameObjectAnimator = gameObject.GetComponent<Animator> ();
		if (gameObject.activeSelf == false) {
			RectTransform rt = gameObject.GetComponent<RectTransform> ();
			rt.anchorMin = new Vector2 (0, 1.0f);
			rt.anchorMax = new Vector2 (1.0f, 1.37f);
			gameObject.SetActive (true);
			gameObjectAnimator.SetTrigger ("InfoBoardFlyIn");
		} else {
			gameObjectAnimator.SetTrigger ("InfoBoardFlyOut");
			yield return new WaitForSeconds (0.5f);
			gameObject.SetActive (false);
		}
		yield return null;
	}

}
