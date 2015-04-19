﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenScript : MonoBehaviour {

	public int score = 0;
	public Text scoreText;

	private void Start(){
		Screen.showCursor = true;
		GameObject notDes = GameObject.FindGameObjectWithTag ("NotDestroyed");
		Score holder = notDes.GetComponent<Score>();
		score = holder.score;
		GameObject points = GameObject.FindGameObjectWithTag ("ScoreText");
		Text scoreText = points.GetComponent<Text>();
		scoreText.text = score.ToString();

	}
	
	public void OnClickPlay(){
		Application.Quit ();
	}
}