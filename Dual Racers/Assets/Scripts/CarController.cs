using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

	public float initialYPos;
	public float finalYPos;
	public GameObject posA;
	public GameObject posB;
	public GameObject posC;
	public GameObject posD;
	public GameObject redCar;
	public GameObject greenCar;
	public Sprite redCarNormal;
	public Sprite redCarSheid;
	public Sprite greenCarNormal;
	public Sprite greenCarSheid;
	public AudioClip carTurn;
	private bool redAtLeft = false;
	private bool greenAtRight = false;
	private float lerpSpeed = 0.15f;

	void Update () {
		if (GameController.engineStart) {
			if (redAtLeft) {
				redCar.transform.position = Vector3.Lerp (redCar.transform.position, new Vector3 (posB.transform.position.x, redCar.transform.position.y, redCar.transform.position.z), lerpSpeed);
			} else {
				redCar.transform.position = Vector3.Lerp (redCar.transform.position, new Vector3 (posA.transform.position.x, redCar.transform.position.y, redCar.transform.position.z), lerpSpeed);
			}

			if (greenAtRight) {
				greenCar.transform.position = Vector3.Lerp (greenCar.transform.position, new Vector3 (posC.transform.position.x, greenCar.transform.position.y, greenCar.transform.position.z), lerpSpeed);
			} else {
				greenCar.transform.position = Vector3.Lerp (greenCar.transform.position, new Vector3 (posD.transform.position.x, greenCar.transform.position.y, greenCar.transform.position.z), lerpSpeed);
			}
		}
	}

	public void ResetCarPos (GameObject car, string carType) {
		car.transform.localRotation = Quaternion.identity;

		if (carType == "Red") {
			car.transform.position = new Vector3 (posA.transform.position.x, car.transform.position.y, car.transform.position.z);
			car.GetComponent<SpriteRenderer> ().sprite = redCarNormal;
			redAtLeft = false;
		} else {
			car.transform.position = new Vector3 (posD.transform.position.x, car.transform.position.y, car.transform.position.z);
			car.GetComponent<SpriteRenderer> ().sprite = greenCarNormal;
			greenAtRight = false;
		}
	}

	public void UpdateCarSprite () {
		if (HUDController.shieldScore > 0) {
			redCar.GetComponent<SpriteRenderer> ().sprite = redCarSheid;
			greenCar.GetComponent<SpriteRenderer> ().sprite = greenCarSheid;
		} else {
			redCar.GetComponent<SpriteRenderer> ().sprite = redCarNormal;
			greenCar.GetComponent<SpriteRenderer> ().sprite = greenCarNormal;
		}
	}

	public void LeftButton () {
		if (AudioController.sound == 1) {
			gameObject.GetComponent<AudioSource> ().PlayOneShot (carTurn);
		}

		if (redAtLeft) {
			redAtLeft = false;
		} else {
			redAtLeft = true;
		}
	}

	public void RightButton () {
		if (AudioController.sound == 1) {
			gameObject.GetComponent<AudioSource> ().PlayOneShot (carTurn);
		}
		if (greenAtRight) {
			greenAtRight = false;
		} else {
			greenAtRight = true;
		}
	}

	public void ToggleCarState (GameObject car) {
		car.SetActive (!car.activeSelf);
		return;
	}

	public IEnumerator MoveCarIntoScene (GameObject car) {
		car.transform.localPosition = new Vector3 (car.transform.localPosition.x, initialYPos, car.transform.localPosition.z);
		iTween.MoveTo (car, new Vector3 (car.transform.position.x, finalYPos, car.transform.position.z), 1.5f);
		yield return new WaitForSeconds (1.5f);
	}
}
