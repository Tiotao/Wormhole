using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	private Maze mazeInstance;
	private bool pauseEnabled = false; 

	private IEnumerator Start () {
		//BeginGame();
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());

	}
	
	private void Update () {
		/*
		if (Input.GetKeyDown(KeyCode.Escape)) {
			//check if game is already paused       
			if(pauseEnabled == true){
				//unpause the game
				Time.timeScale = 1;
				pauseEnabled = false;
			}
			
			//else if game isn't paused, then pause it
			else if(pauseEnabled == false){
				Time.timeScale = 0;
				pauseEnabled = true;
			}
		}
		*/


	}
	
	private void BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Maze;
		StartCoroutine(mazeInstance.Generate());
	}

	private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}