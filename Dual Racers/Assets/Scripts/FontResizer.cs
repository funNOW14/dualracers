using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FontResizer : MonoBehaviour {

	public float fontSize = 20f;
	private Transform myTrans;
	private float heightRatio;

	void Start () {
		myTrans = transform;
		SetFontSize ();
	}

	void SetFontSize () {
		heightRatio = Screen.height * fontSize / 100;
		myTrans.GetComponent<Text> ().fontSize = Mathf.CeilToInt (heightRatio);
	}
}
