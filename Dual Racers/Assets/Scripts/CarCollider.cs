using UnityEngine;
using System.Collections;

public class CarCollider : MonoBehaviour {

	public GameController gameController;
	public CarController carController;
	public ParticleSystem blockBurst;
	public AudioClip crashAudio;
	public AudioClip circleHit;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "block") {
			if (HUDController.shieldScore > 0) {
				HUDController.shieldScore -= 1;
				carController.UpdateCarSprite ();
				if (HUDController.maxScoreWithoutHit < HUDController.currentScoreWithoutHit) {
					HUDController.maxScoreWithoutHit = HUDController.currentScoreWithoutHit;
				}
				HUDController.currentScoreWithoutHit = 0;
			} else {
				gameController.GameOverAction ();
			}
			blockBurst.transform.position = other.transform.position;
			Destroy (other.gameObject);
			blockBurst.Play ();

			if (AudioController.sound == 1) {
				gameObject.GetComponent<AudioSource> ().PlayOneShot (crashAudio);
			}
		} else if (other.gameObject.tag == "circle") {
			Destroy (other.gameObject);
			HUDController.score += 1;
			HUDController.currentScoreWithoutHit += 1;

			if (AudioController.sound == 1) {
				gameObject.GetComponent<AudioSource> ().PlayOneShot (circleHit);
			}
		} else if (other.gameObject.tag == "shield") {
			Destroy (other.gameObject);
			ObstacleSpawner.isShieldAllowed = true;
			HUDController.shieldScore += 1;
			HUDController.totalShieldsCollected += 1;
			carController.UpdateCarSprite ();
		}
	}
}
