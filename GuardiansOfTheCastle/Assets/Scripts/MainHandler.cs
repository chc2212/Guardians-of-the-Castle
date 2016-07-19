using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainHandler : MonoBehaviour {
	public static bool Play, Quit, About;
	private Rect playRect = new Rect (400,200,400,100);
	private Rect aboutRect = new Rect (400,400,400,100);
	private Rect quitRect = new Rect (400,575,400,100);
	private Rect volumeRect = new Rect (100,520,200,200);
	public string FirstLevel;
	public string AboutScene;

	void Start(){
		Play = false;
		Quit = false;
		About = false;
	}

	void Update(){

		if (Play) {
			Application.LoadLevel(FirstLevel);
		}
		if (About) {
			Application.LoadLevel(AboutScene);
		}
		if (Quit) {
			Application.Quit();
		}
		
	}

	void OnGUI(){
		/*
		GUI.backgroundColor = Color.clear;
		GUI.Box (playRect, "");
		GUI.Box (aboutRect, "");
		GUI.Box (quitRect, "");
		GUI.Box (volumeRect, "");
		*/
		Event e = Event.current;
		if (Input.GetMouseButton (0)) {
			if (playRect.Contains (e.mousePosition)) {
					Play = true;
					About = false;
					Quit = false;
					//print ("Play->" + Play + ",About->" + About + ",Quit->" + Quit);
			}
			if (aboutRect.Contains (e.mousePosition)) {
					Play = false;
					About = true;
					Quit = false;
					//print ("Play->" + Play + ",About->" + About + ",Quit->" + Quit);
			}
			if (quitRect.Contains (e.mousePosition)) {
					Play = false;
					About = false;
					Quit = true;
					//print ("Play->" + Play + ",About->" + About + ",Quit->" + Quit);
			}
		}
	}
}

