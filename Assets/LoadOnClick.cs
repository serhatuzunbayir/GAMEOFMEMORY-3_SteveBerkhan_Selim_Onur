using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(int level){
		Application.LoadLevel (level);
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
