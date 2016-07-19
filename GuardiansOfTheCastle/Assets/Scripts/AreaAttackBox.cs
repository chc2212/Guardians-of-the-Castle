using UnityEngine;
using System.Collections;

public class AreaAttackBox : MonoBehaviour {

	private float spawntime;

	//--steve
	void Start () {
		spawntime = Time.time;
	}
	
	void Update () {

		//print (frame);
		if (Time.time - spawntime >= 1) //나중에 정확하게 바꾸기 
			Destroy (this.gameObject);	
	}
}
