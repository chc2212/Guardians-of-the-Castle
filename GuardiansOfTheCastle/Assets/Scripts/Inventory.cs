using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	// Generalized initialization
	//public int slotsX, slotsY;
	public GUISkin skin;
	private bool showInventory;
	public static bool clickedItem;
	private Item clickedWizard;
	//private int prevIndex;
	public static int panelWidthPos=-13,panelHeightPos=450,slotHeight=40,slotWidth=40;
	public List<Item> inventory = new List<Item>();
	public GameObject wizardObject;
	public GameObject Wizard1,Wizard2,Wizard3,Wizard4,Wizard5;
	private GameObject[] allWizards;
	public static Vector3 wizardPosition=new Vector3 (-11.0f, 29.0f, 2.0f);

	// Sky - Kedar
	private static int skyX=500,skyY=100,moveBy=2; // initial position and move by how many x and y values
	private bool goWest,goEast,goSouth,goNorth; // Wizard in Sky movement flags
	private bool goNorthEast,goNorthWest,goSouthEast,goSouthWest;
	private ItemDatabase database;
	public static int numOfHearts = 3;
	
	//Wizard_Control - steve
	public static int wizardNumber; 
	public static bool wizardBasicShooting1,wizardBasicShooting2;
	//public static bool wizardSpecialShooting1,wizardSpecialShooting2;
	
	// Section 1:- GameFlow
	private List<Item> gameFlowSlots = new List<Item>();
	//private Rect gameFlowRect = new Rect (1150,160,130,130);
	private Rect gameFlowRect = new Rect (panelWidthPos+220,panelHeightPos,130,140);

	// Section 2:- Profile
	private List<Item> profileSlots = new List<Item>();
	//private Rect profileRect = new Rect (1150,25,130,130);
	private Rect profileRect = new Rect (panelWidthPos+360,panelHeightPos,130,140);

	// Section 3:- Power
	private List<Item> powerSlots = new List<Item>();
	//private Rect powerRect = new Rect (panelWidthPos+240,panelHeightPos,110,140);
	
	// Section 4:- Wizard
	private List<Item> wizardSlots = new List<Item>();
	private Rect wizardRect = new Rect (panelWidthPos+500,panelHeightPos,520,140);
	
	// Section 5:- Archer
	private List<Item> archerSlots = new List<Item>();
	private Rect archerRectL = new Rect (panelWidthPos+70,panelHeightPos-80,100,220); // Shooting panel
	private Rect archerRectR = new Rect (panelWidthPos+1080,panelHeightPos-60,200,200); // Navigation Panel
	
	
	// Use this for initialization
	void Start () {
		// create empty Slots for all
		for(int i=0; i<6;i++){
			gameFlowSlots.Add(new Item());
			profileSlots.Add(new Item());
			wizardSlots.Add(new Item());
			inventory.Add(new Item());
		}

		for(int i=0; i<10; i++)
			powerSlots.Add (new Item());
		
		//for(int i=0; i<9; i++)
		archerSlots.Add (new Item());
		
		// take reference to the available database
		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
		
		// add game flow slots
		AddGameFlow ();
		// add profile slots
		AddProfile ();
		// add power slots
		AddPower ();
		// add wizard slots
		for(int i=20; i<25; i++)
			AddWizards (i);
		// add archer slots
		AddArcher ();
		
		// showInventory
		wizardNumber = 1;
		showInventory = true;
		clickedItem = false;

		allWizards = new GameObject[5];
		allWizards [0] = Wizard1;
		allWizards [1] = Wizard2;
		allWizards [2] = Wizard3;
		allWizards [3] = Wizard4;
		allWizards [4] = Wizard5;
		for(int i=0; i<5; i++){
			allWizards[i].SetActive(false);
		}
	} // end of Start()

	
	void Update(){
		//if (!Input.GetButtonDown ("Inventory"))
		//showInventory = !showInventory;
	}
	
	void OnGUI(){
		// initialize skin of the slots
		GUI.skin = skin;

		//if(showInventory && numOfHearts>0){
		if(showInventory){
			DrawInventory();
		}
		
		
		/*
		if (numOfHearts == 0) {
			GUI.Box(new Rect (400, 100, 100, 100),"Game Over");
			//GUI.DrawTexture (new Rect (325, 240, 30, 30), powerSlots [0].itemIcon);
		}

		if(clickedItem){
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x,Event.current.mousePosition.y,50,50),draggedItem.itemIcon);
		}
		*/
	}// end of OnGUI()
	
	void DrawInventory(){
		// Draw panel GUI boxes
		GUI.Box (gameFlowRect, "");
		GUI.Box (profileRect, "");
		//GUI.Box (powerRect, "");
		GUI.Box (wizardRect, "");
		GUI.Box (archerRectL, "");
		GUI.Box (archerRectR, archerSlots[0].itemIcon);
		
		// Place gameFlow slots
		//Rect GamePause =new Rect (1160, 220, slotWidth, slotHeight);
		Rect GamePause =new Rect (panelWidthPos+230, panelHeightPos+80, slotWidth, slotHeight);

		GUI.Box (GamePause, "", skin.GetStyle ("Slot"));
		GUI.DrawTexture (GamePause, gameFlowSlots [0].itemIcon);
		Rect GamePlay=new Rect (panelWidthPos+290, panelHeightPos+80, slotWidth, slotHeight);
		GUI.Box (GamePlay, "", skin.GetStyle ("Slot"));
		GUI.DrawTexture (GamePlay, gameFlowSlots [1].itemIcon);
		GUI.DrawTexture (new Rect (panelWidthPos+230, panelHeightPos+10, slotWidth, slotHeight), gameFlowSlots [3].itemIcon);

		// Place profile slots
		int index = 2;
		for (int x=0; x<numOfHearts; x++) {
			GUI.DrawTexture (new Rect (x * slotWidth + 350, panelHeightPos+80, slotWidth, slotHeight), profileSlots [index].itemIcon);
			index++;
		}
		
		// Place power slots
		Rect BasicShootingButton1 = new Rect (panelWidthPos+80, panelHeightPos-70, slotWidth*2, slotHeight*2);
		Rect BasicShootingButton2 = new Rect (panelWidthPos+80, panelHeightPos+40, slotWidth*2, slotHeight*2);

		if (wizardNumber == 1) {
			GUI.DrawTexture (BasicShootingButton1, powerSlots [0].itemIcon);
			GUI.DrawTexture (BasicShootingButton2, powerSlots [1].itemIcon);
		} else if (wizardNumber == 2) {
			GUI.DrawTexture (BasicShootingButton1, powerSlots [2].itemIcon);
			GUI.DrawTexture (BasicShootingButton2, powerSlots [3].itemIcon);
		} else if(wizardNumber==3){
			GUI.DrawTexture (BasicShootingButton1, powerSlots [4].itemIcon);
			GUI.DrawTexture (BasicShootingButton2, powerSlots [5].itemIcon);
		} else if(wizardNumber==4){
			GUI.DrawTexture (BasicShootingButton1, powerSlots [6].itemIcon);
			GUI.DrawTexture (BasicShootingButton2, powerSlots [7].itemIcon);
		} else if(wizardNumber==5){
			GUI.DrawTexture (BasicShootingButton1, powerSlots [8].itemIcon);
			GUI.DrawTexture (BasicShootingButton2, powerSlots [9].itemIcon);
		}
		
		// Place wizard slots
		Event e = Event.current;
		index = 0;
		for (int x=0; x<6; x++) {
			Rect slotRect = new Rect (x * 100 + panelWidthPos + 520, panelHeightPos+20, slotWidth*2, slotHeight*2);
			// Sky - Kedar
			if(x==5){
				slotRect.y=skyY;
				slotRect.x=skyX;
				if(skyX >=200 && skyX <=1100 && skyY >=0 && skyY<=375 && !GameControllerScript.GamePause){
					if(goNorth && skyY>0){
						skyY-=moveBy;
						BulletSpawn.wizardPosition.y += 0.25f;
						wizardObject.transform.Translate(0,0.25f,0);
						for(int i=0; i<5; i++){
							allWizards[i].transform.Translate(0,0.25f,0);
						}
						goNorth=false;
					}
					if(goNorthEast && skyY>0 && skyX<1100){
						skyX+=moveBy;
						skyY-=moveBy;
						BulletSpawn.wizardPosition.x += 0.25f;
						BulletSpawn.wizardPosition.y += 0.25f;
						wizardObject.transform.Translate(0.25f,0.25f,0);
						for(int i=0; i<5; i++){
							allWizards[i].transform.Translate(-0.25f,0.25f,0);
						}
						goNorthEast=false;
					}
					if(goEast && skyX<1100){
						skyX+=moveBy;
						BulletSpawn.wizardPosition.x += 0.25f;
						wizardObject.transform.Translate(0.25f,0,0);
						for(int i=0; i<5; i++){
							allWizards[i].transform.Translate(-0.25f,0,0);
						}
						goEast=false;
					}
					if(goSouthEast && skyX<1100 && skyY<375){
						skyX+=moveBy;
						skyY+=moveBy;
						BulletSpawn.wizardPosition.x += 0.25f;
						BulletSpawn.wizardPosition.y -= 0.25f;
						wizardObject.transform.Translate(0.25f,-0.25f,0);
						for(int i=0; i<5; i++){
							allWizards[i].transform.Translate(-0.25f,-0.25f,0);
						}
						goSouthEast=false;
					}
					if(goSouth && skyY<375){
						skyY+=moveBy;
						BulletSpawn.wizardPosition.y -= 0.25f;
						wizardObject.transform.Translate(0,-0.25f,0);
						for(int i=0; i<5; i++){
							allWizards[i].transform.Translate(0,-0.25f,0);
						}
						goSouth=false;
					}
					if(goSouthWest && skyX>200 && skyY<375){
						skyX-=moveBy;
						skyY+=moveBy;
						BulletSpawn.wizardPosition.x -= 0.25f;
						BulletSpawn.wizardPosition.y -= 0.25f;
						wizardObject.transform.Translate(-0.25f,-0.25f,0);
						for(int i=0; i<5; i++){
							allWizards[i].transform.Translate(0.25f,-0.25f,0);
						}
						goSouthWest=false;
					}
					if(goWest && skyX>200){
						skyX-=moveBy;
						BulletSpawn.wizardPosition.x -= 0.25f;
						wizardObject.transform.Translate(-0.25f,0,0);
						for(int i=0; i<5; i++){
							allWizards[i].transform.Translate(0.25f,0,0);
						}
						goWest=false;
					}
					if(goNorthWest && skyY>0 && skyX>200){
						skyX-=moveBy;
						skyY-=moveBy;
						BulletSpawn.wizardPosition.x -= 0.25f;
						BulletSpawn.wizardPosition.y += 0.25f;
						wizardObject.transform.Translate(-0.25f,0.25f,0);
						for(int i=0; i<5; i++){
							allWizards[i].transform.Translate(0.25f,0.25f,0);
						}
						goNorthWest=false;
					}
					if(skyY<0)
						skyY=0;
					if(skyY>375)
						skyY=375;
					if(skyX<200)
						skyX=200;
					if(skyX>1100)
						skyX=1100;
				}
			}
			if(x<5){
				GUI.Box (slotRect, "", skin.GetStyle ("Slot"));
			
				wizardSlots [x] = inventory [x];
				if (wizardSlots [x].itemName != null){ 
					if(!GameControllerScript.gameOver || x<5)
						GUI.DrawTexture (slotRect, wizardSlots [x].itemIcon); 
				}
			}
			if(inventory[5].itemName==null){
				// Start of the game
				GUI.Box (new Rect (500, 100, 300, 200), "",skin.GetStyle ("chooseWiz"));	
			}
			if (slotRect.Contains (e.mousePosition) && e.type == EventType.mouseDown) {
				// to drag
				/*
				if (e.button == 0 && e.type == EventType.mouseDown && !clickedItem) {
					clickedItem = true;
					prevIndex = index;
					clickedWizard = wizardSlots [index];
					inventory [index] = new Item ();
					wizardNumber=index+1;
				}
				// to drop
				if (e.type == EventType.mouseUp && clickedItem) {
					//if(index == 5)
					inventory [5] = clickedWizard;
					inventory [prevIndex] = clickedWizard;
					clickedItem = false;
					clickedWizard = null;
				}
				*/
				if(index<5){
					clickedItem=true;
					clickedWizard = wizardSlots [index];
					inventory [5] = clickedWizard;
					wizardNumber=index+1;
					for(int i=0; i<5; i++){
						allWizards[i].SetActive(false);
					}
					allWizards[index].SetActive(true);
				}
			}
			index++;
		}
		
		
		
		
		// Place archer slots
		Rect WizardToNorth = new Rect (panelWidthPos+1150, panelHeightPos-55, slotWidth*1.5f, slotHeight*1.5f);
		Rect WizardToNorthEast = new Rect (panelWidthPos+1215, panelHeightPos-55, slotWidth*1.5f, slotHeight*1.5f);
		Rect WizardToEast = new Rect (panelWidthPos+1215, panelHeightPos+10, slotWidth*1.5f, slotHeight*1.5f);
		Rect WizardToSouthEast = new Rect (panelWidthPos+1215, panelHeightPos+80, slotWidth*1.5f, slotHeight*1.5f);
		Rect WizardToSouth = new Rect (panelWidthPos+1150, panelHeightPos+80, slotWidth*1.5f, slotHeight*1.5f);
		Rect WizardToSouthWest = new Rect (panelWidthPos+1085, panelHeightPos+80, slotWidth*1.5f, slotHeight*1.5f);
		Rect WizardToWest = new Rect (panelWidthPos+1085, panelHeightPos+10, slotWidth*1.5f, slotHeight*1.5f);
		Rect WizardToNorthWest = new Rect (panelWidthPos+1085, panelHeightPos-55, slotWidth*1.5f, slotHeight*1.5f);


		/****Click Events---start****///-steve
		
		//If clicked anywhere on the screen!
		if(Input.GetMouseButton (0)){

			// Start Game
			
			if(GamePlay.Contains(e.mousePosition) && clickedItem){
				GameControllerScript.GamePause=false;
			}
			else if(GamePause.Contains(e.mousePosition)){
				GameControllerScript.GamePause=true;
			}

			if (WizardToNorth.Contains (e.mousePosition)) {	
				goNorth=true;
			} else if (WizardToNorthEast.Contains (e.mousePosition)) {		
				goNorthEast=true;
			} else if(WizardToEast.Contains (e.mousePosition)){
				goEast=true;
			} else if(WizardToSouthEast.Contains (e.mousePosition)){
				goSouthEast=true;
			} else if (WizardToSouth.Contains (e.mousePosition)) {		
				goSouth=true;
			} else if(WizardToSouthWest.Contains (e.mousePosition)){
				goSouthWest=true;
			} else if(WizardToWest.Contains (e.mousePosition)){
				goWest=true;
			} else if(WizardToNorthWest.Contains (e.mousePosition)){
				goNorthWest=true;
			}

			//Wizard Basic Shooting
			if (BasicShootingButton1.Contains (e.mousePosition)){
				wizardBasicShooting1 = true;
			}
			else{
				wizardBasicShooting1 = false;
			}
			if (BasicShootingButton2.Contains (e.mousePosition)){
				wizardBasicShooting2 = true;
			}else{
				wizardBasicShooting2 = false;
			}
			//Wizard Special Shooting
			/*
			if (SpecialShootingButton1.Contains (e.mousePosition)) 
				wizardSpecialShooting1 = true;				
			else
				wizardSpecialShooting1 = false;
			if (SpecialShootingButton2.Contains (e.mousePosition)) 
				wizardSpecialShooting2 = true;				
			else
				wizardSpecialShooting2 = false;
			*/
		}else{
			wizardBasicShooting1 = false;
			wizardBasicShooting2 = false;
		}

		/****Click Events---end****///-steve
	}// end of DrawInventory()
	
	void AddGameFlow(){
		for(int i=0; i<gameFlowSlots.Count; i++){
			if(gameFlowSlots[i].itemName == null){
				gameFlowSlots[i]=database.items[i];
			}
		}
	}
	
	void AddProfile(){
		for(int i=0,j=5; i<profileSlots.Count; i++){
			if(profileSlots[i].itemName == null){
				profileSlots[i]=database.items[j];
				j++;
			}
		}
	}
	
	void AddPower(){
		for(int i=0,j=10; i<powerSlots.Count; i++){
			if(powerSlots[i].itemName == null){
				powerSlots[i] = database.items[j];
				j++;
			}
		}
	}
	
	void AddWizards(int id){
		for(int i=0; i<inventory.Count; i++){
			if(inventory[i].itemName == null){
				for(int j=0; j<database.items.Count;j++){
					if(database.items[j].itemID == id){
						inventory[i]=database.items[j];
					}
				}
				break;
			}
		}
	}
	
	void AddArcher(){
		for(int i=0,j=25; i<archerSlots.Count; i++){
			if(archerSlots[i].itemName == null){
				archerSlots[i] = database.items[j];
				j++;
			}
		}
	}
	
	public static void RemoveHeart(){
		numOfHearts--;
	}
	
	
}
