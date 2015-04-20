using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {
	
	public void OnClickPlay(){
		Debug.Log ("clicked");
		Application.LoadLevel("Scene");
	}

}