using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour {

	public GameObject ground;
	public UIController uiController;
	public CarController carController;
	public HUDController hudController;
	public GameObject redCar;
	public GameObject greenCar;
	public ParticleSystem redCarLeftSmoke;
	public ParticleSystem redCarRightSmoke;
	public ParticleSystem greenCarLeftSmoke;
	public ParticleSystem greenCarRightSmoke;
	public ObstacleSpawner redSpawner;
	public ObstacleSpawner greenSpawner;
	public float maxSpeed;
	public float initialSpeed;
	public static float actualSpeed;
	public static float pauseSpeed;
	public static bool engineStart = false;
	private GameObject[] allBlocks;
	private GameObject[] allCircles;
	private GameObject[] allShields;

	void Start () {
		
	}

	void Update () {
		if (engineStart) {
			if (actualSpeed < maxSpeed) {
				actualSpeed += 0.1f;
			}
		}
	}

	public void StartEngine () {
		redSpawner.Spawn ();
		greenSpawner.Spawn ();
		engineStart = true;
		actualSpeed = initialSpeed;
		ObstacleSpawner.isShieldAllowed = true;
	}

	public void DisplayGround () {
		ground.SetActive (true);
		StartCoroutine (GroundFadeIn ());
		carController.ToggleCarState (redCar);
		carController.ToggleCarState (greenCar);
		carController.ResetCarPos (redCar, "Red");
		carController.ResetCarPos (greenCar, "Green");
		StartCoroutine (carController.MoveCarIntoScene (redCar));
		StartCoroutine (carController.MoveCarIntoScene (greenCar));
	}

	private IEnumerator GroundFadeIn () {
		Hashtable ft = new Hashtable ();

		ft.Add ("alpha", 0);
		ft.Add ("time", 0.5f);
		ft.Add ("easetype", iTween.EaseType.easeInOutSine);

		iTween.FadeFrom (ground, ft);

		yield return new WaitForSeconds (0.5f);
	}

	public void PauseGameAction () {
		engineStart = false;
		pauseSpeed = actualSpeed;
		actualSpeed = 0;
		PauseAllParticleEffects ();
	}

	public void ResumeGameAction () {
		engineStart = true;
		actualSpeed = pauseSpeed;
		pauseSpeed = 0;
		PlayAllParticleEffects ();
	}

	public void HomeGameAction () {
		iTween.Stop ();
		engineStart = false;
		allBlocks = GameObject.FindGameObjectsWithTag ("block");
		allCircles = GameObject.FindGameObjectsWithTag ("circle");
		allShields = GameObject.FindGameObjectsWithTag ("shield");
		DestroyObjects (allBlocks);
		DestroyObjects (allCircles);
		DestroyObjects (allShields);
		PlayAllParticleEffects ();
		carController.ResetCarPos (redCar, "Red");
		carController.ResetCarPos (greenCar, "Green");
		hudController.ResetScore ();
	}

	public void RestartGameAction () {
		iTween.Stop ();
		allBlocks = GameObject.FindGameObjectsWithTag ("block");
		allCircles = GameObject.FindGameObjectsWithTag ("circle");
		allShields = GameObject.FindGameObjectsWithTag ("shield");
		DestroyObjects (allBlocks);
		DestroyObjects (allCircles);
		DestroyObjects (allShields);
		PlayAllParticleEffects ();
		StartEngine ();
		carController.ResetCarPos (redCar, "Red");
		carController.ResetCarPos (greenCar, "Green");
		hudController.ResetScore ();
	}

	public void GameOverAction () {
		iTween.Pause ();
		actualSpeed = 0;
		engineStart = false;
		hudController.GameOverScoreUpdate ();
		uiController.GameOverUIAction ();
		PauseAllParticleEffects ();
	}

	public void ShareButtonClick () {
		StartCoroutine (ShareScreenshot ());
	}

	private void DestroyObjects (GameObject[] objects) {
		foreach (GameObject target in objects) {
			GameObject.Destroy (target);
		}
	}

	private void PauseAllParticleEffects () {
		redCarLeftSmoke.Pause ();
		redCarRightSmoke.Pause ();
		greenCarLeftSmoke.Pause ();
		greenCarRightSmoke.Pause ();
	}

	private void PlayAllParticleEffects () {
		redCarLeftSmoke.Play ();
		redCarRightSmoke.Play ();
		greenCarLeftSmoke.Play ();
		greenCarRightSmoke.Play ();
	}

	public IEnumerator ShareScreenshot () {
		yield return new WaitForEndOfFrame();

		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		screenTexture.Apply();

		byte[] dataToSave = screenTexture.EncodeToPNG();
		string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		File.WriteAllBytes(destination, dataToSave);

		if (!Application.isEditor) {
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse","file://" + destination);
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), uriObject);

			intentObject.Call<AndroidJavaObject> ("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");

			currentActivity.Call ("startActivity", intentObject);
		}
	}
}
