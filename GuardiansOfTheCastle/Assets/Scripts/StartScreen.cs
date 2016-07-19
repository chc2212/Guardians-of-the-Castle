using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
	public string FirstLevel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetMouseButtonDown(0))
			return;
		Application.LoadLevel(FirstLevel);
	}

}
