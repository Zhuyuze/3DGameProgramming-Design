using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.ShootThatUFO;

public class UFOFactory : System.Object {

	//Singleton Implementation
	private static UFOFactory _instance = null;

	public static UFOFactory getInstance() {
		if (_instance == null) {
			_instance = new UFOFactory();
		}
		return _instance;
	}

	//Initalizing
	private GameObject _UFOPrefab;
	private GameObject _ExplosionPrefab;

	UFOFactory (){
		    //      GameObject UFOPrimitive = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		    //    UFOPrimitive.transform.localScale = new Vector3 (1f, 0.1f, 1f);
		   //        UFOPrimitive.AddComponent<DetectCollision> ();
	       //    Rigidbody uRB = UFOPrimitive.AddComponent<Rigidbody> ();
	       //   uRB.drag = 1f;
		  //        UFOPrimitive.collider.isTrigger = true;


		//UnityEditor.AssetDatabase.CreateFolder ("Assets/Resources/Prefab");
		 //          _UFOPrefab = UnityEditor.PrefabUtility.CreatePrefab ("Assets/Resources/Prefab/UFOPrefab.prefab", UFOPrimitive);


		_UFOPrefab = Resources.Load ("UFOPrefab") as GameObject;
		   //         UnityEngine.GameObject.Destroy (UFOPrimitive);

		_ExplosionPrefab = Resources.Load ("collide", typeof (GameObject)) as GameObject;

		usedUFO = new List<GameObject> ();
		usingUFO = new List<GameObject> ();
	}

	//List of Gameobjects
	private List<GameObject> usedUFO;
	private List<GameObject> usingUFO;

	public GameObject getNewUFO (float scale, Color color) {

		GameObject aUFO;

		if (usedUFO.Count == 0) {
			aUFO = UnityEngine.GameObject.Instantiate(_UFOPrefab, new Vector3 (0f, SceneController.GROUND + 0.11f, -10f), Quaternion.identity) as GameObject;
			usingUFO.Add (aUFO);
			aUFO.tag = "Finish";
		}
		else {
			aUFO = usedUFO[0];
			usedUFO.Remove(aUFO);
			usingUFO.Add(aUFO);
			aUFO.SetActive (true);
		}

		aUFO.transform.position = new Vector3 (0f, SceneController.GROUND + 0.11f, -10f);
		aUFO.transform.localScale = new Vector3 (1f, 0.07f, 1f);
		aUFO.transform.localScale = aUFO.transform.localScale * scale;
		aUFO.GetComponent<Renderer>().material.color = color;

		return aUFO;
	}

	public void releaseUFO (GameObject UFOtoRelease){
		usingUFO.Remove (UFOtoRelease);
		usedUFO.Add (UFOtoRelease);
		UFOtoRelease.SetActive (false);

		UnityEngine.Object.Instantiate (_ExplosionPrefab, UFOtoRelease.transform.position, Quaternion.identity);
	}

}