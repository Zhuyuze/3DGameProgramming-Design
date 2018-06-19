using UnityEngine;
using System.Collections;
using Com.MyGame;

public class UI : MonoBehaviour {
	IUserActions myActions;
	float btnWidth = (float)Screen.width / 6.0f;
	float btnHeight = (float)Screen.height / 6.0f;
	bool flg = false;
	bool isright = true;
	Random rnd = new Random ();

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
			if (flg) {
				myActions.boatMove ();
				flg = false;
				isright = !isright;
			} else {
				myActions.priestsGetOff ();
				myActions.priestsGetOff ();
				myActions.devilsGetOff ();
				myActions.devilsGetOff ();
				if (isright) {
					if (myActions.brd () < myActions.brp ()) {
						if (Random.value >= 0.5f) {
							myActions.priestsGetOn ();
						} else {
							myActions.devilsGetOn ();
						}
					} else if (myActions.brd () == myActions.brp ()) {
						myActions.devilsGetOn ();
					}
					if (myActions.brd () < myActions.brp ()) {
						if (Random.value < 0.5f) {
							myActions.priestsGetOn ();
						} else {
							myActions.devilsGetOn ();
						}
					} else if (myActions.brd () == myActions.brp ()) {
						myActions.devilsGetOn ();
					}
					while (myActions.bnm() != 2) {
						myActions.devilsGetOn ();
					}
				} else {
					if (myActions.bld () < myActions.blp ()) {
						if (Random.value >= 0.5f) {
							myActions.priestsGetOn ();
						} else {
							myActions.devilsGetOn ();
						}
					} else if (myActions.bld () == myActions.blp ()) {
						myActions.devilsGetOn ();
					}
					if (myActions.bld () < myActions.blp ()) {
						if (Random.value < 0.5f) {
							myActions.priestsGetOn ();
						} else {
							myActions.devilsGetOn ();
						}
					} else if (myActions.bld () == myActions.blp ()) {
						myActions.devilsGetOn ();
					}
					if (myActions.bnm() == 0) {
						myActions.devilsGetOn ();
					}
					
				}

				flg = true;
			}
		}
		if (GUI.Button(new Rect(455, 250, btnWidth, btnHeight), "Devils GetOn")) {
			myActions.devilsGetOn();
		}
		if (GUI.Button(new Rect(605, 250, btnWidth, btnHeight), "Devils GetOff")) {
			myActions.devilsGetOff();
		}

	}
}