using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

	public GameObject soundButton;
	public Sprite soundOnSprite;
	public Sprite soundOffSprite;
	public static int sound = 1;

	void Awake () {
		LoadSoundSettings ();
	}

	public void SoundButtonClick () {
		if (soundButton.GetComponent<Image> ().sprite == soundOnSprite) {
			soundButton.GetComponent<Image> ().sprite = soundOffSprite;
			sound = 0;
			gameObject.GetComponent<AudioSource> ().Pause ();
		} else {
			soundButton.GetComponent<Image> ().sprite = soundOnSprite;
			sound = 1;
			gameObject.GetComponent<AudioSource> ().Play ();
		}
		PlayerPrefs.SetInt (DRConstants.sound_settings, sound);
	}

	public void LoadSoundSettings () {
		sound = PlayerPrefs.GetInt (DRConstants.sound_settings, 1);
		if (sound == 0) {
			soundButton.GetComponent<Image> ().sprite = soundOffSprite;
			gameObject.GetComponent<AudioSource> ().Pause ();
		} else {
			soundButton.GetComponent<Image> ().sprite = soundOnSprite;
			gameObject.GetComponent<AudioSource> ().Play ();
		}
	}
}
