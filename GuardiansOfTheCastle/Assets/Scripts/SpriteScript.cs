using UnityEngine;
using System.Collections;

public class SpriteScript : MonoBehaviour {
	public Sprite[] FireArray;
	public static SpriteRenderer spriterenderer;
	public static bool CollisionDetected;
	// Use this for initialization
	void Start () {
		FireArray = new Sprite[72];
		spriterenderer = gameObject.GetComponent<SpriteRenderer> ();
		CollisionDetected = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isDestroyed ()) {
			print ("collision"+CollisionDetected);
			//SetVolume(PlayerPrefs.GetInt("volume", 50) != 50 ? true : false);
			StartCoroutine(spawnCollision ());
		}
		
		
	}


	IEnumerator spawnCollision(){
		print ("spawn coll");
		Vector3 CollisionPosition=new Vector3 (-5.0f, 29.0f, 2.0f);
		Quaternion rot = Quaternion.identity;
		for(int i=0;i<FireArray.Length;i++)
		{
			print ("fire"+i);
			//spriterenderer.sprite = FireArray[i];
			Instantiate(FireArray[i], CollisionPosition, rot);
		}
		yield return new WaitForSeconds (1);
	}


	private void SetVolume(bool IsOn) {

		if(IsOn) {
			PlayerPrefs.SetInt("volume", 50);
			audio.volume=1.0f;
			
		} else {

			PlayerPrefs.SetInt("volume", 0);
			audio.volume=0.0f;
		}
	}
	
	public bool isDestroyed(){

		return CollisionDetected;
	}
}
	