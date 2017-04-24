using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private float score;

	public GameObject cardPrefab;

	public Text timerText;

	public int gridSize = 4;

	private int typeCount = 0;

	private Card[,] array;

	private Card prevCard;

	private float timer;
	public GameObject timertext;

	public Material logo0;

	public Material logo1;

	public Material logo2;

	public Material logo3;

	public Material logo4;

	public Material logo5;

	public Material logo6;

	public Material logo7;



	void Start () {

		gameOver = false;
		restart = false;
		score = 0;
		restartText.text  = "";
		gameOverText.text = "";
		scoreText.text = "";

	

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

		Material[] materialArray = new Material[8];

		materialArray [0] = logo0;
		materialArray [1] = logo1;
		materialArray [2] = logo2;
		materialArray [3] = logo3;
		materialArray [4] = logo4;
		materialArray [5] = logo5;
		materialArray [6] = logo6;
		materialArray [7] = logo7;


		for (int i = 0; i < gridSize; i++) {
		
			for (int k = 0; k < gridSize; k++) {
				
				GameObject temp	= (GameObject)GameObject.Instantiate ( cardPrefab , Vector3.zero + new Vector3( i * 2 , 1 , k * 3) , cardPrefab.transform.rotation );
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
				
				temp.transform.GetChild(1).gameObject.GetComponent<Renderer> ().material = materialArray[val % 8];

			}
		}


	}
		
	

	void Update () {

		if (gameOver == false) {
			timer += Time.deltaTime;
		}

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


		if (GameObject.FindGameObjectsWithTag ("Card").Length == 0) {
			gameOver = true;
		}

		if (gameOver) {

			timertext.SetActive (false);
			gameOverText.text = "Game Over!";
			scoreText.text = "Score: " + SetScore();
			restartText.text = "Press 'R' for Restart, 'ESC' for Main Menu";
			restart = true;


		}

		if (restart) {
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel (Application.loadedLevel);
				
			}
			else if (Input.GetKeyDown(KeyCode.Escape)){
				Application.LoadLevel(0);
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel (0);
		}



	}

	float SetScore(){
		score = timer;
		return score;
	}
}
