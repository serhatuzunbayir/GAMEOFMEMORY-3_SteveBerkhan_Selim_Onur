﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {

	public GameObject bg;
	public Sprite sprite1;
	public Sprite sprite2;
	public bool startbuttonworked;
	public bool escapebuttonworked;
	public bool exitbuttonworked;
	public int theme;
	private static bool loaded = false;

	void Awake(){
		if (loaded == false) { //Prevent further object creation when you get back to the menu
			
			loaded = true;
			DontDestroyOnLoad (GameObject.FindGameObjectWithTag ("bg")); //We will access to this gameObject from another scene to change theme, so don't destroy	

		} else {
			DestroyImmediate (bg); //Destroy the duplicate gameObject
		}

	}
	 

	public void LoadScene(int level){
		Application.LoadLevel (level); //When the button clicked, load the game
	}

	public void ChangeTheme(){
		if(GameObject.FindGameObjectWithTag("bg").GetComponent<SpriteRenderer> ().sprite == sprite1){ //When the button clicked, change the theme to theme2 if theme1 or vice versa.
			
			GameObject.FindGameObjectWithTag("bg").GetComponent<SpriteRenderer> ().sprite = sprite2; 
			theme = 2;

		}
		else if(GameObject.FindGameObjectWithTag("bg").GetComponent<SpriteRenderer> ().sprite == sprite2)
			GameObject.FindGameObjectWithTag("bg").GetComponent<SpriteRenderer> ().sprite = sprite1;
			theme = 1;
	}

	public void ExitGame(){
		Application.Quit (); //When the button clicked, quit the game
		exitbuttonworked = true;
	}

	//TESTS

	public bool getStartButton(){
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene ("Assets/scene1.unity", UnityEditor.SceneManagement.OpenSceneMode.Single);
		if (UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().buildIndex == 1) {
			startbuttonworked = true;
		}
		if (startbuttonworked == true) {
			return true;
		} else
			return false;
	}

	public bool getExitButton(){
		ExitGame ();
		if (exitbuttonworked == true) {
			return true;
		} else
			return false;
	}

	public bool getEscapeButton(){
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene ("Assets/menu.unity", UnityEditor.SceneManagement.OpenSceneMode.Single);
		if (UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().buildIndex == 0) {
			escapebuttonworked = true;
		}
		if (escapebuttonworked == true) {
			return true;
		} else
			return false;
	}
}
