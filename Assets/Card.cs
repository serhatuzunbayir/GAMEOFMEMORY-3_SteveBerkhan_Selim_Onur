using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

	public int type = -1;

	public bool flipped = false;

	public int rotation;

	private float startAngle;

	public float targetAngle;

	private float startTime;

	public float smooth = 2.0f;

	public float angle;


	void Start(){
		angle = transform.eulerAngles.z; //Get the angle of the card to rotate smoothly.
	}

	public void Flip(){

		rotation = 1; 
		if (flipped == true) { //if the card is flipped, the card will flip to the other rotation
			rotation = -1;
		}

		startTime = Time.time; //To flip the card smoothly, we store the start time, start angle and target angle.
		startAngle = angle;
		targetAngle = targetAngle + 180 * rotation;
	


		flipped = !flipped; //Change the status of the card when it's flipped.

		if (flipped == true) {

			gameObject.GetComponent<Collider> ().enabled = false; //If it's flipped, make the card unclickable (this is to prevent click bug)
			Invoke ("Flip", 2);//Be flipped for 2 seconds, then flip back to first state

		
		} else {
			gameObject.GetComponent<Collider> ().enabled = true; //Make the card clickable again if unflipped
		}
	
	}

	public void SetCardType( int value ){
	
		type = value; //Set card type to pair the cards
	
	}

	public int GetCardType(){

		return type; //Return the type of the card

	}

	public void Success(){

		Destroy (gameObject); //If paired successfully, destroy the cards. Bonus -> https://youtu.be/NJzoBmVPeYw?t=2m6s

	}

	void Update(){

		angle = Mathf.LerpAngle (startAngle, targetAngle, (Time.time - startTime) / smooth); //Using the Mathf.LerpAngle method, assign angle to target angle with the smoothed time value.
		transform.eulerAngles = new Vector3 (0, 0, angle); //Smoothly rotate to the target angle using the transform.eulerAngles method
	}


}

