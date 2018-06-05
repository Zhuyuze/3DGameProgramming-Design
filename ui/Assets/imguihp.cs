using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imguihp : MonoBehaviour {
	public float chp = 0.0f;
	private float hp;
	private Rect ihp;
	private bool onoff;
	public float tmp = 0.1f;
	// Use this for initialization
	void Start () {
		ihp = new Rect(20, 20, 200, 20);  
		hp = chp;

	}
	
	// Update is called once per frame
	void OnGUI()
	{

		chp = Mathf.Lerp(chp, hp, 0.05f);

		GUI.HorizontalScrollbar(ihp, 0.0f, chp, 0.0f, 1.0f);  
	}
	void Update() {
		transform.rotation = Camera.main.transform.rotation;
		if (hp > tmp) {
			tmp = 0.1f;
			hp -= 0.01f;
		} else {
			tmp = 0.9f;
			hp += 0.01f;
		}
	}
}
