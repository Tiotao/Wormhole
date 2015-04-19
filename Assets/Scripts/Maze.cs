﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	public IntVector2 size;
	public MazeCell cellPrefab;
	public float generationStepDelay;
	public MazePassage passagePrefab;
	public MazeWall wallPrefab;

	private MazeCell[,] cells;

	private GameObject player;
	private GameObject gameManager;
	private CharacterMotor playerCharacterMotor;


	public int zombieAmount = 5;
	public int potionAmount = 5;
	public int crateAmount = 5;
	public int golemAmount = 5;
	public int clockAmount = 5;
	public GameObject clockInstance;
	public GameObject golemInstance;
	public GameObject zombieInstance;
	public GameObject potionInstance;
	public GameObject crateInstance;
	private BoxCollider mazeRangeCollider;
	public GameCountDown countDown;

	public int skeletonAmount = 5;
	public GameObject skeletonInstance;
	public int monsterAmount = 5;
	public GameObject monsterInstance;

	float DROP_DISTANCE = 11.0f;
	Vector2 baseI; //x: minI, y:maxI
	Vector2 baseJ; //x: minJ, y:minJ


	void Awake(){
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		countDown = gameManager.GetComponent <GameCountDown> ();

	}

	void OnTriggerExit (Collider other)
	{	
		if(other.gameObject == player)
		{
			countDown.startCounting();
		}
	}
	
	
	void OnTriggerStay (Collider other)
	{
		if(other.gameObject == player)
		{
			countDown.endCounting();
		}
	}

	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	float getRandomXInMaze() {
		float randomX = Random.Range (-2 * size.x, 2 * size.x);
		return randomX;
	}

	float getRandomZInMaze() {
		float randomZ = Random.Range (-2 * size.z, 2 * size.z);
		return randomZ;
	}

	public bool ContainsCoordinates (IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	public MazeCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	public IEnumerator Generate () {
		baseI = new Vector2(1,7);
		//baseJ = new Vector2(size.z-1, size.z-7);
		baseJ = new Vector2(1,7);

		/*
		GameObject leftWall = GameObject.Find ("Long Wall Left");
		GameObject rightWall = GameObject.Find ("Long Wall Right");
		float leftNewZ = 100f + 2 * size.z;
		float NewX = -2 * (size.x+1) + 0.2f;
		float rightNewZ = -100f - 2 * size.z;
		leftWall.transform.position = new Vector3 (NewX, leftWall.transform.position.y, leftNewZ);
		rightWall.transform.position = new Vector3 (NewX, rightWall.transform.position.y, rightNewZ);
		*/

		player = GameObject.FindGameObjectWithTag ("Player");
		playerCharacterMotor = player.GetComponent <CharacterMotor> ();
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		PlopBaseAtSide();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			//yield return delay;
			DoNextGenerationStep(activeCells);
		}
		playerCharacterMotor.movement.gravity = 9.81f;




		/*
		for (int i = 0; i < golemAmount; i++) {
			float randomX = Random.Range (-2 * size.x, 2 * size.x);
			float randomZ = Random.Range (-2 * size.z, 2 * size.z);
			Instantiate(golemInstance, new Vector3(randomX, DROP_DISTANCE, randomZ), Quaternion.identity);
			
		}
		*/

		GameObject exitCell = GameObject.Find ("Maze Cell " + (size.x - 1) + ", " + (size.z - 2));
		GameObject entryCell = GameObject.Find ("Maze Cell "+(int)baseI.y+", "+(int)(baseJ.x+3));
		GameObject otherSideOfEntryCell = GameObject.Find ("Maze Cell "+(int)(baseI.y+1)+", "+(int)(baseJ.x+3));
		
		Destroy (exitCell, 0f);
		Destroy (entryCell, 0f);
		Destroy (otherSideOfEntryCell, 0f);
		for (int i = 0; i < potionAmount; i++) {
			float randomX = Random.Range (0, 2 * size.x);
			float randomZ = Random.Range (0, 2 * size.z);
			if(randomX < -2 * size.x+32 && randomZ < -2 * size.z+32){
				float XorZ = Random.Range(0, 1);
				if(XorZ > 0.5){
					randomX = Random.Range (-2 * size.x+32, 2 * size.x);
				} else {
					randomZ = Random.Range (-2 * size.z+32, 2 * size.z);
				}
			}
			Instantiate(potionInstance, new Vector3(randomX, DROP_DISTANCE, randomZ), Quaternion.identity);
			
		}

		for (int i = 0; i < clockAmount; i++) {
			float randomX = Random.Range (-2 * size.x, 2 * size.x);
			float randomZ = Random.Range (-2 * size.z, 2 * size.z);
			if(randomX < -2 * size.x+32 && randomZ < -2 * size.z+32){
				float XorZ = Random.Range(0, 1);
				if(XorZ > 0.5){
					randomX = Random.Range (-2 * size.x+32, 2 * size.x);
				} else {
					randomZ = Random.Range (-2 * size.z+32, 2 * size.z);
				}
			}

			Instantiate(clockInstance, new Vector3(randomX, DROP_DISTANCE, randomZ), Quaternion.identity);
			
		}


		for (int i = 0; i < crateAmount; i++) {
			float randomX = Random.Range (-2 * size.x, 2 * size.x);
			float randomZ = Random.Range (-2 * size.z, 2 * size.z);

			if(randomX < -2 * size.x+32 && randomZ < -2 * size.z+32){
				float XorZ = Random.Range(0, 1);
				if(XorZ > 0.5){
					randomX = Random.Range (-2 * size.x+32, 2 * size.x);
				} else {
					randomZ = Random.Range (-2 * size.z+32, 2 * size.z);
				}
			}
			Instantiate(crateInstance, new Vector3(randomX, DROP_DISTANCE, randomZ), Quaternion.identity);
			
		}

		for (int i = 0; i < zombieAmount; i++) {
			float randomX = Random.Range (-2 * size.x, 2 * size.x);
			float randomZ = Random.Range (-2 * size.z, 2 * size.z);
			if(randomX < -2 * size.x+40 && randomZ < -2 * size.z+40){
				float XorZ = Random.Range(0, 1);
				if(XorZ > 0.5){
					randomX = Random.Range (-2 * size.x+40, 2 * size.x);
				} else {
					randomZ = Random.Range (-2 * size.z+40, 2 * size.z);
				}
			}
			Instantiate(zombieInstance, new Vector3(randomX, DROP_DISTANCE, randomZ), Quaternion.identity);
			
		}

		for (int i = 0; i < skeletonAmount; i++) {
			float randomX = Random.Range (-2 * size.x+45, 2 * size.x);
			float randomZ = Random.Range (-2 * size.z+45, 2 * size.z);
			Instantiate(skeletonInstance, new Vector3(randomX, DROP_DISTANCE, randomZ), Quaternion.identity);
			
		}

		for (int i = 0; i < monsterAmount; i++) {
			float randomX = Random.Range (-2 * size.x+50, 2 * size.x);
			float randomZ = Random.Range (-2 * size.z+50, 2 * size.z);
			Instantiate(monsterInstance, new Vector3(randomX, DROP_DISTANCE, randomZ), Quaternion.identity);
			
		}
		mazeRangeCollider = gameObject.GetComponent<BoxCollider>();
		yield return new WaitForSeconds(0f);
	}

	private void PlopBaseAtSide(){
		//7x7base
		for (int i=(int)baseI.x; i<=(int)baseI.y; i++){
			for (int j=(int)baseJ.x; j<=(int)baseJ.y; j++){
				CreateCell(new IntVector2(i,j));
			}
		}
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
		}
	}

	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate(wallPrefab) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefab) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}
}