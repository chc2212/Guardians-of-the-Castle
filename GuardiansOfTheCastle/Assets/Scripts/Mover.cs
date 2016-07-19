using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
	public int selector; // to tell whether it is for bullet or alien 0-alien 1-bullet

	// Use this for initialization
	void Start () {
		if (selector == 0) {
						rigidbody.velocity = transform.right * speed;
				} else {
			Vector3 mov = new Vector3(0,1,0);
			rigidbody.velocity = mov * (-speed);

				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
