using UnityEngine;
using System.Collections;

public class BulletSpawn : MonoBehaviour {
	public GameObject acidRain,slowMotion;
	public GameObject firstBullet0,firstBullet1,firstBullet2,firstBullet3,firstBullet4;
	public GameObject secondBullet0,secondBullet1,secondBullet2,secondBullet3,secondBullet4;
	public float Delay0,Delay1,Delay2,Delay3,Delay4,Delay5,Delay6,Delay7,Delay8,Delay9;
	
	//public GameObject[] specialPower;
	public float[] Delay;
	public bool gameOver2,gamePause2,SpawnOnce;
	public static float changeX=1;
	public GameObject[] firstBullet, secondBullet;
	public float bulletSpeed ;
	public float fireRate;
	public float fireRadius ;
	public float spawnWait;
	//public int[] remainingBullets;
	public static Vector3 wizardPosition=new Vector3 (-11.0f, 29.0f, 2.0f);
	
	// Use this for initialization
	void Start () {
		SpawnOnce = true;
		createBulletList ();
		//remainingBullets = new int[6]; // Number 0 : Acid Rain
		//remainingBullets [2] = 3;
	}
	
	IEnumerator spawnBullet()
	{
		while(true) // to do : make this happen as long as user holds the shoot button
		{	
			if(Inventory.wizardBasicShooting1 == true) //go on basic shooting -steve
			{							
				Quaternion rot = Quaternion.identity;
				//firstBullet[Inventory.wizardNumber-1].transform.Rotate(Vector3.up * Time.deltaTime);
				var item = Instantiate(firstBullet[Inventory.wizardNumber-1], wizardPosition, rot);
				if(Inventory.wizardNumber!=5)
					Destroy(item,10);		// 10 secs for rest of the first bullets
				else
					Destroy(item,20);		// 120 secs for wall dissapear
				yield return new WaitForSeconds (Delay[Inventory.wizardNumber-1]);
			}	
			
			if(Inventory.wizardBasicShooting2 == true) //go on basic shooting -steve
			{	
				//BulletControl.fire=false;
				//BulletControl.ice=false;
				//BulletControl.sniper=false;
				
				Quaternion rot = Quaternion.identity;
				if(Inventory.wizardNumber==1){
					if(!GameControllerScript.MultipleUsed){
						Vector3 NewWizardPosition=wizardPosition;
						NewWizardPosition.x-=10;
						changeX=1;
						for(int i=0; i<5; i++){
							Instantiate(secondBullet[Inventory.wizardNumber-1], NewWizardPosition, rot);
							NewWizardPosition.x+=5;
						}
						GameControllerScript.MultipleUsed=true;
					}
				}
				else{
					if(Inventory.wizardNumber-1 == 1 && !GameControllerScript.Bomb1Used){
						Instantiate(secondBullet[1], wizardPosition, rot);
						GameControllerScript.Bomb1Used=true;
					}
					if(Inventory.wizardNumber-1 == 4 && !GameControllerScript.Bomb2Used){
						Instantiate(secondBullet[4], wizardPosition, rot);
						GameControllerScript.Bomb2Used=true;
					}
				}
				yield return new WaitForSeconds (Delay[Inventory.wizardNumber-1]);
			}
			
			if(Input.GetMouseButton(0) && (BulletControl.fire||BulletControl.ice||BulletControl.sniper)){
				Quaternion rot = Quaternion.identity;
				var item = Instantiate(firstBullet[Inventory.wizardNumber-1], wizardPosition, rot);
				Destroy(item,5);
				yield return new WaitForSeconds (Delay[Inventory.wizardNumber-1]);					
			}
			
			/*
				else if(Inventory.wizardSpecialShooting == true) //go on special shooting -steve
				{
				print ("second bullet");

					for(int i=0; i<5;i++)
					{
						if(Inventory.wizardNumber == i)
						{					
							print (i);
							if(remainingBullets[i] >= 1)
							{
							Quaternion rot = Quaternion.identity;
							Instantiate(specialBullet[i], wizardPosition, rot);
							remainingBullets[i]--;
							yield return new WaitForSeconds (spawnWait);
							}
							else
								yield return new WaitForSeconds (1); // if remaning bullets is 0
						}
					}
				}
				*/
			else  //stop shooting -steve
			{
				yield return new WaitForSeconds (1);		
			}
			
		}		
	}
	
	
	// Update is called once per frame
	void Update () {
		/*
		if (GameControllerScript.gameOver)
			gameOver2 = true;
		else
			gameOver2 = false;

		if (GameControllerScript.GamePause)
			gamePause2 = true;
		else
			gamePause2 = false;
		*/
		
		if(!GameControllerScript.GamePause && SpawnOnce && Inventory.clickedItem){
			StartCoroutine(spawnBullet ());
			SpawnOnce=false;
		}
	}
	
	
	
	public void createBulletList()
	{
		firstBullet = new GameObject[5];
		secondBullet = new GameObject[5];
		Delay = new float[10];
		
		firstBullet[0] = firstBullet0;
		firstBullet[1] = firstBullet1;
		firstBullet[2] = firstBullet2;
		firstBullet[3] = firstBullet3;
		firstBullet[4] = firstBullet4;	
		
		secondBullet[0] = secondBullet0;
		secondBullet[1] = secondBullet1;
		secondBullet[2] = secondBullet2;
		secondBullet[3] = secondBullet3;
		secondBullet[4] = secondBullet4;		
		
		Delay [0] = Delay0;
		Delay [1] = Delay1;
		Delay [2] = Delay2;
		Delay [3] = Delay3;
		Delay [4] = Delay4;
		Delay [5] = Delay0;
		Delay [6] = Delay1;
		Delay [7] = Delay2;
		Delay [8] = Delay3;
		Delay [9] = Delay4;
		
	}
	
	
}

