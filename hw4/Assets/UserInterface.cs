using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.ShootThatUFO;

public class UserInterface : MonoBehaviour {

	IUserActions userAction;
	public bool spaceHitted = false;
	public float gameStartTimer = 3f;

	// Use this for initialization
	void Start () {
		userAction = SceneController.GetInstance () as IUserActions;
	}

	void OnGUI () {
		// Make a background box

		if (userAction.getGameStatus() == 1) {
			GUI.Label (new Rect(20,20,80,20), "Round " + (userAction.getRound() + 1));
			GUI.Label (new Rect (20, 40, 80, 20), userAction.getUFONum () + " to Go");
		}

		if (userAction.getGameStatus() == 0 || userAction.getGameStatus() == 2) {
			GUI.Label (new Rect(20,20,300,20), "Shoot That UFO! Hit Space Bar to Begin!");
			GUI.Label (new Rect (20, 40, 100, 20), "Time to start: " + gameStartTimer.ToString("0.0"));
			GUI.Label (new Rect (20, 100, 100, 20), "Next Round: " + (userAction.getRound() + 1));
		}

		if (userAction.getGameStatus () == 3) {
			GUI.Label (new Rect (20, 20, 200, 20), "Failed! Hit Spacebar to Restart.");
			GUI.Label (new Rect (20, 40, 100, 20), "Time to start: " + gameStartTimer.ToString("0.0"));
		}

		if (userAction.getGameStatus () != 4) {
			GUI.Label (new Rect (20, 80, 150, 20), "You have " + (50f - userAction.getTrial()) + " bullets left.");
			GUI.Label (new Rect (700, 20, 100, 20), "Highscore: " + userAction.getHighscore());
		}

		if (userAction.getGameStatus () == 4) {
			GUI.Label (new Rect(20,20,150,20), "Game Cleared!");
		}

		GUI.Label (new Rect (20, 60, 150, 20), "Your score is " + userAction.getPoint());
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (userAction.getGameStatus () != 1) {
			if (Input.GetKey (KeyCode.Space)) {
				spaceHitted = true;
				if (userAction.getGameStatus () == 3){
					userAction.resetGame();
				}
			}
			if (spaceHitted) {
				gameStartTimer -= Time.deltaTime;
			}
			if (gameStartTimer < 0f) {
				userAction.gameStart();
				gameStartTimer = 3f;
				spaceHitted = false;
			}
		}

	}
}