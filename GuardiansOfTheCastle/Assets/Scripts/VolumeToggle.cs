using UnityEngine;
using System.Collections;

public class VolumeToggle : MonoBehaviour {
	public Sprite VolumeOn,VolumeOff;
	public SpriteRenderer spriterenderer;
	private Rect volumeRect = new Rect (100,520,200,200);
	private bool isVol;
	// Use this for initialization
	void Start () {
		spriterenderer = gameObject.GetComponent<SpriteRenderer> ();
		isVol = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (isVol && isTouched ()) {
			SetVolume(PlayerPrefs.GetInt("volume", 50) != 50 ? true : false);
		}

	}

	void OnGUI(){
//		GUI.Box (volumeRect,"");
		Event e = Event.current;
		if (Input.GetMouseButton (0)) {
			if(volumeRect.Contains(e.mousePosition)){
				isVol=true;
			}else{
				isVol=false;
			}
		}
	}


	public void SetVolume(bool IsOn) {
		if(IsOn) {
			spriterenderer.sprite = VolumeOn;
			PlayerPrefs.SetInt("volume", 50);
			audio.volume=1.0f;

		} else {
			spriterenderer.sprite = VolumeOff;
			PlayerPrefs.SetInt("volume", 0);
			audio.volume=0.0f;
		}
	}


	public bool isTouched()
	{

		bool result = false;
	
		if (Input.touchCount == 1) {
			if(Input.touches[0].phase == TouchPhase.Ended)
			{
				Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Vector2 touchPos = new Vector2(wp.x, wp.y);
				if (collider2D == Physics2D.OverlapPoint(touchPos)) {
					result = true;
				}
			}
		}

		if(Input.GetMouseButtonUp(0)) {
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(mousePos)) {
				result = true;
			}
		}
		return result;
	}
}