using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

//	private int index;

	public int type = -1;

	public bool flipped = false;

	public int rotation;

	private float startAngle;

	private float targetAngle;

	private float startTime;

	public float smooth = 2.0f;

	private float angle;


//	public void SetIndex( int value ){
//	
//		index = value;
//	
//	}
//
//	public int GetIndex(){
//	
//		return index;
//	
//	}

	void Start(){
		angle = transform.eulerAngles.z;
	}

	public void Flip(){

		rotation = 1;
		if (flipped == true) {
			rotation = -1;
		}

		startTime = Time.time;
		startAngle = angle;
		targetAngle = targetAngle + 180 * rotation;
	
		//transform.Rotate ( new Vector3( 0 , 0 , 180 * rotation) );


		flipped = !flipped;

		if (flipped == true) {

			gameObject.GetComponent<Collider> ().enabled = false;
			Invoke ("Flip", 2);

		
		} else {
			gameObject.GetComponent<Collider> ().enabled = true;
		}
	
	}

	public void SetCardType( int value ){
	
		type = value;
	
	}

	public int GetCardType(){

		return type;

	}

	public void Success(){

		Destroy (gameObject);

	}

	void Update(){

		angle = Mathf.LerpAngle (startAngle, targetAngle, (Time.time - startTime) / smooth);
		transform.eulerAngles = new Vector3 (0, 0, angle);
	}


}

