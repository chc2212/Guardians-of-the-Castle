using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour {
	public GameObject enemy1,enemy2,enemy3,enemy4,enemy5; 
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public Vector3 spawnValues;
	private GameObject[] lstEnemies;
	public static int score,lastScore;
	private bool displayGold=true;
	private bool showInventory;
	public static bool GamePause;
	public static bool InitStart;
	public string MainMenu;
	private static int tickTock;
	private static bool startTicking;
	public static string enemyName;
	// prem
	public GameObject block;
	public bool SpawnBlockOnce;
	public Vector3 blockPosition;
	public int blockLeftLimit;
	public float blockSpeed;
	private static int numAlienHits;
	private static int totalLives = 3;
	public static int totalAliensToKill;
	private bool levelOver;
	private static int numWizrdHits;
	public static bool gameOver;
	public NextLevel s1 = new NextLevel();
	public static int levelCompleted; 

	public GameObject acidRainParticle;
	public static bool MultipleUsed,Bomb1Used,StopTimeUsed,AcidRainUsed,Bomb2Used;
	public GUISkin levelSkin;
	public GUISkin gameOver1;
	public GUISkin gameComplete;
	public GUISkin skin;
	
	// Use this for initialization
	void Start () {
		//print ("came to start!");
		// start with Level 1
		InitStart = true;
		startTicking = true;
		levelCompleted = 0;
		SpawnBlockOnce = true;
		showInventory = true;
		numAlienHits = 0;
		score = 0;
		resetLevel();
		MultipleUsed = false;
		Bomb1Used = false;
		StopTimeUsed = false;
		AcidRainUsed = false;
		Bomb2Used=false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!GamePause && InitStart){
			StartCoroutine(spawnEnemy ());
			InitStart=false;
		}
		
		// resume
		if(!GamePause && !InitStart){
			Time.timeScale = 1.0f;
		}
		// pause
		if(GamePause && !InitStart){
			Time.timeScale = 0;
		}
		
		// end game conditions
		//print (numWizrdHits+"=="+totalAliensToKill);
		if (numAlienHits >= totalLives) {
			endGame();
		}else if (numWizrdHits >= totalAliensToKill) {
			endLevel();
		}
	}

	void displayScore()
	{
		// code to display GUI text for score
		GUI.Box (new Rect (Inventory.panelWidthPos+280, Inventory.panelHeightPos+10, Inventory.slotWidth*1.5f, Inventory.slotHeight), score.ToString()); // -kedar
	}
	
	void displayLevel()
	{
		// code to display GUI text for score
		GUI.Box (new Rect (Inventory.panelWidthPos+380,Inventory.panelHeightPos+10, Inventory.slotWidth+30, Inventory.slotHeight), "Level: "+levelCompleted);
		
	}
	
	// prem
	public static void endGame()
	{
		gameOver = true;
		DestroyOnContact.DeleteAll ();
	}
	
	public void endLevel()
	{
		levelOver = true;
		DestroyOnContact.DeleteAll ();
	}
	
	
	public void alienHit()
	{
		Inventory.RemoveHeart();
		numAlienHits++;
	}
	
	public void wizardHit(int add)
	{
		score += add;
		numWizrdHits++;
	}
	
	
	public void createList()
	{
		lstEnemies = new GameObject[5];

		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.y = -90;
		enemy1.particleSystem.enableEmission = false;
		enemy2.particleSystem.enableEmission = false;
		enemy3.particleSystem.enableEmission = false;
		enemy4.particleSystem.enableEmission = false;
		enemy5.particleSystem.enableEmission = false;

		lstEnemies[0] = enemy1;
		lstEnemies[1] = enemy2;
		lstEnemies[2] = enemy3;
		lstEnemies[3] = enemy4;
		lstEnemies[4] = enemy5;
	}
	
	IEnumerator spawnEnemy()
	{ 
		yield return new WaitForSeconds (startWait);		
		for (int i=0; i<hazardCount; i++) {
			Vector3 pos = new Vector3 (spawnValues.x, spawnValues.y, spawnValues.z);
			//Quaternion rot = Quaternion.identity;
			Quaternion rot = Quaternion.Euler(0,90,0);
			GameObject enemy = lstEnemies [Random.Range(1,5)];
			//GameObject enemy = lstEnemies [0];
			enemyName = enemy.name;
			/*
			if(enemy.name=="Alien1"){
				rot = Quaternion.Euler(0,-90,0);
			}
			*/
			Instantiate (enemy, pos, rot);
			if(gameOver)
				break;
			yield return new WaitForSeconds (spawnWait);
		}
	}
	
	void OnGUI(){
		GUI.skin = levelSkin;
		GUI.skin = gameOver1;
		GUI.skin = gameComplete;
		GUI.skin = skin;

		displayLevel ();
		if(showInventory && displayGold){
			displayScore ();
		}

		// (towards right(X), towards bottom(Y), width, height)
		if (gameOver) {
			// Display Game Over
			GUI.Box (new Rect (400, 100, 500, 250), "", gameOver1.GetStyle("GameOver"));	
			// buttons
			Rect mainMenuRect = new Rect(600, 280, 75, 75);
			GUI.Box(mainMenuRect,"",skin.GetStyle ("back_button"));
			GUI.backgroundColor = Color.clear;
			GUI.color = Color.black;
			GUI.Box(new Rect (550, 250, 150, 50),""+score);
			GamePause=true;

			Event e = Event.current;
			if(Input.GetMouseButton (0)){
				if(mainMenuRect.Contains(e.mousePosition)){
					gameOver=false;
					Inventory.numOfHearts=3;
					Application.LoadLevel(MainMenu);
				}
			}
		}

		if (levelOver) {
			Rect nextLevelRect = new Rect(635, 330, 150, 50);
			Rect restartLevelRect = new Rect(470, 330, 150, 50);
			Rect mainMenuRect = new Rect(550, 390, 150, 50);

			//print ("Other times "+Time.time);
			if(levelCompleted!=3){
				GUI.Box (new Rect (450, 100, 350, 400),"",levelSkin.GetStyle("levelSlot"));
				// next level buttons 
				GUI.Box(nextLevelRect,"");
				GUI.Box(restartLevelRect,"");
				GUI.Box(mainMenuRect,"");
				
				GUI.backgroundColor = Color.clear;
				GUI.Box(new Rect (550, 300,150, 50),""+score);// -kedar
			}else{
				GUI.Box (new Rect (450, 25, 650, 400),"",levelSkin.GetStyle("gameComplete"));
				//levelOver=false;
				//levelCompleted=0;
				//resetLevel();

			}
			GamePause=true;
			Event e = Event.current;
			if(Input.GetMouseButton (0)){
				if(nextLevelRect.Contains(e.mousePosition)){
					//levelCompleted++;		// increment level
					levelOver=false;
					if(levelCompleted <=3){
						resetLevel();
					}
					GamePause=false;
					/*
					// call next level
					if(levelCompleted == 1){
						resetLevel();
					}
					else if(levelCompleted == 2){
						resetLevel();
					}
					else if(levelCompleted == 3){
						resetLevel();
					}
					*/
					//print ("Level= "+levelCompleted+", total aliens to kill= "+totalAliensToKill);
				}
				else if(restartLevelRect.Contains(e.mousePosition)){
					levelOver=false;
					//resetLevel();
					levelOver = false;
					gameOver = false;
					numWizrdHits = 0;
					createList ();
					GamePause=false;
					score = lastScore;
					//print ("Same level");
				}
				else if(mainMenuRect.Contains(e.mousePosition)){
					levelOver=false;
					levelCompleted=0;
					resetLevel();
					Application.LoadLevel(MainMenu);
				}
			}
		}
		if(!gameOver && !levelOver){
			GUI.DrawTexture(new Rect(25,25,200,100),Resources.Load<Texture2D> ("Alien Icons/spaceship2"));
		}else{
			GamePause=true;
		}
	}
	
	//prem
	public void FixedUpdate()
	{	
		if(block!=null){
			block.renderer.enabled = false;
			block.rigidbody.detectCollisions = false;
			if (levelCompleted==2 && score >= 50) {
				block.rigidbody.detectCollisions = true;
				block.renderer.enabled = true;
				if(block.rigidbody.position.x == 20){
					block.rigidbody.velocity = transform.right * -blockSpeed;
				}
				else if(block.rigidbody.position.x <= blockLeftLimit){
					block.rigidbody.velocity = transform.right * blockSpeed;
				}
			}
		}
		//if (levelCompleted == 1 && score >= 1 && !acidRainUsed) {
		if(Inventory.wizardNumber==4 && Inventory.wizardBasicShooting2 && !AcidRainUsed){
			startAcidRain();
		}
	}
	
	public void startAcidRain()
	{
		AcidRainUsed = true;
		Quaternion rot = Quaternion.identity;
		for(int j=20; j<=200; j+=20){
			for (int i=-100; i<=150; i+=6) {
				Vector3 wizardPosition = new Vector3 (Random.Range (100, -120), Random.Range (20, 100), 2);
				//Vector3 wizardPosition = new Vector3 (i,j, 2);
				var item = Instantiate (acidRainParticle, wizardPosition, rot);

			}
		}
	}

	public void resetLevel(){
		GamePause = true;
		levelOver = false;
		gameOver = false;
		numWizrdHits = 0;
		lastScore = score;
		createList ();
		levelCompleted++;
		// set totalAliensToKill according to the game level
		if (levelCompleted == 1)
			totalAliensToKill = 2;
		else if(levelCompleted == 2)
			totalAliensToKill = 3;
		else if(levelCompleted == 3)
			totalAliensToKill = 4;
		
	}
	
}
