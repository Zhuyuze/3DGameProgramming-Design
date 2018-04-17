using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.ShootThatUFO;

public class GameModel : MonoBehaviour, IU3dActionCompleted {

	SceneController sceneController;
	public ActionManager actionManager;
	public UFOFactory ufoFactory;
	public BulletFactory bulletFactory;
	public float t = 0f;

	// Use this for initialization
	void Start () {
		sceneController = SceneController.GetInstance ();
		actionManager = ActionManager.GetInstance ();
		ufoFactory = UFOFactory.getInstance ();

		actionManager = ActionManager.GetInstance ();
		ufoFactory = UFOFactory.getInstance ();
		bulletFactory = BulletFactory.getInstance ();

	}

	public void OnActionCompleted (U3dAction action){
		sceneController.okToShoot = true;
		ufoFactory.releaseUFO (action.gameObject);
	}

	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButtonDown (0) && sceneController.getGameStatus() == 1 && sceneController.okToShoot) {

			Ray ray = sceneController.mainCam.ScreenPointToRay (Input.mousePosition);

			GameObject bullet = bulletFactory.getNewBullet();
			actionManager.applyShootBullet(bullet, ray.direction);
			//sceneController.okToShoot = false;
		}

		if (sceneController.getGameStatus() == 1){

			t += Time.deltaTime;
			if (t > sceneController.intervalPerRound[sceneController.round]) {
				t = 0;
				if (sceneController.ufoToToss != 0){
					Vector3 tossTo = new Vector3 (Random.Range(-sceneController.tossRange[sceneController.round], 
						sceneController.tossRange[sceneController.round]), 
						SceneController.GROUND, 10f);

					float tossAngle = Random.Range(0.56f, sceneController.tossMaxAngle[sceneController.round]);

					float tossSpeed = Random.Range(15f, sceneController.tossSpeed[sceneController.round]);

					//                    Debug.Log ("Tossing! " + tossTo.x + " " + tossAngle + "　" + tossSpeed);

					GameObject aUFO = ufoFactory.getNewUFO (sceneController.ufoScale[Random.Range(0, 3)], 
						sceneController.ufoColor[Random.Range(0, 3)]);
					actionManager.applyU3dTossUFO (aUFO, 
						aUFO.transform.position, 
						tossTo, 
						tossAngle, 
						tossSpeed, 
						this);
					sceneController.ufoToToss--;
				} else {
					sceneController.round++;
					sceneController.ufoToToss = 20;
					sceneController.gameStatus = 2;
				}
			}

		}
	}
}