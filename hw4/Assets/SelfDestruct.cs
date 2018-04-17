using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.ShootThatUFO;

public class SelfDestruct : MonoBehaviour {

	public float t = 0;

	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;

		if (t > 1.5f) {
			this.gameObject.GetComponent<shootBullet> ().Free();
			BulletFactory.getInstance().releaseBullet(this.gameObject);

			SceneController.GetInstance().trials++;
			SceneController.GetInstance().okToShoot = true;
		}
	}
}
