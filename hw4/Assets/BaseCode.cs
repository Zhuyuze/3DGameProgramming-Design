using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.ShootThatUFO;

public class BaseCode : MonoBehaviour {

	SceneController sceneController;
	public Judge judge;
	public GameModel gameModel;
	public UserInterface ui;

	// Use this for initialization
	void Start () {
		sceneController = SceneController.GetInstance ();

		sceneController.setBaseCode (this);

		gameModel = UnityEngine.Camera.main.gameObject.AddComponent <GameModel> ();

		judge = UnityEngine.Camera.main.gameObject.AddComponent <Judge> ();

		ui = UnityEngine.Camera.main.gameObject.AddComponent <UserInterface> ();
	}

}