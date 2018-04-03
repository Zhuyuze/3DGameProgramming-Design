using UnityEngine;
using System.Collections;
using Com.MyGame;

public class UI : MonoBehaviour {
	IUserActions myActions;
	float btnWidth = (float)Screen.width / 6.0f;
	float btnHeight = (float)Screen.height / 6.0f;

	void Start () {
		myActions = mainSceneController.getInstance() as IUserActions;
	}

	void Update () {

	}

	void OnGUI() {
		if (GUI.Button(new Rect(5, 250, btnWidth, btnHeight), "Priests GetOn")) {
			myActions.priestsGetOn();
		}
		if (GUI.Button(new Rect(155, 250, btnWidth, btnHeight), "Priests GetOff")) {
			myActions.priestsGetOff();
		}
		if (GUI.Button(new Rect(305, 250, btnWidth, btnHeight), "Go!")) {
			myActions.boatMove();
		}
		if (GUI.Button(new Rect(455, 250, btnWidth, btnHeight), "Devils GetOn")) {
			myActions.devilsGetOn();
		}
		if (GUI.Button(new Rect(605, 250, btnWidth, btnHeight), "Devils GetOff")) {
			myActions.devilsGetOff();
		}

	}
}