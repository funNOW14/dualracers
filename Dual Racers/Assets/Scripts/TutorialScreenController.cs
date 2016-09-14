using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScreenController : MonoBehaviour {

	public GameObject imageHolder;
	public GameObject messageHolder;
	public GameObject nextButton;
	public GameObject prevButton;
	public GameObject playButton;
	public GameObject screen;
	public GameObject message;
	public Sprite tutorialScreen1;
	public Sprite tutorialScreen2;
	public Sprite tutorialScreen3;
	public UIController uiController;
	private static int screenId = 1;

	public void ShowTutorialScreen () {
		screenId = 1;
		StartCoroutine (StartTutorial ());
	}

	public void NextButtonClick () {
		screenId++;
		switch (screenId) {
		case 1:
			break;
		case 2:
			StartCoroutine (ToggleObjectWithAnimation (prevButton));
			screen.GetComponent<Image> ().sprite = tutorialScreen2;
			message.GetComponent<Text> ().text = "Avoid all Blocks.";
			break;
		case 3:
			StartCoroutine (ToggleObjectWithAnimation (nextButton));
			StartCoroutine (ToggleObjectWithAnimation (playButton));
			screen.GetComponent<Image> ().sprite = tutorialScreen3;
			message.GetComponent<Text> ().text = "Take a Shield, and you can crash a Block.";
			break;
		}
	}

	public void PrevButtonClick () {
		switch (screenId) {
		case 1:
			break;
		case 2:
			StartCoroutine (ToggleObjectWithAnimation (prevButton));
			screen.GetComponent<Image> ().sprite = tutorialScreen1;
			message.GetComponent<Text> ().text = "Take all Circles.\nDo not miss any Circle.";
			break;
		case 3:
			StartCoroutine (ToggleObjectWithAnimation (playButton));
			StartCoroutine (ToggleObjectWithAnimation (nextButton));
			screen.GetComponent<Image> ().sprite = tutorialScreen2;
			message.GetComponent<Text> ().text = "Avoid all Blocks.";
			break;
		}
		screenId--;
	}

	public void PlayButtonClick () {
		StartCoroutine (EndTutorial ());
	}

	private IEnumerator EndTutorial () {
		UIController.enableTutorial = false;
		StartCoroutine (ToggleObjectWithAnimation (playButton));
		StartCoroutine (ToggleObjectWithAnimation (prevButton));
		StartCoroutine (ShowTutorialMessageFlyAnimation ());
		StartCoroutine (ShowTutorialImageFlyAnimation ());
		yield return new WaitForSeconds (0.5f);
		uiController.PlayButtonInternalCall ();
	}

	private IEnumerator StartTutorial () {
		StartCoroutine (ShowTutorialMessageFlyAnimation ());
		StartCoroutine (ShowTutorialImageFlyAnimation ());
		yield return new WaitForSeconds (0.5f);
		StartCoroutine (ToggleObjectWithAnimation (nextButton));
		screen.GetComponent<Image> ().sprite = tutorialScreen1;
		message.GetComponent<Text> ().text = "Take all Circles.\nDo not miss any Circle.";
	}

	private IEnumerator ShowTutorialImageFlyAnimation () {
		Animator imageHolderObjectAnimator = imageHolder.GetComponent<Animator> ();
		if (imageHolder.activeSelf == false) {
			imageHolder.SetActive (true);
			imageHolderObjectAnimator.SetTrigger ("TutorialImageFlyIn");
		} else {
			imageHolderObjectAnimator.SetTrigger ("TutorialImageFlyOut");
			yield return new WaitForSeconds (0.5f);
			imageHolder.SetActive (false);
		}
		yield return null;
	}

	private IEnumerator ShowTutorialMessageFlyAnimation () {
		Animator messageHolderObjectAnimator = messageHolder.GetComponent<Animator> ();
		if (messageHolder.activeSelf == false) {
			messageHolder.SetActive (true);
			messageHolderObjectAnimator.SetTrigger ("TutorialMessageFlyIn");
		} else {
			messageHolderObjectAnimator.SetTrigger ("TutorialMessageFlyOut");
			yield return new WaitForSeconds (0.5f);
			messageHolder.SetActive (false);
		}
		yield return null;
	}

	private IEnumerator ToggleObjectWithAnimation (GameObject gameObj) {
		Animator gameObjectAnimator = gameObj.GetComponent<Animator> ();
		if (gameObj.activeSelf == false || gameObj.transform.localScale == new Vector3(0, 0, 1.0f)) {
			gameObj.transform.localScale = new Vector3 (0, 0, 1.0f);
			gameObj.SetActive (true);
			gameObjectAnimator.SetTrigger ("ZoomIn");
			yield return new WaitForSeconds (0.5f);
		} else if(gameObj.activeSelf == true || gameObj.transform.localScale == new Vector3(1.0f, 1.0f, 1.0f)) {
			gameObjectAnimator.SetTrigger ("ZoomOut");
			yield return new WaitForSeconds (0.5f);
			gameObj	.SetActive (false);
		}
		yield return null;
	}
}
