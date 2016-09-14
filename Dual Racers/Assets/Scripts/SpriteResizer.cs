using UnityEngine;
using System.Collections;

public class SpriteResizer : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public float barrierWidth;
	public float barrierHeight;
	private Transform myTrans;
	
	void Awake () {
		myTrans = transform;
		ResizeSpriteToScreen ();
	}
	
	public void ResizeSpriteToScreen () {
		if (spriteRenderer != null) {
			myTrans.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			
			float width = spriteRenderer.sprite.bounds.size.x;
			float height = spriteRenderer.sprite.bounds.size.y;
			
			float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
			float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
			
			myTrans.localScale = new Vector3 ((worldScreenWidth / width) * barrierWidth, (worldScreenHeight / height) * barrierHeight, 1.0f);
		}
	}
}
