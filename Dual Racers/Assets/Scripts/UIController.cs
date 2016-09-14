using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class UIController : MonoBehaviour {

	public GameObject funNowLogo;
	public GameObject overlay;
	public GameObject gameTitle;
	public GameObject playButton;
	public GameObject achievementsButton;
	public GameObject leadersBoardButton;
	public GameObject soundButton;
	public GameObject rateButton;
	public GameObject pauseButton;
	public GameObject resumeButton;
	public GameObject homeButtonFromGame;
	public GameObject restartButton;
	public GameObject homeButtonFromGameOver;
	public GameObject replayButton;
	public GameObject shareButton;
	public GameObject hud;
	public GameObject carControl;
	public PopUpController popUpController;
	public InfoBoardController infoBoardController;
	public GameController gameController;
	public GameOverBoardController gameOverBoardController;
	public TutorialScreenController tutorialScreenController;
	public CarController carController;
	public static bool enableTutorial = true;
	private BannerView bannerView;

	void Start () {
		StartCoroutine (FunNowLogoZoomInOut());
		GooglePlayController.ActivateGooglePlayServices ();
		GooglePlayController.GooglePlayLogin ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			LeftCarControlButtonClick ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			RightCarControlButtonClick ();
		}

		if (gameTitle.activeSelf) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				ExitButtonClick ();
			}
		}
	}

	private IEnumerator FunNowLogoZoomInOut () {
		StartCoroutine (ToggleObjectWithAnimation (funNowLogo));
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (ToggleObjectWithAnimation (funNowLogo));
		yield return new WaitForSeconds (0.5f);
		gameController.DisplayGround ();
		StartCoroutine (ToggleOverlay());
		StartCoroutine (ToggleObjectWithAnimation (gameTitle));
		StartCoroutine (ToggleObjectWithAnimation (playButton));
		StartCoroutine (ToggleObjectWithAnimation (achievementsButton));
		StartCoroutine (ToggleObjectWithAnimation (leadersBoardButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
		StartCoroutine (ToggleObjectWithAnimation (rateButton));
		LoadBanner ();
	}

	private void LoadBanner () {
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-8043506625696143/1227313315";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-8043506625696143/5571297712";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		bannerView = new BannerView (adUnitId, AdSize.Banner, AdPosition.Bottom);
		AdRequest request = new AdRequest.Builder ().Build ();
		bannerView.LoadAd (request);
	}

	private IEnumerator ToggleOverlay () {
		overlay.SetActive (!overlay.activeSelf);
		yield return null;
	}

	private IEnumerator ToggleCarControls () {
		carControl.SetActive (!carControl.activeSelf);
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

	private IEnumerator ToggleHUDFlyAnimation (GameObject gameObject) {
		Animator gameObjectAnimator = gameObject.GetComponent<Animator> ();
		if (gameObject.activeSelf == false) {
			RectTransform rt = gameObject.GetComponent<RectTransform> ();
			rt.anchorMin = new Vector2 (0, 1.0f);
			rt.anchorMax = new Vector2 (1.0f, 1.04f);
			gameObject.SetActive (true);
			gameObjectAnimator.SetTrigger ("HUDFlyIn");
		} else {
			gameObjectAnimator.SetTrigger ("HUDFlyOut");
			yield return new WaitForSeconds (0.5f);
			gameObject.SetActive (false);
		}
		yield return null;
	}

	public void PlayButtonClick () {
		bannerView.Hide ();
		StartCoroutine (ToggleObjectWithAnimation (gameTitle));
		StartCoroutine (ToggleObjectWithAnimation (playButton));
		StartCoroutine (ToggleObjectWithAnimation (achievementsButton));
		StartCoroutine (ToggleObjectWithAnimation (leadersBoardButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
		StartCoroutine (ToggleObjectWithAnimation (rateButton));
		StartCoroutine (ToggleOverlay ());
		if (enableTutorial) {
			Debug.Log ("Tutorial enabled");
			StartCoroutine (ShowTutorial ());
		} else {
			Debug.Log ("Tutorial disabled");
			PlayButtonInternalCall ();
		}
	}

	public void PlayButtonInternalCall () {
		StartCoroutine (ToggleCarControls ());
		StartCoroutine (ToggleObjectWithAnimation (pauseButton));
		StartCoroutine (ToggleHUDFlyAnimation (hud));
		gameController.StartEngine ();
	}

	private IEnumerator ShowTutorial () {
		yield return new WaitForSeconds (0.5f);
		tutorialScreenController.ShowTutorialScreen ();
	}

	public void RateButtonClick () {
		Application.OpenURL ("http://play.google.com/store/apps/details?id=" + Application.bundleIdentifier);
	}

	public void ExitButtonClick () {
		StartCoroutine (ToggleObjectWithAnimation (gameTitle));
		StartCoroutine (ToggleObjectWithAnimation (playButton));
		StartCoroutine (ToggleObjectWithAnimation (achievementsButton));
		StartCoroutine (ToggleObjectWithAnimation (leadersBoardButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
		StartCoroutine (ToggleObjectWithAnimation (rateButton));
		PopUpController.popUpAction = PopUpController.PopUpAction.ExitAction;
		popUpController.TogglePopUp ();
	}

	public void ExitActionConfirmed () {
		GooglePlayController.GooglePlayLogout ();
	}

	public void ExitActionCancelled () {
		StartCoroutine (ToggleObjectWithAnimation (gameTitle));
		StartCoroutine (ToggleObjectWithAnimation (playButton));
		StartCoroutine (ToggleObjectWithAnimation (achievementsButton));
		StartCoroutine (ToggleObjectWithAnimation (leadersBoardButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
		StartCoroutine (ToggleObjectWithAnimation (rateButton));
	}

	public void PauseButtonClick () {
		StartCoroutine (ToggleOverlay ());
		StartCoroutine (ToggleCarControls ());
		StartCoroutine (ToggleObjectWithAnimation (pauseButton));
		InfoBoardController.infoBoardAction = InfoBoardController.InfoBoardAction.PauseAction;
		infoBoardController.ToggleInfoBoard ();
		gameController.PauseGameAction ();
		StartCoroutine (ToggleObjectWithAnimation (resumeButton));
		StartCoroutine (ToggleObjectWithAnimation (homeButtonFromGame));
		StartCoroutine (ToggleObjectWithAnimation (restartButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
	}

	public void ResumeButtonClick () {
		StartCoroutine (ToggleOverlay ());
		StartCoroutine (ToggleCarControls ());
		StartCoroutine (ToggleObjectWithAnimation (resumeButton));
		StartCoroutine (ToggleObjectWithAnimation (homeButtonFromGame));
		StartCoroutine (ToggleObjectWithAnimation (restartButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
		InfoBoardController.infoBoardAction = InfoBoardController.InfoBoardAction.ResumeAction;
		infoBoardController.ToggleInfoBoard ();
		gameController.ResumeGameAction ();
		StartCoroutine (ToggleObjectWithAnimation (pauseButton));
	}

	public void HomeButtonClickFromGame () {
		StartCoroutine (ToggleObjectWithAnimation (resumeButton));
		StartCoroutine (ToggleObjectWithAnimation (homeButtonFromGame));
		StartCoroutine (ToggleObjectWithAnimation (restartButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
		InfoBoardController.infoBoardAction = InfoBoardController.InfoBoardAction.HomeActionFromGame;
		infoBoardController.ToggleInfoBoard ();
		StartCoroutine (ToggleHUDFlyAnimation (hud));
		PopUpController.popUpAction = PopUpController.PopUpAction.HomeActionFromGame;
		popUpController.TogglePopUp ();
	}

	public void HomeFromGameActionCommited () {
		StartCoroutine (ToggleObjectWithAnimation (gameTitle));
		StartCoroutine (ToggleObjectWithAnimation (playButton));
		StartCoroutine (ToggleObjectWithAnimation (achievementsButton));
		StartCoroutine (ToggleObjectWithAnimation (leadersBoardButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
		StartCoroutine (ToggleObjectWithAnimation (rateButton));
		gameController.HomeGameAction ();
		bannerView.Show ();
	}

	public void HomeFromGameActionCancelled () {
		InfoBoardController.infoBoardAction = InfoBoardController.InfoBoardAction.PauseAction;
		infoBoardController.ToggleInfoBoard ();
		StartCoroutine (ToggleHUDFlyAnimation (hud));
		StartCoroutine (ToggleObjectWithAnimation (resumeButton));
		StartCoroutine (ToggleObjectWithAnimation (homeButtonFromGame));
		StartCoroutine (ToggleObjectWithAnimation (restartButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
	}

	public void RestartButtonClickFromGame () {
		StartCoroutine (ToggleObjectWithAnimation (resumeButton));
		StartCoroutine (ToggleObjectWithAnimation (homeButtonFromGame));
		StartCoroutine (ToggleObjectWithAnimation (restartButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
		InfoBoardController.infoBoardAction = InfoBoardController.InfoBoardAction.RestartActionFromGame;
		infoBoardController.ToggleInfoBoard ();
		StartCoroutine (ToggleHUDFlyAnimation (hud));
		PopUpController.popUpAction = PopUpController.PopUpAction.RestartActionFromGame;
		popUpController.TogglePopUp ();
	}

	public void RestartFromGameActionCommited () {
		StartCoroutine (ToggleOverlay ());
		StartCoroutine (ToggleCarControls ());
		StartCoroutine (ToggleHUDFlyAnimation (hud));
		StartCoroutine (ToggleObjectWithAnimation (pauseButton));
		gameController.RestartGameAction ();
	}

	public void GameOverUIAction () {
		StartCoroutine (ToggleHUDFlyAnimation (hud));
		gameOverBoardController.ToggleGameOverBoard ();
		StartCoroutine (ToggleObjectWithAnimation (pauseButton));
		StartCoroutine (ToggleOverlay ());
		StartCoroutine (ToggleCarControls ());
		StartCoroutine (ToggleObjectWithAnimation (homeButtonFromGameOver));
		StartCoroutine (ToggleObjectWithAnimation (replayButton));
		StartCoroutine (ToggleObjectWithAnimation (shareButton));
		bannerView.Show ();
	}

	public void HomeButtonClickFromGameOver () {
		gameOverBoardController.ToggleGameOverBoard ();
		StartCoroutine (ToggleObjectWithAnimation (homeButtonFromGameOver));
		StartCoroutine (ToggleObjectWithAnimation (replayButton));
		StartCoroutine (ToggleObjectWithAnimation (shareButton));
		StartCoroutine (ToggleObjectWithAnimation (gameTitle));
		StartCoroutine (ToggleObjectWithAnimation (playButton));
		StartCoroutine (ToggleObjectWithAnimation (achievementsButton));
		StartCoroutine (ToggleObjectWithAnimation (leadersBoardButton));
		StartCoroutine (ToggleObjectWithAnimation (soundButton));
		StartCoroutine (ToggleObjectWithAnimation (rateButton));
		gameController.HomeGameAction ();
	}

	public void ReplayButtonClickFromGameOver () {
		bannerView.Hide ();
		gameOverBoardController.ToggleGameOverBoard ();
		StartCoroutine (ToggleObjectWithAnimation (homeButtonFromGameOver));
		StartCoroutine (ToggleObjectWithAnimation (replayButton));
		StartCoroutine (ToggleObjectWithAnimation (shareButton));
		StartCoroutine (ToggleOverlay ());
		StartCoroutine (ToggleCarControls ());
		StartCoroutine (ToggleHUDFlyAnimation (hud));
		StartCoroutine (ToggleObjectWithAnimation (pauseButton));
		gameController.RestartGameAction ();
	}

	public void LeftCarControlButtonClick () {
		carController.LeftButton ();
	}

	public void RightCarControlButtonClick () {
		carController.RightButton ();
	}
}
