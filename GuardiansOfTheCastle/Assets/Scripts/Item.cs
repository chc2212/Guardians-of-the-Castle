using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

	public string itemName;
	public int itemID;
	public string itemText;
	public Texture2D itemIcon;
	public int itemPower;
	public int itemSpeed;
	public ItemType itemType;
	public enum ItemType{
		gameFlow,
		profile,
		power,
		wizard,
		archer
	}
	
	public Item(string name, int id, string desc, int power, int speed, ItemType type){
		itemName = name;
		itemID = id;
		itemText = desc;
		itemIcon = Resources.Load<Texture2D> ("Item Icons/"+name);
		itemPower = power;
		itemSpeed = speed;
		itemType = type;
	}

	public Item(string name, int id, ItemType type){
		itemName = name;
		itemID = id;
		itemType = type;
		switch(type){
			case ItemType.gameFlow:
				itemIcon = Resources.Load<Texture2D> ("Game Flow Icons/" + name);
				break;
			case ItemType.profile:
				itemIcon = Resources.Load<Texture2D> ("Profile Icons/" + name);
				break;
			case ItemType.power:
				itemIcon = Resources.Load<Texture2D> ("Power Icons/" + name);
				break;
			case ItemType.archer:
				itemIcon = Resources.Load<Texture2D> ("Archer Icons/" + name);
				break;
		}
	}

	public Item(string name, int id, string text, ItemType type){
		itemName = name;
		itemID = id;
		itemText = text;
		itemType = type;
	}

	public Item(){
	}
}
