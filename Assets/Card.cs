using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

//	private int index;

	public int type = -1;

	private bool flipped = false;

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

	public void Flip(){
	
		transform.Rotate ( new Vector3( 0 , 0 , 90) );

		flipped = !flipped;

		if (flipped == true) {
		
			Invoke ("Flip" , 2);
		
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


}

