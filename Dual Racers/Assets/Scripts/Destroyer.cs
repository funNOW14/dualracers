using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "block") {
			Destroy (other.gameObject);
		} else if (other.gameObject.tag == "shield") {
			ObstacleSpawner.isShieldAllowed = true;
			Destroy (other.gameObject);
		}
	}

}
