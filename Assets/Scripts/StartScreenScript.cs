using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {

	public void Update(){
		if (Input.anyKeyDown) {
			Application.LoadLevel("Scene");
		}
	}
	
	public void OnClickPlay(){
		Debug.Log ("clicked");
		Application.LoadLevel("Scene");
	}

}