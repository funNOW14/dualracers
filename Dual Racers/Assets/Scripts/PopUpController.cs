using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour {

	public enum PopUpAction {ExitAction, HomeActionFromGame, RestartActionFromGame};
	public static PopUpAction popUpAction;
	public GameObject popUp;
	public GameObject popUpText;
	public GameObject popUpYesButton;
	public GameObject popUpNoButton;
	public UIController uiController;

	public void TogglePopUp () {
		switch (popUpAction) {
		case PopUpAction.ExitAction:
			popUpText.GetComponent<Text> ().text = DRConstants.exit_action_pop_up_text;
			break;
		case PopUpAction.HomeActionFromGame:
			popUpText.GetComponent<Text> ().text = DRConstants.home_action_from_game_pop_up;
			break;
		case PopUpAction.RestartActionFromGame:
			popUpText.GetComponent<Text> ().text = DRConstants.restart_action_from_game_pop_up;
			break;
		}

		StartCoroutine (TogglePopUpFlyAnimation (popUp));
		StartCoroutine (ToggleObjectWithAnimation (popUpYesButton));
		StartCoroutine (ToggleObjectWithAnimation (popUpNoButton));
	}

	private IEnumerator TogglePopUpFlyAnimation (GameObject gameObject) {
		Animator gameObjectAnimator = gameObject.GetComponent<Animator> ();
		if (gameObject.activeSelf == false) {
			RectTransform rt = gameObject.GetComponent<RectTransform> ();
			rt.anchorMin = new Vector2 (0, 1.0f);
			rt.anchorMax = new Vector2 (1.0f, 1.2f);
			gameObject.SetActive (true);
			gameObjectAnimator.SetTrigger ("PopUpFlyIn");
		} else {
			gameObjectAnimator.SetTrigger ("PopUpFlyOut");
			yield return new WaitForSeconds (0.5f);
			gameObject.SetActive (false);
		}
		yield return null;
	}

	private IEnumerator ToggleObjectWithAnimation (GameObject gameObject) {
		Animator gameObjectAnimator = gameObject.GetComponent<Animator> ();
		if (gameObject.activeSelf == false) {
			gameObject.transform.localScale = new Vector3 (0, 0, 1.0f);
			gameObject.SetActive (true);
			gameObjectAnimator.SetTrigger ("ZoomIn");
			yield return new WaitForSeconds (0.5f);
		} else {
			gameObjectAnimator.SetTrigger ("ZoomOut");
			yield return new WaitForSeconds (0.5f);
			gameObject.SetActive (false);
		}
		yield return null;
	}

	public void PopUpYesButtonClick () {
		switch (popUpAction) {
		case PopUpAction.ExitAction:
			//uiController.ExitActionConfirmed ();
			Application.Quit ();
			break;
		case PopUpAction.HomeActionFromGame:
			StartCoroutine (TogglePopUpFlyAnimation (popUp));
			StartCoroutine (ToggleObjectWithAnimation (popUpYesButton));
			StartCoroutine (ToggleObjectWithAnimation (popUpNoButton));
			uiController.HomeFromGameActionCommited ();
			break;
		case PopUpAction.RestartActionFromGame:
			StartCoroutine (TogglePopUpFlyAnimation (popUp));
			StartCoroutine (ToggleObjectWithAnimation (popUpYesButton));
			StartCoroutine (ToggleObjectWithAnimation (popUpNoButton));
			uiController.RestartFromGameActionCommited ();
			break;
		}
	}

	public void PopUpNoButtonClick () {
		switch (popUpAction) {
		case PopUpAction.ExitAction:
			StartCoroutine (TogglePopUpFlyAnimation (popUp));
			StartCoroutine (ToggleObjectWithAnimation (popUpYesButton));
			StartCoroutine (ToggleObjectWithAnimation (popUpNoButton));
			uiController.ExitActionCancelled ();
			break;
		case PopUpAction.HomeActionFromGame:
			StartCoroutine (TogglePopUpFlyAnimation (popUp));
			StartCoroutine (ToggleObjectWithAnimation (popUpYesButton));
			StartCoroutine (ToggleObjectWithAnimation (popUpNoButton));
			uiController.HomeFromGameActionCancelled ();
			break;
		case PopUpAction.RestartActionFromGame:
			StartCoroutine (TogglePopUpFlyAnimation (popUp));
			StartCoroutine (ToggleObjectWithAnimation (popUpYesButton));
			StartCoroutine (ToggleObjectWithAnimation (popUpNoButton));
			uiController.HomeFromGameActionCancelled ();
			break;
		}
	}

}
