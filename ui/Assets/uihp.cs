using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uihp : MonoBehaviour {
	public float maxhp = 100;
	Slider hpui;
	public float tmp;
	// Use this for initialization
	void Start () {
		hpui = this.GetComponent<Slider> ();
		hpui.maxValue = maxhp;
		hpui.value = maxhp;
		tmp = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Camera.main.transform.rotation;
		if (hpui.value > tmp) {
			tmp = 5f;
			hpui.value -= 0.15f;
		} else {
			tmp = 95f;
			hpui.value += 0.1f;
		}
	}
}
