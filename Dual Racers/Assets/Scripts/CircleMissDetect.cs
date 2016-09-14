using UnityEngine;
using System.Collections;

public class CircleMissDetect : MonoBehaviour {

	public GameController gameController;
	public AudioClip circleMiss; 

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "circle") {
			if (AudioController.sound == 1) {
				gameObject.GetComponent<AudioSource> ().PlayOneShot (circleMiss);
			}
			StartCoroutine (ObjectFlicker (other.gameObject));
			GameController.actualSpeed = 0;
			GameController.engineStart = false;
			gameController.GameOverAction ();
		}
	}

	public IEnumerator ObjectFlicker (GameObject circle) {
		Sprite circleSprite = circle.GetComponent<SpriteRenderer> ().sprite;
		for(int i = 0; i < 15; i++) {
			circle.GetComponent<SpriteRenderer> ().sprite = null;
			yield return new WaitForSeconds (0.01f);
			circle.GetComponent<SpriteRenderer> ().sprite = circleSprite;
			yield return new WaitForSeconds (0.01f);
		}
		yield return null;
	}

}
