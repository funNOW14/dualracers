using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject[] obstacles;
	public GameObject leftLane;
	public GameObject rightLane;
	private GameObject currentObstacle;
	private Vector3 spawnPosition;
	public static bool isShieldAllowed;

	void Start () {
		isShieldAllowed = true;
	}

	public void Spawn () {
		if (GetRandom () % 2 == 0) {
			spawnPosition = new Vector3 (leftLane.transform.position.x, transform.position.y, transform.position.z);
		} else {
			spawnPosition = new Vector3 (rightLane.transform.position.x, transform.position.y, transform.position.z);
		}

		GenerateObstacle ();

		Instantiate (currentObstacle, spawnPosition, Quaternion.identity);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "marker") {
			Spawn ();
		}
	}

	private void GenerateObstacle () {
		currentObstacle = obstacles [Random.Range (0, obstacles.GetLength (0))];

		Debug.Log ("currentObstacle.tag " + currentObstacle.tag);

		if (currentObstacle.tag == "shield") {
			if (HUDController.shieldScore < 3) {
				if (!isShieldAllowed) {
					GenerateObstacle ();
				} else {
					isShieldAllowed = false;
				}
			} else {
				GenerateObstacle ();
			}
		}
	}

	private int GetRandom () {
		int random = (int)Random.Range (3.0f, 20.0f);
		return random;
	}
}
