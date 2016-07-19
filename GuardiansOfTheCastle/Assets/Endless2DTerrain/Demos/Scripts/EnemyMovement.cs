using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	
    public Camera playerCamera;

	// prem
	public float height;
	public float timingOffset;
	private float xPos;
	private float waveTimer;
	private float zPos;
	public float speedWithoutGravity;
	private float xRand;
	public float hSpeed;
	private float objectTime;
	public int selector; // to tell whether it is for bullet or alien 0-alien 1-bullet

	public static bool icedStatus,timeStopped;
    public static float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

	private int timeStop;

    void Start()
    {

		// prem
		objectTime = Time.time;
		xPos = rigidbody.transform.position.x;
		zPos = rigidbody.transform.position.z;
		xRand = Random.Range (1.0f, 1.8f);
		timeStopped = false;

    }

    void Update()
    { 
		this.animation.Play ();
		if (Inventory.wizardNumber==3 && Inventory.wizardBasicShooting2 && !timeStopped) {
			timeStop = (int)Time.time;
			//print ("Stop the time!");
			speed = 0;
			timeStopped=true;
		} else {
			if((int)Time.time - timeStop >=10){
				speed = 6.0F;
				//timeStopped=false;
			}
		}
		//print ("timeStopped?"+timeStopped);
		if (GameControllerScript.levelCompleted == 1 && !timeStopped)
			speed = 5.0F;
		else if(GameControllerScript.levelCompleted == 2 && !timeStopped)
			speed = 10.0F;
		else if(GameControllerScript.levelCompleted == 3 && !timeStopped)
			speed = 15.0F;

		/*
		// prem
		if (selector == 0) {
			float objectTimeTemp = Time.time-objectTime;
			Vector3 mov  = new Vector3(xPos+(objectTimeTemp*hSpeed*xRand),16,zPos+(height*Mathf.Sin(speedWithoutGravity*Time.time))); 
			transform.position = mov;
			//rigidbody.velocity = transform.right * speed;
		} else {
			Vector3 mov = new Vector3(0,0,1);
			rigidbody.velocity = mov * (-speedWithoutGravity);
		}
		//
		*/

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            //moveDirection = new Vector3(1,0, Input.GetAxis("Vertical"));
			//print (GameControllerScript.enemyName);
			moveDirection = new Vector3(0,0,1);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.x = jumpSpeed; 
            }
            if (Input.touchCount > 0)
            {
                moveDirection.x = jumpSpeed;
            }
        }

		if (icedStatus == false) {
			moveDirection.y -= gravity * Time.smoothDeltaTime;
			controller.Move (moveDirection * Time.smoothDeltaTime);
		}


        //After we move, adjust the camera to follow the player
       // playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
    }

}
