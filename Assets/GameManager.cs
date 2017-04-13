using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject cardPrefab;

	public Text timerText;

	public int gridSize = 4;

	private int typeCount = 0;

	private Card[,] array;

	private Card prevCard;

	private float timer;

	void Start () {

		if (gridSize % 2 != 0) {
		
			Debug.LogError ( "Grid size should be even, creating even grid instead" );
			gridSize--;
		
		}
		if (gridSize <= 2) {
		
			Debug.LogError ( "Grid size should be greater than 2" );
		
		}

		typeCount = gridSize * gridSize / 2;

		int[] typeArray = new int[typeCount];

		for (int i = 0; i < typeCount; i++) {
		
			typeArray[i] = 2;

		}

		array = new Card[ gridSize , gridSize ];

		for (int i = 0; i < gridSize; i++) {
		
			for (int k = 0; k < gridSize; k++) {
				
				GameObject temp	= (GameObject)GameObject.Instantiate ( cardPrefab , Vector3.zero + new Vector3( i * 3 , 0 , k * 2) , cardPrefab.transform.rotation );
				array[i,k] = temp.GetComponent<Card>();
				//array [i, k].SetIndex ( i );

				int val = -1;

				do{

					val = Random.Range( 0 , typeCount );
					
					if( typeArray[val] > 0 ){

						typeArray[val]--;
						break;
					}
					else{

						val = -1;

					}

				}
				while( val == -1 );

				array[i,k].SetCardType( val );

			}
		}


	}
	

	void Update () {

		timer += Time.deltaTime;

		timerText.text = "Time : " + timer.ToString ("F");

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
		
			Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
		
			RaycastHit hit;

			if (Physics.Raycast (Camera.main.transform.position, ray.direction , out hit )) {		

				Card current = hit.collider.gameObject.GetComponent<Card> ();

				if (prevCard != null && prevCard.GetInstanceID () != current.GetInstanceID () && prevCard.GetCardType () == current.GetCardType ()) {
				
					// SUCCESS
					prevCard.Success ();
					current.Success ();
				
				}
			
				hit.collider.gameObject.GetComponent<Card> ().Flip ();

				prevCard = hit.collider.gameObject.GetComponent<Card>();

			}

		}

	}
}
