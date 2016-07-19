using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyOnContact : MonoBehaviour {
	public int destroyValue;
	public string type;
	public bool shield;
	public GameControllerScript gameController;
	public float basicDamage,FireDamage,BombDamage,IceDamage,SnipingDamage;
	public GameObject areaFireBox, areaBombBox, areaIceBox;
	
	//steve
	public float hp;
	public int fireShotTime;
	public int IceShotTime;
	public bool burning;
	public bool iced;
	public int bomb_shot_time;
	public int time_temp1;

	public GameObject basicBulletEffect, fireBulletEffect, bomb1Effect, snipingBulletEffect;
	public Vector3 effectTempPosition;

	public static bool Explosion_sound;

	void Start()
	{
		iced = false;
		burning = false;
		bomb_shot_time = -99999;
		fireShotTime = -99999;
		IceShotTime = -99999;
		hp = 100;
		time_temp1 = 0;
		
		
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameControllerScript>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		

	}
	
	
	
	void Update(){

		//check fallen Alien Steve1
		if (this.tag == "alien") {
			if(this.transform.position.y <= -90)
			{	
				Destroy(gameObject);
				gameController.wizardHit (destroyValue);
			}
		}
		
		if(this.tag=="alien")
			this.light.intensity = 0;
		//--steve Alien are damaged for 7 sec after shot by fire. 
		if (this.tag == "alien" ) {
			if ((int)Time.time - (int)this.fireShotTime <= 7) {
				//this.light.intensity = 5;
				//this.light.color = Color.red;
				//print ("test");

				//each 1 sec	
				if ((int)Time.time - time_temp1 >= 1){
					time_temp1 = (int)Time.time;
					burning = true;
					this.hp = this.hp - FireDamage;
					if(this.hp<=0)
					{
						Destroy(gameObject);
						gameController.wizardHit(destroyValue);
						// copy
						SpriteScript.CollisionDetected=true;
					}
					else{
						SpriteScript.CollisionDetected=false;
					}
					//print ("sprite->"+SpriteScript.CollisionDetected);
				}
			} else {
				burning = false;
				this.fireShotTime = -99999;
			}
		}
		
		//--steve Alien are damaged for 7 sec after shot by ice. 
		if (this.tag == "alien" ) {
			if ((int)Time.time - this.IceShotTime <= 7) {
				//print ((int)Time.time - this.IceShotTime);
				this.light.intensity = 8;
				this.light.color = Color.blue;
				EnemyMovement.icedStatus = true;					

				/*
				// to damage by ice
				this.hp=this.hp-IceDamage;
				if(this.hp<=0)
				{
					Destroy(gameObject);
					gameController.wizardHit(destroyValue);
					// copy
					SpriteScript.CollisionDetected=true;
				}
				else{
					SpriteScript.CollisionDetected=false;
				}
				print ("sprite->"+SpriteScript.CollisionDetected);
				*/
			} else {
				EnemyMovement.icedStatus = false;
				iced = false;
				this.IceShotTime = -99999;
			}
		}
		
		
		/*
		if(GameControllerScript.GamePause){
			//Destroy (gameObject);
			DeleteAll();
			print ("DOC Pause");
		}
	//	print ("Pause>"+GameControllerScript.GamePause);
	*/
	}
	
	void OnTriggerEnter(Collider other)
	{ 
		if (this.tag == "alien" && other.tag == "acidrain")
		{
			Destroy(gameObject);
			gameController.wizardHit(destroyValue);
		}
		if(!GameControllerScript.GamePause){
			// alien hit castle
			if (this.tag == "alien" && other.tag == "castle") {
				Explosion_sound = true;
				Destroy (gameObject);
				gameController.alienHit();
			}

			// dunno
			if (this.tag=="alien" && other.tag == "areaFireBox" && burning == false && !this.shield) {
				burning = true;
				this.fireShotTime = (int)Time.time;
				if(hp<=0)
				{
					Destroy (gameObject);
					gameController.wizardHit(destroyValue);
				}
			}
			
			// dunno
			if (this.tag == "alien" && other.tag == "areaBombBox" && (int)Time.time-2 >= bomb_shot_time && !this.shield )//after 2 sec
			{
				bomb_shot_time = (int)Time.time;
				hp = hp - BombDamage;
				if(hp<=0)
				{
					Destroy (gameObject);
					gameController.wizardHit(destroyValue);
				}			
			}
			
			
			// dunno
			if (this.tag == "alien" && other.tag == "areaIceBox" && iced == false && !this.shield) {
				iced = true;
				this.IceShotTime = (int)Time.time;
				hp = hp - IceDamage;
				if(hp<=0)
				{
					Destroy (gameObject);
					gameController.wizardHit(destroyValue);
				}
			}
			
			
			
			
			// bullet1(Single) or bullet2 (Multiple) hit alien
			if (this.tag == "alien" && (other.tag == "bullet_basic" || other.tag == "bullet_multiple")){
				Explosion_sound = true;
				//print ("shield?"+this.shield);
				if(this.shield){
					Component halo = this.GetComponent("Halo");
					halo.GetType().GetProperty("enabled").SetValue(halo,false,null);
					this.shield=false;
				}
				else{
					Destroy (other.gameObject);
					hp = hp - basicDamage;
					//effect
					effectTempPosition = this.transform.position;
					effectTempPosition.y = effectTempPosition.y + 2;
					Quaternion rot = Quaternion.identity;			
					Instantiate(basicBulletEffect,effectTempPosition, rot);
					if(hp<=0)
					{				
						Destroy (gameObject);
						gameController.wizardHit(destroyValue);
					}
				}
			}						
			
			// bullet3(Fire) hit alien
			if (this.tag == "alien" && other.tag == "bullet_fire" && !this.shield) {
				Explosion_sound = true;
				Destroy (other.gameObject);
				//Spawning Area Fire Box 
				Quaternion rot = Quaternion.identity;			
				Instantiate(areaFireBox,other.transform.position, rot);
				//effect
				effectTempPosition = this.transform.position;
				effectTempPosition.y = effectTempPosition.y + 2;
				Instantiate(fireBulletEffect,effectTempPosition, rot);				
				this.particleSystem.enableEmission=true;
			}

			// bullet4(Bomb) hit alien
			if (this.tag == "alien" && other.tag == "bullet_bomb1") {
				Explosion_sound = true;
				Destroy (other.gameObject);
				//Spawning Area Bomb Box 
				Quaternion rot = Quaternion.identity;			
				Instantiate(areaBombBox,other.transform.position, rot);
				//effect
				effectTempPosition = this.transform.position;
				effectTempPosition.y = effectTempPosition.y + 2;
				Instantiate(bomb1Effect,effectTempPosition, rot);
				/*
				hp = hp - BombDamage;
				if(hp<=0)
				{				
					Destroy (gameObject);
					gameController.wizardHit(destroyValue);
				}
				*/
			}

			// bullet5(Ice) hit alien
			if (this.tag == "alien" && other.tag == "bullet_ice" && !this.shield) {
				Explosion_sound = true;
				Destroy (other.gameObject);
				Quaternion rot = Quaternion.identity;			
				Instantiate(areaIceBox,other.transform.position, rot);
			}									
			
			// bullet6(Sniping) hit alien
			if (this.tag == "alien" && other.tag == "bullet_sniping" && !this.shield) {
				Explosion_sound = true;
				Destroy (other.gameObject);
				hp = hp - SnipingDamage;
				//effect
				effectTempPosition = this.transform.position;
				effectTempPosition.y = effectTempPosition.y + 2;
				Quaternion rot = Quaternion.identity;			
				Instantiate(snipingBulletEffect,effectTempPosition, rot);
				if(hp<=0)
				{				
					Destroy (gameObject);
					gameController.wizardHit(destroyValue);
				}				
			}
			

			// bullet hit block
			if(this.tag == "block"){
				if(other.tag == "bullet_basic"
				   ||
				   other.tag == "bullet_multiple"
				   ||
				   other.tag == "bullet_fire"
				   ||
				   other.tag == "bullet_bomb1"
				   ||
				   other.tag == "bullet_ice"
				   ||
				   other.tag == "bullet_sniping"
				   ||
				   other.tag == "bullet_bomb2"
				   ||
				   other.tag == "bullet_wall"
				   )
				Destroy (other.gameObject);
			}
			
			if((this.tag=="wizard1" || this.tag=="wizard2" || this.tag=="wizard3" || this.tag=="wizard4" || this.tag=="wizard5") && other.tag=="coin"){
				Destroy (other.gameObject);
				GameControllerScript.score+=10;
			}
			
			if((this.tag=="wizard1" || this.tag=="wizard2" || this.tag=="wizard3" || this.tag=="wizard4" || this.tag=="wizard5") && other.tag=="alien"){
				Explosion_sound = true;
				Inventory.RemoveHeart();
				if(Inventory.numOfHearts==0)
				{
					GameControllerScript.endGame();
				}					
			}
		}
	}
	
	public static void DeleteAll(){
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
			if(o.tag == "alien")
			{
				Destroy(o);
			}
		}
	}
}
