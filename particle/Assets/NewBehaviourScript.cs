using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour {

	public float engineRevs;
	public float exhaustRate;

	ParticleSystem exhaust;


	void Start () {
		exhaust = GetComponent<ParticleSystem>();
	}


	void Update () {
		exhaust.emissionRate = engineRevs * exhaustRate;
	}

}
