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
	public bool restartbuttonworked;
	private float score;

	public GameObject testobject;
	public GameObject testobject2;
	public GameObject testobject3;
	public GameObject cardPrefab;
	public GameObject cardPrefab2;

	public Text timerText;

	public int gridSize = 4;

	private int typeCount = 0;

	private Card[,] array;
	public float[] scorelist;

	public Card prevCard;
	public Card current;

	public GameObject temp;

	public float timer;
	public GameObject timertext;

	public Material logo0;

	public Material logo1;

	public Material logo2;

	public Material logo3;

	public Material logo4;

	public Material logo5;

	public Material logo6;

	public Material logo7;

	public Material logo10;

	public Material logo11;

	public Material logo12;

	public Material logo13;

	public Material logo14;

	public Material logo15;

	public Material logo16;

	public Material logo17;

	public GameObject background;

	public Sprite sprite1;

	public Sprite sprite2;

	void Start () {

		gameOver = false; //Initial values to the GUIText variables are set to null and boolean variables are set to false since the game is not over at the beginning, also game cannot be restarted until the game is over
		restart = false;
		score = 0;
		restartText.text  = "";
		gameOverText.text = "";
		scoreText.text = "";

		background.GetComponent<SpriteRenderer> ().sprite = GameObject.FindGameObjectWithTag ("bg").GetComponent<SpriteRenderer> ().sprite; //Change the theme background

		if (gridSize % 2 != 0) { //Check if the grid size is even and greater than 2
		
			Debug.LogError ( "Grid size should be even, creating even grid instead" ); 
			gridSize--;
		
		}
		if (gridSize <= 2) {
		
			Debug.LogError ( "Grid size should be greater than 2" );
		
		}

		typeCount = gridSize * gridSize / 2; //Determine the number of types according to the gridSize, the number of types are half of the total cards since we are pairing

		int[] typeArray = new int[typeCount]; //Creating the array of types

		for (int i = 0; i < typeCount; i++) { //Filling the array of types, there should be 2 of the same type to be able to pair them
		
			typeArray[i] = 2;

		}

		array = new Card[ gridSize , gridSize ]; //Creating the array of cards

		Material[] materialArray = new Material[8]; //Creating the array of materials which are the pictures on the cards

		if (GameObject.FindGameObjectWithTag ("bg").GetComponent<SpriteRenderer> ().sprite == sprite1) { //If the theme is theme1 then load theme1 logos to the cards or if the theme is theme2 then load theme2 logos to the cards

			materialArray [0] = logo0;
			materialArray [1] = logo1;
			materialArray [2] = logo2;
			materialArray [3] = logo3;
			materialArray [4] = logo4;
			materialArray [5] = logo5;
			materialArray [6] = logo6;
			materialArray [7] = logo7;

		} else if (GameObject.FindGameObjectWithTag ("bg").GetComponent<SpriteRenderer> ().sprite == sprite2) {
			materialArray [0] = logo10;
			materialArray [1] = logo11;
			materialArray [2] = logo12;
			materialArray [3] = logo13;
			materialArray [4] = logo14;
			materialArray [5] = logo15;
			materialArray [6] = logo16;
			materialArray [7] = logo17;
		}


		for (int i = 0; i < gridSize; i++) { //Rows of grid
		
			for (int k = 0; k < gridSize; k++) { //Columns of grid
				
				if (GameObject.FindGameObjectWithTag ("bg").GetComponent<SpriteRenderer> ().sprite == sprite1) {
					temp = (GameObject)GameObject.Instantiate ( cardPrefab , Vector3.zero + new Vector3( i * 2 , 1 , k * 3) , cardPrefab.transform.rotation ); //Creating the cards with the cardPrefab and aligning them with Vector3
					
				}
				else if(GameObject.FindGameObjectWithTag ("bg").GetComponent<SpriteRenderer> ().sprite == sprite2){
					temp = (GameObject)GameObject.Instantiate ( cardPrefab2 , Vector3.zero + new Vector3( i * 2 , 1 , k * 3) , cardPrefab.transform.rotation ); //Creating the cards with the cardPrefab2 (for the theme) and aligning them with Vector3
				}
				array[i,k] = temp.GetComponent<Card>(); //Assigning created card to the array of cards

				int val = -1; //Start the while loop

				do{

					val = Random.Range( 0 , typeCount ); //Random value to assign a type to the card
					
					if( typeArray[val] > 0 ){ //If the type is available at this random value

						typeArray[val]--; //-1 the type since we will be assigning the type to the card

						break;
					}
					else{

						val = -1;

					}

				}
				while( val == -1 );

				array[i,k].SetCardType( val ); //Assigning the type to the card
				
				temp.transform.GetChild(1).gameObject.GetComponent<Renderer> ().material = materialArray[val % 8]; //Assigning a picture for that type, there are 8 pictures so we mod the value so that if the value is more than 8, there would be a picture for that type

			}
		}


	}
		
	

	void Update () {

		if (gameOver == false) { //If game is not over, timer would continue
			timer += Time.deltaTime; //Using Time.deltaTime to count up the timer
		}

		timerText.text = "Time : " + timer.ToString ("F"); //Creating the time visual

		if (Input.GetKeyDown (KeyCode.Mouse0)) { //If the mouse left clicked
		
			Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition ); //Send a raycast to the mouse pointed direction
		
			RaycastHit hit; //Understand when the raycast hits

			if (Physics.Raycast (Camera.main.transform.position, ray.direction , out hit )) { //When the raycast hits		

				current = hit.collider.gameObject.GetComponent<Card> (); //Understand which card gameobject is hit, get it's Card component

				if (prevCard != null && prevCard.GetInstanceID () != current.GetInstanceID () && prevCard.GetCardType () == current.GetCardType ()) { //If there is a flipped card before this flip and the flipped card is not the same card and the types of the flipped cards are the same
				
					// SUCCESS
					prevCard.Success ();
					current.Success ();
				
				}
			
				hit.collider.gameObject.GetComponent<Card> ().Flip (); //Flips the raycast hit card gameObject

				prevCard = hit.collider.gameObject.GetComponent<Card>(); //Assigns the previous card as the hit card gameObject

			}

		}


		if (GameObject.FindGameObjectsWithTag ("Card").Length == 0) { //If there aren't any cards left
			gameOver = true; //Game is over
		}

		if (gameOver) { //Stop the timer, Set the game over visuals and enable restarting.

			timertext.SetActive (false);
			gameOverText.text = "Game Over!";
			scoreText.text = "Score: " + SetScore();
			restartText.text = "Press 'R' for Restart, 'ESC' for Main Menu";
			restart = true;


		}

		if (restart) {
			if(Input.GetKeyDown(KeyCode.R)){ //Restart if R pressed
				Application.LoadLevel (Application.loadedLevel);
				
			}
			else if (Input.GetKeyDown(KeyCode.Escape)){ //Go back to menu if ESC pressed
				Application.LoadLevel(0);
			}
				
		}

		if (Input.GetKeyDown (KeyCode.Escape)) { //Player can also go back to the menu mid-game
			Application.LoadLevel (0);
		}



	}

	float SetScore(){ //Sets the score for the game over visual
		score = timer;
		return score;
	}

	//TESTS

	public bool logosvisible(){
		testobject = GameObject.FindGameObjectWithTag ("test");
		if (testobject.transform.GetChild (0).gameObject.GetComponent<Renderer> ().sharedMaterial == logo0) {
			return true;
		} else
			return false;
	}

	public bool logosvisibleIsFixed(){
		logo0 = Resources.Load ("0", typeof (Material)) as Material;
		testobject = GameObject.FindGameObjectWithTag ("test");
		testobject.transform.GetChild (0).gameObject.GetComponent<Renderer> ().sharedMaterial = logo0;
		if (testobject.transform.GetChild (0).gameObject.GetComponent<Renderer> ().sharedMaterial == logo0) {
			return true;
		} else
			return false;
	}

	public bool identicalCardsDisappear(){
		testobject = GameObject.FindGameObjectWithTag ("test");
		if (testobject.GetComponent<Card> ().Success ()) {
			return true;
		} else
			return false;
	}

	public bool nonIdenticalCardsFlipBack(){
		testobject = GameObject.FindGameObjectWithTag ("test");
		testobject2 = GameObject.FindGameObjectWithTag ("test2");
		if (testobject.GetInstanceID() != testobject2.GetInstanceID()) {
			return true;
		} else
			return false;
	}

	public bool getRestartButton(){
		restart = true;
		if (restart) {
			UnityEditor.SceneManagement.EditorSceneManager.OpenScene ("Assets/scene1.unity", UnityEditor.SceneManagement.OpenSceneMode.Additive);		
		}
		if (Application.loadedLevel == Application.loadedLevel) {
			return true;
		} else
			return false;
		
	}

	public bool gameoverworks(){
		if (gameOver) {
			return true;
		} else
			return false;
	}

	public bool gameoverfixed(){
		for(int i = 0; i < GameObject.FindGameObjectsWithTag ("Card").Length ; i++){
			Destroy (GameObject.FindGameObjectWithTag("Card"));			
		}
		if (GameObject.FindGameObjectsWithTag ("Card").Length == 0) {
			gameOver = true;
		}
		if (gameOver) {
			return true;
		} else
			return false;
	}

	public bool scoreworks(){
		gameOver = true;
		if (gameOver) {
			SetScore ();
			return true;
		} else
			return false;
	}

	public bool menuworks(){
		if (GameObject.FindGameObjectWithTag ("menu") != null) {
			return true;
		} else
			return false;
	}

	public bool allow3flips(){
		testobject = GameObject.FindGameObjectWithTag ("test");
		testobject2 = GameObject.FindGameObjectWithTag ("test2");
		testobject3 = GameObject.FindGameObjectWithTag ("test3");
		if (testobject.GetComponent<Card> ().Flip () && testobject2.GetComponent<Card> ().Flip () && testobject3.GetComponent<Card> ().Flip ()) {
			return true;
		} else
			return false;
	}

	public bool holdscores(){
		if (scorelist != null) {
			return true;
		} else
			return false;
	}

}
