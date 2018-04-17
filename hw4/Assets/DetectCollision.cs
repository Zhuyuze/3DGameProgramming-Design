using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.ShootThatUFO;

public class DetectCollision : MonoBehaviour {
	public UFOFactory ufoFactory;
	public BulletFactory bulletFactory;
	void Start() {
		ufoFactory = UFOFactory.getInstance ();
		bulletFactory = BulletFactory.getInstance ();
	}


	void OnTriggerEnter (Collider other){
		SceneController.GetInstance ().onHittingUFO ();
		ufoFactory.releaseUFO (this.gameObject);

		other.gameObject.GetComponent<SelfDestruct> ().t = 0f;
		bulletFactory.releaseBullet (other.gameObject);

		SceneController.GetInstance ().okToShoot = true;
	}

}