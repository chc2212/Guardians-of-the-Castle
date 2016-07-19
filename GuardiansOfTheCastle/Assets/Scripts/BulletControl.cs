using UnityEngine;

using System.Collections;



public class BulletControl : MonoBehaviour {
	
	
	
	//private float spawntime=0;
	
	private float gametime;
	
	public float basic_bullet_speed;
	
	public static bool basic, multiple, bomb1, bomb2, fire, ice, sniper, wall;
	
	
	// Use this for initialization
	
	void Start () {	
		basic = false;
		multiple=false;
		fire=false;
		bomb1=false;
		ice=false;
		sniper=false;
		wall=false;
		bomb2=false;
		Vector3 down = new Vector3 (0, 1, 0);
		switch(this.tag){
		case "acidrain":
		case "bullet_basic":
			basic = true;
			rigidbody.velocity = down * (-basic_bullet_speed);
			break;
		
		case "bullet_multiple":
			multiple=true;
			//print ("changeX="+BulletSpawn.changeX);
			BulletSpawn.changeX-=0.4f;
			//print(BulletSpawn.changeX);
			Vector3 multi = new Vector3 (BulletSpawn.changeX, 1, 0);
			this.rigidbody.velocity = multi * (-basic_bullet_speed);
			//BulletSpawn.changeX=1;
			break;
			
		case "bullet_fire":
			fire=true;
			break;
		case "bullet_bomb1":
			bomb1=true;
			rigidbody.velocity = down * (-basic_bullet_speed);
			break;
		case "bullet_ice":
			ice=true;
			break;
		case "bullet_sniping":
			sniper=true;
			break;
		case "bullet_wall":
			wall=true;
			rigidbody.velocity = down * (-basic_bullet_speed);
			break;
		case "bullet_bomb2":
			bomb2=true;
			rigidbody.velocity = down * (-basic_bullet_speed);
			break;
		}
		
	}
	
	
	
	// Update is called once per frame
	
	void Update () {
		
		//Wall 
		//print ("Bottom->"+Inventory.BottomMenu);
		if (Input.GetMouseButton (0) && (fire||ice||sniper)) {
			Vector3 mov = new Vector3 ((float)(0.5f-(Input.mousePosition.x/1400)),(float)(0.5f-(Input.mousePosition.y/600)),0);
			rigidbody.velocity = mov * (-basic_bullet_speed);
		}

		/*
		if (wall) {	
			if (120 <= Time.timeScale)
				Destroy (this.gameObject);
		}
		*/
		
	}
	
	
	
	
	
}

