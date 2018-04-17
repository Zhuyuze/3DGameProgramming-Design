using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.ShootThatUFO;

public class ActionManager :System.Object {
	private static ActionManager _instance;

	public static ActionManager GetInstance(){
		if (_instance == null) {
			_instance = new ActionManager();
		}
		return _instance;
	}

	public U3dAction applyU3dTossUFO (GameObject obj, Vector3 _startPoint, Vector3 _finishPoint, float _initialAngle, float _speed, IU3dActionCompleted _completed){

		U3dTossUFO UTU = obj.AddComponent<U3dTossUFO> ();
		UTU.setting (_startPoint, _finishPoint, _initialAngle, _speed, _completed);
		return UTU;
	}

	public U3dAction applyU3dTossUFO (GameObject obj, Vector3 _startPoint, Vector3 _finishPoint, float _initialAngle, float _speed){
		return applyU3dTossUFO (obj, _startPoint, _finishPoint, _initialAngle, _speed, null);
	}

	public U3dAction applyShootBullet (GameObject obj, Vector3 direction){
		shootBullet sb = obj.AddComponent<shootBullet> ();
		sb.setting (direction);
		return sb;
	}
}

public class U3dActionException : System.Exception {}

public interface IU3dActionCompleted {
	void OnActionCompleted (U3dAction action);
}

public class U3dAction : MonoBehaviour {
	public void Free(){}
}

public class U3dActionAuto : U3dAction {}

public class U3dActionMan : U3dAction {}


public class U3dTossUFO : U3dActionAuto {

	public Vector3 startPoint;
	public Vector3 finishPoint;

	public float forceStrength;
	public float initialAngle;

	private IU3dActionCompleted monitor = null;

	//        initialAngle in Radial
	public void setting (Vector3 _startPoint, Vector3 _finishPoint, float _initialAngle, float _speed, IU3dActionCompleted _monitor){
		this.startPoint = _startPoint;

		this.finishPoint = _finishPoint;

		this.initialAngle = _initialAngle;

		this.forceStrength = _speed;

		this.monitor = _monitor;

		Vector3 force = Vector3.Slerp(Vector3.Normalize(finishPoint - startPoint), Vector3.up, initialAngle);

		Debug.Log (force + " " + forceStrength);

		this.gameObject.GetComponent<Rigidbody>().AddForce (force * forceStrength, ForceMode.Impulse);
	}

	void FixedUpdate() {

		if (transform.position.y < SceneController.GROUND + 0.1f) {
			if (monitor != null){
				monitor.OnActionCompleted (this);
			}
			this.Free();
		}

	}

	public void Free(){
		this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		Destroy (this);
	}

}

public class shootBullet : U3dActionAuto {

	Vector3 direction;
	float t = 0;

	public void setting (Vector3 direction){
		this.gameObject.GetComponent<Rigidbody>().AddForce (direction * 50f, ForceMode.Impulse);
	}

	void FixedUpdate() {
	}

	void OnTriggerEnter() {
		this.Free ();
	}

	public void Free(){
		this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		Destroy (this);
	}

}