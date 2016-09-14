using UnityEngine;
using System.Collections;

public class MoveObstacle : MonoBehaviour {

	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -GameController.actualSpeed * Time.deltaTime);
	}
}
