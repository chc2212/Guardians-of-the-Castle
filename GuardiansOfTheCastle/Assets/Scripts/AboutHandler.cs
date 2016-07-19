using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AboutHandler : MonoBehaviour {

	public static bool About;
	public GUISkin skin;

	private Rect backAboutRect = new Rect(900,500,200,200);
	public string FirstLevel;

	void Start(){
		
	}

	void Update(){

		
	}

	void OnGUI(){
		GUI.skin = skin;
//		GUI.Box(mainMenuRect,"",skin.GetStyle ("back_button"));

		GUI.Box(backAboutRect,"",skin.GetStyle ("back_button"));
		Event e = Event.current;
		if (Input.GetMouseButton (0)) {
			if (backAboutRect.Contains(e.mousePosition)) {
				Application.LoadLevel(FirstLevel);
			}
		}
	}
	
}

