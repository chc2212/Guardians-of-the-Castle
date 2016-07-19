using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {
	Sprite spriteImage;
	// Use this for initialization
	void Start () {
		spriteImage = Resources.Load("fire_001",typeof(Sprite)) as Sprite;
		GetComponent<SpriteRenderer>().sprite = spriteImage;
		print (spriteImage);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
