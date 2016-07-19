using UnityEngine;
using System.Collections;

public class Audio_Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {	
	}
	// Update is called once per frame
	void Update () {	
		if (DestroyOnContact.Explosion_sound == true) {
			DestroyOnContact.Explosion_sound = false;
					this.audio.Play ();
				}	
	}
}
