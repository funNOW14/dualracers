using UnityEngine;
using System.Collections;

public class PositionAdjust : MonoBehaviour {

	public float posRatio;
	private Transform myTrans;
	private Vector3 pos;
	
	void Awake () {
		myTrans = transform;
		AdjustWidth ();
	}
	
	void AdjustWidth () {
		pos = new Vector3 ((float)Screen.width * posRatio, (float)Screen.height/2, myTrans.position.z + 10.0f);
		myTrans.position = Camera.main.ScreenToWorldPoint (pos);
	}
}
