using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {
	
	/*public void OnClickPlay(){
		Application.LoadLevel("Scene");
	}*/

	public Camera camera;
	public Camera camera2;
	public Camera camera3;
	public GameObject UI; 

	void Start() {
		camera = gameObject.GetComponent<Camera>();
		camera.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;
		UI.GetComponent<Canvas> ().enabled = false;
	}
	
	void Update() {
		//This will toggle the enabled state of the two cameras between true and false each time
		if (Input.GetKeyUp (KeyCode.Return)) {
			camera.enabled = false;
			camera2.enabled = true;
			camera3.enabled = true;
			UI.GetComponent<Canvas> ().enabled = true;
		}
	}
}