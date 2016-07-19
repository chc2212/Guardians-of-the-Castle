using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {
	public  string FirstLevel;


	void Update () {
		if(GameControllerScript.levelCompleted == 1)
			Application.LoadLevel(FirstLevel);

		/*else if(GameControllerScript.levelCompleted == 2)
			Application.LoadLevel(FirstLevel);
		else if(GameControllerScript.levelCompleted == 3)
			Application.LoadLevel(FirstLevel);
			*/

	}

	/*
	public void forNextLevel()
	{
		Application.LoadLevel(FirstLevel);
	}
*/

}
