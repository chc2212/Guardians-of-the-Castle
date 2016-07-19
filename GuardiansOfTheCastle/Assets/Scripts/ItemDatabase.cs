using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	
	public List<Item> items = new List<Item>(); 
	
	void Start(){
		// gameFlow items
		items.Add (new Item("pause",0,Item.ItemType.gameFlow));
		items.Add (new Item("play",1,Item.ItemType.gameFlow));
		items.Add (new Item("fastfwd",2,Item.ItemType.gameFlow));
		items.Add (new Item("gold",3,Item.ItemType.gameFlow));
		items.Add (new Item("score",4,"Score",Item.ItemType.gameFlow));
		
		// profile items
		items.Add (new Item("level",5,"Level: "+(GameControllerScript.levelCompleted),Item.ItemType.profile));
		items.Add (new Item("lives",6,"Lives: ",Item.ItemType.profile));
		items.Add (new Item("heart",7,Item.ItemType.profile));
		items.Add (new Item("heart",8,Item.ItemType.profile));
		items.Add (new Item("heart",9,Item.ItemType.profile));
		
		// power items
		//items.Add (new Item("acidrain",10,Item.ItemType.power));
		//items.Add (new Item("slowmotion",11,Item.ItemType.power));
		items.Add (new Item("wiz01_single",10,Item.ItemType.power));
		items.Add (new Item("wiz01_multi",11,Item.ItemType.power));
		items.Add (new Item("wiz02_fire",12,Item.ItemType.power));
		items.Add (new Item("wiz02_bomb",13,Item.ItemType.power));
		items.Add (new Item("wiz03_ice",14,Item.ItemType.power));
		items.Add (new Item("wiz03_stop",15,Item.ItemType.power));
		items.Add (new Item("wiz04_sniper",16,Item.ItemType.power));
		items.Add (new Item("wiz04_acid",17,Item.ItemType.power));
		items.Add (new Item("wiz05_wall",18,Item.ItemType.power));
		items.Add (new Item("wiz05_bomb",19,Item.ItemType.power));
		
		
		// wizards items
		items.Add (new Item("wizardDiamond",20,"Normal",2,0,Item.ItemType.wizard));
		items.Add (new Item("wizardRuby",21,"Fire",2,0,Item.ItemType.wizard));
		items.Add (new Item("wizardSapphire",22,"Freeze",2,0,Item.ItemType.wizard));
		items.Add (new Item("wizardEmerald",23,"Sniper",2,0,Item.ItemType.wizard));
		items.Add (new Item("wizardPearl",24,"Architecture",2,0,Item.ItemType.wizard));
		
		// archer items
		items.Add (new Item("navigate",25,Item.ItemType.archer));
		/*
		items.Add (new Item("up",25,Item.ItemType.archer));
		items.Add (new Item("up",26,Item.ItemType.archer));
		items.Add (new Item("right",27,Item.ItemType.archer));
		items.Add (new Item("up",28,Item.ItemType.archer));
		items.Add (new Item("down",29,Item.ItemType.archer));
		items.Add (new Item("up",30,Item.ItemType.archer));
		items.Add (new Item("left",31,Item.ItemType.archer));
		items.Add (new Item("up",32,Item.ItemType.archer));
		*/
		//items.Add (new Item("blue",29,Item.ItemType.archer));
	}
}
