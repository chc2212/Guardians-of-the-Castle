using UnityEngine;
using System.Collections;

public class WizardAnimation : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (this.tag == "wizard1") {
			if(Inventory.wizardBasicShooting1 == true || Inventory.wizardBasicShooting2 == true)
				this.animation.Play ("spellCasting01-1");	
			else 
				this.animation.Play ("SpellIncantation02");	
		}


		if(this.tag == "wizard2") {
			if(Inventory.wizardBasicShooting1 == true || Inventory.wizardBasicShooting2 == true)
				this.animation.Play ("spellCasting02-1");	
			else 
				this.animation.Play ("SpellIncantation02");	
		}


		if(this.tag == "wizard3") {
			if(Inventory.wizardBasicShooting1 == true || Inventory.wizardBasicShooting2 == true)
				this.animation.Play ("spellCasting03-1");	
			else 
				this.animation.Play ("SpellIncantation03");	
		}


		if(this.tag == "wizard4") {
			if(Inventory.wizardBasicShooting1 == true || Inventory.wizardBasicShooting2 == true )
				this.animation.Play ("spellCasting05-1");	
			else 
				this.animation.Play ("SpellIncantation04");	
		}


		if(this.tag == "wizard5") {
			if(Inventory.wizardBasicShooting1 == true || Inventory.wizardBasicShooting2 == true)
				this.animation.Play ("spellCasting06-1");	
			else 
				this.animation.Play ("SpellIncantation06");	
		}




	}
}
