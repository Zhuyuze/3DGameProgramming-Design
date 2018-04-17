using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : System.Object {

	//Singleton Implementation
	private static BulletFactory _instance = null;

	public static BulletFactory getInstance() {
		if (_instance == null) {
			_instance = new BulletFactory();
		}
		return _instance;
	}

	//Initalizing
	private GameObject _BulletPrefab;

	BulletFactory (){
		//            GameObject BulletPrimitive = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		//            UFOPrimitive.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		//            Rigidbody bRB = BulletPrimitive.AddComponent<Rigidbody> ();
		//            BulletPrimitive.AddComponent<SelfDestruct> ();

		//            _BulletPrefab = UnityEditor.PrefabUtility.CreatePrefab ("Assets/Resources/Prefab/bulletPrefab.prefab", BulletPrimitive);

		usedBullet = new List<GameObject> ();
		usingBullet = new List<GameObject> ();

	}

	//List of Gameobjects
	private List<GameObject> usedBullet;
	private List<GameObject> usingBullet;

	public GameObject getNewBullet () {
		GameObject bullet;

		if (usedBullet.Count == 0) {
			bullet = UnityEngine.Object.Instantiate (Resources.Load ("Bullet", typeof (GameObject))) as GameObject;
			usingBullet.Add (bullet);
		}
		else {
			Debug.Log ("Here");
			bullet = usedBullet[0];
			bullet.transform.position = UnityEngine.Camera.main.transform.position;
			bullet.SetActive(true);
			usedBullet.Remove(bullet);
			usingBullet.Add (bullet);
		}

		bullet.GetComponent<SelfDestruct> ().t = 0f;
		return bullet;
	}

	public void releaseBullet (GameObject bullettoRelease){
		bullettoRelease.SetActive (false);
		usingBullet.Remove (bullettoRelease);
		usedBullet.Add (bullettoRelease);
	}

}